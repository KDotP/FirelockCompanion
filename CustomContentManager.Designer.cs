namespace FirelockCompanion
{
    partial class CustomContentManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomContentManager));
            pizzaDogImage = new PictureBox();
            treeView1 = new TreeView();
            treeView2 = new TreeView();
            button1 = new Button();
            button2 = new Button();
            splitContainer1 = new SplitContainer();
            titleTextbox = new TextBox();
            button3 = new Button();
            label1 = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pizzaDogImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pizzaDogImage
            // 
            pizzaDogImage.Image = (Image)resources.GetObject("pizzaDogImage.Image");
            pizzaDogImage.Location = new Point(12, 165);
            pizzaDogImage.Name = "pizzaDogImage";
            pizzaDogImage.Size = new Size(69, 71);
            pizzaDogImage.SizeMode = PictureBoxSizeMode.Zoom;
            pizzaDogImage.TabIndex = 0;
            pizzaDogImage.TabStop = false;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(12, 24);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(523, 144);
            treeView1.TabIndex = 1;
            // 
            // treeView2
            // 
            treeView2.Location = new Point(12, 217);
            treeView2.Name = "treeView2";
            treeView2.Size = new Size(523, 168);
            treeView2.TabIndex = 2;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(169, 174);
            button1.Name = "button1";
            button1.Size = new Size(37, 40);
            button1.TabIndex = 3;
            button1.Text = " ↑";
            button1.TextAlign = ContentAlignment.TopCenter;
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(343, 174);
            button2.Name = "button2";
            button2.Size = new Size(37, 40);
            button2.TabIndex = 4;
            button2.Text = " ↓";
            button2.TextAlign = ContentAlignment.TopCenter;
            button2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(titleTextbox);
            splitContainer1.Panel1.Controls.Add(button3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(treeView1);
            splitContainer1.Panel2.Controls.Add(button2);
            splitContainer1.Panel2.Controls.Add(treeView2);
            splitContainer1.Panel2.Controls.Add(button1);
            splitContainer1.Panel2.Controls.Add(pizzaDogImage);
            splitContainer1.Panel2.Controls.Add(pictureBox1);
            splitContainer1.Size = new Size(547, 537);
            splitContainer1.SplitterDistance = 28;
            splitContainer1.TabIndex = 5;
            // 
            // titleTextbox
            // 
            titleTextbox.Anchor = AnchorStyles.Top;
            titleTextbox.BackColor = SystemColors.Control;
            titleTextbox.BorderStyle = BorderStyle.None;
            titleTextbox.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            titleTextbox.Location = new Point(169, 4);
            titleTextbox.Name = "titleTextbox";
            titleTextbox.Size = new Size(211, 22);
            titleTextbox.TabIndex = 1;
            titleTextbox.Text = "Custom Content Manager";
            titleTextbox.TextAlign = HorizontalAlignment.Center;
            // 
            // button3
            // 
            button3.Location = new Point(460, 3);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 0;
            button3.Text = "↻ Refresh";
            button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.Location = new Point(245, 0);
            label1.Name = "label1";
            label1.Size = new Size(52, 21);
            label1.TabIndex = 5;
            label1.Text = "Active";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label2.Location = new Point(236, 193);
            label2.Name = "label2";
            label2.Size = new Size(70, 21);
            label2.TabIndex = 5;
            label2.Text = "Disabled";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(460, 165);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(75, 71);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // CustomContentManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(547, 537);
            Controls.Add(splitContainer1);
            Name = "CustomContentManager";
            Text = "CustomContentManager";
            ((System.ComponentModel.ISupportInitialize)pizzaDogImage).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pizzaDogImage;
        private TreeView treeView1;
        private TreeView treeView2;
        private Button button1;
        private Button button2;
        private SplitContainer splitContainer1;
        private TextBox titleTextbox;
        private Button button3;
        private Label label1;
        private Label label2;
        private PictureBox pictureBox1;
    }
}