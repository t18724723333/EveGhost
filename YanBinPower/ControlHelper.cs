using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YanBinPower
{
    public class ControlHelper
    {
        /// <summary>
        /// 给Control中的所有Button添加事件
        /// </summary>
        /// <param name="controlCollection"></param>
        /// <param name="eventHandler">点击事件</param>
        public static void ButtonAddClickHandler(Control.ControlCollection controlCollection, EventHandler eventHandler)
        {
            foreach (Control control in controlCollection) { if (control is Button _bt) { _bt.Click += eventHandler; } }
        }
        public static void DataGridViewDrawIndex(DataGridView dataGridView, DataGridViewRowPostPaintEventArgs e)
        {
            e.Graphics.DrawString(
                (e.RowIndex).ToString(System.Globalization.CultureInfo.CurrentUICulture),
                dataGridView.DefaultCellStyle.Font,
                new SolidBrush(dataGridView.RowHeadersDefaultCellStyle.ForeColor),
                e.RowBounds.Location.X + 20,
                e.RowBounds.Location.Y + 4);
        }
        private readonly string strval;
        private readonly ComboBox comboBox = new ComboBox();
        private readonly DataGridView dgv;
        public ControlHelper() { }
        public ControlHelper(string val,DataTable dataTable,DataGridView dataGridView)
        {
            strval = val;
            dgv = dataGridView;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DataTable dTaccount = new DataView(dataTable).ToTable(true, "val");
            comboBox.DataSource = dTaccount;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv.CurrentCell.Value = ((ComboBox)sender).Text;
        }
        public void DataGridViewADDCurrentCellChangedHandler(DataGridView dataGridView)
        {
            dataGridView.CurrentCellChanged += new EventHandler(ALLCurrentCellChanged);
        }


        private void ALLCurrentCellChanged(object sender, EventArgs e)
        {
            if (sender is DataGridView _dgv && _dgv.SelectedCells.Count>0)
            {
                int colindex = _dgv.Columns[strval].Index;
                if (_dgv.CurrentCell.ColumnIndex == colindex)
                {
                    Rectangle rectangle = _dgv.GetCellDisplayRectangle(colindex, _dgv.CurrentCell.RowIndex, false);
                    comboBox.Left = rectangle.Left;
                    comboBox.Top = rectangle.Top;
                    comboBox.Width = rectangle.Width;
                    comboBox.Height = rectangle.Height;
                    comboBox.Visible = true;
                }
                else
                {
                    comboBox.Visible = false;
                }


                
                //if (_dgv.CurrentCell.ColumnIndex >0 && )
                //{
                //    Console.WriteLine(abc+"   "+);
                //}

                //if (_dgv.CurrentCell.ColumnIndex == _dgv.Columns.IndexOf(_dgv.Columns["预警状态"]))
                //{
                //    Console.WriteLine(_dgv.CurrentCell.RowIndex);
                //}

            }

        }
    }
}
