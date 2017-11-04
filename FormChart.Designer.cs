namespace BitWhiskey
{
    partial class FormChart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChart));
            this.panelChart = new System.Windows.Forms.Panel();
            this.timerTradeLast = new System.Windows.Forms.Timer(this.components);
            this.listBoxTicker = new System.Windows.Forms.ListBox();
            this.buttonLoadDayChart = new System.Windows.Forms.Button();
            this.buttonUpdate5M = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelChart
            // 
            this.panelChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChart.Location = new System.Drawing.Point(2, 28);
            this.panelChart.Margin = new System.Windows.Forms.Padding(2);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(704, 374);
            this.panelChart.TabIndex = 11;
            this.panelChart.Paint += new System.Windows.Forms.PaintEventHandler(this.panelChart_Paint);
            this.panelChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelChart_MouseDown);
            this.panelChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelChart_MouseMove);
            this.panelChart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelChart_MouseUp);
            // 
            // timerTradeLast
            // 
            this.timerTradeLast.Interval = 8000;
            this.timerTradeLast.Tick += new System.EventHandler(this.timerTradeLast_Tick);
            // 
            // listBoxTicker
            // 
            this.listBoxTicker.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listBoxTicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxTicker.FormattingEnabled = true;
            this.listBoxTicker.ItemHeight = 12;
            this.listBoxTicker.Location = new System.Drawing.Point(707, 1);
            this.listBoxTicker.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxTicker.Name = "listBoxTicker";
            this.listBoxTicker.Size = new System.Drawing.Size(77, 400);
            this.listBoxTicker.TabIndex = 12;
            this.listBoxTicker.SelectedIndexChanged += new System.EventHandler(this.listBoxTicker_SelectedIndexChanged);
            // 
            // buttonLoadDayChart
            // 
            this.buttonLoadDayChart.Location = new System.Drawing.Point(4, 2);
            this.buttonLoadDayChart.Name = "buttonLoadDayChart";
            this.buttonLoadDayChart.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadDayChart.TabIndex = 13;
            this.buttonLoadDayChart.Text = "Day Chart";
            this.buttonLoadDayChart.UseVisualStyleBackColor = true;
            this.buttonLoadDayChart.Click += new System.EventHandler(this.buttonLoadDayChart_Click);
            // 
            // buttonUpdate5M
            // 
            this.buttonUpdate5M.Location = new System.Drawing.Point(100, 2);
            this.buttonUpdate5M.Name = "buttonUpdate5M";
            this.buttonUpdate5M.Size = new System.Drawing.Size(76, 23);
            this.buttonUpdate5M.TabIndex = 14;
            this.buttonUpdate5M.Text = "Update5M";
            this.buttonUpdate5M.UseVisualStyleBackColor = true;
            this.buttonUpdate5M.Click += new System.EventHandler(this.buttonUpdate5M_Click);
            // 
            // FormChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 406);
            this.Controls.Add(this.buttonUpdate5M);
            this.Controls.Add(this.buttonLoadDayChart);
            this.Controls.Add(this.listBoxTicker);
            this.Controls.Add(this.panelChart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormChart";
            this.Text = "Chart";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormChart_FormClosed);
            this.Load += new System.EventHandler(this.FormChart_Load);
            this.ResizeEnd += new System.EventHandler(this.FormChart_ResizeEnd);
            this.Resize += new System.EventHandler(this.FormChart_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Timer timerTradeLast;
        private System.Windows.Forms.ListBox listBoxTicker;
        private System.Windows.Forms.Button buttonLoadDayChart;
        private System.Windows.Forms.Button buttonUpdate5M;
    }
}