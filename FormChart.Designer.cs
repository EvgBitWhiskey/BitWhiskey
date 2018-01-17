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
            this.listBoxTicker = new System.Windows.Forms.ListBox();
            this.buttonLoadDayChart = new System.Windows.Forms.Button();
            this.buttonLoad15MinChart = new System.Windows.Forms.Button();
            this.buttonLoadMonthChart = new System.Windows.Forms.Button();
            this.buttonLoadWeekChart = new System.Windows.Forms.Button();
            this.buttonLoadHourChart = new System.Windows.Forms.Button();
            this.buttonLoad5MinChart = new System.Windows.Forms.Button();
            this.labelTotalPeriod = new System.Windows.Forms.Label();
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PeriodDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Period5DaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Period2WeeksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Period2MonthsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Period6MonthsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.PeriodAllDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxVolume = new System.Windows.Forms.CheckBox();
            this.contextMenuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChart
            // 
            this.panelChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChart.ContextMenuStrip = this.contextMenuStripMain;
            this.panelChart.Location = new System.Drawing.Point(2, 28);
            this.panelChart.Margin = new System.Windows.Forms.Padding(2);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(704, 362);
            this.panelChart.TabIndex = 11;
            this.panelChart.Paint += new System.Windows.Forms.PaintEventHandler(this.panelChart_Paint);
            this.panelChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelChart_MouseDown);
            this.panelChart.MouseEnter += new System.EventHandler(this.panelChart_MouseEnter);
            this.panelChart.MouseHover += new System.EventHandler(this.panelChart_MouseHover);
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
            // buttonLoad5MinChart
            // 
            this.buttonLoad5MinChart.Location = new System.Drawing.Point(213, 2);
            this.buttonLoad5MinChart.Name = "buttonLoad5MinChart";
            this.buttonLoad5MinChart.Size = new System.Drawing.Size(41, 23);
            this.buttonLoad5MinChart.TabIndex = 21;
            this.buttonLoad5MinChart.Text = "5M";
            this.buttonLoad5MinChart.UseVisualStyleBackColor = true;
            this.buttonLoad5MinChart.Click += new System.EventHandler(this.buttonLoad5MinChart_Click);
            // 
            // labelTotalPeriod
            // 
            this.labelTotalPeriod.AutoSize = true;
            this.labelTotalPeriod.Location = new System.Drawing.Point(262, 7);
            this.labelTotalPeriod.Name = "labelTotalPeriod";
            this.labelTotalPeriod.Size = new System.Drawing.Size(77, 13);
            this.labelTotalPeriod.TabIndex = 22;
            this.labelTotalPeriod.Text = "Period: Default";
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PeriodDefaultToolStripMenuItem,
            this.Period5DaysToolStripMenuItem,
            this.Period2WeeksToolStripMenuItem,
            this.Period2MonthsToolStripMenuItem,
            this.Period6MonthsToolStripMenuItem1,
            this.PeriodAllDataToolStripMenuItem});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(119, 136);
            // 
            // PeriodDefaultToolStripMenuItem
            // 
            this.PeriodDefaultToolStripMenuItem.Name = "PeriodDefaultToolStripMenuItem";
            this.PeriodDefaultToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.PeriodDefaultToolStripMenuItem.Text = "Default";
            this.PeriodDefaultToolStripMenuItem.Click += new System.EventHandler(this.PeriodDefaultToolStripMenuItem_Click);
            // 
            // Period5DaysToolStripMenuItem
            // 
            this.Period5DaysToolStripMenuItem.Name = "Period5DaysToolStripMenuItem";
            this.Period5DaysToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.Period5DaysToolStripMenuItem.Text = "5 Days";
            this.Period5DaysToolStripMenuItem.Click += new System.EventHandler(this.Period5DaysToolStripMenuItem_Click);
            // 
            // Period2WeeksToolStripMenuItem
            // 
            this.Period2WeeksToolStripMenuItem.Name = "Period2WeeksToolStripMenuItem";
            this.Period2WeeksToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.Period2WeeksToolStripMenuItem.Text = "2 Weeks";
            this.Period2WeeksToolStripMenuItem.Click += new System.EventHandler(this.Period2WeeksToolStripMenuItem_Click);
            // 
            // Period2MonthsToolStripMenuItem
            // 
            this.Period2MonthsToolStripMenuItem.Name = "Period2MonthsToolStripMenuItem";
            this.Period2MonthsToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.Period2MonthsToolStripMenuItem.Text = "2 Months";
            this.Period2MonthsToolStripMenuItem.Click += new System.EventHandler(this.Period2MonthsToolStripMenuItem_Click);
            // 
            // Period6MonthsToolStripMenuItem1
            // 
            this.Period6MonthsToolStripMenuItem1.Name = "Period6MonthsToolStripMenuItem1";
            this.Period6MonthsToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.Period6MonthsToolStripMenuItem1.Text = "6 Months";
            this.Period6MonthsToolStripMenuItem1.Click += new System.EventHandler(this.Period6MonthsToolStripMenuItem1_Click);
            // 
            // PeriodAllDataToolStripMenuItem
            // 
            this.PeriodAllDataToolStripMenuItem.Name = "PeriodAllDataToolStripMenuItem";
            this.PeriodAllDataToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.PeriodAllDataToolStripMenuItem.Text = "All Data";
            this.PeriodAllDataToolStripMenuItem.Click += new System.EventHandler(this.PeriodAllDataToolStripMenuItem_Click);
            // 
            // checkBoxVolume
            // 
            this.checkBoxVolume.AutoSize = true;
            this.checkBoxVolume.Checked = true;
            this.checkBoxVolume.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxVolume.Location = new System.Drawing.Point(369, 6);
            this.checkBoxVolume.Name = "checkBoxVolume";
            this.checkBoxVolume.Size = new System.Drawing.Size(61, 17);
            this.checkBoxVolume.TabIndex = 24;
            this.checkBoxVolume.Text = "Volume";
            this.checkBoxVolume.UseVisualStyleBackColor = true;
            this.checkBoxVolume.CheckedChanged += new System.EventHandler(this.checkBoxVolume_CheckedChanged);
            // 
            // FormChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 406);
            this.Controls.Add(this.checkBoxVolume);
            this.Controls.Add(this.labelTotalPeriod);
            this.Controls.Add(this.buttonLoad5MinChart);
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
            this.contextMenuStripMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.ListBox listBoxTicker;
        private System.Windows.Forms.Button buttonLoadDayChart;
        private System.Windows.Forms.Button buttonLoad15MinChart;
        private System.Windows.Forms.Button buttonLoadMonthChart;
        private System.Windows.Forms.Button buttonLoadWeekChart;
        private System.Windows.Forms.Button buttonLoadHourChart;
        private System.Windows.Forms.Button buttonLoad5MinChart;
        private System.Windows.Forms.Label labelTotalPeriod;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem PeriodDefaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Period5DaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Period2WeeksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Period2MonthsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Period6MonthsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem PeriodAllDataToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxVolume;
    }
}