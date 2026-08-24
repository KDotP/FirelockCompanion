using System.ComponentModel;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FirelockCompanion;

public partial class ArmyBuilder : Form
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ReturnToMenu { get; private set; } = false;

    private static string armyName = "New Army";
    private string currentGroupFormat = "Group X";
    private string currentSaveFormat = "faction";
    private string previousSaveName;
    private List<UnitTemplate> factionUnits;

    // Getting real tired of this """error""" and if I suppress it internally, YOU would still get warned
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<string, Dictionary<string, string>> Formats { get; set; }

    private static readonly Regex LeadTagPattern = new(@"^(Vec|Inf|Air)\s*(\(([^)]*)\))?");
    private static readonly Regex PcPattern = new(@"^PC\s*\((\d+)");
    private static readonly Regex TowCapacityPattern = new(@"^Tow\s*\((\d+)");
    private static readonly Regex TowWeightPattern = new(@"(?:^|,\s*)T(\d+)");
    private static readonly Regex ParamPattern = new(@"\s*\(.*\)\s*$");
    private static readonly Regex DesantCapacityPattern = new(@"^Desant\s*\((\d+)\)"); // For ONE UNIT
    private Dictionary<string, string> normalizedKeywords = new Dictionary<string, string>();
    private static readonly Regex CommandValuePattern = new(@"(?:^|,\s*)C(\d+)(?:\s*,|$)");

    private static readonly TextFormatFlags MeasureFlags = TextFormatFlags.NoPadding | TextFormatFlags.SingleLine; // For special formatting with embark

    // Point management

    private int maxPoints = 300;
    private int RequiredTacoms => (int)Math.Ceiling(maxPoints / 100.0);

    private int nextUnitNumber = 1;

    private record KeywordInfo(string Keyword, string Description);
    private TreeNode currentTargetGroup;
    private static readonly Color TargetGroupColor = Color.PaleGreen;
    private static readonly Color OutOfPositionColor = Color.Firebrick;

    // Scope root (carrier or tow provider) -> the last physically-committed member node in its territory.
    private readonly Dictionary<TreeNode, TreeNode> territoryLastCommitted = new();
    // Node -> its physical position within its group's flat child list (for span comparisons).
    private readonly Dictionary<TreeNode, int> nodePhysicalIndex = new();

    private static string StripParams(string keyword) =>
        ParamPattern.Replace(keyword, "").Trim();

    public ArmyBuilder()
    {
        // Probably don't do anything before this
        InitializeComponent();
    }

    public void SelectFaction(string faction, string newName, int pointsTotal)
    {
        armyName = newName;
        armyNameLabel.Text = armyName;

        maxPoints = pointsTotal;
        pointsLabel.Text = $"Points 000/{maxPoints:000}";

        // Set the header name to the chosen faction
        factionNameLabel.Text = faction;

        activeArmyTree.AllowDrop = true;
        activeArmyTree.ItemDrag += activeArmyTree_ItemDrag;
        activeArmyTree.DragEnter += activeArmyTree_DragEnter;
        activeArmyTree.DragDrop += activeArmyTree_DragDrop;
        activeArmyTree.DragOver += activeArmyTree_DragOver;
        activeArmyTree.DragLeave += activeArmyTree_DragLeave;

        addUnitButton.Enabled = false;

        // Process data
        string jsonString = System.IO.File.ReadAllText("Data.json");
        GameData ruleset = System.Text.Json.JsonSerializer.Deserialize<GameData>(jsonString);

        // Custom content loader
        try
        {
            string[] customContentFiles = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "Custom Content"));
            for (int i = 0; i < customContentFiles.Length; i++)
            {
                if (!customContentFiles[i].EndsWith(".json"))
                    continue;
                string customString = System.IO.File.ReadAllText(customContentFiles[i]) ?? "null";

                if (customString != "null")
                {
                    GameData customData = System.Text.Json.JsonSerializer.Deserialize<GameData>(customString);
                    ruleset.Merge(customData);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading custom content: {ex.Message}", "Custom Content Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Load group names
        if (ruleset.formats != null && ruleset.formats.TryGetValue(faction, out var factionFormat))
        {
            if (factionFormat.TryGetValue("group_name", out var customName))
                currentGroupFormat = customName;
            else
                currentGroupFormat = "Group X";

            // Get shortened save name
            if (factionFormat.TryGetValue("save_name", out var saveName))
                currentSaveFormat = saveName;
            else
                currentSaveFormat = faction.ToLower().Replace(" ", "_"); // Safe fallback
        }
        else
        {
            currentGroupFormat = "Group X";
            currentSaveFormat = faction.ToLower().Replace(" ", "_");
        }

        // Show Tercio button
        if (faction == "Atom Barons of Santagria")
        {
            newTercioButton.Visible = true;
        }

        // Fill out available units
        PopulateAvailableUnits(ruleset, faction);
        RecalculateArmyTotals();

        // First group
        createNewGroup();
    }

    private string BuildFullNodeText(ActiveUnitEntry entry, TreeNode node)
    {
        string connectorPrefix = BuildConnectorPrefix(entry, node, out bool outOfPosition);
        bool isMismatchedTercio = false;

        // Custom Tercio mismatch validation
        if (entry.Unit.name == "Tercios" && node.Nodes.Count > 0)
        {
            var subEntries = node.Nodes.Cast<TreeNode>().Select(n => (ActiveUnitEntry)n.Tag).ToList();
            int committedCount = subEntries.Count(e => e.Status != EmbarkStatus.None);

            // If some units are in a vehicle but not all, the Tercio is broken
            if (committedCount > 0 && committedCount < subEntries.Count)
            {
                isMismatchedTercio = true;
                outOfPosition = true; // Force the parent text to turn red
            }
            else if (committedCount == subEntries.Count && committedCount > 0)
            {
                // If the entire Tercio is securely committed, clear the warning color
                outOfPosition = false;
            }
        }

        node.ForeColor = outOfPosition ? OutOfPositionColor : Color.Empty;

        // Hide the point cost for Tercio children, but keep it for normal units/parents
        bool isTercioChild = node.Parent?.Tag is ActiveUnitEntry pEntry && pEntry.Unit.name == "Tercios";
        string baseText = isTercioChild
            ? $"{entry.Unit.name} "
            : $"{entry.Unit.name} ({entry.Unit.cost} pts) ";

        // Dynamically pull the Size string for the Tercio parent so it never gets erased
        if (entry.Unit.name == "Tercios")
        {
            string groupKw = entry.Unit.keywords?.FirstOrDefault(k => k.StartsWith("Group ("));
            if (groupKw != null)
            {
                var match = Regex.Match(groupKw, @"\d+");
                if (match.Success) baseText += $"({match.Value} size) ";
            }
        }

        baseText += $"\"{entry.CustomName}\"";

        var prefixTags = new List<string>();

        if (entry.HasCarrierAbove)
        {
            if (entry.Status == EmbarkStatus.Embarked) prefixTags.Add("【E】");
            else if (entry.CanEmbark) prefixTags.Add("[E]");

            if (entry.HasDesantCarrierAbove)
            {
                if (entry.Status == EmbarkStatus.Desanted) prefixTags.Add("【D】");
                else if (entry.CanDesant) prefixTags.Add("[D]");
            }
        }

        if (entry.HasTowProviderAbove)
        {
            if (entry.IsTowed) prefixTags.Add("【T】");
            else if (entry.CanTow) prefixTags.Add("[T]");
        }

        string tagPrefix = prefixTags.Count > 0 ? string.Join(" ", prefixTags) + " " : "";

        var suffixParts = new List<string>();
        if (entry.EmbarkCapacityDisplay.HasValue) suffixParts.Add($"Embark {entry.EmbarkUsedDisplay}/{entry.EmbarkCapacityDisplay.Value}");
        if (entry.ShowDesantSuffix) suffixParts.Add($"Desant {entry.DesantUsedDisplay}/{GetDesantCapacity(entry.Unit)}");
        if (entry.TowCapacityDisplay.HasValue) suffixParts.Add($"Tow {entry.TowUsedDisplay}/{entry.TowCapacityDisplay.Value}");

        // Append an error string if the Tercio is mismatched (some units in a vehicle, some not)
        if (isMismatchedTercio) suffixParts.Add("Not all units in the same vehicle!");

        string suffix = suffixParts.Count > 0 ? $" — {string.Join(", ", suffixParts)}" : "";

        return $"{connectorPrefix}{tagPrefix}{baseText}{suffix}";
    }

    private List<TreeNode> GetVisibleAncestorChain(ActiveUnitEntry entry)
    {
        var chain = new List<TreeNode>();
        TreeNode current = entry.RelationParentNode;

        while (current != null)
        {
            if (territoryLastCommitted.ContainsKey(current))
                chain.Add(current);

            current = (current.Tag as ActiveUnitEntry)?.RelationParentNode;
        }

        chain.Reverse();
        return chain;
    }

    private List<TreeNode> GetLogicalNodes(TreeNode groupNode)
    {
        List<TreeNode> list = new List<TreeNode>();
        foreach (TreeNode child in groupNode.Nodes)
        {
            list.Add(child);
            if (child.Tag is ActiveUnitEntry entry && entry.Unit.name == "Tercios")
            {
                foreach (TreeNode subChild in child.Nodes)
                {
                    list.Add(subChild);
                }
            }
        }
        return list;
    }

    private static int GetDesantCapacity(UnitTemplate unit)
    {
        if (unit.keywords != null)
        {
            foreach (string kw in unit.keywords)
            {
                Match m = DesantCapacityPattern.Match(kw);
                if (m.Success) return int.Parse(m.Groups[1].Value);
            }
        }
        return 2; // Default desant capacity
    }

    // Builds the leading connector glyphs (│ ├ └) for one row, one character per visible ancestor
    // territory, outermost to innermost. Also reports whether this row is "out of position": eligible
    // for its own direct territory, physically still inside that territory's span, but not committed.
    private string BuildConnectorPrefix(ActiveUnitEntry entry, TreeNode node, out bool outOfPosition)
    {
        outOfPosition = false;

        List<TreeNode> chain = GetVisibleAncestorChain(entry);
        entry.IndentLevel = chain.Count;

        if (chain.Count == 0) return "";

        int myIndex = nodePhysicalIndex.TryGetValue(node, out int idx) ? idx : -1;
        string prefix = "";

        // A unit is actively committed if it's securely embarked or securely towed.
        bool isCommitted = (IsPassenger(entry.Unit) && entry.Status != EmbarkStatus.None) || entry.IsTowed;

        foreach (TreeNode ancestorRoot in chain)
        {
            TreeNode lastCommittedNode = territoryLastCommitted[ancestorRoot];
            int lastCommittedIndex = nodePhysicalIndex[lastCommittedNode];
            bool isDirectParent = ancestorRoot == entry.RelationParentNode;

            if (myIndex <= lastCommittedIndex)
            {
                if (isDirectParent && isCommitted)
                {
                    bool isLastCommitted = node == lastCommittedNode;
                    prefix += isLastCommitted ? "└" : "├";
                }
                else
                {
                    // Pass-through or uncommitted direct child
                    prefix += "│";

                    // Any unit between related units that isn't involved in that relationship is out of position
                    if (!isCommitted)
                    {
                        outOfPosition = true;
                    }
                }
            }
        }

        return prefix;
    }

    // Resets everything the connector/relationship pass derives, so each RecalculateAll starts clean.
    private void ClearRelationshipState()
    {
        territoryLastCommitted.Clear();
        nodePhysicalIndex.Clear();

        foreach (TreeNode groupNode in activeArmyTree.Nodes)
            foreach (TreeNode child in GetLogicalNodes(groupNode))
                if (child.Tag is ActiveUnitEntry entry)
                {
                    entry.RelationParentNode = null;
                    entry.IndentLevel = 0;
                }
    }

    private void AssignPhysicalIndices()
    {
        foreach (TreeNode groupNode in activeArmyTree.Nodes)
        {
            int i = 0;
            foreach (TreeNode child in GetLogicalNodes(groupNode))
                nodePhysicalIndex[child] = i++;
        }
    }

    private Dictionary<string, string> BuildNormalizedKeywords(Dictionary<string, string> raw)
    {
        var normalized = new Dictionary<string, string>();
        foreach (var kv in raw)
        {
            string baseKey = StripParams(kv.Key);
            if (!normalized.ContainsKey(baseKey))
                normalized[baseKey] = kv.Value;
        }
        return normalized;
    }

    private void RemoveSelectedNode()
    {
        TreeNode node = activeArmyTree.SelectedNode;
        if (node == null) return;

        // Tercio nodes cannot be individually removed
        if (node?.Parent?.Tag is ActiveUnitEntry parentEntry && parentEntry.Unit.name == "Tercios")
        {
            return;
        }

        bool isGroup = node.Tag as string == "GROUP";

        if (isGroup && node.Nodes.Count > 0)
        {
            var confirm = MessageBox.Show(
                $"Remove \"{node.Text}\" and the {node.Nodes.Count} unit(s) inside it?",
                "Confirm Remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;
        }

        if (node == currentTargetGroup)
        {
            ClearTargetGroup();
        }

        node.Remove();

        RecalculateAll();
    }

    private void RenameSelectedNode()
    {
        TreeNode node = activeArmyTree.SelectedNode;
        if (node == null) return;

        if (node.Tag is ActiveUnitEntry entry)
        {
            BeginRenameUnit(node, entry);
        }
        else if (node.Tag as string == "GROUP")
        {
            activeArmyTree.LabelEdit = true;
            node.BeginEdit();
        }
    }

    private void AddSelectedUnitToArmy()
    {
        if (availableArmyTree.SelectedNode?.Tag is UnitTemplate unit)
        {
            AddUnitToActiveArmy(unit);
        }
    }

    // Can't remember why I needed this, leaving it for now
    private static EmbarkRole GetEmbarkRole(UnitTemplate unit)
    {
        Match m = LeadTagPattern.Match(unit.unit_stats ?? "");
        if (!m.Success) return EmbarkRole.None;
        return m.Groups[1].Value == "Vec" ? EmbarkRole.Carrier : EmbarkRole.Passenger;
    }

    private static int EmbarkWeight(UnitTemplate unit)
    {
        if (unit.keywords != null && unit.keywords.Contains("Squad")) return 2;

        Match m = LeadTagPattern.Match(unit.unit_stats ?? "");
        return (m.Success && m.Groups[3].Success && m.Groups[3].Value.Contains("S")) ? 2 : 1;
    }

    private static int? GetPcCapacity(UnitTemplate vehicle)
    {
        if (vehicle.keywords == null) return null;
        foreach (string kw in vehicle.keywords)
        {
            Match m = PcPattern.Match(kw);
            if (m.Success) return int.Parse(m.Groups[1].Value);
        }
        return null;
    }

    private static bool IsCarrier(UnitTemplate unit)
    {
        Match m = LeadTagPattern.Match(unit.unit_stats ?? "");
        if (!m.Success) return false;
        string tag = m.Groups[1].Value;
        return tag == "Vec" || (tag == "Air" && GetPcCapacity(unit).HasValue);
    }

    private static bool IsPassenger(UnitTemplate unit)
    {
        Match m = LeadTagPattern.Match(unit.unit_stats ?? "");
        return m.Success && m.Groups[1].Value == "Inf";
    }

    private static int? GetTowCapacity(UnitTemplate unit)
    {
        if (unit.keywords == null) return null;
        foreach (string kw in unit.keywords)
        {
            Match m = TowCapacityPattern.Match(kw);
            if (m.Success) return int.Parse(m.Groups[1].Value);
        }
        return null;
    }

    private static int GetTowWeight(UnitTemplate unit)
    {
        Match m = TowWeightPattern.Match(unit.unit_stats ?? "");
        return m.Success ? int.Parse(m.Groups[1].Value) : 0;
    }

    private static bool IsVehicle(UnitTemplate unit)
    {
        Match m = LeadTagPattern.Match(unit.unit_stats ?? "");
        return m.Success && m.Groups[1].Value == "Vec";
    }

    private static bool IsAircraft(UnitTemplate unit)
    {
        Match m = LeadTagPattern.Match(unit.unit_stats ?? "");
        return m.Success && m.Groups[1].Value == "Air";
    }

    private static int GetCommandValue(UnitTemplate unit)
    {
        if (unit?.type != "TACOM") return 0;

        Match m = CommandValuePattern.Match(unit.unit_stats ?? "");
        if (m.Success) return int.Parse(m.Groups[1].Value);

        return 0;
    }

    private static bool SupportsDesant(UnitTemplate unit) => IsVehicle(unit);

    private void renameToolStripMenuItem_Click(object sender, EventArgs e) => RenameSelectedNode();
    private void deleteToolStripMenuItem_Click(object sender, EventArgs e) => RemoveSelectedNode();

    private void ClearTargetGroup()
    {
        if (currentTargetGroup != null)
        {
            currentTargetGroup.BackColor = Color.Empty;
            currentTargetGroup = null;
        }
    }

    private void activeArmyTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
        ShowNodeDetails(e.Node);

        TreeNode node = e.Node;
        while (node != null)
        {
            if (node.Tag as string == "GROUP")
            {
                SetTargetGroup(node);
                break;
            }
            node = node.Parent;
        }
    }

    private void SetTargetGroup(TreeNode newGroup)
    {
        if (newGroup == currentTargetGroup) return;

        if (currentTargetGroup != null)
        {
            currentTargetGroup.BackColor = Color.Empty;
        }

        currentTargetGroup = newGroup;
        currentTargetGroup.BackColor = TargetGroupColor;
    }

    private void availableArmyTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
        ShowNodeDetails(e.Node);
        addUnitButton.Enabled = e.Node?.Tag is UnitTemplate;
    }

    private static string FormatDescription(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        // Turn each line break into a full blank-line paragraph gap.
        // \r\n (not just \n) so RichTextBox renders it reliably.
        return text.Replace("\n", "\r\n\r\n");
    }

    private void createNewGroupToolStripMenuItem_Click(object sender, EventArgs e)
    {
        createNewGroup();
    }

    private void createNewGroup()
    {
        int nextGroupNum = activeArmyTree.Nodes.Count + 1;

        // Dynamically insert the number into the format string
        string defaultGroupName = currentGroupFormat.Replace("X", nextGroupNum.ToString());

        TreeNode groupNode = new TreeNode(defaultGroupName);
        groupNode.Tag = "GROUP";

        activeArmyTree.Nodes.Add(groupNode);
        activeArmyTree.ExpandAll();

        SetTargetGroup(groupNode);
        activeArmyTree.SelectedNode = groupNode;
    }

    private void BeginRenameUnit(TreeNode node, ActiveUnitEntry entry)
    {
        node.Text = entry.CustomName; // shrink to just the name for editing
        activeArmyTree.LabelEdit = true;
        node.BeginEdit();
    }

    // After renaming, remove renaming ability
    private void activeArmyTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
        activeArmyTree.LabelEdit = false;

        if (e.Node.Tag is not ActiveUnitEntry entry) return;

        if (string.IsNullOrWhiteSpace(e.Label))
        {
            e.CancelEdit = true;
        }
        else
        {
            entry.CustomName = e.Label.Trim();
        }

        e.CancelEdit = true;

        e.Node.Text = BuildFullNodeText(entry, e.Node);
    }

    // Double click on any node in the active army tree
    private void activeArmyTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node == null) return;
        activeArmyTree.SelectedNode = e.Node; // double-click should also count as selecting, same fix as the right-click case
        RenameSelectedNode();
    }

    private void PopulateAvailableUnits(GameData data, string selectedFactionName)
    {
        availableArmyTree.Nodes.Clear();

        if (!data.factions.ContainsKey(selectedFactionName)) return;

        factionUnits = data.factions[selectedFactionName];
        Dictionary<string, TreeNode> categoryNodes = new Dictionary<string, TreeNode>();
        normalizedKeywords = BuildNormalizedKeywords(data.keywords);

        foreach (UnitTemplate unit in factionUnits)
        {
            // Core Category Generation (e.g., Infantry, Vehicles)
            if (!categoryNodes.ContainsKey(unit.type))
            {
                TreeNode newCategory = new TreeNode(unit.type);
                newCategory.Tag = "CATEGORY";
                categoryNodes[unit.type] = newCategory;
                availableArmyTree.Nodes.Add(newCategory);
            }

            // Unit Root Node (Minimized by default)
            TreeNode unitNode = new TreeNode($"{unit.name} ({unit.cost} pts)");
            unitNode.Tag = unit;
            categoryNodes[unit.type].Nodes.Add(unitNode);

            // Subname / Role
            if (!string.IsNullOrEmpty(unit.subname))
            {
                unitNode.Nodes.Add(new TreeNode(unit.subname));
            }

            // Base Stats
            if (!string.IsNullOrEmpty(unit.unit_stats))
            {
                unitNode.Nodes.Add(new TreeNode(unit.unit_stats));
            }

            // Unit Rules/Keywords (Hidden by default)
            if (unit.keywords != null && unit.keywords.Count > 0)
            {
                unitNode.Nodes.Add(BuildKeywordsNode(unit.keywords, normalizedKeywords));
            }

            // Bonus Traits (if any exist)
            if (!string.IsNullOrEmpty(unit.bonus_traits))
            {
                unitNode.Nodes.Add(new TreeNode(unit.bonus_traits));
            }

            // Weapons
            if (unit.weapons != null)
            {
                foreach (Weapon weapon in unit.weapons)
                {
                    TreeNode wpNode = new TreeNode(weapon.name);
                    unitNode.Nodes.Add(wpNode);

                    // Weapon Profile Subnodes (Expanded by default)
                    if (!string.IsNullOrEmpty(weapon.weapon_stats))
                    {
                        wpNode.Nodes.Add(new TreeNode(weapon.weapon_stats));
                    }

                    // Weapon Keywords
                    if (weapon.keywords != null && weapon.keywords.Count > 0)
                    {
                        wpNode.Nodes.Add(BuildKeywordsNode(weapon.keywords, normalizedKeywords));
                    }

                    // Ammunition Types (Siblings to the weapons))
                    if (weapon.ammos != null)
                    {
                        foreach (Ammo ammo in weapon.ammos)
                        {
                            TreeNode ammoNode = new TreeNode($"-> {ammo.name}"); // Replace with right arrow later
                            wpNode.Nodes.Add(ammoNode); // Added to unitNode directly to remain on weapon level

                            if (!string.IsNullOrEmpty(ammo.ammo_stats))
                            {
                                ammoNode.Nodes.Add(new TreeNode(ammo.ammo_stats));
                            }

                            if (ammo.keywords != null && ammo.keywords.Count > 0)
                            {
                                ammoNode.Nodes.Add(BuildKeywordsNode(ammo.keywords, normalizedKeywords));
                            }
                        }
                    }

                    wpNode.Expand(); // Ensure weapon stats are visible out of the box
                }
            }

            unitNode.Collapse(); // Keep the whole unit closed until clicked
        }

        // Keep the main category folders open
        foreach (var catNode in categoryNodes.Values)
        {
            catNode.Expand();
        }
    }

    private TreeNode BuildKeywordsNode(List<string> keywords, Dictionary<string, string> normalizedKeywords)
    {
        List<KeywordInfo> keywordInfos = keywords.Select(kw =>
        {
            string baseKey = StripParams(kw);
            string description = normalizedKeywords.TryGetValue(baseKey, out var desc)
                ? desc
                : "No definition found.";
            return new KeywordInfo(kw, description);
        }).ToList();

        TreeNode headerNode = new TreeNode(string.Join(", ", keywords));

        if (keywordInfos.Count == 1)
        {
            // If only keyword, no children
            headerNode.Tag = keywordInfos[0];
        }
        else
        {
            // Multiple keywords: clicking the header shows all of them at once.
            headerNode.Tag = keywordInfos;

            // Children still exist for anyone who wants to drill into just one.
            foreach (var info in keywordInfos)
            {
                TreeNode child = new TreeNode(info.Keyword);
                child.Tag = info;
                headerNode.Nodes.Add(child);
            }

            headerNode.Collapse();
        }

        return headerNode;
    }

    // Probably should have always been a seperate function, but these are trying times
    private string FormatUnitDetails(UnitTemplate unit, string headerOverride = null)
    {
        List<string> parts = new List<string>();

        // Core stats
        parts.Add(headerOverride ?? unit.name);
        if (!string.IsNullOrEmpty(unit.subname)) parts.Add(unit.subname);
        if (!string.IsNullOrEmpty(unit.unit_stats)) parts.Add(unit.unit_stats);
        if (!string.IsNullOrEmpty(unit.bonus_traits)) parts.Add(FormatDescription(unit.bonus_traits));
        // Keywords week is BACK
        if (unit.keywords != null && unit.keywords.Count > 0)
        {
            parts.Add($"Keywords: {string.Join(", ", unit.keywords)}");
        }

        // Weapons and ammo profiles
        if (unit.weapons != null && unit.weapons.Count > 0)
        {
            List<string> weaponLines = new List<string>();
            foreach (var w in unit.weapons)
            {
                string wText = $"{w.name} — {w.weapon_stats}";
                if (w.keywords != null && w.keywords.Count > 0) wText += $" [{string.Join(", ", w.keywords)}]";
                weaponLines.Add(wText);

                if (w.ammos != null)
                {
                    foreach (var a in w.ammos)
                    {
                        string aText = $"  -> {a.name} {a.ammo_stats}";
                        if (a.keywords != null && a.keywords.Count > 0) aText += $" [{string.Join(", ", a.keywords)}]";
                        weaponLines.Add(aText);
                    }
                }
            }
            parts.Add("WEAPONS:\r\n" + string.Join("\r\n", weaponLines));
        }

        // Gather all unique keywords across unit, weapons, and ammos
        HashSet<string> uniqueKeywords = new HashSet<string>();

        if (unit.keywords != null)
        {
            foreach (var kw in unit.keywords)
            {
                uniqueKeywords.Add(kw);
            }
        }

        if (unit.weapons != null)
        {
            foreach (var w in unit.weapons)
            {
                if (w.keywords != null)
                    foreach (var kw in w.keywords) uniqueKeywords.Add(kw);

                if (w.ammos != null)
                {
                    foreach (var a in w.ammos)
                    {
                        if (a.keywords != null)
                            foreach (var kw in a.keywords) uniqueKeywords.Add(kw);
                    }
                }
            }
        }

        // Output definition entries for all gathered keywords
        if (uniqueKeywords.Count > 0)
        {
            parts.Add("----------\r\nKeywords:");
            foreach (var kw in uniqueKeywords)
            {
                string baseKey = StripParams(kw);
                string desc = normalizedKeywords.TryGetValue(baseKey, out var foundDesc)
                    ? foundDesc
                    : "No definition found.";

                parts.Add($"---\r\n{kw}\r\n---\r\n{FormatDescription(desc)}");
            }
        }

        return string.Join("\r\n\r\n", parts);
    }

    // Slight different compared to PlayScreen
    private void ShowNodeDetails(TreeNode node)
    {
        if (node?.Tag == null)
        {
            detailsTextBox.Text = node?.Text ?? "";
            return;
        }

        switch (node.Tag)
        {
            case KeywordInfo kwInfo:
                {
                    detailsTextBox.Text = $"{kwInfo.Keyword}\r\n\r\n{FormatDescription(kwInfo.Description)}";
                    break;
                }

            case List<KeywordInfo> kwList:
                {
                    detailsTextBox.Text = string.Join(
                        "\r\n\r\n----------\r\n\r\n",
                        kwList.Select(k => $"{k.Keyword}\r\n\r\n{FormatDescription(k.Description)}")
                    );
                    break;
                }

            case UnitTemplate unit:
                {
                    // Format library units
                    detailsTextBox.Text = FormatUnitDetails(unit);
                    break;
                }

            case ActiveUnitEntry activeEntry:
                {
                    // Format active army units with their custom names
                    detailsTextBox.Text = FormatUnitDetails(activeEntry.Unit, $"{activeEntry.Unit.name} — \"{activeEntry.CustomName}\"");
                    break;
                }

            default:
                detailsTextBox.Text = node.Text;
                break;
        }

        detailsTextBox.SelectionStart = 0;
        detailsTextBox.SelectionLength = 0;
        detailsTextBox.ScrollToCaret();
    }

    private void availableArmyTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node?.Tag is not UnitTemplate unit) return;

        // Undo the automatic toggle that just happened
        if (e.Node.IsExpanded) e.Node.Collapse();
        else e.Node.Expand();

        AddUnitToActiveArmy(unit);
    }

    private void AddUnitToActiveArmy(UnitTemplate unit)
    {
        if (currentTargetGroup == null)
        {
            MessageBox.Show("Select or create a group first.", "No group selected");
            return;
        }

        ActiveUnitEntry entry = new ActiveUnitEntry(unit, $"Unit {nextUnitNumber++}");

        TreeNode unitNode = new TreeNode();
        unitNode.Tag = entry;
        unitNode.Text = BuildFullNodeText(entry, unitNode);

        currentTargetGroup.Nodes.Add(unitNode);
        currentTargetGroup.Expand();

        RecalculateAll();
    }

    private void RecalculateArmyTotals()
    {
        int totalPoints = 0;
        int totalTacoms = 0;
        int totalCommand = 0;

        void Walk(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is ActiveUnitEntry entry)
                {
                    // Check if this node is nested inside a Tercio wrapper
                    bool isTercioChild = node.Parent?.Tag is ActiveUnitEntry pEntry && pEntry.Unit.name == "Tercios";

                    // Only add the point cost if it's not a Tercio subunit
                    // I hate you Tercio
                    if (!isTercioChild)
                    {
                        totalPoints += entry.Unit.cost;
                    }

                    // Shh, these are official rules
                    if (entry.Unit.type == "TACOM" && !entry.Unit.keywords.Contains("Reserve"))
                    {
                        totalTacoms++;
                        totalCommand += GetCommandValue(entry.Unit);
                    }
                }

                if (node.Nodes.Count > 0)
                    Walk(node.Nodes);
            }
        }

        Walk(activeArmyTree.Nodes);

        pointsLabel.Text = $"Points {totalPoints:000}/{maxPoints:000}";
        tacomLabel.Text = $"TACOMs: {totalTacoms}/{RequiredTacoms} – {totalCommand}C";

        pointsLabel.ForeColor = totalPoints > maxPoints ? Color.Red : SystemColors.ControlText;
        tacomLabel.ForeColor = totalTacoms < RequiredTacoms ? Color.Red : SystemColors.ControlText;
    }

    private void RecalculateEmbarkState()
    {
        foreach (TreeNode groupNode in activeArmyTree.Nodes)
        {
            TreeNode vehicleNode = null;
            int? capacity = null;
            bool desantSupported = false;
            List<TreeNode> passengerNodes = new List<TreeNode>();

            void FinalizeVehicle()
            {
                if (vehicleNode == null) return;
                var vEntry = (ActiveUnitEntry)vehicleNode.Tag;
                var entries = passengerNodes.Select(n => (Node: n, Entry: (ActiveUnitEntry)n.Tag)).ToList();

                int maxDesant = GetDesantCapacity(vEntry.Unit);
                int embarkUsed = entries.Where(x => x.Entry.Status == EmbarkStatus.Embarked)
                                         .Sum(x => EmbarkWeight(x.Entry.Unit));
                int desantUsed = desantSupported
                    ? entries.Where(x => x.Entry.Status == EmbarkStatus.Desanted).Sum(x => EmbarkWeight(x.Entry.Unit))
                    : 0;

                foreach (var (node, entry) in entries)
                {
                    int weight = EmbarkWeight(entry.Unit);
                    int othersEmbark = embarkUsed - (entry.Status == EmbarkStatus.Embarked ? weight : 0);
                    int othersDesant = desantUsed - (entry.Status == EmbarkStatus.Desanted ? weight : 0);

                    entry.HasCarrierAbove = true;
                    entry.HasDesantCarrierAbove = desantSupported;
                    entry.CanEmbark = capacity.HasValue && othersEmbark + weight <= capacity.Value;
                    entry.CanDesant = desantSupported && othersDesant + weight <= maxDesant;
                    entry.RelationParentNode = vehicleNode; // in range of this carrier's territory, committed or not

                    if (!desantSupported && entry.Status == EmbarkStatus.Desanted)
                        entry.Status = EmbarkStatus.None;
                }

                // The territory's connector shape closes at the last physically-committed passenger.
                // Zero committed passengers means the territory has no visual shape at all yet.
                TreeNode lastCommittedPassenger = entries
                    .Where(x => x.Entry.Status != EmbarkStatus.None)
                    .Select(x => x.Node)
                    .LastOrDefault();

                if (lastCommittedPassenger != null)
                {
                    if (lastCommittedPassenger.Parent?.Tag is ActiveUnitEntry parentEntry && parentEntry.Unit.name == "Tercios")
                    {
                        lastCommittedPassenger = lastCommittedPassenger.Parent.Nodes[lastCommittedPassenger.Parent.Nodes.Count - 1];
                    }
                    territoryLastCommitted[vehicleNode] = lastCommittedPassenger;
                }

                vEntry.EmbarkCapacityDisplay = capacity;
                vEntry.EmbarkUsedDisplay = embarkUsed;
                vEntry.ShowDesantSuffix = desantSupported && passengerNodes.Count > 0;
                vEntry.DesantUsedDisplay = desantUsed;
            }

            foreach (TreeNode child in GetLogicalNodes(groupNode))
            {
                if (child.Tag is not ActiveUnitEntry entry) continue;

                if (IsCarrier(entry.Unit))
                {
                    FinalizeVehicle();
                    vehicleNode = child;
                    capacity = GetPcCapacity(entry.Unit);
                    desantSupported = SupportsDesant(entry.Unit);
                    passengerNodes = new List<TreeNode>();
                    entry.HasCarrierAbove = false;
                    entry.HasDesantCarrierAbove = false;
                }
                else if (IsPassenger(entry.Unit))
                {
                    // Tercio parents should not have their own embark status, but their children can still be passengers
                    if (entry.Unit.name == "Tercios")
                    {
                        entry.HasCarrierAbove = false;
                        entry.HasDesantCarrierAbove = false;
                        // Status is temporarily cleared here, but synced to its children at the end of the method
                        entry.Status = EmbarkStatus.None;
                    }
                    else if (entry.Unit.keywords.Contains("Horseback"))
                    {
                        // Santagria back at it again with another exception case
                        entry.HasCarrierAbove = false;
                        entry.HasDesantCarrierAbove = false;
                        entry.Status = EmbarkStatus.None;
                    }
                    else if (vehicleNode != null)
                    {
                        passengerNodes.Add(child);
                    }
                    else
                    {
                        entry.HasCarrierAbove = false;
                        entry.HasDesantCarrierAbove = false;
                        entry.Status = EmbarkStatus.None;
                    }
                }
                else
                {
                    // Hopefully fixes the air hierarchy issue
                    FinalizeVehicle();
                    vehicleNode = null;
                    capacity = null;
                    desantSupported = false;
                    passengerNodes = new List<TreeNode>();

                    entry.HasCarrierAbove = false;
                    entry.HasDesantCarrierAbove = false;
                }
            }

            foreach (TreeNode child in groupNode.Nodes)
            {
                if (child.Tag is ActiveUnitEntry entry && entry.Unit.name == "Tercios")
                {
                    // Find the first subunit that is actively interacting
                    var interactingChild = child.Nodes.Cast<TreeNode>()
                        .FirstOrDefault(n => ((ActiveUnitEntry)n.Tag).RelationParentNode != null);

                    if (interactingChild != null)
                    {
                        // Inherit the relationship so the Format button sorts the whole Tercio correctly
                        entry.RelationParentNode = ((ActiveUnitEntry)interactingChild.Tag).RelationParentNode;
                        entry.Status = ((ActiveUnitEntry)interactingChild.Tag).Status;
                    }
                }
            }

            FinalizeVehicle();
        }
    }

    private void RecalculateTowState()
    {
        foreach (TreeNode groupNode in activeArmyTree.Nodes)
        {
            TreeNode providerNode = null;
            int? towCapacity = null;
            List<TreeNode> toweeNodes = new List<TreeNode>();

            void FinalizeProvider()
            {
                if (providerNode == null) return;
                var pEntry = (ActiveUnitEntry)providerNode.Tag;
                var entries = toweeNodes.Select(n => (Node: n, Entry: (ActiveUnitEntry)n.Tag)).ToList();

                bool isAircraftProvider = IsAircraft(pEntry.Unit);
                int usedWeight = 0;
                int countTowed = 0;

                foreach (var (node, entry) in entries)
                {
                    entry.HasTowProviderAbove = true;
                    entry.RelationParentNode = providerNode; // in range of this provider's territory, committed or not

                    int weight = GetTowWeight(entry.Unit);

                    if (entry.IsTowed)
                    {
                        bool weightAllows = towCapacity.HasValue && (usedWeight + weight <= towCapacity.Value);
                        bool limitAllows = !isAircraftProvider || countTowed == 0;

                        if (weightAllows && limitAllows)
                        {
                            usedWeight += weight;
                            countTowed++;
                            entry.CanTow = true;
                        }
                        else
                        {
                            entry.IsTowed = false; // Safely drop the tow if it exceeds capacity
                            entry.CanTow = towCapacity.HasValue && (usedWeight + weight <= towCapacity.Value) && (!isAircraftProvider || countTowed == 0);
                        }
                    }
                    else
                    {
                        entry.CanTow = towCapacity.HasValue && (usedWeight + weight <= towCapacity.Value) && (!isAircraftProvider || countTowed == 0);
                    }
                }

                // Same rule as embark territories: shape closes at the last committed towee, or doesn't exist yet.
                TreeNode lastCommittedTowee = entries
                    .Where(x => x.Entry.IsTowed)
                    .Select(x => x.Node)
                    .LastOrDefault();
                if (lastCommittedTowee != null)
                    territoryLastCommitted[providerNode] = lastCommittedTowee;

                pEntry.TowCapacityDisplay = towCapacity;
                pEntry.TowUsedDisplay = usedWeight;
            }

            foreach (TreeNode child in GetLogicalNodes(groupNode))
            {
                if (child.Tag is not ActiveUnitEntry entry) continue;

                bool isVehicle = IsVehicle(entry.Unit);
                bool isProvider = GetTowCapacity(entry.Unit).HasValue;

                if (isVehicle && providerNode != null)
                {
                    toweeNodes.Add(child);
                }
                else if (isVehicle)
                {
                    entry.HasTowProviderAbove = false;
                    entry.IsTowed = false;
                    entry.CanTow = false;
                }

                // A vehicle currently being towed cannot itself act as a provider
                if (isProvider && !entry.IsTowed && entry.Status == EmbarkStatus.None)
                {
                    FinalizeProvider();
                    providerNode = child;
                    towCapacity = GetTowCapacity(entry.Unit);
                    toweeNodes = new List<TreeNode>();
                }
                else if (isProvider && entry.IsTowed)
                {
                    entry.TowCapacityDisplay = null; // suppressed, don't show a Tow suffix while towed
                }
                else if (!isVehicle)
                {
                    entry.TowCapacityDisplay = null;
                }
            }

            FinalizeProvider();
        }
    }

    private void RecalculateAll()
    {
        ClearRelationshipState();
        AssignPhysicalIndices();

        RecalculateEmbarkState();
        RecalculateTowState();

        foreach (TreeNode groupNode in activeArmyTree.Nodes)
            foreach (TreeNode child in GetLogicalNodes(groupNode))
                if (child.Tag is ActiveUnitEntry entry)
                    child.Text = BuildFullNodeText(entry, child);

        RecalculateArmyTotals();
    }

    private void activeArmyTree_MouseDown(object sender, MouseEventArgs e)
    {
        TreeNode node = activeArmyTree.GetNodeAt(e.X, e.Y);
        if (node == null)
        {
            // Deselect all if not clicking node
            activeArmyTree.SelectedNode = null;
            return;
        }

        if (e.Button == MouseButtons.Right)
        {
            activeArmyTree.SelectedNode = node;
            return;
        }

        if (e.Button != MouseButtons.Left) return;
        if (node.Tag is not ActiveUnitEntry entry) return;

        int x = node.Bounds.Left;
        Font font = activeArmyTree.Font;

        string connectorPrefix = BuildConnectorPrefix(entry, node, out _);
        if (connectorPrefix.Length > 0)
        {
            x += TextRenderer.MeasureText(connectorPrefix, font, Size.Empty, MeasureFlags).Width;
        }

        if (entry.HasCarrierAbove)
        {
            bool showEmbark = entry.Status == EmbarkStatus.Embarked || entry.CanEmbark;
            if (showEmbark)
            {
                string eTag = (entry.Status == EmbarkStatus.Embarked ? "【E】" : "[E]") + " ";
                int eWidth = TextRenderer.MeasureText(eTag, font, Size.Empty, MeasureFlags).Width;

                if (e.X <= x + eWidth)
                {
                    entry.Status = entry.Status == EmbarkStatus.Embarked ? EmbarkStatus.None : EmbarkStatus.Embarked;
                    RecalculateAll();
                    return;
                }
                x += eWidth;
            }

            bool showDesant = entry.HasDesantCarrierAbove && (entry.Status == EmbarkStatus.Desanted || entry.CanDesant);
            if (showDesant)
            {
                string dTag = (entry.Status == EmbarkStatus.Desanted ? "【D】" : "[D]") + " ";
                int dWidth = TextRenderer.MeasureText(dTag, font, Size.Empty, MeasureFlags).Width;

                if (e.X <= x + dWidth)
                {
                    entry.Status = entry.Status == EmbarkStatus.Desanted ? EmbarkStatus.None : EmbarkStatus.Desanted;
                    RecalculateAll();
                    return;
                }
                x += dWidth;
            }
        }

        if (entry.HasTowProviderAbove)
        {
            bool showTow = entry.IsTowed || entry.CanTow;
            if (showTow)
            {
                string tTag = (entry.IsTowed ? "【T】" : "[T]") + " ";
                int tWidth = TextRenderer.MeasureText(tTag, font, Size.Empty, MeasureFlags).Width;

                if (e.X <= x + tWidth)
                {
                    entry.IsTowed = !entry.IsTowed;
                    RecalculateAll();
                }
            }
        }
    }

    private void activeArmyTree_KeyDown(object sender, KeyEventArgs e)
    {
        if (activeArmyTree.LabelEdit) return; // don't hijack Delete/Backspace while actively typing a rename

        if (e.KeyCode == Keys.Delete)
        {
            e.Handled = true;
            RemoveSelectedNode();
        }
    }

    private void newGroupButton_Click(object sender, EventArgs e)
    {
        createNewGroupToolStripMenuItem_Click(sender, e);
    }

    private void renameButton_Click(object sender, EventArgs e)
    {
        RenameSelectedNode();
    }

    private void removeButton_Click(object sender, EventArgs e)
    {
        RemoveSelectedNode();
    }

    private void addUnitButton_Click(object sender, EventArgs e)
    {
        AddSelectedUnitToArmy();
    }

    // --- Drag and drop ---

    private TreeNode draggedNode;
    private TreeNode dropHighlightNode;

    private void activeArmyTree_ItemDrag(object sender, ItemDragEventArgs e)
    {
        if (e.Item is TreeNode node && node.Tag is ActiveUnitEntry)
        {
            // Tercio prevention
            if (node?.Parent?.Tag is ActiveUnitEntry parentEntry && parentEntry.Unit.name == "Tercios")
            {
                return;
            }

            draggedNode = node;
            DoDragDrop(node, DragDropEffects.Move);
        }
    }

    private void activeArmyTree_DragEnter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Move;
    }

    private void activeArmyTree_DragDrop(object sender, DragEventArgs e)
    {
        if (dropHighlightNode != null)
        {
            dropHighlightNode.BackColor = Color.Empty;
            dropHighlightNode = null;
        }

        if (draggedNode == null) return;

        Point clientPoint = activeArmyTree.PointToClient(new Point(e.X, e.Y));
        TreeNode targetNode = activeArmyTree.GetNodeAt(clientPoint);

        if (targetNode == null || targetNode == draggedNode)
        {
            draggedNode = null;
            return;
        }

        TreeNode dragged = draggedNode;
        draggedNode = null;

        dragged.Remove(); // detach from current position first

        if (targetNode.Tag as string == "GROUP")
        {
            // Dropped on a group — becomes the first child of that group
            targetNode.Nodes.Insert(0, dragged);
            targetNode.Expand();
        }
        else if (targetNode.Tag is ActiveUnitEntry)
        {
            // Prevent insertion into a Tercio via drag and drop
            if (targetNode.Parent?.Tag is ActiveUnitEntry parentEntry && parentEntry.Unit.name == "Tercios")
            {
                // Insert it after the Tercio parent instead
                TreeNode groupFolder = targetNode.Parent.Parent;
                int parentIndex = groupFolder.Nodes.IndexOf(targetNode.Parent);
                groupFolder.Nodes.Insert(parentIndex + 1, dragged);
            }
            else
            {
                // If dropped on a normal unit, becomes the next sibling right after it
                TreeNode parentGroup = targetNode.Parent;
                int targetIndex = parentGroup.Nodes.IndexOf(targetNode);
                parentGroup.Nodes.Insert(targetIndex + 1, dragged);
            }
        }
        else
        {
            return;
        }

        activeArmyTree.SelectedNode = dragged;

        if (dragged.Tag is ActiveUnitEntry draggedEntry)
        {
            draggedEntry.Status = EmbarkStatus.None; // moving always clears embark/desant
            draggedEntry.IsTowed = false; // moving also clears tow status

            // If the dragged unit is a Tercio wipe the status of all its subunits
            if (draggedEntry.Unit.name == "Tercios")
            {
                foreach (TreeNode childNode in dragged.Nodes)
                {
                    if (childNode.Tag is ActiveUnitEntry childEntry)
                    {
                        childEntry.Status = EmbarkStatus.None;
                        childEntry.IsTowed = false;
                    }
                }
            }

            // If the dragged unit is a provider, prevent it from hijacking passengers below it
            if (IsCarrier(draggedEntry.Unit) && dragged.Parent != null)
            {
                int draggedIdx = dragged.Parent.Nodes.IndexOf(dragged);
                for (int i = draggedIdx + 1; i < dragged.Parent.Nodes.Count; i++)
                {
                    if (dragged.Parent.Nodes[i].Tag is ActiveUnitEntry siblingEntry)
                    {
                        if (IsCarrier(siblingEntry.Unit)) break; // Stop checking at the next carrier's territory

                        siblingEntry.Status = EmbarkStatus.None;

                        // Also wipe Tercio subunits 
                        if (siblingEntry.Unit.name == "Tercios" && dragged.Parent.Nodes[i].Nodes.Count > 0)
                        {
                            foreach (TreeNode subNode in dragged.Parent.Nodes[i].Nodes)
                            {
                                if (subNode.Tag is ActiveUnitEntry subEntry)
                                {
                                    subEntry.Status = EmbarkStatus.None;
                                }
                            }
                        }
                    }
                }
            }
        }

        RecalculateAll();
    }

    private void activeArmyTree_DragOver(object sender, DragEventArgs e)
    {
        Point clientPoint = activeArmyTree.PointToClient(new Point(e.X, e.Y));
        TreeNode targetNode = activeArmyTree.GetNodeAt(clientPoint);

        // Prevent highlighting or dropping on a Tercio subunit
        if (targetNode?.Parent?.Tag is ActiveUnitEntry pEntry && pEntry.Unit.name == "Tercios")
        {
            e.Effect = DragDropEffects.None;
            if (dropHighlightNode != null)
            {
                dropHighlightNode.BackColor = Color.Empty;
                dropHighlightNode = null;
            }
            return;
        }

        e.Effect = DragDropEffects.Move;

        if (targetNode == dropHighlightNode) return; // no change, skip redundant work

        if (dropHighlightNode != null)
        {
            dropHighlightNode.BackColor = Color.Empty;
        }

        if (targetNode != null && targetNode != draggedNode)
        {
            targetNode.BackColor = Color.LightSteelBlue;
        }

        dropHighlightNode = targetNode;
    }

    private void activeArmyTree_DragLeave(object sender, EventArgs e)
    {
        if (dropHighlightNode != null)
        {
            dropHighlightNode.BackColor = Color.Empty;
        }
        dropHighlightNode = null;
    }

    private void reformatButton_Click(object sender, EventArgs e)
    {
        if (currentTargetGroup == null)
        {
            MessageBox.Show("Please select a group or a unit within a group to format.", "No Group Selected");
            return;
        }

        RecalculateAll();

        activeArmyTree.BeginUpdate();

        List<TreeNode> allNodes = currentTargetGroup.Nodes.Cast<TreeNode>().ToList();

        List<TreeNode> roots = new List<TreeNode>();
        Dictionary<TreeNode, List<TreeNode>> childrenMap = new Dictionary<TreeNode, List<TreeNode>>();

        foreach (TreeNode node in allNodes)
        {
            if (node.Tag is not ActiveUnitEntry entry) continue;

            bool isCommitted = false;
            TreeNode parentProvider = null;

            if (entry.Unit.name == "Tercios")
            {
                // A Tercio parent is committed if any of its children are embarked or desanted
                var subEntries = node.Nodes.Cast<TreeNode>().Select(n => (ActiveUnitEntry)n.Tag).ToList();
                var interactingChild = subEntries.FirstOrDefault(se => se.Status != EmbarkStatus.None && se.RelationParentNode != null);

                if (interactingChild != null)
                {
                    isCommitted = true;
                    parentProvider = interactingChild.RelationParentNode;
                }
            }
            else
            {
                // Standard unit commitment check
                isCommitted = (entry.Status != EmbarkStatus.None || entry.IsTowed) && entry.RelationParentNode != null;
                parentProvider = entry.RelationParentNode;
            }

            if (isCommitted && parentProvider != null)
            {
                if (!childrenMap.ContainsKey(parentProvider))
                    childrenMap[parentProvider] = new List<TreeNode>();

                childrenMap[parentProvider].Add(node);
            }
            else
            {
                roots.Add(node); // Uncommitted / out-of-position units (and broken Tercios) go to roots
            }
        }

        // Sort the independent units into the ideal layout order
        var sortedRoots = roots.OrderBy(n =>
        {
            var entry = (ActiveUnitEntry)n.Tag;
            if (entry.Unit.type == "TACOM") return 0;
            if (IsPassenger(entry.Unit)) return 1;
            if (IsVehicle(entry.Unit)) return 2;
            if (IsAircraft(entry.Unit)) return 3;
            return 4;
        }).ToList();

        List<TreeNode> flattenedOrder = new List<TreeNode>();

        // Recursive function to rebuild the flat list in the correct relationship order
        void FlattenNode(TreeNode node)
        {
            flattenedOrder.Add(node);

            if (childrenMap.TryGetValue(node, out var children))
            {
                var embarked = children.Where(c =>
                {
                    var ce = (ActiveUnitEntry)c.Tag;
                    return ce.Unit.name == "Tercios" || ce.Status == EmbarkStatus.Embarked;
                });

                var desanted = children.Where(c =>
                {
                    var ce = (ActiveUnitEntry)c.Tag;
                    return ce.Unit.name != "Tercios" && ce.Status == EmbarkStatus.Desanted;
                });

                var towed = children.Where(c =>
                {
                    var ce = (ActiveUnitEntry)c.Tag;
                    return ce.Unit.name != "Tercios" && ce.IsTowed;
                });

                // Sort children and flatten recursively
                foreach (var child in embarked.OrderBy(c => ((ActiveUnitEntry)c.Tag).Unit.type == "TACOM" ? 0 : 1))
                    FlattenNode(child);

                foreach (var child in desanted.OrderBy(c => ((ActiveUnitEntry)c.Tag).Unit.type == "TACOM" ? 0 : 1))
                    FlattenNode(child);

                foreach (var child in towed.OrderBy(c => ((ActiveUnitEntry)c.Tag).Unit.type == "TACOM" ? 0 : 1))
                    FlattenNode(child);
            }
        }

        foreach (var root in sortedRoots)
        {
            FlattenNode(root);
        }

        currentTargetGroup.Nodes.Clear();
        currentTargetGroup.Nodes.AddRange(flattenedOrder.ToArray());

        activeArmyTree.EndUpdate();

        RecalculateAll();
    }

    private void newTercioButton_Click(object sender, EventArgs e)
    {
        if (currentTargetGroup == null)
        {
            MessageBox.Show("Select or create a group first.", "No group selected");
            return;
        }

        // Pass the current faction's units into the builder
        using (TercioBuilder builder = new TercioBuilder(factionUnits))
        {
            if (builder.ShowDialog() == DialogResult.OK)
            {
                // Create the parent Tercio dummy unit
                UnitTemplate tercioDummy = new UnitTemplate
                {
                    name = "Tercios",
                    cost = builder.TotalCost,
                    type = "Infantry",
                    subname = "",
                    unit_stats = "Inf",
                    bonus_traits = "",
                    keywords = new List<string> { $"Group ({builder.TotalSize})" }, // Set keyword to read weight
                    weapons = new List<Weapon>()
                };

                ActiveUnitEntry parentEntry = new ActiveUnitEntry(tercioDummy, $"Tercio");

                TreeNode parentNode = new TreeNode();
                parentNode.Tag = parentEntry;
                parentNode.Text = $"Tercios ({builder.TotalCost} pts) ({builder.TotalSize} size) \"{parentEntry.CustomName}\"";

                // Add the selected subunits and assign them default names
                foreach (UnitTemplate member in builder.SelectedTercioUnits)
                {
                    // Give it a default name exactly like standard units get
                    ActiveUnitEntry childEntry = new ActiveUnitEntry(member, $"Unit {nextUnitNumber++}");

                    TreeNode childNode = new TreeNode();
                    childNode.Tag = childEntry;

                    // Display the custom name inline so it can be edited later
                    childNode.Text = "";

                    parentNode.Nodes.Add(childNode);
                }

                currentTargetGroup.Nodes.Add(parentNode);
                currentTargetGroup.ExpandAll();

                RecalculateAll();
            }
        }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
        using (SaveMenu saveDialog = new SaveMenu(currentSaveFormat, maxPoints))
        {
            if (previousSaveName != null)
            {
                saveDialog.armyNameTextBox.Text = previousSaveName;
            }

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                previousSaveName = saveDialog.armyNameTextBox.Text; // Pull pre strip and format for consistancy
                armyName = armyName;
                armyNameLabel.Text = armyName;
                SaveArmyToFile(saveDialog.FinalFileName);
            }
        }
    }

    public void SaveArmyToFile(string customFileName)
    {
        ArmySaveData saveData = new ArmySaveData
        {
            ArmyName = armyName,
            FactionName = factionNameLabel.Text,
            MaxPoints = maxPoints,
            ExistingSaveName = previousSaveName // Can be null
        };

        foreach (TreeNode groupNode in activeArmyTree.Nodes)
        {
            SavedGroup group = new SavedGroup { GroupName = groupNode.Text };

            foreach (TreeNode child in groupNode.Nodes)
            {
                if (child.Tag is not ActiveUnitEntry entry) continue;

                SavedUnit sUnit = new SavedUnit
                {
                    UnitId = entry.Unit.id ?? entry.Unit.name,
                    CustomName = entry.CustomName,
                    EmbarkStatus = entry.Status.ToString(),
                    IsTowed = entry.IsTowed,
                    IsTercioParent = entry.Unit.name == "Tercios"
                };

                // If it's a Tercio parent, grab its stats
                if (sUnit.IsTercioParent)
                {
                    sUnit.TercioCost = entry.Unit.cost;

                    string groupKw = entry.Unit.keywords?.FirstOrDefault(k => k.StartsWith("Group ("));
                    if (groupKw != null)
                    {
                        var match = Regex.Match(groupKw, @"\d+");
                        if (match.Success) sUnit.TercioSize = int.Parse(match.Value);
                    }

                    // Initialize the nested list and populate it with the children
                    sUnit.TercioChildren = new List<SavedUnit>();
                    foreach (TreeNode subChild in child.Nodes)
                    {
                        if (subChild.Tag is not ActiveUnitEntry subEntry) continue;

                        sUnit.TercioChildren.Add(new SavedUnit
                        {
                            UnitId = subEntry.Unit.id ?? subEntry.Unit.name,
                            CustomName = subEntry.CustomName,
                            EmbarkStatus = subEntry.Status.ToString(),
                            IsTowed = subEntry.IsTowed,
                            IsTercioParent = false
                        });
                    }
                }

                // Add the unit to the group (if it's a Tercio, children are nested)
                group.Units.Add(sUnit);
            }
            saveData.Groups.Add(group);
        }

        string savesFolder = Path.Combine(Application.StartupPath, "Saves");
        Directory.CreateDirectory(savesFolder);

        string filePath = Path.Combine(savesFolder, customFileName);

        string jsonOutput = System.Text.Json.JsonSerializer.Serialize(saveData, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, jsonOutput);

        quickSaveButton.Enabled = true; // Unlock quick save once save slot is established
        MessageBox.Show($"Army saved successfully as:\n{customFileName}", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public void LoadArmyFromFile(string filePath)
    {
        if (!System.IO.File.Exists(filePath)) return;

        try
        {
            string jsonString = System.IO.File.ReadAllText(filePath);
            ArmySaveData saveData = System.Text.Json.JsonSerializer.Deserialize<ArmySaveData>(jsonString);

            if (saveData == null) return;

            SelectFaction(saveData.FactionName, saveData.ArmyName, saveData.MaxPoints);

            activeArmyTree.BeginUpdate();
            activeArmyTree.Nodes.Clear();
            ClearTargetGroup();

            // Restore basic army info
            armyName = saveData.ArmyName;
            armyNameLabel.Text = armyName;
            maxPoints = saveData.MaxPoints;
            nextUnitNumber = 1;

            // Restore old save name when loading
            if (saveData.ExistingSaveName != null)
            {
                previousSaveName = saveData.ExistingSaveName;
                quickSaveButton.Enabled = true;
            }

            foreach (var savedGroup in saveData.Groups)
            {
                TreeNode groupNode = new TreeNode(savedGroup.GroupName);
                groupNode.Tag = "GROUP";

                foreach (var sUnit in savedGroup.Units)
                {
                    if (sUnit.IsTercioParent)
                    {
                        // Rebuild the Tercio Dummy Wrapper
                        UnitTemplate tercioDummy = new UnitTemplate
                        {
                            name = "Tercios",
                            cost = sUnit.TercioCost,
                            type = "Infantry",
                            subname = "",
                            unit_stats = "Inf",
                            bonus_traits = "",
                            keywords = new List<string> { $"Group ({sUnit.TercioSize})" },
                            weapons = new List<Weapon>()
                        };

                        ActiveUnitEntry parentEntry = new ActiveUnitEntry(tercioDummy, sUnit.CustomName);
                        TreeNode parentNode = new TreeNode();
                        parentNode.Tag = parentEntry;

                        // Load the nested subunits
                        if (sUnit.TercioChildren != null)
                        {
                            foreach (var sChild in sUnit.TercioChildren)
                            {
                                // Match the saved reference ID against loaded faction units
                                UnitTemplate childTemplate = factionUnits.FirstOrDefault(u => (u.id ?? u.name) == sChild.UnitId);
                                if (childTemplate == null) continue;

                                ActiveUnitEntry childEntry = new ActiveUnitEntry(childTemplate, sChild.CustomName);

                                if (Enum.TryParse(sChild.EmbarkStatus, out EmbarkStatus status))
                                    childEntry.Status = status;
                                childEntry.IsTowed = sChild.IsTowed;

                                TreeNode childNode = new TreeNode();
                                childNode.Tag = childEntry;
                                parentNode.Nodes.Add(childNode);
                            }
                        }

                        groupNode.Nodes.Add(parentNode);
                    }
                    else
                    {
                        // Load standard independent units
                        UnitTemplate unitTemplate = factionUnits.FirstOrDefault(u => (u.id ?? u.name) == sUnit.UnitId);
                        if (unitTemplate == null) continue;

                        ActiveUnitEntry entry = new ActiveUnitEntry(unitTemplate, sUnit.CustomName);

                        if (Enum.TryParse(sUnit.EmbarkStatus, out EmbarkStatus status))
                            entry.Status = status;
                        entry.IsTowed = sUnit.IsTowed;

                        TreeNode node = new TreeNode();
                        node.Tag = entry;
                        groupNode.Nodes.Add(node);
                    }
                }

                activeArmyTree.Nodes.Add(groupNode);
            }

            activeArmyTree.ExpandAll();

            // Auto-focus the last group so the user can immediately keep building
            if (activeArmyTree.Nodes.Count > 0)
                SetTargetGroup(activeArmyTree.Nodes[activeArmyTree.Nodes.Count - 1]);
            else
                createNewGroup(); // Fallback if they managed to save an entirely empty army

            activeArmyTree.EndUpdate();

            // Run the math loop to reconstruct all the lines, prefixes, and capacities!
            RecalculateAll();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load army:\n{ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void playButton_Click(object sender, EventArgs e)
    {
        ArmySaveData army = ExportArmyData();

        PlayScreen playScreen = new PlayScreen();
        playScreen.LoadArmyFromData(army);
        playScreen.Show();

        this.Hide();
        playScreen.FormClosed += (s, args) =>
        {
            if (playScreen.ReturnToMenu)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        };
    }

    // Pass data without saving
    private ArmySaveData ExportArmyData()
    {
        ArmySaveData saveData = new ArmySaveData
        {
            ArmyName = armyName,
            FactionName = factionNameLabel.Text,
            MaxPoints = maxPoints
        };

        foreach (TreeNode groupNode in activeArmyTree.Nodes)
        {
            SavedGroup group = new SavedGroup { GroupName = groupNode.Text };

            foreach (TreeNode child in groupNode.Nodes)
            {
                if (child.Tag is not ActiveUnitEntry entry) continue;

                SavedUnit sUnit = new SavedUnit
                {
                    UnitId = entry.Unit.id ?? entry.Unit.name,
                    CustomName = entry.CustomName,
                    EmbarkStatus = entry.Status.ToString(),
                    IsTowed = entry.IsTowed,
                    IsTercioParent = entry.Unit.name == "Tercios"
                };

                if (sUnit.IsTercioParent)
                {
                    sUnit.TercioCost = entry.Unit.cost;

                    string groupKw = entry.Unit.keywords?.FirstOrDefault(k => k.StartsWith("Group ("));
                    if (groupKw != null)
                    {
                        var match = Regex.Match(groupKw, @"\d+");
                        if (match.Success) sUnit.TercioSize = int.Parse(match.Value);
                    }

                    // Initialize the nested list and populate it with the children
                    sUnit.TercioChildren = new List<SavedUnit>();
                    foreach (TreeNode subChild in child.Nodes)
                    {
                        if (subChild.Tag is not ActiveUnitEntry subEntry) continue;

                        sUnit.TercioChildren.Add(new SavedUnit
                        {
                            UnitId = subEntry.Unit.id ?? subEntry.Unit.name,
                            CustomName = subEntry.CustomName,
                            EmbarkStatus = subEntry.Status.ToString(),
                            IsTowed = subEntry.IsTowed,
                            IsTercioParent = false
                        });
                    }
                }

                // Add the unit to the group
                group.Units.Add(sUnit);
            }
            saveData.Groups.Add(group);
        }

        return saveData;
    }

    private void backToMenuButton_Click(object sender, EventArgs e)
    {
        ReturnToMenu = true;
        this.Close();
    }

    private void quickSaveToolStripMenuItem_Click(object sender, EventArgs e) => quickSaveButton_Click(sender, e);

    private void quickSaveButton_Click(object sender, EventArgs e)
    {
        if (previousSaveName == null)
        {
            MessageBox.Show("No previous save name found.", "Quick Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Modified version from save menu because reusing code is a crime ig
        string rawName = previousSaveName.Trim();

        // Strip any characters that Windows doesn't allow in file names (like \ / : * ? " < > |)
        string safeName = string.Join("_", rawName.ToLower().Replace(" ", "_").Split(Path.GetInvalidFileNameChars()));
        string safeFaction = string.Join("_", currentSaveFormat.Split(Path.GetInvalidFileNameChars()));

        // Construct the final file name automatically
        string FinalFileName = $"{safeName}_{safeFaction}_{maxPoints}_pts.json";

        // Override confirmation if the file already exists
        string savesFolder = Path.Combine(Application.StartupPath, "Saves");
        string filePath = Path.Combine(savesFolder, FinalFileName);

        SaveArmyToFile(FinalFileName);
    }
}