using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace BitWhiskey
{
    public partial class FormAlertMain : Form
    {
        Market market;
        private AppTimer timerUpdateAlerts;
        DataGridViewExWrapper gvAlerts;

        public FormAlertMain(Market market_)
        {
            InitializeComponent();
            market = market_;
            timerUpdateAlerts = new AppTimer(5000, timerUpdateAlerts_Tick, this);
            ShowAlertList();
            timerUpdateAlerts.Start();
        }
        private void timerUpdateAlerts_Tick(object sender, ElapsedEventArgs e)
        {
            ShowAlertList();
        }
        public void ShowAlertList()
        {         

            var dataView = AlertManager.alerts.Select(item => new
            {
                id = item.Value.id.ToString(),
                caption= item.Value.caption,
                tickerPair= item.Value.tickerPair,
                priceAlert = item.Value.priceAlert.ToString(),
                playSound = item.Value.playSound.ToString(),
                showInChart = item.Value.showInChart.ToString(),
                enabled = item.Value.enabled.ToString(),
                alertExecute = item.Value.alertExecute.ToString()
            }).ToList();

            GVButtonColumn buttEnableAlert = new GVButtonColumn("enable", "", "c_button", "On\\Off");
            GVButtonColumn buttEditAlert = new GVButtonColumn("edit", "", "c_button", "Edit");
            GVButtonColumn buttDeleteAlert = new GVButtonColumn("delete", "", "c_button", "Delete");
            List<GVColumn> columns = new List<GVColumn>()
            {
                new GVColumn( "id", "Id","string") ,
                new GVColumn( "caption", "Name","string"),
                new GVColumn( "tickerPair", "Currency","string"),
                new GVColumn( "priceAlert", "Alert Price","string"),
                new GVColumn( "playSound", "PlaySound","string"),
                new GVColumn( "showInChart", "ShowInChart","string"),
                new GVColumn( "enabled", "Enabled","string"),
                new GVColumn( "alertExecute", "Executing","string"),
                buttEnableAlert,
                buttEditAlert,
                buttDeleteAlert
            };

            if (gvAlerts == null)
                gvAlerts = new DataGridViewExWrapper();
            gvAlerts.Create(dGridAlerts, dataView, columns, true);
            gvAlerts.AutoSizeFillExcept("caption");
            gvAlerts.RowColorByCondition("alertExecute", "True", Color.LightPink);

        }
        private void dGridAlerts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (gvAlerts.Column(e.ColumnIndex).Name == "delete")
            {
                string id = (string)gvAlerts.CellValue("id", e.RowIndex);
                int alertid = int.Parse(id);
                AlertManager.Remove(alertid);
                AlertManager.CheckAlerts();
                ShowAlertList();
            }
            else if (gvAlerts.Column(e.ColumnIndex).Name == "edit")
            {
                string id = (string)gvAlerts.CellValue("id", e.RowIndex);
                int alertid = int.Parse(id);
                Alert alert= AlertManager.alerts[alertid];
                FormAlertAdd form = new FormAlertAdd(ExchangeManager.GetMarketByMarketName(alert.market), "", alertid);
                form.ShowDialog();
                if (form.alertAdded || form.alertChanged)
                {
                    AlertManager.SaveAlerts();
                    AlertManager.CheckAlerts();
                }
                ShowAlertList();
            }
            else if (gvAlerts.Column(e.ColumnIndex).Name == "enable")
            {
                string id = (string)gvAlerts.CellValue("id", e.RowIndex);
                int alertid = int.Parse(id);
                Alert alert= AlertManager.alerts[alertid];
                alert.enabled=!alert.enabled;
                if(!alert.enabled)
                  alert.alertExecute = false;
                AlertManager.SaveAlerts();
                AlertManager.CheckAlerts();
                ShowAlertList();
            }
        }
        private void buttonAddAlert_Click(object sender, EventArgs e)
        {
            FormAlertAdd form = new FormAlertAdd(market);
//            form.parent = this;
            form.ShowDialog();
            if (form.alertAdded || form.alertChanged)
            {
                AlertManager.SaveAlerts();
                AlertManager.CheckAlerts();
            }
            ShowAlertList();
        }

        private void buttonDeleteAlerts_Click(object sender, EventArgs e)
        {
            AlertManager.RemoveAll();
            ShowAlertList();
        }

        private void buttonStopAlerts_Click(object sender, EventArgs e)
        {
            AlertManager.ToggleAll();
            AlertManager.CheckAlerts();
            ShowAlertList();
        }

        private void FormAlertMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerUpdateAlerts.Stop();
        }
    }

}
