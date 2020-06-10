using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YanBinPower;

namespace UI
{
    public partial class AccountForm : Form
    {
        readonly DTAccount dtaccoount = new DTAccount();
        public AccountForm() { InitializeComponent(); }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            ControlHelper.ButtonAddClickHandler(splitContainer1.Panel2.Controls, new EventHandler(ALL_Click));
            ControlHelper controlHelper = new ControlHelper("预警状态", dtaccoount, AccountDataGridView);
            controlHelper.DataGridViewADDCurrentCellChangedHandler(AccountDataGridView);
            controlHelper = new ControlHelper("状态", dtaccoount, AccountDataGridView);
            controlHelper.DataGridViewADDCurrentCellChangedHandler(AccountDataGridView);
            AccountDataGridView.DataSource = dtaccoount;
        }


        private void ALL_Click(object sender, EventArgs e)
        {
            string path;
            if (sender is Button _temptsmi)
            {
                switch (_temptsmi.Text)
                {
                    case "读取账号组列表":
                        comboBox1.Items.Clear();
                        comboBox1.Items.AddRange(PathHelper.GetList(PathFileList.Account));
                        comboBox1.SelectedIndex = comboBox1.Items.Count > 0 ? 0 : -1;
                        break;
                    case "打开账号组列表":
                        dtaccoount.Rows.Clear();
                        path = YanBinPower.PathHelper.GetFilePath(PathFileList.Account, comboBox1.Text);

                        if (!string.IsNullOrEmpty(comboBox1.Text) && File.Exists(path))
                        {
                            dtaccoount.ReadXml(path);
                        }
                        if (dtaccoount.Rows.Count == 0)
                        {
                            dtaccoount.Rows.Add();
                        }

                        break;
                    case "保存账号组":
                        if (dtaccoount.Rows.Count > 0)
                        {

                            foreach (DataRow item in dtaccoount.Rows)
                            {
                                dtaccoount.Columns.Contains("");
                            }
                            //AccountDataGridView.CurrentCell = ;
                            AccountDataGridView.Rows[0].Cells[0].Selected = false;
                            AccountDataGridView.Rows[0].Cells[1].Selected = true;
                            AccountDataGridView.Rows[1].Cells[1].Selected = true;
                            DataView dv = new DataView(dtaccoount);
                            DataTable _datatable = dv.ToTable(true, "角色名称");
                            foreach (DataRow item in _datatable.Rows)
                            {
                                Console.WriteLine(item[0]);
                            }
                            dtaccoount.WriteXml(YanBinPower.PathHelper.GetFilePath(PathFileList.Account, comboBox1.Text), XmlWriteMode.WriteSchema);
                        }
                        break;
                    default:
                        Console.WriteLine("未添加 {0} 命令,请联系管理人员添加");
                        break;
                }
            }
        }
        public void ScanDataOnly()
        {

        }
        private void AccountDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            ControlHelper.DataGridViewDrawIndex(AccountDataGridView, e);
        }

        //private void AccountDataGridView_CurrentCellChanged(object sender, EventArgs e)
        //{
        //    if (sender is DataGridView _dgv)
        //    {
        //        Console.WriteLine(_dgv.Columns["预警"].Index);
        //        //if (_dgv.CurrentCell.ColumnIndex == _dgv.Columns.IndexOf(_dgv.Columns["预警状态"]))
        //        //{
        //        //    Console.WriteLine(_dgv.CurrentCell.RowIndex);
        //        //}

        //    }

        //}
    }
}
