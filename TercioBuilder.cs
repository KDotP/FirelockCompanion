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
    public partial class TercioBuilder : Form
    {
        private List<UnitTemplate> _tercioUnits = new List<UnitTemplate>();
        private List<UnitTemplate> _possibleUnits = new List<UnitTemplate>();

        // Public variables
        public List<UnitTemplate> SelectedTercioUnits => _tercioUnits;

        // These throw errors, so I just disabled the errors :)
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TotalCost { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TotalSize { get; private set; }

        public TercioBuilder(List<UnitTemplate> factionUnits)
        {
            InitializeComponent();

            firstUnitBox.DropDownStyle = ComboBoxStyle.DropDownList;
            secondUnitBox.DropDownStyle = ComboBoxStyle.DropDownList;
            thirdUnitBox.DropDownStyle = ComboBoxStyle.DropDownList;

            firstUnitBox.SelectedIndexChanged += UnitBox_SelectedIndexChanged;
            secondUnitBox.SelectedIndexChanged += UnitBox_SelectedIndexChanged;
            thirdUnitBox.SelectedIndexChanged += UnitBox_SelectedIndexChanged;

            SetUnitOptions(factionUnits);
        }

        private void DebugBuild()
        {
            string jsonString = System.IO.File.ReadAllText("Data.json");
            GameData data = System.Text.Json.JsonSerializer.Deserialize<GameData>(jsonString);
            List<UnitTemplate> factionUnits = data.factions["Atom Barons of Santagria"];

            SetUnitOptions(factionUnits);
        }

        public void SetUnitOptions(List<UnitTemplate> units)
        {
            firstUnitBox.Items.Clear();
            secondUnitBox.Items.Clear();
            thirdUnitBox.Items.Clear();
            _possibleUnits.Clear();

            foreach (var unit in units)
            {
                if (unit.keywords != null && unit.keywords.Contains("Tercio"))
                {
                    _possibleUnits.Add(unit);

                    string squadTag = unit.keywords.Contains("Squad") ? " (S)" : "";
                    string displayText = $"{unit.name}{squadTag} - {unit.cost} pts";

                    firstUnitBox.Items.Add(displayText);
                    secondUnitBox.Items.Add(displayText);
                    thirdUnitBox.Items.Add(displayText);
                }
            }

            // For easy selection
            firstUnitBox.SelectedIndex = 0;
            secondUnitBox.SelectedIndex = 0;
            thirdUnitBox.SelectedIndex = 0;
        }

        private void UnitBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTotals();
        }

        private void CalculateTotals()
        {
            int totalCost = 0;
            int totalSize = 0;

            _tercioUnits.Clear();

            ComboBox[] boxes = { firstUnitBox, secondUnitBox, thirdUnitBox };

            foreach (ComboBox box in boxes)
            {
                int selectedIndex = box.SelectedIndex;

                // Crash related to changing index before loading finishes
                if (selectedIndex < 0 || selectedIndex >= _possibleUnits.Count)
                    continue;

                totalCost += _possibleUnits[selectedIndex].cost;
                if (_possibleUnits[selectedIndex].keywords.Contains("Squad"))
                {
                    totalSize += 2;
                }
                else
                {
                    totalSize += 1;
                }

                _tercioUnits.Add(_possibleUnits[selectedIndex]);
            }

            TotalCost = totalCost;
            TotalSize = totalSize;

            // Update the UI labels
            costLabel.Text = $"Cost: {totalCost} pts";
            sizeLabel.Text = $"Size: {totalSize}";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            // Shouldn't happen, but just in case
            if (_tercioUnits.Count != 3)
            {
                MessageBox.Show("Please ensure 3 valid units are selected.", "Invalid Tercio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
