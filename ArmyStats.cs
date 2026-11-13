using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelockCompanion
{
    public partial class ArmyStats : Form
    {
        private Dictionary<string, Label> roleCountLabels = new Dictionary<string, Label>();

        public ArmyStats()
        {
            InitializeComponent();
        }

        public async Task FollowWindow(Form follow, FollowSide side)
        {
            while (!IsDisposed && !follow.IsDisposed)
            {
                // if (follow.WindowState == FormWindowState.Maximized) return; // This is a copy of a function from armystats, update this too when you figure out what to do when maximized

                await Task.Delay(10); // I don't think this draws performance too much, but keep an eye on this
                if (side == FollowSide.Right) Location = new Point(follow.Location.X + follow.Width - 16, follow.Location.Y);
                else Location = new Point(follow.Location.X - Width + 16, follow.Location.Y);

                if (!follow.Visible) Visible = false;
                else Visible = true;
            }
        }

        public void PopulateRoles(Dictionary<string, RoleInfo> roleDefinitions)
        {
            rolesTable.SuspendLayout();

            rolesTable.Controls.Clear();
            roleCountLabels.Clear();

            // Cull non-shown roles
            var visibleRoles = (roleDefinitions ?? new Dictionary<string, RoleInfo>()).Where(role => role.Value != null && role.Value.show_in_info).ToList(); // This line made with the help of AI
            int visibleRoleCount = visibleRoles.Count; // Because this got called so many times

            // Add roles to table
            rolesTable.RowCount = visibleRoleCount;
            for (int i = 0; i < visibleRoleCount; i++)
            {
                string roleName = visibleRoles[i].Key;
                Label roleCountLabel = new Label
                {
                    Name = $"{roleName.Replace(" ", "")}CountLabel",
                    Text = "X",
                    AutoSize = true,
                    Anchor = AnchorStyles.Right
                };

                Label roleLabel = new Label
                {
                    Name = $"{roleName.Replace(" ", "")}Label",
                    Text = roleName,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left
                };

                rolesTable.Controls.Add(roleCountLabel, 0, i);
                rolesTable.Controls.Add(roleLabel, 1, i);
                roleCountLabels[roleName] = roleCountLabel;
            }

            rolesTable.ResumeLayout();

            // Resize named group thing
            roleBox.Size = new Size(roleBox.Size.Width, (visibleRoleCount * 20) + 23); // I know, it annoys me too

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

                if (unit.Unit.roles == null) continue;

                // Distinct so a role listed twice on one unit can't count twice
                foreach (string role in unit.Unit.roles.Where(r => r != null).Distinct())
                {
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
            pointAverageCountLabel.Text = MathF.Round(totalPoints / totalUnits, 0).ToString();
        }
    }
}
