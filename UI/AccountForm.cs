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
            foreach (Control control in splitContainer1.Panel2.Controls) { if (control is Button _bt) { _bt.Click += new EventHandler(ALL_Click); } }
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
                        if (dtaccoount.Rows.Count ==0)
                        {
                            dtaccoount.Rows.Add();
                        }

                        break;
                    case "保存账号组":
                        if (dtaccoount.Rows.Count > 0)
                        {
                            dtaccoount.WriteXml(YanBinPower.PathHelper.GetFilePath(PathFileList.Account, comboBox1.Text), XmlWriteMode.WriteSchema);
                        }
                        break;
                    default:
                        Console.WriteLine("未添加 {0} 命令,请联系管理人员添加");
                        break;
                }
            }
        }





    }
}
