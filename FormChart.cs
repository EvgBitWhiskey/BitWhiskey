using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BitWhiskey
{
    public partial class FormChart : Form
    {
        public Form parent;
        public string ticker;
        PriceChart chart;
        Market market;
        FormWindowState LastWindowState = FormWindowState.Normal;

        public FormChart(Market market_)
        {
            InitializeComponent();
            market = market_;
        }

        private void FormChart_Load(object sender, EventArgs e)
        {
            Text = ticker + " Chart";
            Text = "Chart " + market.MarketName() + "  " + ticker;
            LoadTickers();
        }

        private void LoadTickers()
        {
            market.GetTradePairs();

            listBoxTicker.Items.Clear();
            foreach (KeyValuePair<string, TradePair> pair in market.tradePairs.OrderBy(p => p.Key))
            {
                listBoxTicker.Items.Add(pair.Key);
            }

        }

        private void panelChart_Paint(object sender, PaintEventArgs e)
        {
            if (chart == null)
                return;
            chart.ReDrawFull();

        }

        private void panelChart_MouseDown(object sender, MouseEventArgs e)
        {
            if (chart == null)
                return;
            if (e.Button == MouseButtons.Left)
            {
                chart.MouseClick(e.X, e.Y, 1);
            }

        }

        private void panelChart_MouseUp(object sender, MouseEventArgs e)
        {
            if (chart == null)
                return;
            if (e.Button == MouseButtons.Left)
            {
                chart.MouseClick(e.X, e.Y, 0);
            }

        }

        private void panelChart_MouseMove(object sender, MouseEventArgs e)
        {
            if (chart == null)
                return;
            chart.MouseMove(e.X, e.Y);

        }
        private void timerTradeLast_Tick(object sender, EventArgs e)
        {
        }

        private void FormChart_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Activate();
            if (parent.WindowState == FormWindowState.Minimized)
                parent.WindowState = FormWindowState.Normal;
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void listBoxTicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ticker = listBoxTicker.SelectedItem.ToString();
            market.GetPriceHistoryByPeriod(ticker, "Min5", DateTime.Now.AddDays(-6), DateTime.Now.AddYears(10));
            chart = new PriceChart(ticker, Graphics.FromHwnd(panelChart.Handle), 0, 0, panelChart.Width, panelChart.Height);
            chart.SetData(market.priceHistory, ConvertDataType.BAR_5, ConvertDataType.BAR_15);
            chart.ReDrawFull();
            Text = "Chart " + market.MarketName() + "  " + ticker;
        }

        private void buttonLoadDayChart_Click(object sender, EventArgs e)
        {
            if (ticker == null)
                return;

            market.GetPriceHistoryByPeriod(ticker, "Day", DateTime.Now.AddMonths(-16), DateTime.Now.AddYears(10));
            chart = new PriceChart(ticker, Graphics.FromHwnd(panelChart.Handle), 0, 0, panelChart.Width, panelChart.Height);
            chart.SetData(market.priceHistory, ConvertDataType.BAR_DAY, ConvertDataType.BAR_DAY);
            chart.ReDrawFull();
        }

        private void buttonUpdate5M_Click(object sender, EventArgs e)
        {
            if (ticker == null)
                return;
            market.GetPriceHistoryByPeriod(ticker, "Min5", DateTime.Now.AddDays(-3), DateTime.Now.AddYears(10));
            chart = new PriceChart(ticker, Graphics.FromHwnd(panelChart.Handle), 0, 0, panelChart.Width, panelChart.Height);
            chart.SetData(market.priceHistory, ConvertDataType.BAR_5, ConvertDataType.BAR_15);
            chart.ReDrawFull();
        }

        private void FormChart_ResizeEnd(object sender, EventArgs e)
        {
            ResizeChart();
        }
        private void FormChart_Resize(object sender, EventArgs e)
        {
            FormWindowState wstate = this.WindowState;
            if (LastWindowState == FormWindowState.Normal && wstate == FormWindowState.Maximized)
                ResizeChart();
            else if (LastWindowState == FormWindowState.Maximized && wstate == FormWindowState.Normal)
                ResizeChart();
            LastWindowState = wstate;
        }
        private void ResizeChart()
        {
            int neww = Width - listBoxTicker.Width - 20;
            int newh = Height - 40;
            panelChart.Size = new Size(neww, newh - 15);
            listBoxTicker.Size = new Size(listBoxTicker.Size.Width, newh);
            listBoxTicker.Location = new Point(neww + 8, listBoxTicker.Location.Y);
            chart.Resize(Graphics.FromHwnd(panelChart.Handle), 0, 0, panelChart.Width, panelChart.Height);
            chart.ReDrawFull();

        }

    }
}
