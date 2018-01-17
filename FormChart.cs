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
        public enum TotalPeriod
        {
            Default=0,
            Days5,
            Week2,
            Month2,
            Month6,
            All
        };

        public TotalPeriod totalPeriod;

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
            panelChart.MouseWheel += panelChart_MouseWheel;
            market = market_;
            ticker = "BTC_BTC";
            totalPeriod = TotalPeriod.Default;
            PrintTotalPeriod();
            tradeLogic = new FormTradeLogic(ticker, market);
            timerLastPrice = new AppTimer(7000, UpdatePrice_Tick, this);
        }
        public FormChart(Market market_,string ticker_)
        {
            InitializeComponent();
            panelChart.MouseWheel += panelChart_MouseWheel;
            market = market_;
            ticker = ticker_;
            totalPeriod = TotalPeriod.Default;
            PrintTotalPeriod();
            tradeLogic = new FormTradeLogic(ticker, market);
            timerLastPrice = new AppTimer(7000, UpdatePrice_Tick, this);

        }
        private void FormChart_Load(object sender, EventArgs e)
        {
            Text = "Chart " + market.MarketName();
            LoadTickers();

            // form created with some ticker
            if (ticker != "BTC_BTC")
            {
                timerLastPrice.Stop();
                chart = null;
                tradeLogic = new FormTradeLogic(ticker, market);
                int dayCount = 6;
                if (totalPeriod != TotalPeriod.Default)
                    dayCount = GetTotalPeriodDayCount();
                tradeLogic.GetPriceHistoryByPeriod("Min5", DateTime.Now.AddDays(-dayCount), DateTime.Now.AddYears(10), listBoxTicker_SelectedIndexChanged_UIResultHandler);
                totalPeriod = TotalPeriod.Default;
                PrintTotalPeriod();
            }
        }
        private void PrintTotalPeriod()
        {
            ToolStripMenuItem item;
            for (int n = 0; n < contextMenuStripMain.Items.Count; n++)
            {
                item = (ToolStripMenuItem)contextMenuStripMain.Items[n];
                item.Checked = false;
            }
            switch(totalPeriod)
            {
                case TotalPeriod.Default:
                    item = (ToolStripMenuItem)contextMenuStripMain.Items[0];
                    item.Checked = true;
                    labelTotalPeriod.Text = "Period: Default";
                    break;
                case TotalPeriod.Days5:
                    item = (ToolStripMenuItem)contextMenuStripMain.Items[1];
                    item.Checked = true;
                    labelTotalPeriod.Text = "Period: 5 Days";
                    break;
                case TotalPeriod.Week2:
                    item = (ToolStripMenuItem)contextMenuStripMain.Items[2];
                    item.Checked = true;
                    labelTotalPeriod.Text = "Period: 2 Weeks";
                    break;
                case TotalPeriod.Month2:
                    item = (ToolStripMenuItem)contextMenuStripMain.Items[3];
                    item.Checked = true;
                    labelTotalPeriod.Text = "Period: 2 Months";
                    break;
                case TotalPeriod.Month6:
                    item = (ToolStripMenuItem)contextMenuStripMain.Items[4];
                    item.Checked = true;
                    labelTotalPeriod.Text = "Period: 6 Months";
                    break;
                case TotalPeriod.All:
                    item = (ToolStripMenuItem)contextMenuStripMain.Items[5];
                    item.Checked = true;
                    labelTotalPeriod.Text = "Period: All Data";
                    break;
            }

        }
        private int GetTotalPeriodDayCount()
        {
            switch (totalPeriod)
            {
                case TotalPeriod.Default:
                    break;
                case TotalPeriod.Days5:
                    return 5;
                case TotalPeriod.Week2:
                    return 14;
                case TotalPeriod.Month2:
                    return 62;
                case TotalPeriod.Month6:
                    return 6*31;
                case TotalPeriod.All:
                    return 31*12*7;
            }

            return 0;
        }
        private void LoadTickers()
        {
            listBoxTicker.Items.Clear();
            tradeLogic.GetTradePairs(LoadTickers_UIResultHandler);
        }
        public void LoadTickers_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
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
        private void panelChart_MouseEnter(object sender, EventArgs e)
        {

        }
        private void panelChart_MouseWheel(object sender, MouseEventArgs e)
        {
            if (chart == null || ticker == null)
                return;

            if (e.Delta > 0)
            {
                chart.Zoom(true);
            }
            else
            {
                chart.Zoom(false);
            }
            chart.ReDrawFull();

        }

        private void panelChart_MouseHover(object sender, EventArgs e)
        {
            panelChart.Focus();
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
            int dayCount = 6;
            if (totalPeriod != TotalPeriod.Default)
                dayCount = GetTotalPeriodDayCount();
            tradeLogic.GetPriceHistoryByPeriod("Min5", DateTime.Now.AddDays(-dayCount), DateTime.Now.AddYears(10), listBoxTicker_SelectedIndexChanged_UIResultHandler);
            totalPeriod = TotalPeriod.Default;
            PrintTotalPeriod();
        }
        public void listBoxTicker_SelectedIndexChanged_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            Dictionary<int, PriceCandle> priceHistory = (Dictionary<int, PriceCandle>)resultResponse.items[0].result.resultData;

            chart = new PriceChart(ticker, Graphics.FromHwnd(panelChart.Handle), 0, 0, panelChart.Width, panelChart.Height);
            chart.SetData(priceHistory, ConvertDataType.BAR_5, ConvertDataType.BAR_15);
            chart.ShowVolume(checkBoxVolume.Checked);
            chart.ClearMouseSelection();
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
            if (RequestManager.IsResultHasErrors(resultResponse))
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
        /*
        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            if (chart == null || ticker == null)
                return;
            chart.Zoom(true);
            chart.ReDrawFull();
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            if (chart == null || ticker == null)
                return;
            chart.Zoom(false);
            chart.ReDrawFull();

        }
        */
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
            int dayCount = 31*24;
            if (totalPeriod != TotalPeriod.Default)
                dayCount = GetTotalPeriodDayCount();
            tradeLogic.GetPriceHistoryByPeriod("Day", DateTime.Now.AddDays(-dayCount), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);
            totalPeriod = TotalPeriod.Default;
            PrintTotalPeriod();
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
            int dayCount = 31 * 24;
            if (totalPeriod != TotalPeriod.Default)
                dayCount = GetTotalPeriodDayCount();
            tradeLogic.GetPriceHistoryByPeriod("Day", DateTime.Now.AddDays(-dayCount), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);
            totalPeriod = TotalPeriod.Default;
            PrintTotalPeriod();
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
            int dayCount = 31 * 24;
            if (totalPeriod != TotalPeriod.Default)
                dayCount = GetTotalPeriodDayCount();
            tradeLogic.GetPriceHistoryByPeriod("Day", DateTime.Now.AddDays(-dayCount), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);
            totalPeriod = TotalPeriod.Default;
            PrintTotalPeriod();
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
            int dayCount = 5;
            if (totalPeriod != TotalPeriod.Default)
                dayCount = GetTotalPeriodDayCount();
            tradeLogic.GetPriceHistoryByPeriod("Min5", DateTime.Now.AddDays(-dayCount), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);
            totalPeriod = TotalPeriod.Default;
            PrintTotalPeriod();
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
            int dayCount = 4;
            if (totalPeriod != TotalPeriod.Default)
                dayCount = GetTotalPeriodDayCount();
            tradeLogic.GetPriceHistoryByPeriod("Min5", DateTime.Now.AddDays(-dayCount), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);
            totalPeriod = TotalPeriod.Default;
            PrintTotalPeriod();
        }
        private void buttonLoad5MinChart_Click(object sender, EventArgs e)
        {
            if (loadRequeststarted)
                return;
            if (chart == null || ticker == null)
                return;
            timerLastPrice.Stop();
            convertFromPeriod = ConvertDataType.BAR_5;
            viewResultPeriod = ConvertDataType.BAR_5;
            loadRequeststarted = true;
            int dayCount = 2;
            if (totalPeriod != TotalPeriod.Default)
                dayCount = GetTotalPeriodDayCount();
            tradeLogic.GetPriceHistoryByPeriod("Min5", DateTime.Now.AddDays(-dayCount), DateTime.Now.AddYears(10), buttonLoadChartData_UIResultHandler);
            totalPeriod = TotalPeriod.Default;
            PrintTotalPeriod();
        }
        public void buttonLoadChartData_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            Dictionary<int, PriceCandle> priceHistory = (Dictionary<int, PriceCandle>)resultResponse.items[0].result.resultData;
            if (chart == null || ticker == null)
                return;

            chart = new PriceChart(ticker, Graphics.FromHwnd(panelChart.Handle), 0, 0, panelChart.Width, panelChart.Height);
            chart.SetData(priceHistory, convertFromPeriod, viewResultPeriod);
            chart.ShowVolume(checkBoxVolume.Checked);
            chart.ReDrawFull();
            loadRequeststarted = false;
            timerLastPrice.Start();
        }

        private void PeriodDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            totalPeriod = TotalPeriod.Default;
            PrintTotalPeriod();
        }

        private void Period5DaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            totalPeriod = TotalPeriod.Days5;
            PrintTotalPeriod();
        }

        private void Period2WeeksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            totalPeriod = TotalPeriod.Week2;
            PrintTotalPeriod();

        }

        private void Period2MonthsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            totalPeriod = TotalPeriod.Month2;
            PrintTotalPeriod();

        }

        private void Period6MonthsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            totalPeriod = TotalPeriod.Month6;
            PrintTotalPeriod();

        }

        private void PeriodAllDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            totalPeriod = TotalPeriod.All;
            PrintTotalPeriod();

        }

        private void checkBoxVolume_CheckedChanged(object sender, EventArgs e)
        {
            if (chart == null)
                return;
            chart.ShowVolume(checkBoxVolume.Checked);
            chart.ReDraw();
        }
    }
}
