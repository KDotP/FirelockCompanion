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

                    // Tie the Start Menu's life to the Army Builder
                    armyBuilder.FormClosed += (s, args) => this.Close();

                    // Show the builder and hide the Start Menu
                    armyBuilder.Show();
                    this.Hide();
                }
            }
        }
    }
}
