using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelockCompanion
{
    public partial class ArmyStats : Form
    {
        private Dictionary<string, Label> roleCountLabels = new Dictionary<string, Label>();
        private bool useClasses = false; // True = use the official classes, false = use my roles

        public ArmyStats()
        {
            InitializeComponent();
        }

        public void PopulateRoles(Dictionary<string, RoleInfo> roleDefinitions)
        {
            useClasses = false; // A.k.a the right way :unamused:
            // Cull non-shown roles
            var visibleRoles = (roleDefinitions ?? new Dictionary<string, RoleInfo>()).Where(role => role.Value != null && role.Value.show_in_info).Select(r => r.Key).ToList(); // This line made with the help of AI
            BuildTables(visibleRoles);
        }

        // Load official classes instead of roles into table
        public void PopualteClasses(List<string> classes)
        {
            useClasses = true;
            var visibleClasses = (classes ?? Enumerable.Empty<string>()).Where(c => c != null).Distinct().ToList(); // Also made this line with AI because I refuse to learn LINQ
            BuildTables(visibleClasses); // Also visible classes is not really a good name beacuse they're all visible but naming unity
        }

        private void BuildTables(List<string> names)
        {
            rolesTable.SuspendLayout();
            rolesTable.Controls.Clear();
            roleCountLabels.Clear();

            rolesTable.RowCount = names.Count; // Because this got called so many times
            for (int i = 0; i < names.Count; i++)
            {
                string name = names[i];
                var countLabel = new Label
                {
                    Name = $"{name.Replace(" ", "")}CountLabel",
                    Text = "X",
                    AutoSize = true,
                    Anchor = AnchorStyles.Right
                };
                var nameLabel = new Label
                {
                    Name = $"{name.Replace(" ", "")}Label",
                    Text = name,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left
                };

                rolesTable.Controls.Add(countLabel, 0, i);
                rolesTable.Controls.Add(nameLabel, 1, i);
                roleCountLabels[name] = countLabel;
            }

            rolesTable.ResumeLayout();
            roleBox.Size = new Size(roleBox.Size.Width, (names.Count * 20) + 23);

            // To do: set the window size to contain the group box, no more, and no less than the other groups
        }

        public void UpdateStats(List<ArmyStatsEntry> units)
        {
            var roleCounts = roleCountLabels.Keys.ToDictionary(role => role, role => 0);
            int totalUnits = 0;
            int totalCommand = 0;
            int nonLoaded = 0;
            int totalPoints = 0;
            int tacoms = 0;
            int infantry = 0;
            int vehicles = 0; 
            int aircraft = 0;

            // Probably should have changed the intake variable name but meh
            foreach (ArmyStatsEntry unit in units ?? Enumerable.Empty<ArmyStatsEntry>())
            {
                if (unit?.Unit == null) continue;

                // Oh hey, nice tercio exception where'd you get it? Anywhere.
                // Filters tercio parents except for things which they are actually counted for
                if (unit.IsTercioParent)
                {
                    totalPoints += unit.Unit.cost;
                    if (!unit.IsLoaded) nonLoaded++;
                    continue;
                }

                totalUnits++;
                totalCommand += unit.CommandValue;

                // Tercio exceptions probably make up >1% of the total codebase
                if (!unit.IsTercioChild)
                {
                    totalPoints += unit.Unit.cost;
                    if (!unit.IsLoaded) nonLoaded++;
                }

                switch (unit.Unit.type.ToLower())
                {
                    case "tacom": tacoms++; break;
                    case "infantry": infantry++; break;
                    case "vehicle": vehicles++; break;
                    case "aircraft": aircraft++; break;
                }

                if (useClasses)
                {
                    // One class per unit, optional
                    string c = unit.Unit.unit_class;
                    if (!string.IsNullOrEmpty(c) && roleCounts.ContainsKey(c))
                        roleCounts[c]++;
                }
                else if (unit.Unit.roles != null)
                {
                    // Distinct so a role listed twice on one unit can't count twice
                    foreach (string role in unit.Unit.roles.Where(r => r != null).Distinct())
                        if (roleCounts.ContainsKey(role))
                            roleCounts[role]++;
                }
            }

            foreach (var kvp in roleCounts) roleCountLabels[kvp.Key].Text = kvp.Value.ToString();

            tacomCountLabel.Text = tacoms.ToString();
            infantryCountLabel.Text = infantry.ToString();
            vehicleCountLabel.Text = vehicles.ToString();
            aircraftCountLabel.Text = aircraft.ToString();

            totalUnitCountLabel.Text = totalUnits.ToString();
            commandCountLabel.Text = totalCommand.ToString();
            nonLoadedCountLabel.Text = nonLoaded.ToString();
            pointAverageCountLabel.Text = totalUnits != 0 ? MathF.Round((float) totalPoints / (float) totalUnits, 1).ToString() : "0"; // Divide by zero catch
        }
    }
}
