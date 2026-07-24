namespace FirelockCompanion
{
    partial class NewMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMenu));
            label1 = new Label();
            factionBox = new ComboBox();
            createButton = new Button();
            cancelButton = new Button();
            armyTextbox = new TextBox();
            label2 = new Label();
            pointsBox = new ComboBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(333, 40);
            label1.TabIndex = 0;
            label1.Text = "Hmm... Today I Shall Play";
            // 
            // factionBox
            // 
            factionBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            factionBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            factionBox.FormattingEnabled = true;
            factionBox.Location = new Point(12, 52);
            factionBox.Name = "factionBox";
            factionBox.Size = new Size(333, 23);
            factionBox.TabIndex = 1;
            // 
            // createButton
            // 
            createButton.Location = new Point(189, 139);
            createButton.Name = "createButton";
            createButton.Size = new Size(75, 23);
            createButton.TabIndex = 2;
            createButton.Text = "Create";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(270, 139);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // armyTextbox
            // 
            armyTextbox.Location = new Point(92, 81);
            armyTextbox.Name = "armyTextbox";
            armyTextbox.Size = new Size(253, 23);
            armyTextbox.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 84);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 5;
            label2.Text = "Army Name:";
            // 
            // pointsBox
            // 
            pointsBox.FormattingEnabled = true;
            pointsBox.Items.AddRange(new object[] { "100", "200", "300", "400" });
            pointsBox.Location = new Point(270, 110);
            pointsBox.Name = "pointsBox";
            pointsBox.Size = new Size(75, 23);
            pointsBox.TabIndex = 6;
            pointsBox.Text = "100";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(189, 113);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 7;
            label3.Text = "Max Points:";
            // 
            // NewMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(360, 174);
            Controls.Add(label3);
            Controls.Add(pointsBox);
            Controls.Add(label2);
            Controls.Add(armyTextbox);
            Controls.Add(cancelButton);
            Controls.Add(createButton);
            Controls.Add(factionBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Create New Army";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox factionBox;
        private Button createButton;
        private Button cancelButton;
        private TextBox armyTextbox;
        private Label label2;
        private ComboBox pointsBox;
        private Label label3;
    }
}