namespace BitWhiskey
{
    partial class FormBalance
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBalance));
            this.panelBalance = new System.Windows.Forms.Panel();
            this.labelBalanceUsd = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelBalanceBaseValue = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonUpdateBalances = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxExchange = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgridBalance = new System.Windows.Forms.DataGridView();
            this.panelBalance.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridBalance)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBalance
            // 
            this.panelBalance.Controls.Add(this.labelBalanceUsd);
            this.panelBalance.Controls.Add(this.label7);
            this.panelBalance.Controls.Add(this.labelBalanceBaseValue);
            this.panelBalance.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBalance.Location = new System.Drawing.Point(0, 0);
            this.panelBalance.Margin = new System.Windows.Forms.Padding(2);
            this.panelBalance.Name = "panelBalance";
            this.panelBalance.Size = new System.Drawing.Size(425, 32);
            this.panelBalance.TabIndex = 1;
            // 
            // labelBalanceUsd
            // 
            this.labelBalanceUsd.AutoSize = true;
            this.labelBalanceUsd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBalanceUsd.ForeColor = System.Drawing.Color.Green;
            this.labelBalanceUsd.Location = new System.Drawing.Point(226, 3);
            this.labelBalanceUsd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBalanceUsd.Name = "labelBalanceUsd";
            this.labelBalanceUsd.Size = new System.Drawing.Size(18, 20);
            this.labelBalanceUsd.TabIndex = 7;
            this.labelBalanceUsd.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(1, 4);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Total Balance BTC";
            // 
            // labelBalanceBaseValue
            // 
            this.labelBalanceBaseValue.AutoSize = true;
            this.labelBalanceBaseValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBalanceBaseValue.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelBalanceBaseValue.Location = new System.Drawing.Point(127, 3);
            this.labelBalanceBaseValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBalanceBaseValue.Name = "labelBalanceBaseValue";
            this.labelBalanceBaseValue.Size = new System.Drawing.Size(94, 20);
            this.labelBalanceBaseValue.TabIndex = 1;
            this.labelBalanceBaseValue.Text = "0.00000000";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonUpdateBalances);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBoxExchange);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 27);
            this.panel1.TabIndex = 8;
            // 
            // buttonUpdateBalances
            // 
            this.buttonUpdateBalances.Location = new System.Drawing.Point(232, 3);
            this.buttonUpdateBalances.Name = "buttonUpdateBalances";
            this.buttonUpdateBalances.Size = new System.Drawing.Size(75, 21);
            this.buttonUpdateBalances.TabIndex = 2;
            this.buttonUpdateBalances.Text = "Update";
            this.buttonUpdateBalances.UseVisualStyleBackColor = true;
            this.buttonUpdateBalances.Click += new System.EventHandler(this.buttonUpdateBalances_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Exchange";
            // 
            // comboBoxExchange
            // 
            this.comboBoxExchange.FormattingEnabled = true;
            this.comboBoxExchange.Location = new System.Drawing.Point(64, 2);
            this.comboBoxExchange.Name = "comboBoxExchange";
            this.comboBoxExchange.Size = new System.Drawing.Size(155, 21);
            this.comboBoxExchange.TabIndex = 0;
            this.comboBoxExchange.SelectedIndexChanged += new System.EventHandler(this.comboBoxExchange_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgridBalance);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(425, 402);
            this.panel2.TabIndex = 9;
            // 
            // dgridBalance
            // 
            this.dgridBalance.AllowUserToAddRows = false;
            this.dgridBalance.AllowUserToDeleteRows = false;
            this.dgridBalance.AllowUserToOrderColumns = true;
            this.dgridBalance.AllowUserToResizeRows = false;
            this.dgridBalance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgridBalance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgridBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgridBalance.Location = new System.Drawing.Point(0, 0);
            this.dgridBalance.Margin = new System.Windows.Forms.Padding(2);
            this.dgridBalance.MultiSelect = false;
            this.dgridBalance.Name = "dgridBalance";
            this.dgridBalance.ReadOnly = true;
            this.dgridBalance.RowHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgridBalance.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgridBalance.RowTemplate.Height = 24;
            this.dgridBalance.RowTemplate.ReadOnly = true;
            this.dgridBalance.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgridBalance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridBalance.ShowCellToolTips = false;
            this.dgridBalance.ShowEditingIcon = false;
            this.dgridBalance.Size = new System.Drawing.Size(425, 402);
            this.dgridBalance.TabIndex = 9;
            // 
            // FormBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 461);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelBalance);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBalance";
            this.Text = "Balances";
            this.panelBalance.ResumeLayout(false);
            this.panelBalance.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgridBalance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBalance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelBalanceBaseValue;
        private System.Windows.Forms.Label labelBalanceUsd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxExchange;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgridBalance;
        private System.Windows.Forms.Button buttonUpdateBalances;
    }
}