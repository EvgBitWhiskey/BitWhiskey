using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitWhiskey;

namespace BitWhiskey
{
    public partial class FormTicker : Form
    {
        public string selectedTicker="";

        DataGridViewExWrapper gvTicker;

        Dictionary<string, TradePair> tradePairs;
        string groupFilter;

        List<TradePair> globalPairs = new List<TradePair>();
        List<TradePair> selPairs = new List<TradePair>();

        public FormTicker(Dictionary<string, TradePair> tradePairs_,string groupFilter_)
        {
            InitializeComponent();

            tradePairs = tradePairs_;
            groupFilter = groupFilter_;

            globalPairs = new List<TradePair>();

            foreach (KeyValuePair<string, TradePair> pair in tradePairs.OrderBy(p => p.Key))
            {
                if (groupFilter == "BTC")
                {
                    if (pair.Key.StartsWith(groupFilter))
                        globalPairs.Add(pair.Value);
                }
                else
                {
                    if (!pair.Key.StartsWith("BTC"))
                        globalPairs.Add(pair.Value);
                }
            }
            selPairs = globalPairs.OrderBy(x=>x.currency2).ToList();

            InitGrid();
        }
        private void InitGrid()
        {
            List<TickerGridRow> rows = new List<TickerGridRow>();

            TickerGridRow row;
            int total=selPairs.Count;
            for (int f = 0; f < total; f += 8)
            {
                row = new TickerGridRow();
                if (f < total)
                    row.f1 = selPairs[f].ticker;
                if (f+1 < total)
                    row.f2 = selPairs[f+1].ticker;
                if (f + 2 < total)
                    row.f3 = selPairs[f+2].ticker;
                if (f + 3 < total)
                    row.f4 = selPairs[f+3].ticker;
                if (f + 4 < total)
                    row.f5 = selPairs[f+4].ticker;
                if (f + 5 < total)
                    row.f6 = selPairs[f+5].ticker;
                if (f + 6 < total)
                    row.f7 = selPairs[f+6].ticker;
                if (f + 7 < total)
                    row.f8 = selPairs[f+7].ticker;
                rows.Add(row);
            }

            List<GVColumn> columns = new List<GVColumn>()
            {
                new GVColumn( "f1", "F1","string") ,
                new GVColumn( "f2", "F2","string") ,
                new GVColumn( "f3", "F3","string") ,
                new GVColumn( "f4", "F4","string") ,
                new GVColumn( "f5", "F5","string") ,
                new GVColumn( "f6", "F6","string") ,
                new GVColumn( "f7", "F7","string") ,
                new GVColumn( "f8", "F8","string") 
            };
            //            GVState gvstate = new GVState();
            if (gvTicker == null)
                gvTicker = new DataGridViewExWrapper();
            gvTicker.Create(dGridTicker,rows, columns);
            gvTicker.HighlightMouseMoveCell(true);
            DataGridViewCellStyle styleLeft = new DataGridViewCellStyle { Font = new Font("Tahoma", 9.0F, FontStyle.Regular), Alignment = DataGridViewContentAlignment.MiddleLeft};
            for(int n=1;n<=8;n++)
              gvTicker.SetColumnStyle("f"+n, styleLeft);

        }

        private void textBoxAutoFind_TextChanged(object sender, EventArgs e)
        {
            string text = textBoxAutoFind.Text.ToUpper();
            if(text.Length>0)
            {
                selPairs = globalPairs.FindAll(x => x.ticker.Contains(text)).OrderBy(x => x.currency2).ToList();
            }
            else
            {
                selPairs = globalPairs.OrderBy(x => x.currency2).ToList();
            }
            InitGrid();
        }

        private void dGridTicker_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedTicker=(string)gvTicker.CellValue(gvTicker.FieldName(e.ColumnIndex), e.RowIndex);
            Close();
        }
    }
}
