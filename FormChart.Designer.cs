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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChart));
            this.panelChart = new System.Windows.Forms.Panel();
            this.listBoxTicker = new System.Windows.Forms.ListBox();
            this.buttonLoadDayChart = new System.Windows.Forms.Button();
            this.buttonLoad15MinChart = new System.Windows.Forms.Button();
            this.buttonLoadMonthChart = new System.Windows.Forms.Button();
            this.buttonLoadWeekChart = new System.Windows.Forms.Button();
            this.buttonLoadHourChart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelChart
            // 
            this.panelChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChart.Location = new System.Drawing.Point(2, 28);
            this.panelChart.Margin = new System.Windows.Forms.Padding(2);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(704, 362);
            this.panelChart.TabIndex = 11;
            this.panelChart.Paint += new System.Windows.Forms.PaintEventHandler(this.panelChart_Paint);
            this.panelChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelChart_MouseDown);
            this.panelChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelChart_MouseMove);
            this.panelChart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelChart_MouseUp);
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
            this.buttonLoadDayChart.Location = new System.Drawing.Point(88, 2);
            this.buttonLoadDayChart.Name = "buttonLoadDayChart";
            this.buttonLoadDayChart.Size = new System.Drawing.Size(40, 23);
            this.buttonLoadDayChart.TabIndex = 13;
            this.buttonLoadDayChart.Text = "D";
            this.buttonLoadDayChart.UseVisualStyleBackColor = true;
            this.buttonLoadDayChart.Click += new System.EventHandler(this.buttonLoadDayChart_Click);
            // 
            // buttonLoad15MinChart
            // 
            this.buttonLoad15MinChart.Location = new System.Drawing.Point(170, 2);
            this.buttonLoad15MinChart.Name = "buttonLoad15MinChart";
            this.buttonLoad15MinChart.Size = new System.Drawing.Size(41, 23);
            this.buttonLoad15MinChart.TabIndex = 14;
            this.buttonLoad15MinChart.Text = "15M";
            this.buttonLoad15MinChart.UseVisualStyleBackColor = true;
            this.buttonLoad15MinChart.Click += new System.EventHandler(this.buttonLoad15MinChart_Click);
            // 
            // buttonLoadMonthChart
            // 
            this.buttonLoadMonthChart.Location = new System.Drawing.Point(2, 2);
            this.buttonLoadMonthChart.Name = "buttonLoadMonthChart";
            this.buttonLoadMonthChart.Size = new System.Drawing.Size(40, 23);
            this.buttonLoadMonthChart.TabIndex = 16;
            this.buttonLoadMonthChart.Text = "M";
            this.buttonLoadMonthChart.UseVisualStyleBackColor = true;
            this.buttonLoadMonthChart.Click += new System.EventHandler(this.buttonLoadMonthChart_Click);
            // 
            // buttonLoadWeekChart
            // 
            this.buttonLoadWeekChart.Location = new System.Drawing.Point(45, 2);
            this.buttonLoadWeekChart.Name = "buttonLoadWeekChart";
            this.buttonLoadWeekChart.Size = new System.Drawing.Size(40, 23);
            this.buttonLoadWeekChart.TabIndex = 17;
            this.buttonLoadWeekChart.Text = "W";
            this.buttonLoadWeekChart.UseVisualStyleBackColor = true;
            this.buttonLoadWeekChart.Click += new System.EventHandler(this.buttonLoadWeekChart_Click);
            // 
            // buttonLoadHourChart
            // 
            this.buttonLoadHourChart.Location = new System.Drawing.Point(131, 2);
            this.buttonLoadHourChart.Name = "buttonLoadHourChart";
            this.buttonLoadHourChart.Size = new System.Drawing.Size(36, 23);
            this.buttonLoadHourChart.TabIndex = 18;
            this.buttonLoadHourChart.Text = "H";
            this.buttonLoadHourChart.UseVisualStyleBackColor = true;
            this.buttonLoadHourChart.Click += new System.EventHandler(this.buttonLoadHourChart_Click);
            // 
            // FormChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 406);
            this.Controls.Add(this.buttonLoadHourChart);
            this.Controls.Add(this.buttonLoadWeekChart);
            this.Controls.Add(this.buttonLoadMonthChart);
            this.Controls.Add(this.buttonLoad15MinChart);
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
        private System.Windows.Forms.ListBox listBoxTicker;
        private System.Windows.Forms.Button buttonLoadDayChart;
        private System.Windows.Forms.Button buttonLoad15MinChart;
        private System.Windows.Forms.Button buttonLoadMonthChart;
        private System.Windows.Forms.Button buttonLoadWeekChart;
        private System.Windows.Forms.Button buttonLoadHourChart;
    }
}