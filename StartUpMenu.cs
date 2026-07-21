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
    public partial class StartUpMenu : Form
    {
        public StartUpMenu()
        {
            InitializeComponent();
        }

        private void newArmyButton_Click(object sender, EventArgs e)
        {
            // Open the popup
            using (NewMenu popup = new NewMenu())
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ArmyBuilder armyBuilder = new ArmyBuilder();
                    armyBuilder.SelectFaction(popup.SelectedFaction, popup.ArmyName, popup.SelectedPoints);

                    armyBuilder.FormClosed += (s, args) => this.Close();
                    armyBuilder.Show();
                    this.Hide();
                }
            }
        }

        private void loadEditButton_Click(object sender, EventArgs e)
        {
            using (LoadAndEdit loadDialog = new LoadAndEdit())
            {
                if (loadDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileToLoad = loadDialog.SelectedFilePath;

                    // Create the ArmyBuilder window
                    ArmyBuilder builderForm = new ArmyBuilder();

                    // Ensure all UI elements are shown
                    builderForm.Show();

                    // Push the data into the builder
                    builderForm.LoadArmyFromFile(fileToLoad);

                    this.Hide();
                    builderForm.FormClosed += (s, args) => this.Close();
                }
            }
        }
    }
}
