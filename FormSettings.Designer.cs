namespace BitWhiskey
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPoloniexKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPoloniexSecret = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxBittrexKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxBittrexSecret = new System.Windows.Forms.TextBox();
            this.checkBoxDefLimitTrade = new System.Windows.Forms.CheckBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(187, 299);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxProfile
            // 
            this.comboBoxProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfile.FormattingEnabled = true;
            this.comboBoxProfile.Location = new System.Drawing.Point(82, 4);
            this.comboBoxProfile.Name = "comboBoxProfile";
            this.comboBoxProfile.Size = new System.Drawing.Size(272, 21);
            this.comboBoxProfile.TabIndex = 1;
            this.comboBoxProfile.SelectedIndexChanged += new System.EventHandler(this.comboBoxProfile_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Profile";
            // 
            // textBoxPoloniexKey
            // 
            this.textBoxPoloniexKey.Location = new System.Drawing.Point(86, 17);
            this.textBoxPoloniexKey.Name = "textBoxPoloniexKey";
            this.textBoxPoloniexKey.Size = new System.Drawing.Size(430, 20);
            this.textBoxPoloniexKey.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "API Key";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "API Secret";
            // 
            // textBoxPoloniexSecret
            // 
            this.textBoxPoloniexSecret.Location = new System.Drawing.Point(85, 43);
            this.textBoxPoloniexSecret.Name = "textBoxPoloniexSecret";
            this.textBoxPoloniexSecret.Size = new System.Drawing.Size(431, 20);
            this.textBoxPoloniexSecret.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxPoloniexKey);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxPoloniexSecret);
            this.groupBox1.Location = new System.Drawing.Point(3, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 70);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Poloniex";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxBittrexKey);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxBittrexSecret);
            this.groupBox2.Location = new System.Drawing.Point(3, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 70);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bittrex";
            // 
            // textBoxBittrexKey
            // 
            this.textBoxBittrexKey.Location = new System.Drawing.Point(86, 17);
            this.textBoxBittrexKey.Name = "textBoxBittrexKey";
            this.textBoxBittrexKey.Size = new System.Drawing.Size(430, 20);
            this.textBoxBittrexKey.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "API Key";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "API Secret";
            // 
            // textBoxBittrexSecret
            // 
            this.textBoxBittrexSecret.Location = new System.Drawing.Point(85, 43);
            this.textBoxBittrexSecret.Name = "textBoxBittrexSecret";
            this.textBoxBittrexSecret.Size = new System.Drawing.Size(431, 20);
            this.textBoxBittrexSecret.TabIndex = 5;
            // 
            // checkBoxDefLimitTrade
            // 
            this.checkBoxDefLimitTrade.AutoSize = true;
            this.checkBoxDefLimitTrade.Checked = true;
            this.checkBoxDefLimitTrade.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDefLimitTrade.Location = new System.Drawing.Point(7, 185);
            this.checkBoxDefLimitTrade.Name = "checkBoxDefLimitTrade";
            this.checkBoxDefLimitTrade.Size = new System.Drawing.Size(208, 17);
            this.checkBoxDefLimitTrade.TabIndex = 10;
            this.checkBoxDefLimitTrade.Text = "Default Limit Orders (No auto Buy\\Sell)";
            this.checkBoxDefLimitTrade.UseVisualStyleBackColor = true;
            this.checkBoxDefLimitTrade.Visible = false;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(470, 4);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(60, 13);
            this.labelVersion.TabIndex = 11;
            this.labelVersion.Text = "Version 0.2";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 341);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.checkBoxDefLimitTrade);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxProfile);
            this.Controls.Add(this.buttonSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSettings_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPoloniexKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPoloniexSecret;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxBittrexKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxBittrexSecret;
        private System.Windows.Forms.CheckBox checkBoxDefLimitTrade;
        private System.Windows.Forms.Label labelVersion;
    }
}