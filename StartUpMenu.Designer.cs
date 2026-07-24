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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartUpMenu));
            titleLabel = new Label();
            newArmyButton = new Button();
            loadEditButton = new Button();
            loadPlayButton = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.Anchor = AnchorStyles.None;
            titleLabel.Font = new Font("Bombardier", 35.9999962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            titleLabel.Location = new Point(35, 9);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(396, 51);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Firelock Companion";
            // 
            // newArmyButton
            // 
            newArmyButton.Anchor = AnchorStyles.None;
            newArmyButton.Location = new Point(35, 63);
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
            loadEditButton.Location = new Point(181, 63);
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
            loadPlayButton.Location = new Point(336, 63);
            loadPlayButton.Name = "loadPlayButton";
            loadPlayButton.Size = new Size(95, 23);
            loadPlayButton.TabIndex = 3;
            loadPlayButton.Text = "Load and Play";
            loadPlayButton.UseVisualStyleBackColor = true;
            loadPlayButton.Click += loadPlayButton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = Properties.Resources.thatdog;
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(26, 92);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(369, 223);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 261);
            label1.Name = "label1";
            label1.Size = new Size(92, 34);
            label1.TabIndex = 5;
            label1.Text = "Firelock 198X\r\nby RifleInfantry";
            // 
            // StartUpMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 304);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(loadPlayButton);
            Controls.Add(loadEditButton);
            Controls.Add(newArmyButton);
            Controls.Add(titleLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "StartUpMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Firelock Companion Menu";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label titleLabel;
        private Button newArmyButton;
        private Button loadEditButton;
        private Button loadPlayButton;
        private PictureBox pictureBox1;
        private Label label1;
    }
}