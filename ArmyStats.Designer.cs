namespace FirelockCompanion
{
    partial class ArmyStats
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArmyStats));
            tacomLabel = new Label();
            typeTable = new TableLayoutPanel();
            infantryLabel = new Label();
            vehiclesLabel = new Label();
            aircraftLabel = new Label();
            tacomCountLabel = new Label();
            infantryCountLabel = new Label();
            vehicleCountLabel = new Label();
            aircraftCountLabel = new Label();
            typeBox = new GroupBox();
            roleBox = new GroupBox();
            rolesTable = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            typeTable.SuspendLayout();
            typeBox.SuspendLayout();
            roleBox.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tacomLabel
            // 
            tacomLabel.Anchor = AnchorStyles.Left;
            tacomLabel.AutoSize = true;
            tacomLabel.Font = new Font("Segoe UI", 9F);
            tacomLabel.Location = new Point(39, 2);
            tacomLabel.Name = "tacomLabel";
            tacomLabel.Size = new Size(53, 15);
            tacomLabel.TabIndex = 0;
            tacomLabel.Text = "TACOMs";
            // 
            // typeTable
            // 
            typeTable.ColumnCount = 2;
            typeTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            typeTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            typeTable.Controls.Add(tacomLabel, 1, 0);
            typeTable.Controls.Add(infantryLabel, 1, 1);
            typeTable.Controls.Add(vehiclesLabel, 1, 2);
            typeTable.Controls.Add(aircraftLabel, 1, 3);
            typeTable.Controls.Add(tacomCountLabel, 0, 0);
            typeTable.Controls.Add(infantryCountLabel, 0, 1);
            typeTable.Controls.Add(vehicleCountLabel, 0, 2);
            typeTable.Controls.Add(aircraftCountLabel, 0, 3);
            typeTable.Dock = DockStyle.Fill;
            typeTable.Location = new Point(3, 19);
            typeTable.Name = "typeTable";
            typeTable.RowCount = 4;
            typeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            typeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            typeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            typeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            typeTable.Size = new Size(122, 79);
            typeTable.TabIndex = 1;
            // 
            // infantryLabel
            // 
            infantryLabel.Anchor = AnchorStyles.Left;
            infantryLabel.AutoSize = true;
            infantryLabel.Font = new Font("Segoe UI", 9F);
            infantryLabel.Location = new Point(39, 22);
            infantryLabel.Name = "infantryLabel";
            infantryLabel.Size = new Size(48, 15);
            infantryLabel.TabIndex = 0;
            infantryLabel.Text = "Infantry";
            // 
            // vehiclesLabel
            // 
            vehiclesLabel.Anchor = AnchorStyles.Left;
            vehiclesLabel.AutoSize = true;
            vehiclesLabel.Font = new Font("Segoe UI", 9F);
            vehiclesLabel.Location = new Point(39, 42);
            vehiclesLabel.Name = "vehiclesLabel";
            vehiclesLabel.Size = new Size(49, 15);
            vehiclesLabel.TabIndex = 0;
            vehiclesLabel.Text = "Vehicles";
            // 
            // aircraftLabel
            // 
            aircraftLabel.Anchor = AnchorStyles.Left;
            aircraftLabel.AutoSize = true;
            aircraftLabel.Font = new Font("Segoe UI", 9F);
            aircraftLabel.Location = new Point(39, 62);
            aircraftLabel.Name = "aircraftLabel";
            aircraftLabel.Size = new Size(46, 15);
            aircraftLabel.TabIndex = 0;
            aircraftLabel.Text = "Aircraft";
            // 
            // tacomCountLabel
            // 
            tacomCountLabel.Anchor = AnchorStyles.Right;
            tacomCountLabel.AutoSize = true;
            tacomCountLabel.Font = new Font("Segoe UI", 9F);
            tacomCountLabel.Location = new Point(20, 2);
            tacomCountLabel.Name = "tacomCountLabel";
            tacomCountLabel.Size = new Size(13, 15);
            tacomCountLabel.TabIndex = 1;
            tacomCountLabel.Text = "0";
            // 
            // infantryCountLabel
            // 
            infantryCountLabel.Anchor = AnchorStyles.Right;
            infantryCountLabel.AutoSize = true;
            infantryCountLabel.Font = new Font("Segoe UI", 9F);
            infantryCountLabel.Location = new Point(20, 22);
            infantryCountLabel.Name = "infantryCountLabel";
            infantryCountLabel.Size = new Size(13, 15);
            infantryCountLabel.TabIndex = 1;
            infantryCountLabel.Text = "0";
            // 
            // vehicleCountLabel
            // 
            vehicleCountLabel.Anchor = AnchorStyles.Right;
            vehicleCountLabel.AutoSize = true;
            vehicleCountLabel.Font = new Font("Segoe UI", 9F);
            vehicleCountLabel.Location = new Point(20, 42);
            vehicleCountLabel.Name = "vehicleCountLabel";
            vehicleCountLabel.Size = new Size(13, 15);
            vehicleCountLabel.TabIndex = 1;
            vehicleCountLabel.Text = "0";
            // 
            // aircraftCountLabel
            // 
            aircraftCountLabel.Anchor = AnchorStyles.Right;
            aircraftCountLabel.AutoSize = true;
            aircraftCountLabel.Font = new Font("Segoe UI", 9F);
            aircraftCountLabel.Location = new Point(20, 62);
            aircraftCountLabel.Name = "aircraftCountLabel";
            aircraftCountLabel.Size = new Size(13, 15);
            aircraftCountLabel.TabIndex = 1;
            aircraftCountLabel.Text = "0";
            // 
            // typeBox
            // 
            typeBox.Controls.Add(typeTable);
            typeBox.Location = new Point(12, 12);
            typeBox.Name = "typeBox";
            typeBox.Size = new Size(128, 101);
            typeBox.TabIndex = 2;
            typeBox.TabStop = false;
            typeBox.Text = "Unit Types";
            // 
            // roleBox
            // 
            roleBox.Controls.Add(rolesTable);
            roleBox.Location = new Point(12, 226);
            roleBox.Name = "roleBox";
            roleBox.Size = new Size(128, 281);
            roleBox.TabIndex = 3;
            roleBox.TabStop = false;
            roleBox.Text = "Unit Roles";
            // 
            // rolesTable
            // 
            rolesTable.ColumnCount = 2;
            rolesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            rolesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            rolesTable.Dock = DockStyle.Fill;
            rolesTable.Location = new Point(3, 19);
            rolesTable.Name = "rolesTable";
            rolesTable.RowCount = 1;
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            rolesTable.Size = new Size(122, 259);
            rolesTable.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Location = new Point(12, 119);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(128, 101);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Army Info";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.Controls.Add(label1, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(label4, 0, 1);
            tableLayoutPanel1.Controls.Add(label5, 1, 2);
            tableLayoutPanel1.Controls.Add(label6, 0, 2);
            tableLayoutPanel1.Controls.Add(label7, 1, 3);
            tableLayoutPanel1.Controls.Add(label8, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(122, 79);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(39, 2);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 0;
            label1.Text = "Total Units";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(39, 22);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 0;
            label2.Text = "Command";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(20, 2);
            label3.Name = "label3";
            label3.Size = new Size(13, 15);
            label3.TabIndex = 1;
            label3.Text = "0";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.Location = new Point(20, 22);
            label4.Name = "label4";
            label4.Size = new Size(13, 15);
            label4.TabIndex = 1;
            label4.Text = "0";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(39, 42);
            label5.Name = "label5";
            label5.Size = new Size(74, 15);
            label5.TabIndex = 0;
            label5.Text = "Non-Loaded";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.Location = new Point(20, 42);
            label6.Name = "label6";
            label6.Size = new Size(13, 15);
            label6.TabIndex = 1;
            label6.Text = "0";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new Point(39, 62);
            label7.Name = "label7";
            label7.Size = new Size(64, 15);
            label7.TabIndex = 0;
            label7.Text = "Pt Average";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(20, 62);
            label8.Name = "label8";
            label8.Size = new Size(13, 15);
            label8.TabIndex = 1;
            label8.Text = "0";
            // 
            // ArmyStats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(152, 515);
            Controls.Add(groupBox1);
            Controls.Add(roleBox);
            Controls.Add(typeBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ArmyStats";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Army Stats";
            typeTable.ResumeLayout(false);
            typeTable.PerformLayout();
            typeBox.ResumeLayout(false);
            roleBox.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label tacomLabel;
        private TableLayoutPanel typeTable;
        private Label infantryLabel;
        private Label vehiclesLabel;
        private Label aircraftLabel;
        private Label tacomCountLabel;
        private Label infantryCountLabel;
        private Label vehicleCountLabel;
        private Label aircraftCountLabel;
        private GroupBox typeBox;
        private GroupBox roleBox;
        private TableLayoutPanel rolesTable;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
    }
}