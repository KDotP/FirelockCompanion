using System.Text.RegularExpressions;

namespace FirelockCompanion;

public partial class Window : Form
{
    // Toggles whether to display incompatible options
    private static readonly bool Verbose = false;

    // -- Embark Status --
    private static readonly Regex LeadTagPattern = new(@"^(Vec|Inf|Air)\s*(\(([^)]*)\))?");
    private static readonly Regex PcPattern = new(@"^PC\s*\((\d+)");
    private static readonly Regex TowCapacityPattern = new(@"^Tow\s*\((\d+)");
    private static readonly Regex TowWeightPattern = new(@"(?:^|,\s*)T(\d+)");
    private static readonly TextFormatFlags MeasureFlags = TextFormatFlags.NoPadding | TextFormatFlags.SingleLine; // For special formatting with embark

    // Point management

    private int maxPoints = 300;
    private int RequiredTacoms => (int)Math.Ceiling(maxPoints / 100.0);

    private int nextUnitNumber = 1;

    private static readonly Regex ParamPattern = new(@"\s*\(.*\)\s*$");
    private record KeywordInfo(string Keyword, string Description);
    private TreeNode currentTargetGroup;
    private static readonly Color TargetGroupColor = Color.PaleGreen;
    private static readonly Color OutOfPositionColor = Color.Firebrick;

    // -- Connector / relationship rendering --
    // Scope root (carrier or tow provider) -> the last physically-committed member node in its territory.
    // A scope only has a visual shape at all if it appears as a key here.
    private readonly Dictionary<TreeNode, TreeNode> territoryLastCommitted = new();
    // Node -> its physical position within its group's flat child list (for span comparisons).
    private readonly Dictionary<TreeNode, int> nodePhysicalIndex = new();

    private static string StripParams(string keyword) =>
        ParamPattern.Replace(keyword, "").Trim();

    public Window()
    {
        // Probably don't do anything before this
        InitializeComponent();

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

        // Fill out available units
        PopulateAvailableUnits(ruleset, "The Army of the Ebon Forest");
        RecalculateArmyTotals();

        // First group
        createNewGroup();
    }

    private string BuildFullNodeText(ActiveUnitEntry entry, TreeNode node)
    {
        string connectorPrefix = BuildConnectorPrefix(entry, node, out bool outOfPosition);
        node.ForeColor = outOfPosition ? OutOfPositionColor : Color.Empty;

        string baseText = $"{entry.Unit.name} ({entry.Unit.cost} pts) \"{entry.CustomName}\"";
        var prefixTags = new List<string>();

        if (entry.HasCarrierAbove)
        {
            prefixTags.Add(entry.Status == EmbarkStatus.Embarked ? "【E】" : entry.CanEmbark ? "[E]" : "[-E-]");
            if (entry.HasDesantCarrierAbove)
                prefixTags.Add(entry.Status == EmbarkStatus.Desanted ? "【D】" : entry.CanDesant ? "[D]" : "[-D-]");
        }

        if (entry.HasTowProviderAbove)
            prefixTags.Add(entry.IsTowed ? "【T】" : entry.CanTow ? "[T]" : "[-T-]");

        string tagPrefix = prefixTags.Count > 0 ? string.Join(" ", prefixTags) + " " : "";

        var suffixParts = new List<string>();
        if (entry.EmbarkCapacityDisplay.HasValue) suffixParts.Add($"Embark {entry.EmbarkUsedDisplay}/{entry.EmbarkCapacityDisplay.Value}");
        if (entry.ShowDesantSuffix) suffixParts.Add($"Desant {entry.DesantUsedDisplay}/2");
        if (entry.TowCapacityDisplay.HasValue) suffixParts.Add($"Tow {entry.TowUsedDisplay}/{entry.TowCapacityDisplay.Value}");

        string suffix = suffixParts.Count > 0 ? $" — {string.Join(", ", suffixParts)}" : "";

        return $"{connectorPrefix}{tagPrefix}{baseText}{suffix}";
    }

    // The chain of ancestor territories this entry sits inside, outermost first — skipping any
    // ancestor whose territory currently has zero committed members (it has no visual shape at all,
    // so it contributes no column and no indentation, per the "Idle Trooper" rule).
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

    // Builds the leading connector glyphs (│ ├ └) for one row, one character per visible ancestor
    // territory, outermost to innermost. Also reports whether this row is "out of position": eligible
    // for its own direct territory, physically still inside that territory's span, but not committed.
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

                    // If this unit is physically inside ANY active territory span and is NOT 
                    // successfully committed to its assigned direct parent, it is out of position.
                    if (!isCommitted)
                    {
                        outOfPosition = true;
                    }
                }
            }
            else
            {
                // Past this territory's close — it's already resolved, nothing more to show here.
                prefix += " ";
            }
        }

        return prefix + " ";
    }

    // Resets everything the connector/relationship pass derives, so each RecalculateAll starts clean.
    private void ClearRelationshipState()
    {
        territoryLastCommitted.Clear();
        nodePhysicalIndex.Clear();

        foreach (TreeNode groupNode in activeArmyTree.Nodes)
            foreach (TreeNode child in groupNode.Nodes)
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
            foreach (TreeNode child in groupNode.Nodes)
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

    private static EmbarkRole GetEmbarkRole(UnitTemplate unit)
    {
        Match m = LeadTagPattern.Match(unit.unit_stats ?? "");
        if (!m.Success) return EmbarkRole.None;
        return m.Groups[1].Value == "Vec" ? EmbarkRole.Carrier : EmbarkRole.Passenger;
    }

    private static int EmbarkWeight(UnitTemplate unit)
    {
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

    // Refactored out of the old SupportsDesant — same underlying check, now reused for towing too
    private static bool IsVehicle(UnitTemplate unit)
    {
        Match m = LeadTagPattern.Match(unit.unit_stats ?? "");
        return m.Success && m.Groups[1].Value == "Vec";
    }

    private static bool SupportsDesant(UnitTemplate unit) => IsVehicle(unit);

    private void renameToolStripMenuItem_Click(object sender, EventArgs e) => RenameSelectedNode();
    private void deleteToolStripMenuItem_Click(object sender, EventArgs e) => RemoveSelectedNode();

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
    {

    }

    private void textBox1_TextChanged_1(object sender, EventArgs e)
    {

    }

    private void CreateNewGroup_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {

    }

    private void CreateGroupParent_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {

    }

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
        string defaultGroupName = $"{activeArmyTree.Nodes.Count + 1}e Groupe";

        TreeNode groupNode = new TreeNode(defaultGroupName);
        groupNode.Tag = "GROUP";

        activeArmyTree.Nodes.Add(groupNode);
        activeArmyTree.ExpandAll();

        SetTargetGroup(groupNode); // Automatically set focus to new group
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

        if (e.Node.Tag is not ActiveUnitEntry entry) return; // groups: default behavior is fine as-is

        if (string.IsNullOrWhiteSpace(e.Label))
        {
            e.CancelEdit = true;
        }
        else
        {
            entry.CustomName = e.Label.Trim();
        }

        e.Node.Text = BuildFullNodeText(entry, e.Node); // always rebuild — covers both "typed something" and "cancelled" cases
        e.CancelEdit = true; // we're setting Text ourselves either way
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

        List<UnitTemplate> factionUnits = data.factions[selectedFactionName];
        Dictionary<string, TreeNode> categoryNodes = new Dictionary<string, TreeNode>();
        Dictionary<string, string> normalizedKeywords = BuildNormalizedKeywords(data.keywords);

        foreach (UnitTemplate unit in factionUnits)
        {
            // 1. Core Category Generation (e.g., Infantry, Vehicles)
            if (!categoryNodes.ContainsKey(unit.type))
            {
                TreeNode newCategory = new TreeNode(unit.type);
                newCategory.Tag = "CATEGORY";
                categoryNodes[unit.type] = newCategory;
                availableArmyTree.Nodes.Add(newCategory);
            }

            // 2. Unit Root Node (Minimized by default)
            TreeNode unitNode = new TreeNode($"{unit.name} ({unit.cost} pts)");
            unitNode.Tag = unit;
            categoryNodes[unit.type].Nodes.Add(unitNode);

            // --- Layer 1 Subnodes ---

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
            // Only one keyword: the header IS the keyword. No children, no expand arrow.
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

    private void ShowNodeDetails(TreeNode node)
    {
        detailsTextBox.SelectionStart = 0;
        detailsTextBox.SelectionLength = 0;
        detailsTextBox.ScrollToCaret();

        if (node?.Tag == null)
        {
            detailsTextBox.Text = node?.Text ?? "";
            return;
        }

        string BuildUnitDetailsText(UnitTemplate unit, string headerOverride = null)
        {
            var parts = new List<string> { headerOverride ?? unit.name };
            if (!string.IsNullOrEmpty(unit.subname)) parts.Add(unit.subname);
            if (!string.IsNullOrEmpty(unit.unit_stats)) parts.Add(unit.unit_stats);
            if (!string.IsNullOrEmpty(unit.bonus_traits)) parts.Add(FormatDescription(unit.bonus_traits));
            return string.Join("\r\n\r\n", parts);
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
                    detailsTextBox.Text = BuildUnitDetailsText(unit);
                    break;
                }

            case ActiveUnitEntry activeEntry:
                {
                    detailsTextBox.Text = BuildUnitDetailsText(activeEntry.Unit, $"{activeEntry.Unit.name} — \"{activeEntry.CustomName}\"");
                    break;
                }

            default:
                detailsTextBox.Text = node.Text;
                break;
        }
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
        unitNode.Text = BuildFullNodeText(entry, unitNode); // placeholder text — RecalculateAll below rebuilds it correctly

        currentTargetGroup.Nodes.Add(unitNode);
        currentTargetGroup.Expand();

        RecalculateAll();
    }

    private void RecalculateArmyTotals()
    {
        int totalPoints = 0;
        int totalTacoms = 0;

        void Walk(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is ActiveUnitEntry entry)
                {
                    totalPoints += entry.Unit.cost;
                    if (entry.Unit.type == "TACOM")
                        totalTacoms++;
                }

                if (node.Nodes.Count > 0)
                    Walk(node.Nodes);
            }
        }

        Walk(activeArmyTree.Nodes);

        pointsLabel.Text = $"Points {totalPoints:000}/{maxPoints:000}";
        tacomLabel.Text = $"TACOMs: {totalTacoms}/{RequiredTacoms}";

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
                    entry.CanDesant = desantSupported && othersDesant + weight <= 2;
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
                    territoryLastCommitted[vehicleNode] = lastCommittedPassenger;

                vEntry.EmbarkCapacityDisplay = capacity;
                vEntry.EmbarkUsedDisplay = embarkUsed;
                vEntry.ShowDesantSuffix = desantSupported && passengerNodes.Count > 0;
                vEntry.DesantUsedDisplay = desantUsed;
            }

            foreach (TreeNode child in groupNode.Nodes)
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
                    if (vehicleNode != null)
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
                    entry.EmbarkCapacityDisplay = null;
                    entry.ShowDesantSuffix = false;
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

                int used = entries.Where(x => x.Entry.IsTowed).Sum(x => GetTowWeight(x.Entry.Unit));

                foreach (var (node, entry) in entries)
                {
                    int weight = GetTowWeight(entry.Unit);
                    int others = used - (entry.IsTowed ? weight : 0);

                    entry.HasTowProviderAbove = true;
                    entry.CanTow = towCapacity.HasValue && others + weight <= towCapacity.Value;
                    entry.RelationParentNode = providerNode; // in range of this provider's territory, committed or not

                    if (entry.IsTowed && !entry.CanTow) entry.IsTowed = false; // safety net, e.g. after a structural change
                }

                // Same rule as embark territories: shape closes at the last committed towee, or doesn't exist yet.
                TreeNode lastCommittedTowee = entries
                    .Where(x => x.Entry.IsTowed)
                    .Select(x => x.Node)
                    .LastOrDefault();
                if (lastCommittedTowee != null)
                    territoryLastCommitted[providerNode] = lastCommittedTowee;

                pEntry.TowCapacityDisplay = towCapacity;
                pEntry.TowUsedDisplay = used;
            }

            foreach (TreeNode child in groupNode.Nodes)
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

                // A vehicle currently being towed cannot itself act as a provider —
                // its own Tow(X), if any, is suppressed while it's the one being pulled.
                if (isProvider && !entry.IsTowed && entry.Status == EmbarkStatus.None)
                {
                    FinalizeProvider();
                    providerNode = child;
                    towCapacity = GetTowCapacity(entry.Unit);
                    toweeNodes = new List<TreeNode>();
                }
                else if (isProvider && entry.IsTowed)
                {
                    entry.TowCapacityDisplay = null; // suppressed — don't show a Tow suffix while towed
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
            foreach (TreeNode child in groupNode.Nodes)
                if (child.Tag is ActiveUnitEntry entry)
                    child.Text = BuildFullNodeText(entry, child);

        RecalculateArmyTotals();
    }

    private void activeArmyTree_MouseDown(object sender, MouseEventArgs e)
    {
        TreeNode node = activeArmyTree.GetNodeAt(e.X, e.Y);
        if (node == null) return;

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
            string eTag = (entry.Status == EmbarkStatus.Embarked ? "【E】" : entry.CanEmbark ? "[E]" : "[-E-]") + " ";
            int eWidth = TextRenderer.MeasureText(eTag, font, Size.Empty, MeasureFlags).Width;

            if (e.X <= x + eWidth)
            {
                if (entry.Status == EmbarkStatus.Embarked || entry.CanEmbark)
                {
                    entry.Status = entry.Status == EmbarkStatus.Embarked ? EmbarkStatus.None : EmbarkStatus.Embarked;
                    RecalculateAll();
                }
                return;
            }
            x += eWidth;

            if (!entry.HasDesantCarrierAbove) return;

            string dTag = (entry.Status == EmbarkStatus.Desanted ? "【D】" : entry.CanDesant ? "[D]" : "[-D-]") + " ";
            int dWidth = TextRenderer.MeasureText(dTag, font, Size.Empty, MeasureFlags).Width;

            if (e.X <= x + dWidth && (entry.Status == EmbarkStatus.Desanted || entry.CanDesant))
            {
                entry.Status = entry.Status == EmbarkStatus.Desanted ? EmbarkStatus.None : EmbarkStatus.Desanted;
                RecalculateAll();
            }
            return;
        }

        if (entry.HasTowProviderAbove)
        {
            string tTag = (entry.IsTowed ? "【T】" : entry.CanTow ? "[T]" : "[-T-]") + " ";
            int tWidth = TextRenderer.MeasureText(tTag, font, Size.Empty, MeasureFlags).Width;

            if (e.X <= x + tWidth && (entry.IsTowed || entry.CanTow))
            {
                entry.IsTowed = !entry.IsTowed;
                RecalculateAll();
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
            // Dropped on a unit — becomes the next sibling right after it, no top/bottom split
            TreeNode parentGroup = targetNode.Parent;
            int targetIndex = parentGroup.Nodes.IndexOf(targetNode);
            parentGroup.Nodes.Insert(targetIndex + 1, dragged);
        }
        else
        {
            // Dropped on something else (category, keyword, etc. — shouldn't happen in this tree)
            return;
        }

        activeArmyTree.SelectedNode = dragged;

        if (dragged.Tag is ActiveUnitEntry draggedEntry)
        {
            draggedEntry.Status = EmbarkStatus.None; // moving always clears embark/desant, per your rule
        }

        RecalculateAll();
    }

    private void activeArmyTree_DragOver(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Move;

        Point clientPoint = activeArmyTree.PointToClient(new Point(e.X, e.Y));
        TreeNode targetNode = activeArmyTree.GetNodeAt(clientPoint);

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
}