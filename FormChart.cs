using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace BitWhiskey
{
    public partial class FormChart : Form
    {
        public Form parent;
        public string ticker;
        PriceChart chart;
        public FormTradeLogic tradeLogic;
        Market market;
        FormWindowState LastWindowState = FormWindowState.Normal;
        private AppTimer timerLastPrice;
        private bool loadRequeststarted =false;
        private ConvertDataType convertFromPeriod;
        private ConvertDataType viewResultPeriod;

        public FormChart(Market market_)
        {
            InitializeComponent();
            market = market_;
            ticker = "BTC_BTC";
            tradeLogic = new FormTradeLogic(ticker, market);
            timerLastPrice = new AppTimer(9000, UpdatePrice_Tick, this);
        }
        private void FormChart_Load(object sender, EventArgs e)
        {
            Text = "Chart " + market.MarketName();
            LoadTickers();
        }

        private void LoadTickers()
        {
            listBoxTicker.Items.Clear();
            tradeLogic.GetTradePairs(LoadTickers_UIResultHandler);
        }
        public void LoadTickers_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (Helper.IsResultHasErrors(resultResponse))
                return;
            Dictionary<string, TradePair> tradePairs = (Dictionary<string, TradePair>)resultResponse.items[0].result.resultData;

            foreach (KeyValuePair<string, TradePair> pair in tradePairs.OrderBy(p => p.Key))
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
        private void FormChart_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerLastPrice.Stop();
            if (chart != null)
            {
                chart.Dispose();
                chart = null;
            }
            parent.Activate();
            if (parent.WindowState == FormWindowState.Minimized)
                parent.WindowState = FormWindowState.Normal;
        }

        private void listBoxTicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerLastPrice.Stop();
            chart = null;
            ticker = listBoxTicker.SelectedItem.ToString();
            tradeLogic = new FormTradeLogic(ticker, market);
            tradeLogic.GetPriceHistoryByPeriod("Min5", DateTime.Now.AddDays(-6), DateTime.Now.AddYears(10), listBoxTicker_SelectedIndexChanged_UIResultHandler);
        }
        public void listBoxTicker_SelectedIndexChanged_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (Helper.IsResultHasErrors(resultResponse))
                return;
            Dictionary<int, PriceCandle> priceHistory = (Dictionary<int, PriceCandle>)resultResponse.items[0].result.resultData;

            chart = new PriceChart(ticker, Graphics.FromHwnd(panelChart.Handle), 0, 0, panelChart.Width, panelChart.Height);
            chart.SetData(priceHistory, ConvertDataType.BAR_5, ConvertDataType.BAR_15);
            chart.ReDrawFull();
            Text = "Chart " + market.MarketName() + "  " + ticker;

            timerLastPrice.Start();
        }
        private void UpdatePrice_Tick(object sender, ElapsedEventArgs e)
        {
            timerLastPrice.Stop();
            tradeLogic.GetLastMarketPrice(LastMarketPrice_UIResultHandler);
        }
        public void LastMarketPrice_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (Helper.IsResultHasErrors(resultResponse))
                return;
            TradeLast tradelast = (TradeLast)resultResponse.items[0].result.resultData;
            if (chart == null)
                return;
            chart.UpdatePrice(tradelast.last);
            chart.ReDrawFull();
            timerLastPrice.Start();
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

            int neww = ClientRectangle.Width - listBoxTicker.Width - 20;
            int newh = ClientRectangle.Height - 40;
            panelChart.Size = new Size(neww, newh - 15);
            listBoxTicker.Size = new Size(listBoxTicker.Size.Width, newh);
            listBoxTicker.Location = new Point(neww + 8, listBoxTicker.Location.Y);

            if (chart == null || ticker == null)
                return;
            chart.Resize(Graphics.FromHwnd(panelChart.Handle), 0, 0, panelChart.Width, panelChart.Height);
            chart.ReDrawFull();

        }

        private void buttonLoadMonthChart_Click(object sender, EventArgs e)
        {
            if (loadRequeststarted)
                return;
            if (chart == null || ticker == null)
                return;
            timerLastPrice.Stop();
            convertFromPeriod = ConvertDataType.BAR_DAY;
            viewResultPeriod = ConvertDataType.BAR_MONTH;
            loadRequeststarted = true;
            tradeLogic.GetPriceHistoryByPeriod("Day", DateTime.Now.AddMonths(-20), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);
        }

        private void buttonLoadWeekChart_Click(object sender, EventArgs e)
        {
            if (loadRequeststarted)
                return;
            if (chart == null || ticker == null)
                return;
            timerLastPrice.Stop();
            convertFromPeriod = ConvertDataType.BAR_DAY;
            viewResultPeriod = ConvertDataType.BAR_WEEK;
            loadRequeststarted = true;
            tradeLogic.GetPriceHistoryByPeriod("Day", DateTime.Now.AddMonths(-20), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);

        }
        private void buttonLoadDayChart_Click(object sender, EventArgs e)
        {
            if (loadRequeststarted)
                return;
            if (chart == null || ticker == null)
                return;
            timerLastPrice.Stop();
            convertFromPeriod= ConvertDataType.BAR_DAY;
            viewResultPeriod = ConvertDataType.BAR_DAY;
            loadRequeststarted = true;
            tradeLogic.GetPriceHistoryByPeriod("Day", DateTime.Now.AddMonths(-20), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);
        }

        private void buttonLoadHourChart_Click(object sender, EventArgs e)
        {
            if (loadRequeststarted)
                return;
            if (chart == null || ticker == null)
                return;
            timerLastPrice.Stop();
            convertFromPeriod = ConvertDataType.BAR_5;
            viewResultPeriod = ConvertDataType.BAR_HOUR;
            loadRequeststarted = true;
            tradeLogic.GetPriceHistoryByPeriod("Min5", DateTime.Now.AddDays(-5), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);

        }

        private void buttonLoad15MinChart_Click(object sender, EventArgs e)
        {
            if (loadRequeststarted)
                return;
            if (chart == null || ticker == null)
                return;
            timerLastPrice.Stop();
            convertFromPeriod = ConvertDataType.BAR_5;
            viewResultPeriod = ConvertDataType.BAR_15;
            loadRequeststarted = true;
            tradeLogic.GetPriceHistoryByPeriod("Min5", DateTime.Now.AddDays(-4), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);

        }
        public void buttonLoadChartData_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (Helper.IsResultHasErrors(resultResponse))
                return;
            Dictionary<int, PriceCandle> priceHistory = (Dictionary<int, PriceCandle>)resultResponse.items[0].result.resultData;
            if (chart == null || ticker == null)
                return;

            chart = new PriceChart(ticker, Graphics.FromHwnd(panelChart.Handle), 0, 0, panelChart.Width, panelChart.Height);
            chart.SetData(priceHistory, convertFromPeriod, viewResultPeriod);
            chart.ReDrawFull();
            loadRequeststarted = false;
            timerLastPrice.Start();
        }


    }
}
