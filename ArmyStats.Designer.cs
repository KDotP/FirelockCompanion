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
            tacomLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            infantryLabel = new Label();
            vehiclesLabel = new Label();
            aircraftLabel = new Label();
            tacomCountLabel = new Label();
            infantryCountLabel = new Label();
            vehicleCountLabel = new Label();
            aircraftCountLabel = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tacomLabel
            // 
            tacomLabel.Anchor = AnchorStyles.Left;
            tacomLabel.AutoSize = true;
            tacomLabel.Font = new Font("Segoe UI", 14.25F);
            tacomLabel.Location = new Point(53, 0);
            tacomLabel.Name = "tacomLabel";
            tacomLabel.Size = new Size(83, 25);
            tacomLabel.TabIndex = 0;
            tacomLabel.Text = "TACOMs";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.Controls.Add(tacomLabel, 1, 0);
            tableLayoutPanel1.Controls.Add(infantryLabel, 1, 1);
            tableLayoutPanel1.Controls.Add(vehiclesLabel, 1, 2);
            tableLayoutPanel1.Controls.Add(aircraftLabel, 1, 3);
            tableLayoutPanel1.Controls.Add(tacomCountLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(infantryCountLabel, 0, 1);
            tableLayoutPanel1.Controls.Add(vehicleCountLabel, 0, 2);
            tableLayoutPanel1.Controls.Add(aircraftCountLabel, 0, 3);
            tableLayoutPanel1.Location = new Point(114, 147);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(168, 107);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // infantryLabel
            // 
            infantryLabel.Anchor = AnchorStyles.Left;
            infantryLabel.AutoSize = true;
            infantryLabel.Font = new Font("Segoe UI", 14.25F);
            infantryLabel.Location = new Point(53, 26);
            infantryLabel.Name = "infantryLabel";
            infantryLabel.Size = new Size(77, 25);
            infantryLabel.TabIndex = 0;
            infantryLabel.Text = "Infantry";
            // 
            // vehiclesLabel
            // 
            vehiclesLabel.Anchor = AnchorStyles.Left;
            vehiclesLabel.AutoSize = true;
            vehiclesLabel.Font = new Font("Segoe UI", 14.25F);
            vehiclesLabel.Location = new Point(53, 52);
            vehiclesLabel.Name = "vehiclesLabel";
            vehiclesLabel.Size = new Size(81, 25);
            vehiclesLabel.TabIndex = 0;
            vehiclesLabel.Text = "Vehicles";
            // 
            // aircraftLabel
            // 
            aircraftLabel.Anchor = AnchorStyles.Left;
            aircraftLabel.AutoSize = true;
            aircraftLabel.Font = new Font("Segoe UI", 14.25F);
            aircraftLabel.Location = new Point(53, 80);
            aircraftLabel.Name = "aircraftLabel";
            aircraftLabel.Size = new Size(74, 25);
            aircraftLabel.TabIndex = 0;
            aircraftLabel.Text = "Aircraft";
            // 
            // tacomCountLabel
            // 
            tacomCountLabel.Anchor = AnchorStyles.Right;
            tacomCountLabel.AutoSize = true;
            tacomCountLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tacomCountLabel.Location = new Point(25, 0);
            tacomCountLabel.Name = "tacomCountLabel";
            tacomCountLabel.Size = new Size(22, 25);
            tacomCountLabel.TabIndex = 1;
            tacomCountLabel.Text = "0";
            // 
            // infantryCountLabel
            // 
            infantryCountLabel.Anchor = AnchorStyles.Right;
            infantryCountLabel.AutoSize = true;
            infantryCountLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            infantryCountLabel.Location = new Point(25, 26);
            infantryCountLabel.Name = "infantryCountLabel";
            infantryCountLabel.Size = new Size(22, 25);
            infantryCountLabel.TabIndex = 1;
            infantryCountLabel.Text = "0";
            // 
            // vehicleCountLabel
            // 
            vehicleCountLabel.Anchor = AnchorStyles.Right;
            vehicleCountLabel.AutoSize = true;
            vehicleCountLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            vehicleCountLabel.Location = new Point(25, 52);
            vehicleCountLabel.Name = "vehicleCountLabel";
            vehicleCountLabel.Size = new Size(22, 25);
            vehicleCountLabel.TabIndex = 1;
            vehicleCountLabel.Text = "0";
            // 
            // aircraftCountLabel
            // 
            aircraftCountLabel.Anchor = AnchorStyles.Right;
            aircraftCountLabel.AutoSize = true;
            aircraftCountLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            aircraftCountLabel.Location = new Point(25, 80);
            aircraftCountLabel.Name = "aircraftCountLabel";
            aircraftCountLabel.Size = new Size(22, 25);
            aircraftCountLabel.TabIndex = 1;
            aircraftCountLabel.Text = "0";
            // 
            // ArmyStats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "ArmyStats";
            Text = "ArmyStats";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label tacomLabel;
        private TableLayoutPanel tableLayoutPanel1;
        private Label infantryLabel;
        private Label vehiclesLabel;
        private Label aircraftLabel;
        private Label tacomCountLabel;
        private Label infantryCountLabel;
        private Label vehicleCountLabel;
        private Label aircraftCountLabel;
    }
}