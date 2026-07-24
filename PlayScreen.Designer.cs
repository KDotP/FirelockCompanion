namespace FirelockCompanion
{
    partial class PlayScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayScreen));
            splitContainer1 = new SplitContainer();
            commandPointsLabel = new Label();
            factionNameLabel = new Label();
            armyNameLabel = new Label();
            splitContainer2 = new SplitContainer();
            activeArmyContainer = new SplitContainer();
            ActiveArmy = new Label();
            activeArmyTree = new TreeView();
            splitContainer4 = new SplitContainer();
            label1 = new Label();
            splitContainer3 = new SplitContainer();
            unitInfoText = new RichTextBox();
            detailsTextBox = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)activeArmyContainer).BeginInit();
            activeArmyContainer.Panel1.SuspendLayout();
            activeArmyContainer.Panel2.SuspendLayout();
            activeArmyContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.ControlDark;
            splitContainer1.Panel1.Controls.Add(commandPointsLabel);
            splitContainer1.Panel1.Controls.Add(factionNameLabel);
            splitContainer1.Panel1.Controls.Add(armyNameLabel);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1013, 637);
            splitContainer1.SplitterDistance = 34;
            splitContainer1.TabIndex = 0;
            // 
            // commandPointsLabel
            // 
            commandPointsLabel.Anchor = AnchorStyles.Top;
            commandPointsLabel.BackColor = Color.Transparent;
            commandPointsLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            commandPointsLabel.Location = new Point(735, 0);
            commandPointsLabel.Name = "commandPointsLabel";
            commandPointsLabel.Size = new Size(278, 30);
            commandPointsLabel.TabIndex = 7;
            commandPointsLabel.Text = "Command: X/X (VIS)";
            commandPointsLabel.TextAlign = ContentAlignment.MiddleRight;
            commandPointsLabel.Visible = false;
            // 
            // factionNameLabel
            // 
            factionNameLabel.Anchor = AnchorStyles.Top;
            factionNameLabel.BackColor = Color.Transparent;
            factionNameLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            factionNameLabel.Location = new Point(367, 0);
            factionNameLabel.Name = "factionNameLabel";
            factionNameLabel.Size = new Size(278, 30);
            factionNameLabel.TabIndex = 6;
            factionNameLabel.Text = "Faction Name Banner";
            factionNameLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // armyNameLabel
            // 
            armyNameLabel.AutoEllipsis = true;
            armyNameLabel.BackColor = Color.Transparent;
            armyNameLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            armyNameLabel.Location = new Point(0, 0);
            armyNameLabel.MaximumSize = new Size(450, 30);
            armyNameLabel.Name = "armyNameLabel";
            armyNameLabel.Size = new Size(361, 30);
            armyNameLabel.TabIndex = 5;
            armyNameLabel.Text = "Unnamed Army";
            // 
            // splitContainer2
            // 
            splitContainer2.BorderStyle = BorderStyle.FixedSingle;
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(activeArmyContainer);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(splitContainer4);
            splitContainer2.Size = new Size(1013, 599);
            splitContainer2.SplitterDistance = 505;
            splitContainer2.TabIndex = 0;
            // 
            // activeArmyContainer
            // 
            activeArmyContainer.BorderStyle = BorderStyle.FixedSingle;
            activeArmyContainer.Dock = DockStyle.Fill;
            activeArmyContainer.FixedPanel = FixedPanel.Panel1;
            activeArmyContainer.IsSplitterFixed = true;
            activeArmyContainer.Location = new Point(0, 0);
            activeArmyContainer.Name = "activeArmyContainer";
            activeArmyContainer.Orientation = Orientation.Horizontal;
            // 
            // activeArmyContainer.Panel1
            // 
            activeArmyContainer.Panel1.BackColor = SystemColors.ControlLight;
            activeArmyContainer.Panel1.Controls.Add(ActiveArmy);
            // 
            // activeArmyContainer.Panel2
            // 
            activeArmyContainer.Panel2.Controls.Add(activeArmyTree);
            activeArmyContainer.Size = new Size(505, 599);
            activeArmyContainer.SplitterDistance = 25;
            activeArmyContainer.TabIndex = 0;
            // 
            // ActiveArmy
            // 
            ActiveArmy.Anchor = AnchorStyles.Top;
            ActiveArmy.AutoSize = true;
            ActiveArmy.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ActiveArmy.Location = new Point(195, 0);
            ActiveArmy.Name = "ActiveArmy";
            ActiveArmy.Size = new Size(112, 25);
            ActiveArmy.TabIndex = 1;
            ActiveArmy.Text = "Active Army";
            ActiveArmy.TextAlign = ContentAlignment.TopCenter;
            // 
            // activeArmyTree
            // 
            activeArmyTree.Dock = DockStyle.Fill;
            activeArmyTree.Location = new Point(0, 0);
            activeArmyTree.Name = "activeArmyTree";
            activeArmyTree.Size = new Size(503, 568);
            activeArmyTree.TabIndex = 0;
            // 
            // splitContainer4
            // 
            splitContainer4.BorderStyle = BorderStyle.FixedSingle;
            splitContainer4.Dock = DockStyle.Fill;
            splitContainer4.FixedPanel = FixedPanel.Panel1;
            splitContainer4.IsSplitterFixed = true;
            splitContainer4.Location = new Point(0, 0);
            splitContainer4.Name = "splitContainer4";
            splitContainer4.Orientation = Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.BackColor = SystemColors.ControlLight;
            splitContainer4.Panel1.Controls.Add(label1);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(splitContainer3);
            splitContainer4.Size = new Size(504, 599);
            splitContainer4.SplitterDistance = 25;
            splitContainer4.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(195, 1);
            label1.Name = "label1";
            label1.Size = new Size(85, 25);
            label1.TabIndex = 2;
            label1.Text = "Unit Info";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // splitContainer3
            // 
            splitContainer3.BorderStyle = BorderStyle.FixedSingle;
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.BackColor = SystemColors.InactiveBorder;
            splitContainer3.Panel1.Controls.Add(unitInfoText);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(detailsTextBox);
            splitContainer3.Size = new Size(504, 570);
            splitContainer3.SplitterDistance = 25;
            splitContainer3.TabIndex = 0;
            // 
            // unitInfoText
            // 
            unitInfoText.DetectUrls = false;
            unitInfoText.Dock = DockStyle.Fill;
            unitInfoText.Location = new Point(0, 0);
            unitInfoText.Name = "unitInfoText";
            unitInfoText.ReadOnly = true;
            unitInfoText.Size = new Size(502, 23);
            unitInfoText.TabIndex = 0;
            unitInfoText.Text = "This menu is still under development.";
            // 
            // detailsTextBox
            // 
            detailsTextBox.BackColor = SystemColors.Control;
            detailsTextBox.Dock = DockStyle.Fill;
            detailsTextBox.Location = new Point(0, 0);
            detailsTextBox.Name = "detailsTextBox";
            detailsTextBox.ReadOnly = true;
            detailsTextBox.Size = new Size(502, 539);
            detailsTextBox.TabIndex = 0;
            detailsTextBox.Text = "Unit/Keyword info will show up here! (You can also resize this menu)";
            // 
            // PlayScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1013, 637);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PlayScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Play Menu";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            activeArmyContainer.Panel1.ResumeLayout(false);
            activeArmyContainer.Panel1.PerformLayout();
            activeArmyContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)activeArmyContainer).EndInit();
            activeArmyContainer.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel1.PerformLayout();
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Label factionNameLabel;
        private Label armyNameLabel;
        private Label commandPointsLabel;
        private SplitContainer activeArmyContainer;
        private SplitContainer splitContainer4;
        private TreeView activeArmyTree;
        private SplitContainer splitContainer3;
        private Label ActiveArmy;
        private Label label1;
        private RichTextBox detailsTextBox;
        private RichTextBox unitInfoText;
    }
}