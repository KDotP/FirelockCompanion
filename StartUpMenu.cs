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
            using (NewMenu popup = new NewMenu())
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ArmyBuilder armyBuilder = new ArmyBuilder();
                    armyBuilder.SelectFaction(popup.SelectedFaction, popup.ArmyName, popup.SelectedPoints);

                    // Close if window closed, show menu if back is used
                    armyBuilder.FormClosed += (s, args) =>
                    {
                        if (armyBuilder.ReturnToMenu)
                        {
                            this.Show();
                        }
                        else
                        {
                            this.Close();
                        }
                    };
                    armyBuilder.Show(); // No idea why this is needed, but it's only needed for this call
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
                    builderForm.FormClosed += (s, args) =>
                    {
                        if (builderForm.ReturnToMenu)
                        {
                            this.Show();
                        }
                        else
                        {
                            this.Close();
                        }
                    };
                }
            }
        }

        private void loadPlayButton_Click(object sender, EventArgs e)
        {
            using (LoadAndPlay loadDialog = new LoadAndPlay())
            {
                if (loadDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileToLoad = loadDialog.SelectedFilePath;

                    // Create the PlayScreen window
                    PlayScreen playScreen = new PlayScreen();

                    // Ensure all UI elements are shown
                    playScreen.Show();

                    // Push the data into the builder
                    playScreen.LoadArmyFromFile(fileToLoad);

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
            }
        }
    }
}
