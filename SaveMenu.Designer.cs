namespace FirelockCompanion
{
    partial class SaveMenu
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
            armyNameTextBox = new TextBox();
            nameSuffixLabel = new Label();
            saveButton = new Button();
            cancelButton = new Button();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // armyNameTextBox
            // 
            armyNameTextBox.Location = new Point(12, 65);
            armyNameTextBox.Name = "armyNameTextBox";
            armyNameTextBox.Size = new Size(373, 23);
            armyNameTextBox.TabIndex = 0;
            armyNameTextBox.Text = "New Army";
            armyNameTextBox.WordWrap = false;
            // 
            // nameSuffixLabel
            // 
            nameSuffixLabel.AutoSize = true;
            nameSuffixLabel.BackColor = Color.Transparent;
            nameSuffixLabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            nameSuffixLabel.Location = new Point(12, 91);
            nameSuffixLabel.Name = "nameSuffixLabel";
            nameSuffixLabel.Size = new Size(313, 13);
            nameSuffixLabel.TabIndex = 1;
            nameSuffixLabel.Text = "All saves automatically add \"Faction Name X pts.json\" at the end";
            // 
            // saveButton
            // 
            saveButton.Location = new Point(229, 110);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 2;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(310, 110);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(133, 9);
            label2.Name = "label2";
            label2.Size = new Size(130, 32);
            label2.TabIndex = 4;
            label2.Text = "Save Army";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(84, 41);
            label3.Name = "label3";
            label3.Size = new Size(220, 15);
            label3.TabIndex = 5;
            label3.Text = "Save files are saved to a local save folder";
            // 
            // SaveMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(399, 145);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cancelButton);
            Controls.Add(saveButton);
            Controls.Add(nameSuffixLabel);
            Controls.Add(armyNameTextBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "SaveMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SaveMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox armyNameTextBox;
        private Label nameSuffixLabel;
        private Button saveButton;
        private Button cancelButton;
        private Label label2;
        private Label label3;
    }
}