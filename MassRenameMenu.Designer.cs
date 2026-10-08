namespace FirelockCompanion
{
    partial class MassRenameMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MassRenameMenu));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            poolsComboBox = new ComboBox();
            renameButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(25, 114);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(247, 289);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("SimSun-ExtG", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(269, 29);
            label1.TabIndex = 1;
            label1.Text = "Rename Initiated";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(89, 38);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 2;
            label2.Text = "Select name pool";
            // 
            // poolsComboBox
            // 
            poolsComboBox.FormattingEnabled = true;
            poolsComboBox.Location = new Point(12, 56);
            poolsComboBox.Name = "poolsComboBox";
            poolsComboBox.Size = new Size(269, 23);
            poolsComboBox.TabIndex = 3;
            // 
            // renameButton
            // 
            renameButton.Location = new Point(47, 85);
            renameButton.Name = "renameButton";
            renameButton.Size = new Size(75, 23);
            renameButton.TabIndex = 4;
            renameButton.Text = "Rename";
            renameButton.UseVisualStyleBackColor = true;
            renameButton.Click += renameButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(167, 85);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // MassRenameMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(295, 242);
            ControlBox = false;
            Controls.Add(cancelButton);
            Controls.Add(renameButton);
            Controls.Add(poolsComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MassRenameMenu";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Mass Rename Menu";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private ComboBox poolsComboBox;
        private Button renameButton;
        private Button cancelButton;
    }
}