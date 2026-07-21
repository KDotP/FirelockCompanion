namespace FirelockCompanion
{
    partial class TercioBuilder
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
            label1 = new Label();
            firstUnitBox = new ComboBox();
            secondUnitBox = new ComboBox();
            thirdUnitBox = new ComboBox();
            cancelButton = new Button();
            confirmButton = new Button();
            costLabel = new Label();
            sizeLabel = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(139, 9);
            label1.Name = "label1";
            label1.Size = new Size(127, 25);
            label1.TabIndex = 0;
            label1.Text = "Tercio Builder";
            // 
            // firstUnitBox
            // 
            firstUnitBox.FormattingEnabled = true;
            firstUnitBox.Location = new Point(104, 39);
            firstUnitBox.Name = "firstUnitBox";
            firstUnitBox.Size = new Size(185, 23);
            firstUnitBox.TabIndex = 1;
            firstUnitBox.Text = "Lead Unit...";
            // 
            // secondUnitBox
            // 
            secondUnitBox.FormattingEnabled = true;
            secondUnitBox.Location = new Point(12, 68);
            secondUnitBox.Name = "secondUnitBox";
            secondUnitBox.Size = new Size(185, 23);
            secondUnitBox.TabIndex = 2;
            secondUnitBox.Text = "Second Unit...";
            // 
            // thirdUnitBox
            // 
            thirdUnitBox.FormattingEnabled = true;
            thirdUnitBox.Location = new Point(203, 68);
            thirdUnitBox.Name = "thirdUnitBox";
            thirdUnitBox.Size = new Size(185, 23);
            thirdUnitBox.TabIndex = 3;
            thirdUnitBox.Text = "Third Unit...";
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(313, 113);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // confirmButton
            // 
            confirmButton.Location = new Point(232, 113);
            confirmButton.Name = "confirmButton";
            confirmButton.Size = new Size(75, 23);
            confirmButton.TabIndex = 5;
            confirmButton.Text = "Confirm";
            confirmButton.UseVisualStyleBackColor = true;
            confirmButton.Click += confirmButton_Click;
            // 
            // costLabel
            // 
            costLabel.AutoSize = true;
            costLabel.Location = new Point(12, 104);
            costLabel.Name = "costLabel";
            costLabel.Size = new Size(62, 15);
            costLabel.TabIndex = 6;
            costLabel.Text = "Cost: 0 pts";
            // 
            // sizeLabel
            // 
            sizeLabel.AutoSize = true;
            sizeLabel.Location = new Point(12, 122);
            sizeLabel.Name = "sizeLabel";
            sizeLabel.Size = new Size(39, 15);
            sizeLabel.TabIndex = 7;
            sizeLabel.Text = "Size: 0";
            // 
            // TercioBuilder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(399, 146);
            Controls.Add(sizeLabel);
            Controls.Add(costLabel);
            Controls.Add(confirmButton);
            Controls.Add(cancelButton);
            Controls.Add(thirdUnitBox);
            Controls.Add(secondUnitBox);
            Controls.Add(firstUnitBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "TercioBuilder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TercioBuilder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox firstUnitBox;
        private ComboBox secondUnitBox;
        private ComboBox thirdUnitBox;
        private Button cancelButton;
        private Button confirmButton;
        private Label costLabel;
        private Label sizeLabel;
    }
}