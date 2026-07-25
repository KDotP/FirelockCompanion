using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FirelockCompanion;

public partial class PlayScreen : Form
{
    // Core Game Data caches
    private List<UnitTemplate> factionUnits;
    private Dictionary<string, string> normalizedKeywords = new Dictionary<string, string>();

    private static readonly Regex LeadTagPattern = new(@"^(Vec|Inf|Air)\s*(\(([^)]*)\))?");
    private static readonly Regex PcPattern = new(@"^PC\s*\((\d+)");
    private static readonly Regex TowCapacityPattern = new(@"^Tow\s*\((\d+)");
    private static readonly Regex TowWeightPattern = new(@"(?:^|,\s*)T(\d+)");
    private static readonly Regex ParamPattern = new(@"\s*\(.*\)\s*$");
    private static readonly Regex DesantCapacityPattern = new(@"^Desant\s*\((\d+)\)");
    private static readonly Regex LeviathanPattern = new(@"^Leviathan\s*\((\d+)\)");

    private static readonly TextFormatFlags MeasureFlags = TextFormatFlags.NoPadding | TextFormatFlags.SingleLine;

    private TreeNode currentTargetGroup;
    private static readonly Color TargetGroupColor = Color.PaleGreen;
    private static readonly Color OutOfPositionColor = Color.Firebrick;

    private readonly Dictionary<TreeNode, TreeNode> territoryLastCommitted = new();
    private readonly Dictionary<TreeNode, int> nodePhysicalIndex = new();

    private static string StripParams(string keyword) => ParamPattern.Replace(keyword, "").Trim();

    public PlayScreen()
    {
        InitializeComponent();

        activeArmyTree.AllowDrop = true;
        activeArmyTree.ItemDrag += activeArmyTree_ItemDrag;
        activeArmyTree.DragEnter += activeArmyTree_DragEnter;
        activeArmyTree.DragDrop += activeArmyTree_DragDrop;
        activeArmyTree.DragOver += activeArmyTree_DragOver;
        activeArmyTree.DragLeave += activeArmyTree_DragLeave;
        activeArmyTree.MouseDown += activeArmyTree_MouseDown;
        activeArmyTree.AfterSelect += activeArmyTree_AfterSelect;
    }

    // Function is very similarly named to LoadGameData, but this is for loading an army from a save file, not the core game data
    public void LoadArmyFromData(ArmySaveData saveData)
    {
        try
        {
            // Load the core ruleset to grab keywords and faction units for rebuilding
            LoadGameData(saveData.FactionName);

            armyNameLabel.Text = saveData.ArmyName;

            activeArmyTree.BeginUpdate();
            activeArmyTree.Nodes.Clear();
            ClearTargetGroup();

            foreach (var savedGroup in saveData.Groups)
            {
                TreeNode groupNode = new TreeNode(savedGroup.GroupName);
                groupNode.Tag = "GROUP";

                foreach (var sUnit in savedGroup.Units)
                {
                    if (sUnit.IsTercioParent)
                    {
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

                        if (sUnit.TercioChildren != null)
                        {
                            foreach (var sChild in sUnit.TercioChildren)
                            {
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

            if (activeArmyTree.Nodes.Count > 0)
                SetTargetGroup(activeArmyTree.Nodes[activeArmyTree.Nodes.Count - 1]);

            activeArmyTree.EndUpdate();
            RecalculateAll();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load army data:\n{ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void LoadArmyFromFile(string filePath)
    {
        if (!System.IO.File.Exists(filePath)) return;

        try
        {
            string jsonString = System.IO.File.ReadAllText(filePath);
            ArmySaveData saveData = System.Text.Json.JsonSerializer.Deserialize<ArmySaveData>(jsonString);

            if (saveData == null) return;

            LoadArmyFromData(saveData);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load army data:\n{ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadGameData(string factionName)
    {
        if (!System.IO.File.Exists("Data.json")) return;

        string jsonString = System.IO.File.ReadAllText("Data.json");
        GameData ruleset = System.Text.Json.JsonSerializer.Deserialize<GameData>(jsonString);

        factionNameLabel.Text = factionName;

        // Cache the units for this faction to reference while loading army
        if (ruleset.factions.ContainsKey(factionName))
        {
            factionUnits = ruleset.factions[factionName];
        }

        // Cache and normalize all game keywords for the detail panel
        normalizedKeywords.Clear();
        if (ruleset.keywords != null)
        {
            foreach (var kv in ruleset.keywords)
            {
                string baseKey = StripParams(kv.Key);
                if (!normalizedKeywords.ContainsKey(baseKey))
                    normalizedKeywords[baseKey] = kv.Value;
            }
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

    private void ShowNodeDetails(TreeNode node)
    {
        if (node?.Tag is not ActiveUnitEntry entry)
        {
            detailsTextBox.Text = node?.Text ?? "";
            return;
        }

        UnitTemplate unit = entry.Unit;
        List<string> parts = new List<string>();

        // Core stats
        parts.Add($"{unit.name} — \"{entry.CustomName}\"");
        if (!string.IsNullOrEmpty(unit.subname)) parts.Add(unit.subname);
        if (!string.IsNullOrEmpty(unit.unit_stats)) parts.Add(unit.unit_stats);
        if (!string.IsNullOrEmpty(unit.bonus_traits)) parts.Add(FormatDescription(unit.bonus_traits));
        if (unit.keywords != null && unit.keywords.Count > 0)
        {
            parts.Add($"Keywords: {string.Join(", ", unit.keywords)}");
        }

        // Weapons and ammo
        if (unit.weapons != null && unit.weapons.Count > 0)
        {
            List<string> weaponLines = new List<string>();
            foreach (var w in unit.weapons)
            {
                string wText = $"{w.name} {w.weapon_stats}";
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

        // Gather keywords
        HashSet<string> uniqueKeywords = new HashSet<string>();

        if (unit.keywords != null)
            foreach (var kw in unit.keywords) uniqueKeywords.Add(kw);

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

        // Write out all keywords
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

        detailsTextBox.Text = string.Join("\r\n\r\n", parts);
        detailsTextBox.SelectionStart = 0;
        detailsTextBox.SelectionLength = 0;
        detailsTextBox.ScrollToCaret();
    }

    private static string FormatDescription(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        return text.Replace("\n", "\r\n\r\n");
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

    private void ClearTargetGroup()
    {
        if (currentTargetGroup != null)
        {
            currentTargetGroup.BackColor = Color.Empty;
            currentTargetGroup = null;
        }
    }

    // Nvm about shared functions, adding more functionalities
    private string BuildFullNodeText(ActiveUnitEntry entry, TreeNode node)
    {
        string connectorPrefix = BuildConnectorPrefix(entry, node, out bool outOfPosition);
        bool isMismatchedTercio = false;

        // Depletion tracking
        int maxDep = GetMaxDepletions(entry.Unit);
        bool isDepleted = maxDep > 0 && entry.DepletionsTaken >= maxDep;

        if (entry.Unit.name == "Tercios" && node.Nodes.Count > 0)
        {
            var subEntries = node.Nodes.Cast<TreeNode>().Select(n => (ActiveUnitEntry)n.Tag).ToList();
            int committedCount = subEntries.Count(e => e.Status != EmbarkStatus.None);

            if (committedCount > 0 && committedCount < subEntries.Count)
            {
                isMismatchedTercio = true;
                outOfPosition = true;
            }
            else if (committedCount == subEntries.Count && committedCount > 0)
            {
                outOfPosition = false;
            }
        }

        if (isDepleted)
        {
            node.ForeColor = Color.Gray;
        }
        else
        {
            node.ForeColor = outOfPosition ? OutOfPositionColor : Color.Empty;
        }

        bool isTercioChild = node.Parent?.Tag is ActiveUnitEntry pEntry && pEntry.Unit.name == "Tercios";
        string baseText = isTercioChild
            ? $"{entry.Unit.name} "
            : $"{entry.Unit.name} ({entry.Unit.cost} pts) ";

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

        // Depletion display
        string depString = "";
        if (maxDep > 0)
        {
            int remaining = maxDep - entry.DepletionsTaken;
            bool isClicked = entry.DepletionsTaken > 0;
            depString = isClicked ? $"【{remaining}/{maxDep}】 " : $"[{remaining}/{maxDep}] ";
        }

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

        if (isMismatchedTercio) suffixParts.Add("Not all units in the same vehicle!");

        string suffix = suffixParts.Count > 0 ? $" — {string.Join(", ", suffixParts)}" : "";

        return $"{connectorPrefix}{depString}{tagPrefix}{baseText}{suffix}";
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
        return 2;
    }

    private static int GetMaxDepletions(UnitTemplate unit)
    {
        // TECIOSSSSSSSSS
        if (unit.name == "Tercios") return 0;

        if (unit.keywords != null)
        {
            foreach (string kw in unit.keywords)
            {
                // Leviathans
                Match m = LeviathanPattern.Match(kw);
                if (m.Success) return int.Parse(m.Groups[1].Value);
            }
        }

        Match leadMatch = LeadTagPattern.Match(unit.unit_stats ?? "");
        bool isInfantry = leadMatch.Success && leadMatch.Groups[1].Value == "Inf";

        bool hasSquadKeyword = unit.keywords != null && unit.keywords.Contains("Squad");
        bool hasSSuffix = leadMatch.Success && leadMatch.Groups[3].Success && leadMatch.Groups[3].Value.Contains("S");

        // Twice for infantry squads
        if (isInfantry && (hasSquadKeyword || hasSSuffix))
        {
            return 2;
        }

        // Default for everyone else
        return 1;
    }

    private string BuildConnectorPrefix(ActiveUnitEntry entry, TreeNode node, out bool outOfPosition)
    {
        outOfPosition = false;

        List<TreeNode> chain = GetVisibleAncestorChain(entry);
        entry.IndentLevel = chain.Count;

        if (chain.Count == 0) return "";

        int myIndex = nodePhysicalIndex.TryGetValue(node, out int idx) ? idx : -1;
        string prefix = "";

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
                    prefix += "│";
                    if (!isCommitted) outOfPosition = true;
                }
            }
        }

        return prefix;
    }

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

    private static bool SupportsDesant(UnitTemplate unit) => IsVehicle(unit);

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
                    entry.RelationParentNode = vehicleNode;

                    if (!desantSupported && entry.Status == EmbarkStatus.Desanted)
                        entry.Status = EmbarkStatus.None;
                }

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
                    if (entry.Unit.name == "Tercios")
                    {
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
                    var interactingChild = child.Nodes.Cast<TreeNode>()
                        .FirstOrDefault(n => ((ActiveUnitEntry)n.Tag).RelationParentNode != null);

                    if (interactingChild != null)
                    {
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
                    entry.RelationParentNode = providerNode;

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
                            entry.IsTowed = false;
                            entry.CanTow = towCapacity.HasValue && (usedWeight + weight <= towCapacity.Value) && (!isAircraftProvider || countTowed == 0);
                        }
                    }
                    else
                    {
                        entry.CanTow = towCapacity.HasValue && (usedWeight + weight <= towCapacity.Value) && (!isAircraftProvider || countTowed == 0);
                    }
                }

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

                if (isProvider && !entry.IsTowed && entry.Status == EmbarkStatus.None)
                {
                    FinalizeProvider();
                    providerNode = child;
                    towCapacity = GetTowCapacity(entry.Unit);
                    toweeNodes = new List<TreeNode>();
                }
                else if (isProvider && entry.IsTowed)
                {
                    entry.TowCapacityDisplay = null;
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

        int maxDep = GetMaxDepletions(entry.Unit);
        if (maxDep > 0)
        {
            int remaining = maxDep - entry.DepletionsTaken;
            bool isClicked = entry.DepletionsTaken > 0;
            string dTag = isClicked ? $"【{remaining}/{maxDep}】 " : $"[{remaining}/{maxDep}] ";
            int dWidth = TextRenderer.MeasureText(dTag, font, Size.Empty, MeasureFlags).Width;

            if (e.X <= x + dWidth)
            {
                // Cycles up by 1 under the hood. If it goes past max, it resets to 0.
                entry.DepletionsTaken = (entry.DepletionsTaken + 1) % (maxDep + 1);
                RecalculateAll();
                return;
            }
            x += dWidth;
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

    // Drag and drop logic
    private TreeNode draggedNode;
    private TreeNode dropHighlightNode;

    private void activeArmyTree_ItemDrag(object sender, ItemDragEventArgs e)
    {
        if (e.Item is TreeNode node && node.Tag is ActiveUnitEntry)
        {
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

        dragged.Remove();

        if (targetNode.Tag as string == "GROUP")
        {
            targetNode.Nodes.Insert(0, dragged);
            targetNode.Expand();
        }
        else if (targetNode.Tag is ActiveUnitEntry)
        {
            if (targetNode.Parent?.Tag is ActiveUnitEntry parentEntry && parentEntry.Unit.name == "Tercios")
            {
                TreeNode groupFolder = targetNode.Parent.Parent;
                int parentIndex = groupFolder.Nodes.IndexOf(targetNode.Parent);
                groupFolder.Nodes.Insert(parentIndex + 1, dragged);
            }
            else
            {
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
            draggedEntry.Status = EmbarkStatus.None;
            draggedEntry.IsTowed = false;

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

            if (IsCarrier(draggedEntry.Unit) && dragged.Parent != null)
            {
                int draggedIdx = dragged.Parent.Nodes.IndexOf(dragged);
                for (int i = draggedIdx + 1; i < dragged.Parent.Nodes.Count; i++)
                {
                    if (dragged.Parent.Nodes[i].Tag is ActiveUnitEntry siblingEntry)
                    {
                        if (IsCarrier(siblingEntry.Unit)) break;

                        siblingEntry.Status = EmbarkStatus.None;

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

        if (targetNode == dropHighlightNode) return;

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

    // Reformat button (not added yet)
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
                roots.Add(node);
            }
        }

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
}