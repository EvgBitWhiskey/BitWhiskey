namespace BitWhiskey
{
    partial class FormAlertAdd
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
            this.listBoxTicker = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxPriceAlert = new System.Windows.Forms.CheckBox();
            this.textBoxPriceAlert = new System.Windows.Forms.TextBox();
            this.labelCurrencyName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDisplayInChart = new System.Windows.Forms.CheckBox();
            this.checkBoxStartBat = new System.Windows.Forms.CheckBox();
            this.checkBoxTrayIcon = new System.Windows.Forms.CheckBox();
            this.checkBoxPlaySound = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxAlertName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPriceCurrent = new System.Windows.Forms.TextBox();
            this.labelCurrencyName2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelAlertText = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxTicker
            // 
            this.listBoxTicker.FormattingEnabled = true;
            this.listBoxTicker.Location = new System.Drawing.Point(3, 28);
            this.listBoxTicker.Name = "listBoxTicker";
            this.listBoxTicker.Size = new System.Drawing.Size(88, 290);
            this.listBoxTicker.TabIndex = 0;
            this.listBoxTicker.SelectedIndexChanged += new System.EventHandler(this.listBoxTicker_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Currency Pair";
            // 
            // checkBoxPriceAlert
            // 
            this.checkBoxPriceAlert.AutoSize = true;
            this.checkBoxPriceAlert.Checked = true;
            this.checkBoxPriceAlert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPriceAlert.Location = new System.Drawing.Point(103, 70);
            this.checkBoxPriceAlert.Name = "checkBoxPriceAlert";
            this.checkBoxPriceAlert.Size = new System.Drawing.Size(74, 17);
            this.checkBoxPriceAlert.TabIndex = 2;
            this.checkBoxPriceAlert.Text = "Alert Price";
            this.checkBoxPriceAlert.UseVisualStyleBackColor = true;
            // 
            // textBoxPriceAlert
            // 
            this.textBoxPriceAlert.Location = new System.Drawing.Point(189, 69);
            this.textBoxPriceAlert.Name = "textBoxPriceAlert";
            this.textBoxPriceAlert.Size = new System.Drawing.Size(100, 20);
            this.textBoxPriceAlert.TabIndex = 3;
            this.textBoxPriceAlert.Text = "0";
            this.textBoxPriceAlert.TextChanged += new System.EventHandler(this.textBoxPriceAlert_TextChanged);
            // 
            // labelCurrencyName
            // 
            this.labelCurrencyName.AutoSize = true;
            this.labelCurrencyName.Location = new System.Drawing.Point(292, 73);
            this.labelCurrencyName.Name = "labelCurrencyName";
            this.labelCurrencyName.Size = new System.Drawing.Size(30, 13);
            this.labelCurrencyName.TabIndex = 4;
            this.labelCurrencyName.Text = "USD";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxDisplayInChart);
            this.groupBox1.Controls.Add(this.checkBoxStartBat);
            this.groupBox1.Controls.Add(this.checkBoxTrayIcon);
            this.groupBox1.Controls.Add(this.checkBoxPlaySound);
            this.groupBox1.Location = new System.Drawing.Point(103, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 192);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // checkBoxDisplayInChart
            // 
            this.checkBoxDisplayInChart.AutoSize = true;
            this.checkBoxDisplayInChart.Enabled = false;
            this.checkBoxDisplayInChart.Location = new System.Drawing.Point(15, 96);
            this.checkBoxDisplayInChart.Name = "checkBoxDisplayInChart";
            this.checkBoxDisplayInChart.Size = new System.Drawing.Size(100, 17);
            this.checkBoxDisplayInChart.TabIndex = 9;
            this.checkBoxDisplayInChart.Text = "Display In Chart";
            this.checkBoxDisplayInChart.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartBat
            // 
            this.checkBoxStartBat.AutoSize = true;
            this.checkBoxStartBat.Enabled = false;
            this.checkBoxStartBat.Location = new System.Drawing.Point(15, 65);
            this.checkBoxStartBat.Name = "checkBoxStartBat";
            this.checkBoxStartBat.Size = new System.Drawing.Size(75, 17);
            this.checkBoxStartBat.TabIndex = 8;
            this.checkBoxStartBat.Text = "Start .BAT";
            this.checkBoxStartBat.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrayIcon
            // 
            this.checkBoxTrayIcon.AutoSize = true;
            this.checkBoxTrayIcon.Enabled = false;
            this.checkBoxTrayIcon.Location = new System.Drawing.Point(15, 42);
            this.checkBoxTrayIcon.Name = "checkBoxTrayIcon";
            this.checkBoxTrayIcon.Size = new System.Drawing.Size(71, 17);
            this.checkBoxTrayIcon.TabIndex = 7;
            this.checkBoxTrayIcon.Text = "Tray Icon";
            this.checkBoxTrayIcon.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlaySound
            // 
            this.checkBoxPlaySound.AutoSize = true;
            this.checkBoxPlaySound.Checked = true;
            this.checkBoxPlaySound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlaySound.Location = new System.Drawing.Point(15, 19);
            this.checkBoxPlaySound.Name = "checkBoxPlaySound";
            this.checkBoxPlaySound.Size = new System.Drawing.Size(80, 17);
            this.checkBoxPlaySound.TabIndex = 6;
            this.checkBoxPlaySound.Text = "Play Sound";
            this.checkBoxPlaySound.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(139, 328);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(249, 328);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxAlertName
            // 
            this.textBoxAlertName.Location = new System.Drawing.Point(189, 22);
            this.textBoxAlertName.Name = "textBoxAlertName";
            this.textBoxAlertName.Size = new System.Drawing.Size(100, 20);
            this.textBoxAlertName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(103, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Alert Name";
            // 
            // textBoxPriceCurrent
            // 
            this.textBoxPriceCurrent.Location = new System.Drawing.Point(189, 46);
            this.textBoxPriceCurrent.Name = "textBoxPriceCurrent";
            this.textBoxPriceCurrent.Size = new System.Drawing.Size(100, 20);
            this.textBoxPriceCurrent.TabIndex = 10;
            this.textBoxPriceCurrent.Text = "0";
            this.textBoxPriceCurrent.TextChanged += new System.EventHandler(this.textBoxPriceCurrent_TextChanged);
            // 
            // labelCurrencyName2
            // 
            this.labelCurrencyName2.AutoSize = true;
            this.labelCurrencyName2.Location = new System.Drawing.Point(293, 50);
            this.labelCurrencyName2.Name = "labelCurrencyName2";
            this.labelCurrencyName2.Size = new System.Drawing.Size(30, 13);
            this.labelCurrencyName2.TabIndex = 11;
            this.labelCurrencyName2.Text = "USD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Current Price";
            // 
            // labelAlertText
            // 
            this.labelAlertText.AutoSize = true;
            this.labelAlertText.Location = new System.Drawing.Point(97, 94);
            this.labelAlertText.Name = "labelAlertText";
            this.labelAlertText.Size = new System.Drawing.Size(31, 13);
            this.labelAlertText.TabIndex = 13;
            this.labelAlertText.Text = "Alert ";
            // 
            // FormAlertAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 357);
            this.Controls.Add(this.labelAlertText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelCurrencyName2);
            this.Controls.Add(this.textBoxPriceCurrent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxAlertName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelCurrencyName);
            this.Controls.Add(this.textBoxPriceAlert);
            this.Controls.Add(this.checkBoxPriceAlert);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxTicker);
            this.Name = "FormAlertAdd";
            this.Text = "Create Alert";
            this.Load += new System.EventHandler(this.FormAlertAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxTicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxPriceAlert;
        private System.Windows.Forms.TextBox textBoxPriceAlert;
        private System.Windows.Forms.Label labelCurrencyName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxDisplayInChart;
        private System.Windows.Forms.CheckBox checkBoxStartBat;
        private System.Windows.Forms.CheckBox checkBoxTrayIcon;
        private System.Windows.Forms.CheckBox checkBoxPlaySound;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxAlertName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPriceCurrent;
        private System.Windows.Forms.Label labelCurrencyName2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelAlertText;
    }
}