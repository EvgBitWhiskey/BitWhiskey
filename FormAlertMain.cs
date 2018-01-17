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

        public FormAlertMain(Market market_)
        {
            InitializeComponent();
            market = market_;
            timerUpdateAlerts = new AppTimer(3000, timerUpdateAlerts_Tick, this);
            ShowAlertList();
            timerUpdateAlerts.Start();
        }
        private void timerUpdateAlerts_Tick(object sender, ElapsedEventArgs e)
        {
            ShowAlertList();
        }
        public void ShowAlertList()
        {         

            var dataView =Global.alerts.Select(item => new
            {
                id = item.Value.id.ToString(),
                caption= item.Value.caption,
                tickerPair= item.Value.tickerPair,
                priceAlert = item.Value.priceAlert.ToString(),
                playSound = item.Value.playSound.ToString(),
                showInChart = item.Value.showInChart.ToString(),
                alertExecute = item.Value.alertExecute.ToString()
            }).ToList();

            List<DGVColumn> columns = new List<DGVColumn>()
            {
                new DGVColumn( "id", "Id","string") ,
                new DGVColumn( "caption", "Name","string"),
                new DGVColumn( "tickerPair", "Currency","string"),
                new DGVColumn( "priceAlert", "Alert Price","string"),
                new DGVColumn( "playSound", "PlaySound","string"),
                new DGVColumn( "showInChart", "ShowInChart","string"),
                new DGVColumn( "alertExecute", "Executing","string")
            };
            DataGridViewWrapper gv = new DataGridViewWrapper(dGridAlerts,false);
            gv.Create(dataView, columns);
            gv.RowColorByCondition("alertExecute", "True", Color.LightPink);

        }
        private void buttonAddAlert_Click(object sender, EventArgs e)
        {
            FormAlertAdd form = new FormAlertAdd(market);
//            form.parent = this;
            form.ShowDialog();
            if(form.alert!=null)
              Global.alerts.Add(form.alert.id, form.alert);
            ShowAlertList();
        }

        private void buttonDeleteAlerts_Click(object sender, EventArgs e)
        {
            Global.alerts.Clear();
            if (Global.player != null)
                Global.player.Stop();
            ShowAlertList();
        }

        private void buttonStopAlerts_Click(object sender, EventArgs e)
        {
            foreach (Alert alert in Global.alerts.Values)
            {
                if (alert.alertExecute)
                    alert.alertExecute = false;
            }
            if (Global.player != null)
                Global.player.Stop();
            ShowAlertList();
        }


    }

}
