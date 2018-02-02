namespace BitWhiskey
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dgridMarkets = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxMarket = new System.Windows.Forms.ToolStripComboBox();
            this.toolDropDownTickerBtc = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolDropDownTickerUsdt = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolButtonTickerBtc = new System.Windows.Forms.ToolStripButton();
            this.toolButtonTickerUsd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonChart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBalance = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlerts = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelChart = new System.Windows.Forms.Panel();
            this.labelInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgridMarkets)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgridMarkets
            // 
            this.dgridMarkets.AllowUserToAddRows = false;
            this.dgridMarkets.AllowUserToDeleteRows = false;
            this.dgridMarkets.AllowUserToOrderColumns = true;
            this.dgridMarkets.AllowUserToResizeRows = false;
            this.dgridMarkets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgridMarkets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgridMarkets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridMarkets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgridMarkets.Location = new System.Drawing.Point(0, 0);
            this.dgridMarkets.Margin = new System.Windows.Forms.Padding(2);
            this.dgridMarkets.MultiSelect = false;
            this.dgridMarkets.Name = "dgridMarkets";
            this.dgridMarkets.ReadOnly = true;
            this.dgridMarkets.RowHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgridMarkets.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgridMarkets.RowTemplate.Height = 24;
            this.dgridMarkets.RowTemplate.ReadOnly = true;
            this.dgridMarkets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgridMarkets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridMarkets.ShowCellToolTips = false;
            this.dgridMarkets.ShowEditingIcon = false;
            this.dgridMarkets.Size = new System.Drawing.Size(540, 372);
            this.dgridMarkets.TabIndex = 9;
            this.dgridMarkets.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgridMarkets_DataBindingComplete);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxMarket,
            this.toolDropDownTickerBtc,
            this.toolDropDownTickerUsdt,
            this.toolButtonTickerBtc,
            this.toolButtonTickerUsd,
            this.toolStripButtonChart,
            this.toolStripButtonBalance,
            this.toolStripButtonAlerts,
            this.toolStripButtonSettings});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(540, 23);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripComboBoxMarket
            // 
            this.toolStripComboBoxMarket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxMarket.Items.AddRange(new object[] {
            "Poloniex",
            "Bittrex"});
            this.toolStripComboBoxMarket.Name = "toolStripComboBoxMarket";
            this.toolStripComboBoxMarket.Size = new System.Drawing.Size(121, 21);
            this.toolStripComboBoxMarket.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxMarket_SelectedIndexChanged);
            // 
            // toolDropDownTickerBtc
            // 
            this.toolDropDownTickerBtc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDropDownTickerBtc.Image = ((System.Drawing.Image)(resources.GetObject("toolDropDownTickerBtc.Image")));
            this.toolDropDownTickerBtc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDropDownTickerBtc.Name = "toolDropDownTickerBtc";
            this.toolDropDownTickerBtc.Size = new System.Drawing.Size(29, 20);
            this.toolDropDownTickerBtc.Text = "Trade BTC";
            this.toolDropDownTickerBtc.Visible = false;
            this.toolDropDownTickerBtc.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolDropDownTickerBtc_DropDownItemClicked);
            // 
            // toolDropDownTickerUsdt
            // 
            this.toolDropDownTickerUsdt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDropDownTickerUsdt.Image = ((System.Drawing.Image)(resources.GetObject("toolDropDownTickerUsdt.Image")));
            this.toolDropDownTickerUsdt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDropDownTickerUsdt.Name = "toolDropDownTickerUsdt";
            this.toolDropDownTickerUsdt.Size = new System.Drawing.Size(29, 20);
            this.toolDropDownTickerUsdt.Text = "Trade USDT";
            this.toolDropDownTickerUsdt.Visible = false;
            this.toolDropDownTickerUsdt.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolDropDownTickerUsdt_DropDownItemClicked);
            // 
            // toolButtonTickerBtc
            // 
            this.toolButtonTickerBtc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolButtonTickerBtc.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonTickerBtc.Image")));
            this.toolButtonTickerBtc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonTickerBtc.Name = "toolButtonTickerBtc";
            this.toolButtonTickerBtc.Size = new System.Drawing.Size(23, 20);
            this.toolButtonTickerBtc.Text = "Trade BTC";
            this.toolButtonTickerBtc.Click += new System.EventHandler(this.toolButtonTickerBtc_Click);
            // 
            // toolButtonTickerUsd
            // 
            this.toolButtonTickerUsd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolButtonTickerUsd.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonTickerUsd.Image")));
            this.toolButtonTickerUsd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonTickerUsd.Name = "toolButtonTickerUsd";
            this.toolButtonTickerUsd.Size = new System.Drawing.Size(23, 20);
            this.toolButtonTickerUsd.Text = "Trade USD";
            this.toolButtonTickerUsd.Click += new System.EventHandler(this.toolButtonTickerUsd_Click);
            // 
            // toolStripButtonChart
            // 
            this.toolStripButtonChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonChart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonChart.Image")));
            this.toolStripButtonChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChart.Name = "toolStripButtonChart";
            this.toolStripButtonChart.Size = new System.Drawing.Size(23, 20);
            this.toolStripButtonChart.Text = "Price Chart";
            this.toolStripButtonChart.Click += new System.EventHandler(this.toolStripButtonChart_Click);
            // 
            // toolStripButtonBalance
            // 
            this.toolStripButtonBalance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBalance.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBalance.Image")));
            this.toolStripButtonBalance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBalance.Name = "toolStripButtonBalance";
            this.toolStripButtonBalance.Size = new System.Drawing.Size(23, 20);
            this.toolStripButtonBalance.Text = "Balances";
            this.toolStripButtonBalance.Click += new System.EventHandler(this.toolStripButtonBalance_Click);
            // 
            // toolStripButtonAlerts
            // 
            this.toolStripButtonAlerts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlerts.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlerts.Image")));
            this.toolStripButtonAlerts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlerts.Name = "toolStripButtonAlerts";
            this.toolStripButtonAlerts.Size = new System.Drawing.Size(23, 20);
            this.toolStripButtonAlerts.Text = "Alerts";
            this.toolStripButtonAlerts.Click += new System.EventHandler(this.toolStripButtonAlerts_Click);
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSettings.Image")));
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(23, 20);
            this.toolStripButtonSettings.Text = "Settings";
            this.toolStripButtonSettings.Click += new System.EventHandler(this.toolStripButtonSettings_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelInfo);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 45);
            this.panel1.TabIndex = 11;
            // 
            // panelChart
            // 
            this.panelChart.Controls.Add(this.dgridMarkets);
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChart.Location = new System.Drawing.Point(0, 45);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(540, 372);
            this.panelChart.TabIndex = 12;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.ForeColor = System.Drawing.Color.Tomato;
            this.labelInfo.Location = new System.Drawing.Point(44, 25);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(47, 15);
            this.labelInfo.TabIndex = 11;
            this.labelInfo.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 417);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Bit Whiskey";
            ((System.ComponentModel.ISupportInitialize)(this.dgridMarkets)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolDropDownTickerBtc;
        private System.Windows.Forms.ToolStripDropDownButton toolDropDownTickerUsdt;
        private System.Windows.Forms.ToolStripButton toolStripButtonChart;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMarket;
        private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
        private System.Windows.Forms.ToolStripButton toolStripButtonBalance;
        private System.Windows.Forms.DataGridView dgridMarkets;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.ToolStripButton toolStripButtonAlerts;
        private System.Windows.Forms.ToolStripButton toolButtonTickerBtc;
        private System.Windows.Forms.ToolStripButton toolButtonTickerUsd;
        private System.Windows.Forms.Label labelInfo;
    }
}

