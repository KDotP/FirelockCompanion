namespace FirelockCompanion
{
    partial class HitCalculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HitCalculator));
            textBox1 = new TextBox();
            label1 = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label2 = new Label();
            textBox2 = new TextBox();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            groupBox3 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            textBox3 = new TextBox();
            checkBox5 = new CheckBox();
            label5 = new Label();
            textBox4 = new TextBox();
            label6 = new Label();
            textBox5 = new TextBox();
            resultsBox = new GroupBox();
            label8 = new Label();
            label7 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            resultsBox.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 27);
            textBox1.MaxLength = 2;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(128, 23);
            textBox1.TabIndex = 0;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(128, 23);
            label1.TabIndex = 1;
            label1.Text = "Base Accuracy (X+)";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(6, 22);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(86, 19);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Blind Firing";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(6, 47);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(65, 19);
            checkBox2.TabIndex = 2;
            checkBox2.Text = "Scoped";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Location = new Point(12, 56);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(128, 77);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Blind Fires";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(checkBox4);
            groupBox2.Controls.Add(checkBox3);
            groupBox2.Location = new Point(12, 139);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(128, 123);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "To-Hit Modifiers";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 91);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 3;
            label2.Text = "Total Pin";
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.Window;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point(6, 89);
            textBox2.MaxLength = 1;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(19, 23);
            textBox2.TabIndex = 5;
            textBox2.Text = "0";
            textBox2.TextAlign = HorizontalAlignment.Center;
            textBox2.WordWrap = false;
            // 
            // checkBox4
            // 
            checkBox4.Location = new Point(8, 47);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(114, 36);
            checkBox4.TabIndex = 4;
            checkBox4.Text = "Target spotted through smoke";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(8, 22);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(84, 19);
            checkBox3.TabIndex = 3;
            checkBox3.Text = "Half Range";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(textBox3);
            groupBox3.Controls.Add(checkBox5);
            groupBox3.Location = new Point(12, 268);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(128, 81);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "To-Kill Modifiers";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(31, 59);
            label4.Name = "label4";
            label4.Size = new Size(93, 13);
            label4.TabIndex = 4;
            label4.Text = "After Ignore Cover";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 44);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 3;
            label3.Text = "Cover Modifier";
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.Window;
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Location = new Point(6, 47);
            textBox3.MaxLength = 1;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(19, 23);
            textBox3.TabIndex = 7;
            textBox3.Text = "0";
            textBox3.TextAlign = HorizontalAlignment.Center;
            textBox3.WordWrap = false;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(6, 22);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(106, 19);
            checkBox5.TabIndex = 6;
            checkBox5.Text = "Shaped Charge";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Location = new Point(12, 352);
            label5.Name = "label5";
            label5.Size = new Size(128, 23);
            label5.TabIndex = 1;
            label5.Text = "Attack Strength";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(12, 370);
            textBox4.MaxLength = 2;
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(128, 23);
            textBox4.TabIndex = 8;
            textBox4.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.Location = new Point(12, 396);
            label6.Name = "label6";
            label6.Size = new Size(128, 23);
            label6.TabIndex = 1;
            label6.Text = "Target Toughness";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(12, 414);
            textBox5.MaxLength = 2;
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(128, 23);
            textBox5.TabIndex = 9;
            textBox5.TextAlign = HorizontalAlignment.Right;
            // 
            // resultsBox
            // 
            resultsBox.BackColor = Color.AntiqueWhite;
            resultsBox.Controls.Add(label8);
            resultsBox.Controls.Add(label7);
            resultsBox.Location = new Point(12, 443);
            resultsBox.Name = "resultsBox";
            resultsBox.Size = new Size(128, 60);
            resultsBox.TabIndex = 7;
            resultsBox.TabStop = false;
            resultsBox.Text = "Results";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(9, 36);
            label8.Name = "label8";
            label8.Size = new Size(62, 15);
            label8.TabIndex = 0;
            label8.Text = "Kills on 4+";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(9, 19);
            label7.Name = "label7";
            label7.Size = new Size(62, 15);
            label7.TabIndex = 0;
            label7.Text = "Hits on 4+";
            // 
            // HitCalculator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(152, 515);
            Controls.Add(resultsBox);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HitCalculator";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Hit Calculator";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            resultsBox.ResumeLayout(false);
            resultsBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label2;
        private TextBox textBox2;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private GroupBox groupBox3;
        private Label label4;
        private Label label3;
        private TextBox textBox3;
        private CheckBox checkBox5;
        private Label label5;
        private TextBox textBox4;
        private Label label6;
        private TextBox textBox5;
        private GroupBox resultsBox;
        private Label label8;
        private Label label7;
    }
}