using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FirelockCompanion
{
    public partial class MassRenameMenu : Form
    {
        private Dictionary<string, List<string>> pools;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> SelectedNames { get; private set; } = new List<string>();

        public MassRenameMenu()
        {
            InitializeComponent();

            PopulateNamePools();

            poolsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            poolsComboBox.SelectedIndex = 0; // Start on first entry
        }

        private void PopulateNamePools()
        {
            try
            {
                string poolsJson = System.IO.File.ReadAllText("NamePools.json");

                using JsonDocument doc = JsonDocument.Parse(poolsJson);
                pools = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(doc.RootElement.GetProperty("pools").GetRawText());

                poolsComboBox.Items.AddRange(pools.Keys.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pools: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
            string selectedPool = poolsComboBox.SelectedItem?.ToString();
            if (selectedPool == null) MessageBox.Show("Select a name pool to pull from.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            SelectedNames = pools[selectedPool];

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
