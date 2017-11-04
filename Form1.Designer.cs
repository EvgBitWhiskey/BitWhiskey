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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.panelChart = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxMarket = new System.Windows.Forms.ToolStripComboBox();
            this.toolDropDownTickerBtc = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolDropDownTickerUsdt = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButtonChart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 1100;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // panelChart
            // 
            this.panelChart.Location = new System.Drawing.Point(1, 32);
            this.panelChart.Margin = new System.Windows.Forms.Padding(2);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(125, 83);
            this.panelChart.TabIndex = 9;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxMarket,
            this.toolDropDownTickerBtc,
            this.toolDropDownTickerUsdt,
            this.toolStripButtonChart,
            this.toolStripButtonSettings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(431, 25);
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
            this.toolStripComboBoxMarket.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBoxMarket.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxMarket_SelectedIndexChanged);
            // 
            // toolDropDownTickerBtc
            // 
            this.toolDropDownTickerBtc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDropDownTickerBtc.Image = ((System.Drawing.Image)(resources.GetObject("toolDropDownTickerBtc.Image")));
            this.toolDropDownTickerBtc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDropDownTickerBtc.Name = "toolDropDownTickerBtc";
            this.toolDropDownTickerBtc.Size = new System.Drawing.Size(29, 22);
            this.toolDropDownTickerBtc.Text = "Trade BTC";
            this.toolDropDownTickerBtc.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolDropDownTickerBtc_DropDownItemClicked);
            // 
            // toolDropDownTickerUsdt
            // 
            this.toolDropDownTickerUsdt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDropDownTickerUsdt.Image = ((System.Drawing.Image)(resources.GetObject("toolDropDownTickerUsdt.Image")));
            this.toolDropDownTickerUsdt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDropDownTickerUsdt.Name = "toolDropDownTickerUsdt";
            this.toolDropDownTickerUsdt.Size = new System.Drawing.Size(29, 22);
            this.toolDropDownTickerUsdt.Text = "Trade USDT";
            this.toolDropDownTickerUsdt.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolDropDownTickerUsdt_DropDownItemClicked);
            // 
            // toolStripButtonChart
            // 
            this.toolStripButtonChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonChart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonChart.Image")));
            this.toolStripButtonChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChart.Name = "toolStripButtonChart";
            this.toolStripButtonChart.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonChart.Text = "Price Chart";
            this.toolStripButtonChart.Click += new System.EventHandler(this.toolStripButtonChart_Click);
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSettings.Image")));
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSettings.Text = "Settings";
            this.toolStripButtonSettings.Click += new System.EventHandler(this.toolStripButtonSettings_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 275);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panelChart);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Bit Whiskey";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolDropDownTickerBtc;
        private System.Windows.Forms.ToolStripDropDownButton toolDropDownTickerUsdt;
        private System.Windows.Forms.ToolStripButton toolStripButtonChart;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMarket;
        private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
    }
}

