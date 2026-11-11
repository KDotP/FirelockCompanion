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
        // I'm putting this inside the ArmyStats class because they're not needed elsewhere, but this failed when I tried it ealier so idk
        private class RoleInfo
        {
            public string desc { get; set; }
            public bool show_in_info { get; set; }
        }

        private class RolesData
        {
            public Dictionary<string, RoleInfo> roles { get; set; }
        }

        public ArmyStats()
        {
            InitializeComponent();
            PopulateRoles();
        }

        private void PopulateRoles()
        {
            string jsonString = System.IO.File.ReadAllText("Data.json");
            RolesData roles = System.Text.Json.JsonSerializer.Deserialize<RolesData>(jsonString);

            if (roles.Equals(null) || roles.roles == null)
            {
                MessageBox.Show("Failed to load roles data from Data.json.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cull non-shown roles
            var visibleRoles = roles.roles.Where(role => role.Value.show_in_info).ToList();
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
            }

            // Resize named group thing
            roleBox.Size = new Size(roleBox.Size.Width, (visibleRoleCount * 20) + 23); // I know, it annoys me too
        }
    }
}
