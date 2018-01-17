using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;


namespace BitWhiskey
{
    public class DGVColumn
    {
        public string caption { get; set; }
        public string fieldname { get; set; }
        public string datatype { get; set; }

        public DGVColumn(string fieldname_, string caption_,string datatype_)
        {
            fieldname = fieldname_;
            caption = caption_;
            datatype = datatype_;
        }
    }

    public class DataGridViewWrapper
    {
        protected DataGridView gridView;
        protected List<DGVColumn> columns;
        protected bool hideselection = false;

        public DataGridViewWrapper(DataGridView gridView_,bool hideselection_=false)
        {
            gridView = gridView_;
            hideselection = hideselection_;
        }
        public void Create<T>(List<T> rowList, List<DGVColumn> columns_)
        {
            columns = columns_;
            DataTable table = Helper.ToDataTable(rowList);
            //            gridView.DataSource = rowList;
            gridView.DataSource = table;

            foreach (DGVColumn col in columns)
            {
                gridView.Columns[col.fieldname].HeaderText = col.caption;
                gridView.Columns[col.fieldname].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gridView.Columns[col.fieldname].Resizable = DataGridViewTriState.True;
            }
            if (hideselection)
              DataGridHideSelection();
        }
        public void SetColumnStyle(string field, DataGridViewCellStyle style)
        {
            gridView.Columns[field].DefaultCellStyle = style;
        }
        private void DataGridHideSelection()
        {
//            gridView.DefaultCellStyle.SelectionBackColor = gridView.DefaultCellStyle.BackColor;
//            gridView.DefaultCellStyle.SelectionForeColor = gridView.DefaultCellStyle.ForeColor;
            gridView.ClearSelection();
        }
        public void ShowColumnHeaders(bool show=true)
        {
            gridView.ColumnHeadersVisible=show;
        }
        public void AutoSizeFillExcept(string displayedCellsField)
        {
            foreach (DataGridViewColumn col in gridView.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (col.Name== displayedCellsField)
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        public void AutoSizeDisplayedExcept(string displayedCellsField)
        {
            foreach (DataGridViewColumn col in gridView.Columns)
            {
//                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
//                if (col.Name == displayedCellsField)
//                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        public void RowColorByCondition(string field, string condValue, Color color)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (Convert.ToString(row.Cells[field].Value) == condValue)
                    row.DefaultCellStyle.BackColor = color;
            }

        }
        public void RowColorByCondition(string field, Predicate<string> cond, Color color)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                string val = Convert.ToString(row.Cells[field].Value);
                bool condResult=cond(val);
                if (condResult)
                    row.DefaultCellStyle.BackColor = color;
            }

        }

    }



}
