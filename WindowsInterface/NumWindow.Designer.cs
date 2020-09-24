namespace WindowsInterface
{
    partial class NumWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.binForceLength = new System.Windows.Forms.TextBox();
            this.calculateResults = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.inputBaseDropDown = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.binTwoComplOutput = new System.Windows.Forms.TextBox();
            this.hexValOutput = new System.Windows.Forms.TextBox();
            this.decValOutput = new System.Windows.Forms.TextBox();
            this.binValOutput = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.inputLabel = new System.Windows.Forms.Label();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.hexForceLength = new System.Windows.Forms.TextBox();
            this.binTwoComplForceLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.statusMessages = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // binForceLength
            // 
            this.binForceLength.Location = new System.Drawing.Point(521, 295);
            this.binForceLength.Name = "binForceLength";
            this.binForceLength.PlaceholderText = "0";
            this.binForceLength.Size = new System.Drawing.Size(200, 39);
            this.binForceLength.TabIndex = 1;
            // 
            // calculateResults
            // 
            this.calculateResults.Location = new System.Drawing.Point(820, 681);
            this.calculateResults.Name = "calculateResults";
            this.calculateResults.Size = new System.Drawing.Size(272, 46);
            this.calculateResults.TabIndex = 2;
            this.calculateResults.Text = "Calculate";
            this.calculateResults.UseVisualStyleBackColor = true;
            this.calculateResults.Click += new System.EventHandler(this.calculateResults_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(521, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "Force Length";
            // 
            // inputBaseDropDown
            // 
            this.inputBaseDropDown.FormattingEnabled = true;
            this.inputBaseDropDown.Items.AddRange(new object[] {
            "Binary",
            "Decimal",
            "Hexadecimal"});
            this.inputBaseDropDown.Location = new System.Drawing.Point(177, 52);
            this.inputBaseDropDown.Name = "inputBaseDropDown";
            this.inputBaseDropDown.Size = new System.Drawing.Size(313, 40);
            this.inputBaseDropDown.TabIndex = 6;
            this.inputBaseDropDown.Text = "--select--";
            this.inputBaseDropDown.SelectedIndexChanged += new System.EventHandler(this.inputBaseDropDown_Changed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 32);
            this.label3.TabIndex = 8;
            this.label3.Text = "Input base:";
            // 
            // binTwoComplOutput
            // 
            this.binTwoComplOutput.Location = new System.Drawing.Point(36, 688);
            this.binTwoComplOutput.Name = "binTwoComplOutput";
            this.binTwoComplOutput.ReadOnly = true;
            this.binTwoComplOutput.Size = new System.Drawing.Size(454, 39);
            this.binTwoComplOutput.TabIndex = 7;
            // 
            // hexValOutput
            // 
            this.hexValOutput.Location = new System.Drawing.Point(36, 557);
            this.hexValOutput.Name = "hexValOutput";
            this.hexValOutput.ReadOnly = true;
            this.hexValOutput.Size = new System.Drawing.Size(454, 39);
            this.hexValOutput.TabIndex = 6;
            // 
            // decValOutput
            // 
            this.decValOutput.Location = new System.Drawing.Point(36, 426);
            this.decValOutput.Name = "decValOutput";
            this.decValOutput.ReadOnly = true;
            this.decValOutput.Size = new System.Drawing.Size(454, 39);
            this.decValOutput.TabIndex = 5;
            // 
            // binValOutput
            // 
            this.binValOutput.Location = new System.Drawing.Point(36, 295);
            this.binValOutput.Name = "binValOutput";
            this.binValOutput.ReadOnly = true;
            this.binValOutput.Size = new System.Drawing.Size(454, 39);
            this.binValOutput.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 653);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(279, 32);
            this.label9.TabIndex = 3;
            this.label9.Text = "Binary Twos Compliment";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 522);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 32);
            this.label8.TabIndex = 2;
            this.label8.Text = "Hex Value";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 391);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 32);
            this.label7.TabIndex = 1;
            this.label7.Text = "Decimal Value";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 260);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 32);
            this.label6.TabIndex = 0;
            this.label6.Text = "Binary Value";
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(36, 142);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(70, 32);
            this.inputLabel.TabIndex = 9;
            this.inputLabel.Text = "Input";
            // 
            // InputBox
            // 
            this.InputBox.Location = new System.Drawing.Point(36, 177);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(454, 39);
            this.InputBox.TabIndex = 10;
            // 
            // hexForceLength
            // 
            this.hexForceLength.Location = new System.Drawing.Point(521, 557);
            this.hexForceLength.Name = "hexForceLength";
            this.hexForceLength.PlaceholderText = "0";
            this.hexForceLength.Size = new System.Drawing.Size(200, 39);
            this.hexForceLength.TabIndex = 11;
            // 
            // binTwoComplForceLength
            // 
            this.binTwoComplForceLength.Location = new System.Drawing.Point(521, 688);
            this.binTwoComplForceLength.Name = "binTwoComplForceLength";
            this.binTwoComplForceLength.PlaceholderText = "0";
            this.binTwoComplForceLength.Size = new System.Drawing.Size(200, 39);
            this.binTwoComplForceLength.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(521, 522);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 32);
            this.label4.TabIndex = 5;
            this.label4.Text = "Force Length";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(521, 653);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 32);
            this.label5.TabIndex = 5;
            this.label5.Text = "Force Length";
            // 
            // statusMessages
            // 
            this.statusMessages.Location = new System.Drawing.Point(820, 189);
            this.statusMessages.Name = "statusMessages";
            this.statusMessages.ReadOnly = true;
            this.statusMessages.Size = new System.Drawing.Size(472, 419);
            this.statusMessages.TabIndex = 13;
            this.statusMessages.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(820, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 32);
            this.label1.TabIndex = 14;
            this.label1.Text = "Messages";
            // 
            // NumWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 827);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusMessages);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.binTwoComplForceLength);
            this.Controls.Add(this.hexForceLength);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.binTwoComplOutput);
            this.Controls.Add(this.hexValOutput);
            this.Controls.Add(this.decValOutput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.binValOutput);
            this.Controls.Add(this.inputBaseDropDown);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.calculateResults);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.binForceLength);
            this.Controls.Add(this.label6);
            this.Name = "NumWindow";
            this.Text = "Number Conversion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox binForceLength;
        private System.Windows.Forms.Button calculateResults;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox inputBaseDropDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox binTwoComplOutput;
        private System.Windows.Forms.TextBox hexValOutput;
        private System.Windows.Forms.TextBox decValOutput;
        private System.Windows.Forms.TextBox binValOutput;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.TextBox hexForceLength;
        private System.Windows.Forms.TextBox binTwoComplForceLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox statusMessages;
        private System.Windows.Forms.Label label1;
    }
}

