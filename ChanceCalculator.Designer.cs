namespace FirelockCompanion
{
    partial class ChanceCalculator
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
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            checkBox2 = new CheckBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            checkBox3 = new CheckBox();
            groupBox3 = new GroupBox();
            label4 = new Label();
            textBox3 = new TextBox();
            checkBox4 = new CheckBox();
            textBox4 = new TextBox();
            label5 = new Label();
            checkBox5 = new CheckBox();
            textBox5 = new TextBox();
            label6 = new Label();
            textBox6 = new TextBox();
            label7 = new Label();
            textBox7 = new TextBox();
            label8 = new Label();
            groupBox1 = new GroupBox();
            label9 = new Label();
            textBox8 = new TextBox();
            textBox9 = new TextBox();
            label10 = new Label();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(267, 45);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(309, 23);
            comboBox2.TabIndex = 1;
            comboBox2.Text = "Unit targeted...";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "Front", "Side", "Rear" });
            comboBox3.Location = new Point(664, 45);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(98, 23);
            comboBox3.TabIndex = 2;
            comboBox3.Text = "Front";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(139, 47);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(127, 19);
            checkBox2.TabIndex = 4;
            checkBox2.Text = "Target Specific Unit";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(582, 49);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 5;
            label1.Text = "Targeted Arc:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox7);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(comboBox3);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(comboBox2);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(checkBox2);
            groupBox2.Location = new Point(12, 101);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(776, 100);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Chance to Kill";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(111, 16);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(22, 23);
            textBox1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(102, 15);
            label2.TabIndex = 1;
            label2.Text = "Weapon Strength:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 48);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 3;
            label3.Text = "Target Toughness:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(111, 45);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(22, 23);
            textBox2.TabIndex = 2;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(654, 22);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(116, 19);
            checkBox3.TabIndex = 7;
            checkBox3.Text = "Target Unspotted";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(textBox6);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(textBox5);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(checkBox5);
            groupBox3.Controls.Add(textBox4);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(checkBox4);
            groupBox3.Controls.Add(textBox3);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(checkBox3);
            groupBox3.Location = new Point(12, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(776, 83);
            groupBox3.TabIndex = 8;
            groupBox3.TabStop = false;
            groupBox3.Text = "Chance to Hit";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 25);
            label4.Name = "label4";
            label4.Size = new Size(115, 15);
            label4.TabIndex = 8;
            label4.Text = "Stationary Accuracy:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(120, 22);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(22, 23);
            textBox3.TabIndex = 9;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(303, 25);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(67, 19);
            checkBox4.TabIndex = 11;
            checkBox4.Text = "Moving";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(256, 22);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(22, 23);
            textBox4.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Enabled = false;
            label5.Location = new Point(153, 25);
            label5.Name = "label5";
            label5.Size = new Size(103, 15);
            label5.TabIndex = 12;
            label5.Text = "Moving Accuracy:";
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(654, 44);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(84, 19);
            checkBox5.TabIndex = 14;
            checkBox5.Text = "Half Range";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(80, 48);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(22, 23);
            textBox5.TabIndex = 16;
            textBox5.Text = "0";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Enabled = false;
            label6.Location = new Point(6, 51);
            label6.Name = "label6";
            label6.Size = new Size(74, 15);
            label6.TabIndex = 15;
            label6.Text = "Attacker Pin:";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(194, 49);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(22, 23);
            textBox6.TabIndex = 18;
            textBox6.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Enabled = false;
            label7.Location = new Point(120, 52);
            label7.Name = "label7";
            label7.Size = new Size(74, 15);
            label7.TabIndex = 17;
            label7.Text = "Attacker Pin:";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(367, 16);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(22, 23);
            textBox7.TabIndex = 20;
            textBox7.Text = "0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Enabled = false;
            label8.Location = new Point(237, 19);
            label8.Name = "label8";
            label8.Size = new Size(124, 15);
            label8.TabIndex = 19;
            label8.Text = "Target Cover Modifier:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox9);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(textBox8);
            groupBox1.Controls.Add(label9);
            groupBox1.Location = new Point(12, 207);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 100);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Outcome";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 19);
            label9.Name = "label9";
            label9.Size = new Size(83, 15);
            label9.TabIndex = 0;
            label9.Text = "Chance to Hit:";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(89, 16);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(22, 23);
            textBox8.TabIndex = 6;
            // 
            // textBox9
            // 
            textBox9.Location = new Point(89, 45);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(22, 23);
            textBox9.TabIndex = 8;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 48);
            label10.Name = "label10";
            label10.Size = new Size(83, 15);
            label10.TabIndex = 7;
            label10.Text = "Chance to Kill:";
            // 
            // ChanceCalculator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Name = "ChanceCalculator";
            Text = "ChanceCalculator";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private CheckBox checkBox2;
        private Label label1;
        private GroupBox groupBox2;
        private CheckBox checkBox3;
        private Label label3;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox1;
        private GroupBox groupBox3;
        private TextBox textBox4;
        private Label label5;
        private CheckBox checkBox4;
        private TextBox textBox3;
        private Label label4;
        private TextBox textBox5;
        private Label label6;
        private CheckBox checkBox5;
        private TextBox textBox6;
        private Label label7;
        private TextBox textBox7;
        private Label label8;
        private GroupBox groupBox1;
        private TextBox textBox9;
        private Label label10;
        private TextBox textBox8;
        private Label label9;
    }
}