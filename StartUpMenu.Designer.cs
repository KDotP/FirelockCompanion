namespace FirelockCompanion
{
    partial class StartUpMenu
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
            titleLabel = new Label();
            newArmyButton = new Button();
            loadEditButton = new Button();
            loadPlayButton = new Button();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.Anchor = AnchorStyles.None;
            titleLabel.Font = new Font("Bombardier", 35.9999962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            titleLabel.Location = new Point(31, 19);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(396, 51);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Firelock Companion";
            // 
            // newArmyButton
            // 
            newArmyButton.Anchor = AnchorStyles.None;
            newArmyButton.Location = new Point(31, 92);
            newArmyButton.Name = "newArmyButton";
            newArmyButton.Size = new Size(75, 23);
            newArmyButton.TabIndex = 1;
            newArmyButton.Text = "New Army";
            newArmyButton.UseVisualStyleBackColor = true;
            newArmyButton.Click += newArmyButton_Click;
            // 
            // loadEditButton
            // 
            loadEditButton.Anchor = AnchorStyles.None;
            loadEditButton.Location = new Point(171, 92);
            loadEditButton.Name = "loadEditButton";
            loadEditButton.Size = new Size(94, 23);
            loadEditButton.TabIndex = 2;
            loadEditButton.Text = "Load and Edit";
            loadEditButton.UseVisualStyleBackColor = true;
            loadEditButton.Click += loadEditButton_Click;
            // 
            // loadPlayButton
            // 
            loadPlayButton.Anchor = AnchorStyles.None;
            loadPlayButton.Location = new Point(332, 92);
            loadPlayButton.Name = "loadPlayButton";
            loadPlayButton.Size = new Size(95, 23);
            loadPlayButton.TabIndex = 3;
            loadPlayButton.Text = "Load and Play";
            loadPlayButton.UseVisualStyleBackColor = true;
            // 
            // StartUpMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 148);
            Controls.Add(loadPlayButton);
            Controls.Add(loadEditButton);
            Controls.Add(newArmyButton);
            Controls.Add(titleLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "StartUpMenu";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Firelock Companion Menu";
            ResumeLayout(false);
        }

        #endregion

        private Label titleLabel;
        private Button newArmyButton;
        private Button loadEditButton;
        private Button loadPlayButton;
    }
}