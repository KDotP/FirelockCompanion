using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; // Added to access Directory and Path methods
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelockCompanion
{
    public partial class LoadAndEdit : Form
    {
        // This is the property the main form will read after the dialog closes
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedFilePath { get; private set; }

        public LoadAndEdit()
        {
            InitializeComponent();
            fileComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Automatically find the Saves directory and populate on load
            string savesFolder = Path.Combine(Application.StartupPath, "Saves");
            PopulateDropdown(savesFolder);
        }

        private void PopulateDropdown(string directoryPath)
        {
            // Safely ensure the directory exists before trying to read it
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Grab all JSON files
            string[] files = Directory.GetFiles(directoryPath, "*.json");

            // Sort files by newest first (Last Write Time)
            var sortedFiles = files.OrderByDescending(f => File.GetLastWriteTime(f)).ToArray();

            foreach (string file in sortedFiles)
            {
                // Add our custom item so the UI looks clean, but we still retain the full file path
                fileComboBox.Items.Add(new SaveItem
                {
                    DisplayText = Path.GetFileNameWithoutExtension(file),
                    FullPath = file
                });
            }

            // Select the first item by default, or disable the edit button if the folder is empty
            if (fileComboBox.Items.Count > 0)
            {
                fileComboBox.SelectedIndex = 0;
            }
            else
            {
                editButton.Enabled = false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (fileComboBox.SelectedItem is SaveItem selectedItem)
            {
                SelectedFilePath = selectedItem.FullPath;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }

    // A tiny helper class to separate the display text from the actual backend data
    public class SaveItem
    {
        public string DisplayText { get; set; }
        public string FullPath { get; set; }

        // The ComboBox internally calls ToString() on whatever object you give it to decide what text to show.
        public override string ToString()
        {
            return DisplayText;
        }
    }
}