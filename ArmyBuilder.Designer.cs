namespace FirelockCompanion;

partial class ArmyBuilder
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArmyBuilder));
        initalSplit = new SplitContainer();
        commandRibbon = new MenuStrip();
        testToolStripMenuItem = new ToolStripMenuItem();
        saveToolStripMenuItem = new ToolStripMenuItem();
        saveAsToolStripMenuItem = new ToolStripMenuItem();
        backToolStripMenuItem = new ToolStripMenuItem();
        editToolStripMenuItem = new ToolStripMenuItem();
        pointTotalToolStripMenuItem = new ToolStripMenuItem();
        formatUnitsToolStripMenuItem = new ToolStripMenuItem();
        deleteUnitToolStripMenuItem = new ToolStripMenuItem();
        deleteGroupToolStripMenuItem = new ToolStripMenuItem();
        addToolStripMenuItem = new ToolStripMenuItem();
        newGroupToolStripMenuItem = new ToolStripMenuItem();
        selectedUnitToolStripMenuItem = new ToolStripMenuItem();
        newTercioToolStripMenuItem = new ToolStripMenuItem();
        renameToolStripMenuItem1 = new ToolStripMenuItem();
        armyToolStripMenuItem = new ToolStripMenuItem();
        groupToolStripMenuItem = new ToolStripMenuItem();
        unitToolStripMenuItem = new ToolStripMenuItem();
        massRenameToolStripMenuItem1 = new ToolStripMenuItem();
        pointsLabel = new Label();
        factionNameLabel = new Label();
        armyNameLabel = new Label();
        allUnitsSplit = new SplitContainer();
        activeArmySplit = new SplitContainer();
        playButton = new Button();
        quickSaveButton = new Button();
        saveButton = new Button();
        saveContextMenu = new ContextMenuStrip(components);
        quickSaveToolStripMenuItem = new ToolStripMenuItem();
        reformatButton = new Button();
        removeButton = new Button();
        renameButton = new Button();
        newGroupButton = new Button();
        ActiveArmy = new Label();
        activeArmyTree = new TreeView();
        GroupManager = new ContextMenuStrip(components);
        createNewGroupToolStripMenuItem = new ToolStripMenuItem();
        renameToolStripMenuItem = new ToolStripMenuItem();
        deleteToolStripMenuItem = new ToolStripMenuItem();
        massRenameToolStripMenuItem = new ToolStripMenuItem();
        availableUnitsSplit = new SplitContainer();
        newTercioButton = new Button();
        addUnitButton = new Button();
        tacomLabel = new Label();
        label1 = new Label();
        availableUnitsSubSplit = new SplitContainer();
        availableArmyTree = new TreeView();
        backToMenuButton = new Button();
        detailsTextBox = new RichTextBox();
        availableUnitsMenu = new ContextMenuStrip(components);
        addUnitToolStripMenuItem = new ToolStripMenuItem();
        exportToolStripMenuItem = new ToolStripMenuItem();
        asTXTToolStripMenuItem = new ToolStripMenuItem();
        asToolStripMenuItem = new ToolStripMenuItem();
        ((System.ComponentModel.ISupportInitialize)initalSplit).BeginInit();
        initalSplit.Panel1.SuspendLayout();
        initalSplit.Panel2.SuspendLayout();
        initalSplit.SuspendLayout();
        commandRibbon.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)allUnitsSplit).BeginInit();
        allUnitsSplit.Panel1.SuspendLayout();
        allUnitsSplit.Panel2.SuspendLayout();
        allUnitsSplit.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)activeArmySplit).BeginInit();
        activeArmySplit.Panel1.SuspendLayout();
        activeArmySplit.Panel2.SuspendLayout();
        activeArmySplit.SuspendLayout();
        saveContextMenu.SuspendLayout();
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
        initalSplit.Panel1.Controls.Add(commandRibbon);
        initalSplit.Panel1.Controls.Add(pointsLabel);
        initalSplit.Panel1.Controls.Add(factionNameLabel);
        initalSplit.Panel1.Controls.Add(armyNameLabel);
        // 
        // initalSplit.Panel2
        // 
        initalSplit.Panel2.Controls.Add(allUnitsSplit);
        initalSplit.Size = new Size(1013, 637);
        initalSplit.SplitterDistance = 56;
        initalSplit.TabIndex = 0;
        // 
        // commandRibbon
        // 
        commandRibbon.Items.AddRange(new ToolStripItem[] { testToolStripMenuItem, editToolStripMenuItem, addToolStripMenuItem, renameToolStripMenuItem1 });
        commandRibbon.Location = new Point(0, 0);
        commandRibbon.Name = "commandRibbon";
        commandRibbon.Size = new Size(1013, 24);
        commandRibbon.TabIndex = 6;
        commandRibbon.Text = "You shouldn't be able to see this";
        // 
        // testToolStripMenuItem
        // 
        testToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, saveAsToolStripMenuItem, exportToolStripMenuItem, backToolStripMenuItem });
        testToolStripMenuItem.Name = "testToolStripMenuItem";
        testToolStripMenuItem.Size = new Size(37, 20);
        testToolStripMenuItem.Text = "File";
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.Size = new Size(180, 22);
        saveToolStripMenuItem.Text = "Save";
        saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        saveAsToolStripMenuItem.Size = new Size(180, 22);
        saveAsToolStripMenuItem.Text = "Save As";
        saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
        // 
        // backToolStripMenuItem
        // 
        backToolStripMenuItem.Name = "backToolStripMenuItem";
        backToolStripMenuItem.Size = new Size(180, 22);
        backToolStripMenuItem.Text = "Back";
        backToolStripMenuItem.Click += backToolStripMenuItem_Click;
        // 
        // editToolStripMenuItem
        // 
        editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pointTotalToolStripMenuItem, formatUnitsToolStripMenuItem, deleteUnitToolStripMenuItem, deleteGroupToolStripMenuItem });
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        editToolStripMenuItem.Size = new Size(39, 20);
        editToolStripMenuItem.Text = "Edit";
        // 
        // pointTotalToolStripMenuItem
        // 
        pointTotalToolStripMenuItem.Name = "pointTotalToolStripMenuItem";
        pointTotalToolStripMenuItem.Size = new Size(154, 22);
        pointTotalToolStripMenuItem.Text = "Point Total";
        pointTotalToolStripMenuItem.Click += pointTotalToolStripMenuItem_Click;
        // 
        // formatUnitsToolStripMenuItem
        // 
        formatUnitsToolStripMenuItem.Name = "formatUnitsToolStripMenuItem";
        formatUnitsToolStripMenuItem.Size = new Size(154, 22);
        formatUnitsToolStripMenuItem.Text = "Format Units";
        formatUnitsToolStripMenuItem.Click += formatUnitsToolStripMenuItem_Click;
        // 
        // deleteUnitToolStripMenuItem
        // 
        deleteUnitToolStripMenuItem.Name = "deleteUnitToolStripMenuItem";
        deleteUnitToolStripMenuItem.Size = new Size(154, 22);
        deleteUnitToolStripMenuItem.Text = "Delete Selected";
        deleteUnitToolStripMenuItem.Click += deleteUnitToolStripMenuItem_Click;
        // 
        // deleteGroupToolStripMenuItem
        // 
        deleteGroupToolStripMenuItem.Name = "deleteGroupToolStripMenuItem";
        deleteGroupToolStripMenuItem.Size = new Size(154, 22);
        deleteGroupToolStripMenuItem.Text = "Delete Group";
        deleteGroupToolStripMenuItem.Click += deleteGroupToolStripMenuItem_Click;
        // 
        // addToolStripMenuItem
        // 
        addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newGroupToolStripMenuItem, selectedUnitToolStripMenuItem, newTercioToolStripMenuItem });
        addToolStripMenuItem.Name = "addToolStripMenuItem";
        addToolStripMenuItem.Size = new Size(41, 20);
        addToolStripMenuItem.Text = "Add";
        // 
        // newGroupToolStripMenuItem
        // 
        newGroupToolStripMenuItem.Name = "newGroupToolStripMenuItem";
        newGroupToolStripMenuItem.Size = new Size(143, 22);
        newGroupToolStripMenuItem.Text = "New Group";
        newGroupToolStripMenuItem.Click += newGroupToolStripMenuItem_Click;
        // 
        // selectedUnitToolStripMenuItem
        // 
        selectedUnitToolStripMenuItem.Name = "selectedUnitToolStripMenuItem";
        selectedUnitToolStripMenuItem.Size = new Size(143, 22);
        selectedUnitToolStripMenuItem.Text = "Selected Unit";
        selectedUnitToolStripMenuItem.Click += selectedUnitToolStripMenuItem_Click;
        // 
        // newTercioToolStripMenuItem
        // 
        newTercioToolStripMenuItem.Enabled = false;
        newTercioToolStripMenuItem.Name = "newTercioToolStripMenuItem";
        newTercioToolStripMenuItem.Size = new Size(143, 22);
        newTercioToolStripMenuItem.Text = "New Tercio";
        newTercioToolStripMenuItem.Click += newTercioToolStripMenuItem_Click;
        // 
        // renameToolStripMenuItem1
        // 
        renameToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { armyToolStripMenuItem, groupToolStripMenuItem, unitToolStripMenuItem, massRenameToolStripMenuItem1 });
        renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
        renameToolStripMenuItem1.Size = new Size(62, 20);
        renameToolStripMenuItem1.Text = "Rename";
        // 
        // armyToolStripMenuItem
        // 
        armyToolStripMenuItem.Name = "armyToolStripMenuItem";
        armyToolStripMenuItem.Size = new Size(147, 22);
        armyToolStripMenuItem.Text = "Army";
        armyToolStripMenuItem.Click += armyToolStripMenuItem_Click;
        // 
        // groupToolStripMenuItem
        // 
        groupToolStripMenuItem.Name = "groupToolStripMenuItem";
        groupToolStripMenuItem.Size = new Size(147, 22);
        groupToolStripMenuItem.Text = "Group";
        groupToolStripMenuItem.Click += groupToolStripMenuItem_Click;
        // 
        // unitToolStripMenuItem
        // 
        unitToolStripMenuItem.Name = "unitToolStripMenuItem";
        unitToolStripMenuItem.Size = new Size(147, 22);
        unitToolStripMenuItem.Text = "Unit";
        unitToolStripMenuItem.Click += unitToolStripMenuItem_Click;
        // 
        // massRenameToolStripMenuItem1
        // 
        massRenameToolStripMenuItem1.Name = "massRenameToolStripMenuItem1";
        massRenameToolStripMenuItem1.Size = new Size(147, 22);
        massRenameToolStripMenuItem1.Text = "Mass Rename";
        massRenameToolStripMenuItem1.Click += massRenameToolStripMenuItem1_Click;
        // 
        // pointsLabel
        // 
        pointsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        pointsLabel.BackColor = Color.Transparent;
        pointsLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        pointsLabel.Location = new Point(822, 24);
        pointsLabel.Name = "pointsLabel";
        pointsLabel.Size = new Size(192, 30);
        pointsLabel.TabIndex = 5;
        pointsLabel.Text = "Points 000/XXX";
        pointsLabel.TextAlign = ContentAlignment.TopRight;
        pointsLabel.DoubleClick += pointsLabel_DoubleClick;
        // 
        // factionNameLabel
        // 
        factionNameLabel.Anchor = AnchorStyles.Top;
        factionNameLabel.BackColor = Color.Transparent;
        factionNameLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        factionNameLabel.Location = new Point(367, 24);
        factionNameLabel.Name = "factionNameLabel";
        factionNameLabel.Size = new Size(278, 30);
        factionNameLabel.TabIndex = 4;
        factionNameLabel.Text = "Faction Name Banner";
        factionNameLabel.TextAlign = ContentAlignment.TopCenter;
        // 
        // armyNameLabel
        // 
        armyNameLabel.AutoEllipsis = true;
        armyNameLabel.BackColor = Color.Transparent;
        armyNameLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        armyNameLabel.Location = new Point(0, 25);
        armyNameLabel.MaximumSize = new Size(450, 30);
        armyNameLabel.Name = "armyNameLabel";
        armyNameLabel.Size = new Size(371, 30);
        armyNameLabel.TabIndex = 3;
        armyNameLabel.Text = "Unnamed Army";
        armyNameLabel.DoubleClick += armyNameLabel_DoubleClick;
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
        allUnitsSplit.Size = new Size(1013, 577);
        allUnitsSplit.SplitterDistance = 505;
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
        activeArmySplit.Panel1.Controls.Add(playButton);
        activeArmySplit.Panel1.Controls.Add(quickSaveButton);
        activeArmySplit.Panel1.Controls.Add(saveButton);
        activeArmySplit.Panel1.Controls.Add(reformatButton);
        activeArmySplit.Panel1.Controls.Add(removeButton);
        activeArmySplit.Panel1.Controls.Add(renameButton);
        activeArmySplit.Panel1.Controls.Add(newGroupButton);
        activeArmySplit.Panel1.Controls.Add(ActiveArmy);
        // 
        // activeArmySplit.Panel2
        // 
        activeArmySplit.Panel2.Controls.Add(activeArmyTree);
        activeArmySplit.Size = new Size(505, 577);
        activeArmySplit.SplitterDistance = 25;
        activeArmySplit.TabIndex = 0;
        // 
        // playButton
        // 
        playButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        playButton.BackColor = SystemColors.ButtonFace;
        playButton.Location = new Point(449, -2);
        playButton.Name = "playButton";
        playButton.Size = new Size(55, 27);
        playButton.TabIndex = 6;
        playButton.Text = "Play";
        playButton.UseVisualStyleBackColor = false;
        playButton.Click += playButton_Click;
        // 
        // quickSaveButton
        // 
        quickSaveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        quickSaveButton.BackColor = Color.PaleGreen;
        quickSaveButton.Enabled = false;
        quickSaveButton.Location = new Point(327, -2);
        quickSaveButton.Name = "quickSaveButton";
        quickSaveButton.Size = new Size(55, 27);
        quickSaveButton.TabIndex = 5;
        quickSaveButton.Text = "Save";
        quickSaveButton.UseVisualStyleBackColor = false;
        quickSaveButton.Click += quickSaveButton_Click;
        // 
        // saveButton
        // 
        saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        saveButton.BackColor = Color.PaleGreen;
        saveButton.ContextMenuStrip = saveContextMenu;
        saveButton.Location = new Point(388, -2);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(55, 27);
        saveButton.TabIndex = 5;
        saveButton.Text = "Save As";
        saveButton.UseVisualStyleBackColor = false;
        saveButton.Click += saveButton_Click;
        // 
        // saveContextMenu
        // 
        saveContextMenu.Items.AddRange(new ToolStripItem[] { quickSaveToolStripMenuItem });
        saveContextMenu.Name = "saveContextMenu";
        saveContextMenu.Size = new Size(133, 26);
        // 
        // quickSaveToolStripMenuItem
        // 
        quickSaveToolStripMenuItem.Name = "quickSaveToolStripMenuItem";
        quickSaveToolStripMenuItem.Size = new Size(132, 22);
        quickSaveToolStripMenuItem.Text = "Quick Save";
        quickSaveToolStripMenuItem.Click += quickSaveToolStripMenuItem_Click;
        // 
        // reformatButton
        // 
        reformatButton.BackColor = SystemColors.ButtonFace;
        reformatButton.FlatAppearance.BorderSize = 2;
        reformatButton.Font = new Font("Noto Serif Lao", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
        reformatButton.Location = new Point(98, -2);
        reformatButton.Name = "reformatButton";
        reformatButton.Size = new Size(70, 26);
        reformatButton.TabIndex = 3;
        reformatButton.Text = "Format";
        reformatButton.TextAlign = ContentAlignment.TopCenter;
        reformatButton.UseVisualStyleBackColor = false;
        reformatButton.Click += reformatButton_Click;
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
        ActiveArmy.Location = new Point(190, 0);
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
        activeArmyTree.Size = new Size(503, 546);
        activeArmyTree.TabIndex = 1;
        activeArmyTree.AfterLabelEdit += activeArmyTree_AfterLabelEdit;
        activeArmyTree.AfterSelect += activeArmyTree_AfterSelect;
        activeArmyTree.NodeMouseDoubleClick += activeArmyTree_NodeMouseDoubleClick;
        activeArmyTree.KeyDown += activeArmyTree_KeyDown;
        activeArmyTree.MouseDown += activeArmyTree_MouseDown;
        // 
        // GroupManager
        // 
        GroupManager.Items.AddRange(new ToolStripItem[] { createNewGroupToolStripMenuItem, renameToolStripMenuItem, deleteToolStripMenuItem, massRenameToolStripMenuItem });
        GroupManager.Name = "contextMenuStrip1";
        GroupManager.Size = new Size(172, 92);
        GroupManager.Text = "Group Manager";
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
        // massRenameToolStripMenuItem
        // 
        massRenameToolStripMenuItem.Name = "massRenameToolStripMenuItem";
        massRenameToolStripMenuItem.Size = new Size(171, 22);
        massRenameToolStripMenuItem.Text = "Mass Rename";
        massRenameToolStripMenuItem.Click += massRenameToolStripMenuItem_Click;
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
        availableUnitsSplit.Panel1.Controls.Add(newTercioButton);
        availableUnitsSplit.Panel1.Controls.Add(addUnitButton);
        availableUnitsSplit.Panel1.Controls.Add(tacomLabel);
        availableUnitsSplit.Panel1.Controls.Add(label1);
        // 
        // availableUnitsSplit.Panel2
        // 
        availableUnitsSplit.Panel2.Controls.Add(availableUnitsSubSplit);
        availableUnitsSplit.Size = new Size(504, 577);
        availableUnitsSplit.SplitterDistance = 25;
        availableUnitsSplit.TabIndex = 0;
        // 
        // newTercioButton
        // 
        newTercioButton.BackColor = SystemColors.ButtonFace;
        newTercioButton.Location = new Point(32, -1);
        newTercioButton.Name = "newTercioButton";
        newTercioButton.Size = new Size(75, 26);
        newTercioButton.TabIndex = 4;
        newTercioButton.Text = "New Tercio";
        newTercioButton.UseVisualStyleBackColor = false;
        newTercioButton.Visible = false;
        newTercioButton.Click += newTercioButton_Click;
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
        tacomLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        tacomLabel.Location = new Point(312, -1);
        tacomLabel.Name = "tacomLabel";
        tacomLabel.Size = new Size(192, 25);
        tacomLabel.TabIndex = 2;
        tacomLabel.Text = "TACOMS: 0/X – 0C";
        tacomLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Top;
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label1.Location = new Point(169, -1);
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
        availableUnitsSubSplit.Panel2.Controls.Add(backToMenuButton);
        availableUnitsSubSplit.Panel2.Controls.Add(detailsTextBox);
        availableUnitsSubSplit.Size = new Size(502, 546);
        availableUnitsSubSplit.SplitterDistance = 347;
        availableUnitsSubSplit.TabIndex = 1;
        // 
        // availableArmyTree
        // 
        availableArmyTree.Dock = DockStyle.Fill;
        availableArmyTree.Location = new Point(0, 0);
        availableArmyTree.Name = "availableArmyTree";
        availableArmyTree.Size = new Size(502, 347);
        availableArmyTree.TabIndex = 0;
        availableArmyTree.AfterSelect += availableArmyTree_AfterSelect;
        availableArmyTree.NodeMouseDoubleClick += availableArmyTree_NodeMouseDoubleClick;
        // 
        // backToMenuButton
        // 
        backToMenuButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        backToMenuButton.BackColor = SystemColors.ControlLight;
        backToMenuButton.FlatStyle = FlatStyle.Popup;
        backToMenuButton.Location = new Point(429, 173);
        backToMenuButton.Name = "backToMenuButton";
        backToMenuButton.Size = new Size(75, 23);
        backToMenuButton.TabIndex = 6;
        backToMenuButton.Text = "🠜 Back";
        backToMenuButton.UseVisualStyleBackColor = false;
        backToMenuButton.Click += backToMenuButton_Click;
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
        detailsTextBox.Size = new Size(502, 195);
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
        // exportToolStripMenuItem
        // 
        exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { asTXTToolStripMenuItem, asToolStripMenuItem });
        exportToolStripMenuItem.Name = "exportToolStripMenuItem";
        exportToolStripMenuItem.Size = new Size(180, 22);
        exportToolStripMenuItem.Text = "Export...";
        // 
        // asTXTToolStripMenuItem
        // 
        asTXTToolStripMenuItem.Name = "asTXTToolStripMenuItem";
        asTXTToolStripMenuItem.Size = new Size(180, 22);
        asTXTToolStripMenuItem.Text = "To Text";
        asTXTToolStripMenuItem.Click += asTXTToolStripMenuItem_Click;
        // 
        // asToolStripMenuItem
        // 
        asToolStripMenuItem.Name = "asToolStripMenuItem";
        asToolStripMenuItem.Size = new Size(180, 22);
        asToolStripMenuItem.Text = "To Spreadsheet";
        asToolStripMenuItem.Click += asToolStripMenuItem_Click;
        // 
        // ArmyBuilder
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1013, 637);
        Controls.Add(initalSplit);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = commandRibbon;
        Name = "ArmyBuilder";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Army Builder";
        initalSplit.Panel1.ResumeLayout(false);
        initalSplit.Panel1.PerformLayout();
        initalSplit.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)initalSplit).EndInit();
        initalSplit.ResumeLayout(false);
        commandRibbon.ResumeLayout(false);
        commandRibbon.PerformLayout();
        allUnitsSplit.Panel1.ResumeLayout(false);
        allUnitsSplit.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)allUnitsSplit).EndInit();
        allUnitsSplit.ResumeLayout(false);
        activeArmySplit.Panel1.ResumeLayout(false);
        activeArmySplit.Panel1.PerformLayout();
        activeArmySplit.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)activeArmySplit).EndInit();
        activeArmySplit.ResumeLayout(false);
        saveContextMenu.ResumeLayout(false);
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
    private Button reformatButton;
    private Button newTercioButton;
    private Button playButton;
    private Button saveButton;
    private Button backToMenuButton;
    private ContextMenuStrip saveContextMenu;
    private ToolStripMenuItem quickSaveToolStripMenuItem;
    private Button quickSaveButton;
    private ToolStripMenuItem massRenameToolStripMenuItem;
    private MenuStrip commandRibbon;
    private ToolStripMenuItem testToolStripMenuItem;
    private ToolStripMenuItem renameToolStripMenuItem1;
    private ToolStripMenuItem armyToolStripMenuItem;
    private ToolStripMenuItem groupToolStripMenuItem;
    private ToolStripMenuItem unitToolStripMenuItem;
    private ToolStripMenuItem massRenameToolStripMenuItem1;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem saveAsToolStripMenuItem;
    private ToolStripMenuItem backToolStripMenuItem;
    private ToolStripMenuItem addToolStripMenuItem;
    private ToolStripMenuItem newGroupToolStripMenuItem;
    private ToolStripMenuItem selectedUnitToolStripMenuItem;
    private ToolStripMenuItem newTercioToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem pointTotalToolStripMenuItem;
    private ToolStripMenuItem formatUnitsToolStripMenuItem;
    private ToolStripMenuItem deleteUnitToolStripMenuItem;
    private ToolStripMenuItem deleteGroupToolStripMenuItem;
    private ToolStripMenuItem exportToolStripMenuItem;
    private ToolStripMenuItem asTXTToolStripMenuItem;
    private ToolStripMenuItem asToolStripMenuItem;
}
