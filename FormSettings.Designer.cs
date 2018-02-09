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
            this.checkBoxPoloniexDisabled = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxBittrexDisabled = new System.Windows.Forms.CheckBox();
            this.textBoxBittrexKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxBittrexSecret = new System.Windows.Forms.TextBox();
            this.checkBoxDefLimitTrade = new System.Windows.Forms.CheckBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxYobitDisabled = new System.Windows.Forms.CheckBox();
            this.textBoxYobitKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxYobitSecret = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(187, 381);
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
            this.groupBox1.Controls.Add(this.checkBoxPoloniexDisabled);
            this.groupBox1.Controls.Add(this.textBoxPoloniexKey);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxPoloniexSecret);
            this.groupBox1.Location = new System.Drawing.Point(3, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 93);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Poloniex";
            // 
            // checkBoxPoloniexDisabled
            // 
            this.checkBoxPoloniexDisabled.AutoSize = true;
            this.checkBoxPoloniexDisabled.Location = new System.Drawing.Point(9, 69);
            this.checkBoxPoloniexDisabled.Name = "checkBoxPoloniexDisabled";
            this.checkBoxPoloniexDisabled.Size = new System.Drawing.Size(115, 17);
            this.checkBoxPoloniexDisabled.TabIndex = 8;
            this.checkBoxPoloniexDisabled.Text = "Отключить биржу";
            this.checkBoxPoloniexDisabled.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxBittrexDisabled);
            this.groupBox2.Controls.Add(this.textBoxBittrexKey);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxBittrexSecret);
            this.groupBox2.Location = new System.Drawing.Point(3, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 92);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bittrex";
            // 
            // checkBoxBittrexDisabled
            // 
            this.checkBoxBittrexDisabled.AutoSize = true;
            this.checkBoxBittrexDisabled.Location = new System.Drawing.Point(6, 69);
            this.checkBoxBittrexDisabled.Name = "checkBoxBittrexDisabled";
            this.checkBoxBittrexDisabled.Size = new System.Drawing.Size(115, 17);
            this.checkBoxBittrexDisabled.TabIndex = 8;
            this.checkBoxBittrexDisabled.Text = "Отключить биржу";
            this.checkBoxBittrexDisabled.UseVisualStyleBackColor = true;
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
            this.checkBoxDefLimitTrade.Location = new System.Drawing.Point(7, 350);
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
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelVersion.Location = new System.Drawing.Point(445, 5);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(91, 15);
            this.labelVersion.TabIndex = 11;
            this.labelVersion.Text = "Version 0.4.1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxYobitDisabled);
            this.groupBox3.Controls.Add(this.textBoxYobitKey);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBoxYobitSecret);
            this.groupBox3.Location = new System.Drawing.Point(3, 224);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(527, 96);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Yobit";
            // 
            // checkBoxYobitDisabled
            // 
            this.checkBoxYobitDisabled.AutoSize = true;
            this.checkBoxYobitDisabled.Location = new System.Drawing.Point(9, 69);
            this.checkBoxYobitDisabled.Name = "checkBoxYobitDisabled";
            this.checkBoxYobitDisabled.Size = new System.Drawing.Size(115, 17);
            this.checkBoxYobitDisabled.TabIndex = 7;
            this.checkBoxYobitDisabled.Text = "Отключить биржу";
            this.checkBoxYobitDisabled.UseVisualStyleBackColor = true;
            // 
            // textBoxYobitKey
            // 
            this.textBoxYobitKey.Location = new System.Drawing.Point(86, 17);
            this.textBoxYobitKey.Name = "textBoxYobitKey";
            this.textBoxYobitKey.Size = new System.Drawing.Size(430, 20);
            this.textBoxYobitKey.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "API Key";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "API Secret";
            // 
            // textBoxYobitSecret
            // 
            this.textBoxYobitSecret.Location = new System.Drawing.Point(85, 43);
            this.textBoxYobitSecret.Name = "textBoxYobitSecret";
            this.textBoxYobitSecret.Size = new System.Drawing.Size(431, 20);
            this.textBoxYobitSecret.TabIndex = 5;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 408);
            this.Controls.Add(this.groupBox3);
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
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxYobitKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxYobitSecret;
        private System.Windows.Forms.CheckBox checkBoxYobitDisabled;
        private System.Windows.Forms.CheckBox checkBoxPoloniexDisabled;
        private System.Windows.Forms.CheckBox checkBoxBittrexDisabled;
    }
}