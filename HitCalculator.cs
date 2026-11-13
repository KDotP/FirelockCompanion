using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelockCompanion
{
    public enum FollowSide { Left, Right }

    public partial class HitCalculator : Form
    {
        Dictionary<string, Dictionary<string, int>> lookupTable = new Dictionary<string, Dictionary<string, int>>();

        public HitCalculator()
        {
            InitializeComponent();
            LoadLookupTable();
            SubscribeElements();
        }

        public async Task FollowWindow(Form follow, FollowSide side)
        {
            while (!IsDisposed && !follow.IsDisposed)
            {
                // if (follow.WindowState == FormWindowState.Maximized) return; // This is a copy of a function from armystats, update this too when you figure out what to do when maximized

                await Task.Delay(10); // I don't think this draws performance too much, but keep an eye on this
                if (side == FollowSide.Right) Location = new Point(follow.Location.X + follow.Width - 16, follow.Location.Y);
                else Location = new Point(follow.Location.X - Width + 16, follow.Location.Y);

                if (!follow.Visible) Visible = false;
                else Visible = true;
            }
        }

        private void SubscribeElements()
        {
            accuracyTextbox.TextChanged += (s, e) => calculateResults();
            blindFireCheck.CheckedChanged += (s, e) => calculateResults();
            scopedCheck.CheckedChanged += (s, e) => calculateResults();
            halfRangeCheck.CheckedChanged += (s, e) => calculateResults();
            throughSmokeCheck.CheckedChanged += (s, e) => calculateResults();
            shapedChargeCheck.CheckedChanged += (s, e) => calculateResults();
            pinTextbox.TextChanged += (s, e) => calculateResults();
            coverTextbox.TextChanged += (s, e) => calculateResults();
            strengthTextbox.TextChanged += (s, e) => calculateResults();
            toughnessTextbox.TextChanged += (s, e) => calculateResults();
        }

        private void CalculateAccuracy()
        {
            // I had a really cool and tight single line thing here, but it ended up not being the best solution :(
            if (!int.TryParse(accuracyTextbox.Text, out int accuracy))
            {
                hitLabel.Text = "Hits on N/A";
                return;
            }

            if (blindFireCheck.Checked)
            {
                if (scopedCheck.Checked)
                {
                    if (accuracy < 4) accuracy = 4; // Round down chance to hit
                }
                else accuracy = 6;
            }
            // No positive modifiers applied if blind firing
            else
            {
                if (halfRangeCheck.Checked) accuracy = Math.Max(accuracy - 1, 1);
            }

            // Plus is bad, minus is good, not the best handling but c'est la vie
            if (throughSmokeCheck.Checked) accuracy = Math.Min(accuracy + 1, 6);

            if (!int.TryParse(pinTextbox.Text, out int pin))
            {
                hitLabel.Text = "Hits on N/A";
                return;
            }
            accuracy += (int)Math.Floor(pin * 0.5); // Round down pin for -1 per 2

            if (accuracy <= 1)
            {
                hitLabel.Text = $"Cannot miss!";
            }
            else if (accuracy > 6)
            {
                hitLabel.Text = $"Cannot hit";
                return;
            }
            else
            {
                hitLabel.Text = $"Hits on {accuracy}+";
            }
        }

        private void CalculateKill()
        {
            string strength = strengthTextbox.Text.Trim();
            string toughness = toughnessTextbox.Text.Trim();

            if (!lookupTable.TryGetValue(strength, out var row))
            {
                killLabel.Text = "Kills on N/A";
                return;
            }

            if (!row.TryGetValue(toughness, out int killChance))
            {
                killLabel.Text = "Kills on N/A";
                return;
            }

            if (shapedChargeCheck.Checked)
            {
                int tempStrength;
                int tempToughness;

                // Sometimes, you get the feeling this game wasn't designed for programmers
                if (strength == "1-" || strength == "1+")
                {
                    tempStrength = 1;
                }
                else
                {
                    // This is so wildly unsafe it's kinda funny, but any number that gets to this point is safe so idk
                    tempStrength = int.Parse(strength);
                }

                if (toughness == "1-" || toughness == "1+")
                {
                    tempToughness = 1;
                }
                else
                {
                    tempToughness = int.Parse(toughness);
                }

                if (tempStrength > tempToughness)
                {
                    killChance -= 1; // This can go to 0 or negative
                }
            }

            int cover = int.TryParse(coverTextbox.Text, out int result) ? result : 10;
            killChance += cover;

            if (killChance > 6)
            {
                killLabel.Text = "Could Never Kill";
                hitLabel.Text = "Hits on N/A"; // You cannot shoot pointless shots
            }
            else if (killChance <= 1)
            {
                killLabel.Text = "Will always kill";
            }
            else
            {
                killLabel.Text = $"Kills on {killChance}+";
            }
        }

        private void calculateResults()
        {
            CalculateAccuracy();
            CalculateKill();
        }

        private void LoadLookupTable()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/HitTable.csv");
            string[] lines = File.ReadAllLines(path);

            string[] headers = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');

                string rowName = values[0].Trim().Trim('\'', '"', '\'', '‘');

                var row = new Dictionary<string, int>();

                for (int j = 1; j < values.Length; j++)
                {
                    string columnName = headers[j].Trim().Trim('\'', '"', '\'', '‘');

                    row[columnName] = int.Parse(values[j].Trim());
                }

                lookupTable[rowName] = row;
            }
        }
    }
}
