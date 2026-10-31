using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelockCompanion
{
    public partial class StartUpMenu : Form
    {
        private PrivateFontCollection customFonts = new PrivateFontCollection();
        private Font titleFont;
        private bool versionControlEnabled = true;
        private string version = "0.0.0";
        private string availableVersion = "0.0.0";

        private enum VersionResult
        {
            UpToDate,
            Outdated,
            Failed
        }

        public StartUpMenu()
        {
            InitializeComponent();
            LoadConfig();
            if (!versionControlEnabled)
            {
                versionLabel.Visible = false;
                versionControlLinkLabel.Visible = false;
            }
            else
            {
                VersionResult result = CheckVersion();
                SetVersionControlLink(result);
            }
            LoadTitleFont();
        }

        private void newArmyButton_Click(object sender, EventArgs e)
        {
            using (NewMenu popup = new NewMenu())
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ArmyBuilder armyBuilder = new ArmyBuilder();
                    armyBuilder.Verbose = verboseCheckbox.Checked;
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

        private void LoadTitleFont()
        {
            string fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Bombardier-Regular.ttf");
            customFonts.AddFontFile(fontPath);
            titleFont = new Font(customFonts.Families[0], 36, FontStyle.Regular);
            titleLabel.Font = titleFont;
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

                    builderForm.Verbose = verboseCheckbox.Checked;

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

                    playScreen.Verbose = verboseCheckbox.Checked;

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

        private void LoadConfig()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/config.json");
            if (File.Exists(configPath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(configPath);
                    var configData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonContent);
                    if (configData == null) return;
                    if (configData.ContainsKey("version"))
                    {
                        version = configData["version"].ToString();
                    }
                    if (configData.ContainsKey("versionControlEnabled"))
                    {
                        versionControlEnabled = Convert.ToBoolean(configData["versionControlEnabled"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private VersionResult CheckVersion()
        {
            try
            {
                string url = "https://api.github.com/repos/KDotP/FirelockCompanion/releases/latest";
                // I miss you WebClient, but you also sucked
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "FirelockCompanion");

                string jsonResponse = client.GetStringAsync(url).GetAwaiter().GetResult();

                using JsonDocument doc = JsonDocument.Parse(jsonResponse);

                string? latestVersion = doc.RootElement.GetProperty("tag_name").GetString();
                if (latestVersion == null)
                {
                    Console.WriteLine("Failed to retrieve the latest version from GitHub API.");
                    return VersionResult.Failed;
                }
                if (latestVersion == version)
                {
                    return VersionResult.UpToDate;
                }
                availableVersion = latestVersion;
                return VersionResult.Outdated;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return VersionResult.Failed;
            }
        }

        private void SetVersionControlLink(VersionResult result)
        {
            switch (result)
            {
                case VersionResult.UpToDate:
                    versionControlLinkLabel.Text = "Up to Date!";
                    versionControlLinkLabel.ForeColor = Color.Green;
                    break;
                case VersionResult.Outdated:
                    versionControlLinkLabel.Text = $"{availableVersion} Available!";
                    versionControlLinkLabel.ForeColor = Color.Orange;
                    versionControlLinkLabel.Font = new Font(versionControlLinkLabel.Font, FontStyle.Underline);
                    versionControlLinkLabel.Click += VersionControlLinkLabel_Click;
                    break;
                case VersionResult.Failed:
                    versionControlLinkLabel.Text = "Check Failed";
                    versionControlLinkLabel.ForeColor = Color.Red;
                    break;
            }
        }

        private void VersionControlLinkLabel_Click(object? sender, EventArgs e)
        {
            // Opens link in default browser
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/KDotP/FirelockCompanion/releases/latest",
                UseShellExecute = true
            });
        }
    }
}
