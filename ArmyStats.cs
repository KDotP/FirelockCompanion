using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FirelockCompanion
{
    public partial class ArmyStats : Form
    {
        private Dictionary<string, Label> roleCountLabels = new Dictionary<string, Label>();

        public ArmyStats()
        {
            InitializeComponent();
        }

        public async Task FollowMainWindow(Form follow)
        {
            while (true)
            {
                await Task.Delay(10); // I don't think this draws performance too much, but keep an eye on this
                this.Location = new Point(follow.Location.X - this.Width + 16, follow.Location.Y);
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
                    Text = "0",
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

        public void UpdateStats(List<UnitTemplate> units)
        {
            var roleCounts = roleCountLabels.Keys.ToDictionary(role => role, role => 0);

            foreach (UnitTemplate unit in units ?? Enumerable.Empty<UnitTemplate>())
            {
                if (unit?.roles == null)
                    continue;

                // Distinct so a role listed twice on one unit can't count twice
                foreach (string role in unit.roles.Where(r => r != null).Distinct())
                {
                    if (roleCounts.ContainsKey(role))
                        roleCounts[role]++;
                }
            }

            foreach (var kvp in roleCounts) roleCountLabels[kvp.Key].Text = kvp.Value.ToString();
        }
    }
}
