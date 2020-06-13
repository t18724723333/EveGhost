using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using YanBinPower;

namespace BLL
{
    public class BllContarner
    {
        /// <summary>
        /// 返回menustrip菜单
        /// </summary>
        /// <param name="menuStrip1">menustrip</param>
        /// <param name="permission">权限---备用</param>
        public static void GetMenuStrip(MenuStrip menuStrip1, string permission)
        {
            menuStrip1.Items.Clear();
            DataTable _dataTable = YanBinPower.DataBaseHelper.GetMenuStrip(permission);
            foreach (DataRow item in _dataTable.Select("colid = 0", "colsort ASC"))
            {
                string colname = item[3].ToString();
                if (!string.IsNullOrWhiteSpace(colname))
                {
                    string _imgfile = PathHelper.GetFilePath(PathFileList.Resources, colname);
                    ToolStripMenuItem _toolStripMenuItem = new ToolStripMenuItem(colname) { Tag = colname, Image = File.Exists(_imgfile) ? Image.FromFile(_imgfile) : null };
                    string id = item[0].ToString();
                    if (_dataTable.Select("colid = " + id, "colsort ASC").Length > 0)
                    {
                        SetMUB(_dataTable, id, _toolStripMenuItem);
                    }
                    menuStrip1.Items.Add(_toolStripMenuItem);
                }

            }
        }

        public static void GetFlowLayoutPanel(FlowLayoutPanel flowLayoutPanel1)
        {
            //throw new NotImplementedException();
        }

        private static void SetMUB(DataTable dataTable, string v, ToolStripMenuItem toolStripMenuItem)
        {

            foreach (DataRow dataRow in dataTable.Select("colid =" + v, "colsort ASC"))
            {
                string colname = dataRow[3].ToString();
                if (!string.IsNullOrWhiteSpace(colname))
                {
                    string _imgfile = PathHelper.GetFilePath(PathFileList.Resources, colname);
                    ToolStripMenuItem _toolStripMenuItem = new ToolStripMenuItem(colname) { Tag = colname, Image = File.Exists(_imgfile) ? Image.FromFile(_imgfile) : null };
                    string id = dataRow[0].ToString();
                    if (dataTable.Select("colid = " + id, "colsort ASC").Length > 0)
                    {
                        SetMUB(dataTable, id, _toolStripMenuItem);
                    }
                    toolStripMenuItem.DropDownItems.Add(_toolStripMenuItem);
                }
            }
        }
    }
}
