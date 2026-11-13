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
            accuracyTextbox = new TextBox();
            label1 = new Label();
            blindFireCheck = new CheckBox();
            scopedCheck = new CheckBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label2 = new Label();
            pinTextbox = new TextBox();
            throughSmokeCheck = new CheckBox();
            halfRangeCheck = new CheckBox();
            groupBox3 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            coverTextbox = new TextBox();
            shapedChargeCheck = new CheckBox();
            label5 = new Label();
            strengthTextbox = new TextBox();
            label6 = new Label();
            toughnessTextbox = new TextBox();
            resultsBox = new GroupBox();
            killLabel = new Label();
            hitLabel = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            resultsBox.SuspendLayout();
            SuspendLayout();
            // 
            // accuracyTextbox
            // 
            accuracyTextbox.Location = new Point(12, 27);
            accuracyTextbox.MaxLength = 2;
            accuracyTextbox.Name = "accuracyTextbox";
            accuracyTextbox.Size = new Size(128, 23);
            accuracyTextbox.TabIndex = 0;
            accuracyTextbox.Text = "4";
            accuracyTextbox.TextAlign = HorizontalAlignment.Center;
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
            // blindFireCheck
            // 
            blindFireCheck.AutoSize = true;
            blindFireCheck.Location = new Point(6, 22);
            blindFireCheck.Name = "blindFireCheck";
            blindFireCheck.Size = new Size(86, 19);
            blindFireCheck.TabIndex = 1;
            blindFireCheck.Text = "Blind Firing";
            blindFireCheck.UseVisualStyleBackColor = true;
            // 
            // scopedCheck
            // 
            scopedCheck.AutoSize = true;
            scopedCheck.Location = new Point(6, 47);
            scopedCheck.Name = "scopedCheck";
            scopedCheck.Size = new Size(65, 19);
            scopedCheck.TabIndex = 2;
            scopedCheck.Text = "Scoped";
            scopedCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(blindFireCheck);
            groupBox1.Controls.Add(scopedCheck);
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
            groupBox2.Controls.Add(pinTextbox);
            groupBox2.Controls.Add(throughSmokeCheck);
            groupBox2.Controls.Add(halfRangeCheck);
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
            // pinTextbox
            // 
            pinTextbox.BackColor = SystemColors.Window;
            pinTextbox.BorderStyle = BorderStyle.FixedSingle;
            pinTextbox.Location = new Point(6, 89);
            pinTextbox.MaxLength = 1;
            pinTextbox.Name = "pinTextbox";
            pinTextbox.Size = new Size(19, 23);
            pinTextbox.TabIndex = 5;
            pinTextbox.Text = "0";
            pinTextbox.TextAlign = HorizontalAlignment.Center;
            pinTextbox.WordWrap = false;
            // 
            // throughSmokeCheck
            // 
            throughSmokeCheck.Location = new Point(8, 47);
            throughSmokeCheck.Name = "throughSmokeCheck";
            throughSmokeCheck.Size = new Size(114, 36);
            throughSmokeCheck.TabIndex = 4;
            throughSmokeCheck.Text = "Target spotted through smoke";
            throughSmokeCheck.UseVisualStyleBackColor = true;
            // 
            // halfRangeCheck
            // 
            halfRangeCheck.AutoSize = true;
            halfRangeCheck.Location = new Point(8, 22);
            halfRangeCheck.Name = "halfRangeCheck";
            halfRangeCheck.Size = new Size(84, 19);
            halfRangeCheck.TabIndex = 3;
            halfRangeCheck.Text = "Half Range";
            halfRangeCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(coverTextbox);
            groupBox3.Controls.Add(shapedChargeCheck);
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
            // coverTextbox
            // 
            coverTextbox.BackColor = SystemColors.Window;
            coverTextbox.BorderStyle = BorderStyle.FixedSingle;
            coverTextbox.Location = new Point(6, 47);
            coverTextbox.MaxLength = 1;
            coverTextbox.Name = "coverTextbox";
            coverTextbox.Size = new Size(19, 23);
            coverTextbox.TabIndex = 7;
            coverTextbox.Text = "0";
            coverTextbox.TextAlign = HorizontalAlignment.Center;
            coverTextbox.WordWrap = false;
            // 
            // shapedChargeCheck
            // 
            shapedChargeCheck.AutoSize = true;
            shapedChargeCheck.Location = new Point(6, 22);
            shapedChargeCheck.Name = "shapedChargeCheck";
            shapedChargeCheck.Size = new Size(106, 19);
            shapedChargeCheck.TabIndex = 6;
            shapedChargeCheck.Text = "Shaped Charge";
            shapedChargeCheck.UseVisualStyleBackColor = true;
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
            // strengthTextbox
            // 
            strengthTextbox.Location = new Point(12, 370);
            strengthTextbox.MaxLength = 2;
            strengthTextbox.Name = "strengthTextbox";
            strengthTextbox.Size = new Size(128, 23);
            strengthTextbox.TabIndex = 8;
            strengthTextbox.Text = "3";
            strengthTextbox.TextAlign = HorizontalAlignment.Center;
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
            // toughnessTextbox
            // 
            toughnessTextbox.Location = new Point(12, 414);
            toughnessTextbox.MaxLength = 2;
            toughnessTextbox.Name = "toughnessTextbox";
            toughnessTextbox.Size = new Size(128, 23);
            toughnessTextbox.TabIndex = 9;
            toughnessTextbox.Text = "3";
            toughnessTextbox.TextAlign = HorizontalAlignment.Center;
            // 
            // resultsBox
            // 
            resultsBox.BackColor = Color.AntiqueWhite;
            resultsBox.Controls.Add(killLabel);
            resultsBox.Controls.Add(hitLabel);
            resultsBox.Location = new Point(12, 443);
            resultsBox.Name = "resultsBox";
            resultsBox.Size = new Size(128, 60);
            resultsBox.TabIndex = 7;
            resultsBox.TabStop = false;
            resultsBox.Text = "Results";
            // 
            // killLabel
            // 
            killLabel.AutoSize = true;
            killLabel.Location = new Point(9, 36);
            killLabel.Name = "killLabel";
            killLabel.Size = new Size(62, 15);
            killLabel.TabIndex = 0;
            killLabel.Text = "Kills on 4+";
            // 
            // hitLabel
            // 
            hitLabel.AutoSize = true;
            hitLabel.Location = new Point(9, 19);
            hitLabel.Name = "hitLabel";
            hitLabel.Size = new Size(62, 15);
            hitLabel.TabIndex = 0;
            hitLabel.Text = "Hits on 4+";
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
            Controls.Add(toughnessTextbox);
            Controls.Add(strengthTextbox);
            Controls.Add(accuracyTextbox);
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

        private TextBox accuracyTextbox;
        private Label label1;
        private CheckBox blindFireCheck;
        private CheckBox scopedCheck;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label2;
        private TextBox pinTextbox;
        private CheckBox throughSmokeCheck;
        private CheckBox halfRangeCheck;
        private GroupBox groupBox3;
        private Label label4;
        private Label label3;
        private TextBox coverTextbox;
        private CheckBox shapedChargeCheck;
        private Label label5;
        private TextBox strengthTextbox;
        private Label label6;
        private TextBox toughnessTextbox;
        private GroupBox resultsBox;
        private Label killLabel;
        private Label hitLabel;
    }
}