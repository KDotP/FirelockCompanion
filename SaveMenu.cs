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
    public partial class SaveMenu : Form
    {
        private string _factionName;
        private int _maxPoints;

        // Public properties for the ArmyBuilder to read after the form closes
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FinalFileName { get; private set; }

        public SaveMenu(string factionName, int maxPoints)
        {
            InitializeComponent();
            _factionName = factionName;
            _maxPoints = maxPoints;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string rawName = armyNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(rawName))
            {
                MessageBox.Show("Please enter a name for your army.", "Missing Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Strip any characters that Windows doesn't allow in file names (like \ / : * ? " < > |)
            string safeName = string.Join("_", rawName.ToLower().Replace(" ", "_").Split(Path.GetInvalidFileNameChars()));
            string safeFaction = string.Join("_", _factionName.Split(Path.GetInvalidFileNameChars()));

            // Construct the final file name automatically
            FinalFileName = $"{safeName}_{safeFaction}_{_maxPoints}_pts.json";

            // Override confirmation if the file already exists
            string savesFolder = Path.Combine(Application.StartupPath, "Saves");
            string filePath = Path.Combine(savesFolder, FinalFileName);

            if (File.Exists(filePath))
            {
                var confirm = MessageBox.Show(
                    $"A save file named \"{FinalFileName}\" already exists.\nDo you want to overwrite it?",
                    "File Already Exists",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                {
                    return; // Stop and let them change the name or cancel
                }
            }

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
