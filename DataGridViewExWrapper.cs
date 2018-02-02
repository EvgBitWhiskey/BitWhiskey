using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;
//using GridViewLab;

namespace BitWhiskey
{
    public class GVState
    {
        public string sortField="";
        public ListSortDirection sortDirection;

        public GVState()
        {
            
        }

    }
        public class GVColumn
    {
        public string caption { get; set; }
        public string fieldname { get; set; }
        public string datatype { get; set; }
        public string displayField { get; set; }
        public bool visible { get; set; }
        public string buttonCaption { get; set; }
        

        public GVColumn(string fieldname_, string caption_,string datatype_, string displayField_ ="", bool visible_ =true)
        {
            fieldname = fieldname_;
            caption = caption_;
            datatype = datatype_;
            visible = visible_;
            displayField = displayField_;
        }
    }

    public class GVButtonColumn: GVColumn
    {
        public GVButtonColumn(string fieldname_, string caption_, string datatype_, string buttonCaption_ = "", bool visible_ = true)
            :base(fieldname_,caption_,datatype_,"",true)
        {
            buttonCaption = buttonCaption_;
        }
    }

    public class DataGridViewExWrapper
    {
        protected DataGridView gridView;
        protected List<GVColumn> columns;
        protected bool gridViewIsReady;
        protected GVState gridState = new GVState();
        protected bool hideselection = false;
        protected bool highlightMouseMoveCell = false;


        public DataGridViewExWrapper()
        {
            gridViewIsReady = false;
            //            grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(grid_CellDoubleClick);

        }
        public void Create<T>(DataGridView gridView_, List<T> rowList, List<GVColumn> columns_, bool hideselection_ = false)
        {
            gridViewIsReady = false;
            gridView = gridView_;
            hideselection = hideselection_;
            if (!SystemInformation.TerminalServerSession)
            {
                Type dgvType = gridView.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(gridView, true, null);
                //                pi.SetValue(gridView,null);
            }

            //gridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //gridView.RowHeadersVisible = false;


            columns = columns_;
            DataTable table = Helper.ToDataTable(rowList);

            gridView.DataSource = null;
            gridView.Rows.Clear();
//            gridView.Refresh();
            gridView.Columns.Clear();
            //            gridView.Refresh();
            gridView.DataSource = null;

            foreach (GVColumn col in columns)
            {
                if (col.datatype.StartsWith("c_"))
                {
                    if (col.datatype == "c_checkbox")
                    {
                        DataGridViewCheckBoxColumn colChk = new DataGridViewCheckBoxColumn();
                        colChk.HeaderText = col.caption;
                        colChk.FalseValue = "False";
                        colChk.TrueValue = "True";
                        colChk.DataPropertyName = col.fieldname;
                        colChk.Name = col.fieldname;
                        //                    gridView.Columns.Insert(0, colChk);

                        //                        colChk.Visible=col.visible;

                        gridView.Columns.Add(colChk);
                    }
                    else if (col.datatype == "c_button")
                    {
                        DataGridViewButtonColumn colChk = new DataGridViewButtonColumn();
                        colChk.HeaderText = col.caption;
                        colChk.DataPropertyName = col.fieldname;
                        colChk.Name = col.fieldname;
                        colChk.Text = col.buttonCaption;
                        colChk.UseColumnTextForButtonValue = true;
                        DataGridViewCellStyle cs = new DataGridViewCellStyle();
                        cs.BackColor = Color.LightGray;
                        cs.SelectionBackColor = Color.Gray;
                        colChk.DefaultCellStyle = cs;
                        colChk.FlatStyle = FlatStyle.Standard;
                        gridView.Columns.Add(colChk);
                    }
                }

            }

            gridView.DataSource = table;

            int displayIndex = 0;
            foreach (GVColumn col in columns)
            {
                gridView.Columns[col.fieldname].DisplayIndex = displayIndex;
                gridView.Columns[col.fieldname].HeaderText = col.caption;
                //if (col.datatype=="double")
                //    gridView.Columns[col.fieldname].DefaultCellStyle.Format = "123";

                gridView.Columns[col.fieldname].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                if (col.datatype.StartsWith("c_"))
                {
                    if (col.datatype == "c_checkbox")
                        gridView.Columns[col.fieldname].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    else if (col.datatype == "c_button")
                        gridView.Columns[col.fieldname].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
                else
                    gridView.Columns[col.fieldname].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                displayIndex++;
                gridView.Columns[col.fieldname].Visible = col.visible;
                //                gridView.Columns[col.fieldname].SortMode=DataGridViewColumnSortMode.
            }

            if (gridState.sortField != "")
            {
                gridView.Sort(gridView.Columns[gridState.sortField], gridState.sortDirection);
            }
            if (rowList.Count() > 0)
                SelectCell(Column(0).Name, 0);

            if (hideselection)
                DataGridHideSelection();

            gridView.CellFormatting -= new DataGridViewCellFormattingEventHandler(this.EventCellFormatting);
            gridView.ColumnHeaderMouseClick -= new DataGridViewCellMouseEventHandler(this.EventColumnHeaderMouseClick);
            gridView.CellFormatting += new DataGridViewCellFormattingEventHandler(this.EventCellFormatting);
            gridView.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.EventColumnHeaderMouseClick);

            gridView.CellMouseMove -= new DataGridViewCellMouseEventHandler(this.EventCellMouseMove);
            gridView.CellMouseMove += new DataGridViewCellMouseEventHandler(this.EventCellMouseMove);
            gridView.CellMouseLeave -= new DataGridViewCellEventHandler(this.EventCellMouseLeave);
            gridView.CellMouseLeave += new DataGridViewCellEventHandler(this.EventCellMouseLeave);

            gridViewIsReady = true;
        }
        public bool GridViewIsReady()
        {
            return gridViewIsReady;
        }
        /*
        public GVState GetState()
        {
            return gridState;
        }
        */
        public void HighlightMouseMoveCell(bool highlightEnabled)
        {
            highlightMouseMoveCell= highlightEnabled;
        }
        private void DataGridHideSelection()
        {
            gridView.ClearSelection();
        }
        public void AutoSizeFillExcept(string displayedCellsField)
        {
            gridView.Columns[displayedCellsField].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
        public void ShowColumnHeaders(bool show = true)
        {
            gridView.ColumnHeadersVisible = show;
        }
        public void SetColumnStyle(string field, DataGridViewCellStyle style)
        {
            gridView.Columns[field].DefaultCellStyle = style;
        }
        public void RowColorByCondition(string field, string condValue, Color color)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (Convert.ToString(row.Cells[field].Value) == condValue)
                    row.DefaultCellStyle.BackColor = color;
            }

        }
        public void RowColorByCondition(string field, Predicate<object> cond, Color color)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                object val = row.Cells[field].Value;
                //                string val = Convert.ToString(row.Cells[field].Value);
                bool condResult = cond(val);
                if (condResult)
                    row.DefaultCellStyle.BackColor = color;
            }
        }
        public DataGridViewCheckBoxCell GetCheckBoxCell(string field, int RowIndex)//, int ColumnIndex)
        {
            int ColumnIndex = gridView.Columns[field].Index;


            if (RowIndex >= 0 && gridView.Columns[ColumnIndex] is DataGridViewCheckBoxColumn
                //                &&  && ColumnIndex == gridView.Columns[field].Index
                )
            {
                DataGridViewCheckBoxCell ch = (DataGridViewCheckBoxCell)gridView.Rows[RowIndex].Cells[ColumnIndex];
                return ch;
            }
            return null;
        }
        public DataGridViewColumn Column(string field)
        {
            return gridView.Columns[field];
        }
        public DataGridViewColumn Column(int field)
        {
            return gridView.Columns[field];
        }
        public DataGridViewCell Cell(string field, int RowIndex)
        {
            int ColumnIndex = gridView.Columns[field].Index;
            DataGridViewCell c = gridView.Rows[RowIndex].Cells[ColumnIndex];
            return c;
        }
        public object CellValue(string field, int RowIndex)
        {
            int ColumnIndex = gridView.Columns[field].Index;
            DataGridViewCell c = gridView.Rows[RowIndex].Cells[ColumnIndex];
            return c.Value;
        }
        public string FieldName(int ColIndex)
        {
           return gridView.Columns[ColIndex].Name;
        }
        /*
        public void RemoveRow(e.RowIndex)
        {
            // dataGridMy.Rows.Remove(dataGridMy.Rows[e.RowIndex]);
        }
        */
        public void SelectRow(int rowIndex)
        {
            gridView.Rows[rowIndex].Selected = true;
        }
        public void SelectCell(string field, int RowIndex)
        {
            int ColumnIndex = gridView.Columns[field].Index;
            gridView.Rows[RowIndex].Cells[ColumnIndex].Selected = true;
        }
        public int CurrentRow()
        {
            if (gridView.CurrentRow == null)
                return -1;
            return gridView.CurrentRow.Index;
        }
        public void ToggleCheckBoxCell(string field, int RowIndex)
        {
            DataGridViewCheckBoxCell ch = GetCheckBoxCell(field, RowIndex);
            //                if (gv.GetCheckBoxCell("favorite", e.RowIndex, e.ColumnIndex) != null)
            //            DataGridViewCheckBoxCell ch = GetCheckBoxCell(field,RowIndex,ColumnIndex);
            if (ch == null)
                return;
            if (ch.Value.ToString() == "True")
            {
                ch.Value = "False";
            }
            else
            {
                ch.Value = "True";
            }
        }
        public bool? GetCheckBoxState(string field, int RowIndex)
        {
            DataGridViewCheckBoxCell ch = GetCheckBoxCell(field, RowIndex);
            //            DataGridViewCheckBoxCell ch = GetCheckBoxCell(field, RowIndex, ColumnIndex);
            if (ch == null)
                return null;
            if (ch.Value.ToString() == "True")
                return true;
            return false;
        }
        public DataTable GetDataTable()
        {
            return (DataTable)gridView.DataSource;
        }

        private void EventCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            GVColumn col = columns.FirstOrDefault(f => f.fieldname == gridView.Columns[e.ColumnIndex].Name);

            if (col == null)
                return;

            switch (col.datatype)
            {
                //                case "PriceBtc":
                //                    string str= Helper.PriceToStringBtc((double)e.Value);
                //                    e.Value = col.template.replace("###",str)
                //                    break;
                case "DisplayField":
                    e.Value = CellValue(col.displayField, e.RowIndex).ToString();
                    break;
            }
        }
        private void EventColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            gridState.sortField = gridView.Columns[e.ColumnIndex].Name;
            if (gridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                gridState.sortDirection = ListSortDirection.Ascending;
            else
                gridState.sortDirection = ListSortDirection.Descending;
        }
        private void EventCellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(highlightMouseMoveCell && e.RowIndex>=0 && e.ColumnIndex>=0)
              gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
        }
        private void EventCellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (highlightMouseMoveCell && e.RowIndex >= 0 && e.ColumnIndex >= 0)
                gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
        }

    }

}
