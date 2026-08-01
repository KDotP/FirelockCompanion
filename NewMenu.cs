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
    public partial class NewMenu : Form
    {
        // These throw errors, so I just disabled the errors :)
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedFaction { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ArmyName { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedPoints { get; private set; }

        public NewMenu()
        {
            InitializeComponent();

            factionBox.DropDownStyle = ComboBoxStyle.DropDownList;

            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            try
            {
                string jsonString = System.IO.File.ReadAllText("Data.json");
                GameData data = System.Text.Json.JsonSerializer.Deserialize<GameData>(jsonString);

                // Custom content loader
                try
                {
                    string customString = System.IO.File.ReadAllText("Custom_Content.json") ?? "null";
                    if (customString != "null")
                    {
                        GameData customData = System.Text.Json.JsonSerializer.Deserialize<GameData>(customString);
                        data.Merge(customData);
                    }
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show($"No Custom Content Found: {ex.Message}", "Custom Content Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading custom content: {ex.Message}", "Custom Content Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (data?.factions == null || data.factions.Count == 0)
                {
                    MessageBox.Show("No factions found in Data.json. Please check the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                factionBox.Items.Clear();

                factionBox.Items.Add("Select a faction...");
                factionBox.SelectedIndex = 0;

                foreach (string factionName in data.factions.Keys)
                {
                    factionBox.Items.Add(factionName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading factions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string selectedFaction = factionBox.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedFaction) || selectedFaction == "Select a faction...")
            {
                MessageBox.Show("Please select a faction before creating a new army.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(armyTextbox.Text))
            {
                MessageBox.Show("Give your army a name before creating it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save data as public
            SelectedFaction = selectedFaction;
            ArmyName = armyTextbox.Text.Trim();
            try
            {
                SelectedPoints = int.Parse(pointsBox.Text);
                if (SelectedPoints % 100 != 0)
                {
                    MessageBox.Show($"Army points must be a multiple of 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch
            {
                MessageBox.Show($"Army points cannot contain non-integers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Mark the dialog as successful and close the popup
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