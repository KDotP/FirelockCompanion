namespace FirelockCompanion;

partial class Window
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        initalSplit = new SplitContainer();
        pointsLabel = new Label();
        factionNameLabel = new Label();
        armyNameLabel = new Label();
        allUnitsSplit = new SplitContainer();
        activeArmySplit = new SplitContainer();
        button1 = new Button();
        removeButton = new Button();
        renameButton = new Button();
        newGroupButton = new Button();
        ActiveArmy = new Label();
        activeArmyTree = new TreeView();
        GroupManager = new ContextMenuStrip(components);
        createNewGroupToolStripMenuItem = new ToolStripMenuItem();
        renameToolStripMenuItem = new ToolStripMenuItem();
        deleteToolStripMenuItem = new ToolStripMenuItem();
        availableUnitsSplit = new SplitContainer();
        addUnitButton = new Button();
        tacomLabel = new Label();
        label1 = new Label();
        availableUnitsSubSplit = new SplitContainer();
        availableArmyTree = new TreeView();
        detailsTextBox = new RichTextBox();
        availableUnitsMenu = new ContextMenuStrip(components);
        addUnitToolStripMenuItem = new ToolStripMenuItem();
        ((System.ComponentModel.ISupportInitialize)initalSplit).BeginInit();
        initalSplit.Panel1.SuspendLayout();
        initalSplit.Panel2.SuspendLayout();
        initalSplit.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)allUnitsSplit).BeginInit();
        allUnitsSplit.Panel1.SuspendLayout();
        allUnitsSplit.Panel2.SuspendLayout();
        allUnitsSplit.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)activeArmySplit).BeginInit();
        activeArmySplit.Panel1.SuspendLayout();
        activeArmySplit.Panel2.SuspendLayout();
        activeArmySplit.SuspendLayout();
        GroupManager.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)availableUnitsSplit).BeginInit();
        availableUnitsSplit.Panel1.SuspendLayout();
        availableUnitsSplit.Panel2.SuspendLayout();
        availableUnitsSplit.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)availableUnitsSubSplit).BeginInit();
        availableUnitsSubSplit.Panel1.SuspendLayout();
        availableUnitsSubSplit.Panel2.SuspendLayout();
        availableUnitsSubSplit.SuspendLayout();
        availableUnitsMenu.SuspendLayout();
        SuspendLayout();
        // 
        // initalSplit
        // 
        initalSplit.Dock = DockStyle.Fill;
        initalSplit.FixedPanel = FixedPanel.Panel1;
        initalSplit.IsSplitterFixed = true;
        initalSplit.Location = new Point(0, 0);
        initalSplit.Name = "initalSplit";
        initalSplit.Orientation = Orientation.Horizontal;
        // 
        // initalSplit.Panel1
        // 
        initalSplit.Panel1.AccessibleName = "AbsoluteTop";
        initalSplit.Panel1.BackColor = SystemColors.ControlDark;
        initalSplit.Panel1.Controls.Add(pointsLabel);
        initalSplit.Panel1.Controls.Add(factionNameLabel);
        initalSplit.Panel1.Controls.Add(armyNameLabel);
        // 
        // initalSplit.Panel2
        // 
        initalSplit.Panel2.Controls.Add(allUnitsSplit);
        initalSplit.Panel2.Paint += splitContainer1_Panel2_Paint;
        initalSplit.Size = new Size(1013, 637);
        initalSplit.SplitterDistance = 34;
        initalSplit.TabIndex = 0;
        // 
        // pointsLabel
        // 
        pointsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        pointsLabel.AutoSize = true;
        pointsLabel.BackColor = Color.Transparent;
        pointsLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        pointsLabel.Location = new Point(864, 0);
        pointsLabel.Name = "pointsLabel";
        pointsLabel.Size = new Size(152, 30);
        pointsLabel.TabIndex = 5;
        pointsLabel.Text = "Points 000/XXX";
        pointsLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // factionNameLabel
        // 
        factionNameLabel.Anchor = AnchorStyles.Top;
        factionNameLabel.AutoSize = true;
        factionNameLabel.BackColor = Color.Transparent;
        factionNameLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        factionNameLabel.Location = new Point(339, 0);
        factionNameLabel.Name = "factionNameLabel";
        factionNameLabel.Size = new Size(278, 30);
        factionNameLabel.TabIndex = 4;
        factionNameLabel.Text = "The Army of the Ebon Forest";
        // 
        // armyNameLabel
        // 
        armyNameLabel.AutoSize = true;
        armyNameLabel.BackColor = Color.Transparent;
        armyNameLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        armyNameLabel.Location = new Point(0, 0);
        armyNameLabel.MaximumSize = new Size(450, 30);
        armyNameLabel.Name = "armyNameLabel";
        armyNameLabel.Size = new Size(133, 30);
        armyNameLabel.TabIndex = 3;
        armyNameLabel.Text = "Dogs Army 1";
        // 
        // allUnitsSplit
        // 
        allUnitsSplit.BorderStyle = BorderStyle.FixedSingle;
        allUnitsSplit.Dock = DockStyle.Fill;
        allUnitsSplit.IsSplitterFixed = true;
        allUnitsSplit.Location = new Point(0, 0);
        allUnitsSplit.Name = "allUnitsSplit";
        // 
        // allUnitsSplit.Panel1
        // 
        allUnitsSplit.Panel1.Controls.Add(activeArmySplit);
        // 
        // allUnitsSplit.Panel2
        // 
        allUnitsSplit.Panel2.Controls.Add(availableUnitsSplit);
        allUnitsSplit.Size = new Size(1013, 599);
        allUnitsSplit.SplitterDistance = 473;
        allUnitsSplit.TabIndex = 0;
        // 
        // activeArmySplit
        // 
        activeArmySplit.BorderStyle = BorderStyle.FixedSingle;
        activeArmySplit.Dock = DockStyle.Fill;
        activeArmySplit.FixedPanel = FixedPanel.Panel1;
        activeArmySplit.IsSplitterFixed = true;
        activeArmySplit.Location = new Point(0, 0);
        activeArmySplit.Name = "activeArmySplit";
        activeArmySplit.Orientation = Orientation.Horizontal;
        // 
        // activeArmySplit.Panel1
        // 
        activeArmySplit.Panel1.BackColor = SystemColors.ControlLight;
        activeArmySplit.Panel1.Controls.Add(button1);
        activeArmySplit.Panel1.Controls.Add(removeButton);
        activeArmySplit.Panel1.Controls.Add(renameButton);
        activeArmySplit.Panel1.Controls.Add(newGroupButton);
        activeArmySplit.Panel1.Controls.Add(ActiveArmy);
        // 
        // activeArmySplit.Panel2
        // 
        activeArmySplit.Panel2.Controls.Add(activeArmyTree);
        activeArmySplit.Size = new Size(473, 599);
        activeArmySplit.SplitterDistance = 25;
        activeArmySplit.TabIndex = 0;
        // 
        // button1
        // 
        button1.BackColor = SystemColors.ButtonFace;
        button1.FlatAppearance.BorderSize = 2;
        button1.Font = new Font("Noto Serif Lao", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
        button1.Location = new Point(98, -2);
        button1.Name = "button1";
        button1.Size = new Size(64, 26);
        button1.TabIndex = 3;
        button1.Text = "Format";
        button1.TextAlign = ContentAlignment.TopCenter;
        button1.UseVisualStyleBackColor = false;
        // 
        // removeButton
        // 
        removeButton.BackColor = Color.LightCoral;
        removeButton.FlatAppearance.BorderSize = 2;
        removeButton.Font = new Font("Noto Serif Lao", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
        removeButton.ForeColor = Color.Maroon;
        removeButton.Location = new Point(65, -2);
        removeButton.Name = "removeButton";
        removeButton.Size = new Size(27, 26);
        removeButton.TabIndex = 2;
        removeButton.Text = "X";
        removeButton.TextAlign = ContentAlignment.TopCenter;
        removeButton.UseVisualStyleBackColor = false;
        removeButton.Click += removeButton_Click;
        // 
        // renameButton
        // 
        renameButton.BackColor = SystemColors.ButtonFace;
        renameButton.FlatAppearance.BorderSize = 2;
        renameButton.Font = new Font("Noto Serif Lao", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
        renameButton.Location = new Point(32, -2);
        renameButton.Name = "renameButton";
        renameButton.Size = new Size(27, 26);
        renameButton.TabIndex = 2;
        renameButton.Text = "I";
        renameButton.TextAlign = ContentAlignment.TopCenter;
        renameButton.UseVisualStyleBackColor = false;
        renameButton.Click += renameButton_Click;
        // 
        // newGroupButton
        // 
        newGroupButton.BackColor = SystemColors.ButtonFace;
        newGroupButton.FlatAppearance.BorderSize = 2;
        newGroupButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        newGroupButton.Location = new Point(-1, -2);
        newGroupButton.Name = "newGroupButton";
        newGroupButton.Size = new Size(27, 26);
        newGroupButton.TabIndex = 1;
        newGroupButton.Text = "*";
        newGroupButton.TextAlign = ContentAlignment.TopCenter;
        newGroupButton.UseVisualStyleBackColor = false;
        newGroupButton.Click += newGroupButton_Click;
        // 
        // ActiveArmy
        // 
        ActiveArmy.Anchor = AnchorStyles.Top;
        ActiveArmy.AutoSize = true;
        ActiveArmy.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        ActiveArmy.Location = new Point(174, 0);
        ActiveArmy.Name = "ActiveArmy";
        ActiveArmy.Size = new Size(112, 25);
        ActiveArmy.TabIndex = 0;
        ActiveArmy.Text = "Active Army";
        ActiveArmy.TextAlign = ContentAlignment.TopCenter;
        // 
        // activeArmyTree
        // 
        activeArmyTree.AllowDrop = true;
        activeArmyTree.ContextMenuStrip = GroupManager;
        activeArmyTree.Dock = DockStyle.Fill;
        activeArmyTree.Location = new Point(0, 0);
        activeArmyTree.Name = "activeArmyTree";
        activeArmyTree.Size = new Size(471, 568);
        activeArmyTree.TabIndex = 1;
        activeArmyTree.AfterLabelEdit += activeArmyTree_AfterLabelEdit;
        activeArmyTree.AfterSelect += activeArmyTree_AfterSelect;
        activeArmyTree.NodeMouseDoubleClick += activeArmyTree_NodeMouseDoubleClick;
        activeArmyTree.KeyDown += activeArmyTree_KeyDown;
        activeArmyTree.MouseDown += activeArmyTree_MouseDown;
        // 
        // GroupManager
        // 
        GroupManager.Items.AddRange(new ToolStripItem[] { createNewGroupToolStripMenuItem, renameToolStripMenuItem, deleteToolStripMenuItem });
        GroupManager.Name = "contextMenuStrip1";
        GroupManager.Size = new Size(172, 70);
        GroupManager.Text = "Group Manager";
        GroupManager.Opening += CreateGroupParent_Opening;
        // 
        // createNewGroupToolStripMenuItem
        // 
        createNewGroupToolStripMenuItem.Name = "createNewGroupToolStripMenuItem";
        createNewGroupToolStripMenuItem.Size = new Size(171, 22);
        createNewGroupToolStripMenuItem.Text = "Create New Group";
        createNewGroupToolStripMenuItem.Click += createNewGroupToolStripMenuItem_Click;
        // 
        // renameToolStripMenuItem
        // 
        renameToolStripMenuItem.Name = "renameToolStripMenuItem";
        renameToolStripMenuItem.Size = new Size(171, 22);
        renameToolStripMenuItem.Text = "Rename";
        renameToolStripMenuItem.Click += renameToolStripMenuItem_Click;
        // 
        // deleteToolStripMenuItem
        // 
        deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
        deleteToolStripMenuItem.Size = new Size(171, 22);
        deleteToolStripMenuItem.Text = "Delete";
        deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
        // 
        // availableUnitsSplit
        // 
        availableUnitsSplit.BorderStyle = BorderStyle.FixedSingle;
        availableUnitsSplit.Dock = DockStyle.Fill;
        availableUnitsSplit.FixedPanel = FixedPanel.Panel1;
        availableUnitsSplit.IsSplitterFixed = true;
        availableUnitsSplit.Location = new Point(0, 0);
        availableUnitsSplit.Name = "availableUnitsSplit";
        availableUnitsSplit.Orientation = Orientation.Horizontal;
        // 
        // availableUnitsSplit.Panel1
        // 
        availableUnitsSplit.Panel1.BackColor = SystemColors.ControlLight;
        availableUnitsSplit.Panel1.Controls.Add(addUnitButton);
        availableUnitsSplit.Panel1.Controls.Add(tacomLabel);
        availableUnitsSplit.Panel1.Controls.Add(label1);
        // 
        // availableUnitsSplit.Panel2
        // 
        availableUnitsSplit.Panel2.Controls.Add(availableUnitsSubSplit);
        availableUnitsSplit.Size = new Size(536, 599);
        availableUnitsSplit.SplitterDistance = 25;
        availableUnitsSplit.TabIndex = 0;
        // 
        // addUnitButton
        // 
        addUnitButton.BackColor = SystemColors.ButtonFace;
        addUnitButton.FlatAppearance.BorderSize = 2;
        addUnitButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        addUnitButton.ForeColor = Color.ForestGreen;
        addUnitButton.Location = new Point(-1, -2);
        addUnitButton.Name = "addUnitButton";
        addUnitButton.Size = new Size(27, 26);
        addUnitButton.TabIndex = 3;
        addUnitButton.Text = "+";
        addUnitButton.TextAlign = ContentAlignment.TopCenter;
        addUnitButton.UseVisualStyleBackColor = false;
        addUnitButton.Click += addUnitButton_Click;
        // 
        // tacomLabel
        // 
        tacomLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        tacomLabel.AutoSize = true;
        tacomLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        tacomLabel.Location = new Point(414, -2);
        tacomLabel.Name = "tacomLabel";
        tacomLabel.Size = new Size(121, 25);
        tacomLabel.TabIndex = 2;
        tacomLabel.Text = "TACOMS: 0/3";
        tacomLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Top;
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label1.Location = new Point(185, -1);
        label1.Name = "label1";
        label1.Size = new Size(137, 25);
        label1.TabIndex = 1;
        label1.Text = "Available Units";
        label1.TextAlign = ContentAlignment.TopCenter;
        // 
        // availableUnitsSubSplit
        // 
        availableUnitsSubSplit.Dock = DockStyle.Fill;
        availableUnitsSubSplit.FixedPanel = FixedPanel.Panel2;
        availableUnitsSubSplit.Location = new Point(0, 0);
        availableUnitsSubSplit.Name = "availableUnitsSubSplit";
        availableUnitsSubSplit.Orientation = Orientation.Horizontal;
        // 
        // availableUnitsSubSplit.Panel1
        // 
        availableUnitsSubSplit.Panel1.Controls.Add(availableArmyTree);
        // 
        // availableUnitsSubSplit.Panel2
        // 
        availableUnitsSubSplit.Panel2.Controls.Add(detailsTextBox);
        availableUnitsSubSplit.Size = new Size(534, 568);
        availableUnitsSubSplit.SplitterDistance = 369;
        availableUnitsSubSplit.TabIndex = 1;
        // 
        // availableArmyTree
        // 
        availableArmyTree.Dock = DockStyle.Fill;
        availableArmyTree.Location = new Point(0, 0);
        availableArmyTree.Name = "availableArmyTree";
        availableArmyTree.Size = new Size(534, 369);
        availableArmyTree.TabIndex = 0;
        availableArmyTree.AfterSelect += availableArmyTree_AfterSelect;
        availableArmyTree.NodeMouseDoubleClick += availableArmyTree_NodeMouseDoubleClick;
        // 
        // detailsTextBox
        // 
        detailsTextBox.BackColor = SystemColors.Control;
        detailsTextBox.BorderStyle = BorderStyle.FixedSingle;
        detailsTextBox.Dock = DockStyle.Fill;
        detailsTextBox.Location = new Point(0, 0);
        detailsTextBox.Name = "detailsTextBox";
        detailsTextBox.ReadOnly = true;
        detailsTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
        detailsTextBox.Size = new Size(534, 195);
        detailsTextBox.TabIndex = 0;
        detailsTextBox.Text = "Unit/Keyword info will show up here!";
        // 
        // availableUnitsMenu
        // 
        availableUnitsMenu.Items.AddRange(new ToolStripItem[] { addUnitToolStripMenuItem });
        availableUnitsMenu.Name = "availableUnitsMenu";
        availableUnitsMenu.Size = new Size(122, 26);
        // 
        // addUnitToolStripMenuItem
        // 
        addUnitToolStripMenuItem.Name = "addUnitToolStripMenuItem";
        addUnitToolStripMenuItem.Size = new Size(121, 22);
        addUnitToolStripMenuItem.Text = "Add Unit";
        // 
        // Window
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1013, 637);
        Controls.Add(initalSplit);
        Name = "Window";
        Text = "Form1";
        initalSplit.Panel1.ResumeLayout(false);
        initalSplit.Panel1.PerformLayout();
        initalSplit.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)initalSplit).EndInit();
        initalSplit.ResumeLayout(false);
        allUnitsSplit.Panel1.ResumeLayout(false);
        allUnitsSplit.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)allUnitsSplit).EndInit();
        allUnitsSplit.ResumeLayout(false);
        activeArmySplit.Panel1.ResumeLayout(false);
        activeArmySplit.Panel1.PerformLayout();
        activeArmySplit.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)activeArmySplit).EndInit();
        activeArmySplit.ResumeLayout(false);
        GroupManager.ResumeLayout(false);
        availableUnitsSplit.Panel1.ResumeLayout(false);
        availableUnitsSplit.Panel1.PerformLayout();
        availableUnitsSplit.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)availableUnitsSplit).EndInit();
        availableUnitsSplit.ResumeLayout(false);
        availableUnitsSubSplit.Panel1.ResumeLayout(false);
        availableUnitsSubSplit.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)availableUnitsSubSplit).EndInit();
        availableUnitsSubSplit.ResumeLayout(false);
        availableUnitsMenu.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private SplitContainer initalSplit;
    private SplitContainer allUnitsSplit;
    private Label pointsLabel;
    private Label factionNameLabel;
    private Label armyNameLabel;
    private SplitContainer activeArmySplit;
    private Label ActiveArmy;
    private SplitContainer availableUnitsSplit;
    private Label label1;
    private TreeView availableArmyTree;
    private TreeView activeArmyTree;
    private ContextMenuStrip GroupManager;
    private ToolStripMenuItem createNewGroupToolStripMenuItem;
    private ToolStripMenuItem renameToolStripMenuItem;
    private SplitContainer availableUnitsSubSplit;
    private RichTextBox detailsTextBox;
    private ContextMenuStrip availableUnitsMenu;
    private ToolStripMenuItem addUnitToolStripMenuItem;
    private Label tacomLabel;
    private Button newGroupButton;
    private ToolStripMenuItem deleteToolStripMenuItem;
    private Button renameButton;
    private Button removeButton;
    private Button addUnitButton;
    private Button button1;
}
