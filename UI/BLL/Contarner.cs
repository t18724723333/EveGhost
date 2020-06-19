using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YanBinPower;

namespace EveGhost.BLL
{
    public class Contarner
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
            Console.WriteLine(_dataTable.Rows.Count);
            foreach (DataRow _dataRow in _dataTable.Select("colid = 0", "colsort ASC"))
            {
                string _colname = _dataRow["colname"].ToString();
                if (!string.IsNullOrWhiteSpace(_colname))
                {
                    string _imgfile = PathHelper.GetFilePath(PathFileList.Resources, _colname);
                    ToolStripMenuItem _toolStripMenuItem = new ToolStripMenuItem(_colname) { Tag = _colname, Image = File.Exists(_imgfile) ? Image.FromFile(_imgfile) : null };
                    string _id = _dataRow["id"].ToString();
                    if (_dataTable.Select("colid = " + _id, "colsort ASC").Length > 0)
                    {
                        SetMUB(_dataTable, _id, _toolStripMenuItem);
                    }
                    string _guide = _dataRow["description"].ToString();
                    _toolStripMenuItem.ToolTipText = string.IsNullOrWhiteSpace(_guide) ? _colname : _guide;
                    menuStrip1.Items.Add(_toolStripMenuItem);
                }

            }
        }
        /// <summary>
        /// 递归设置导航子菜单
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="v"></param>
        /// <param name="toolStripMenuItem"></param>
        private static void SetMUB(DataTable dataTable, string v, ToolStripMenuItem toolStripMenuItem)
        {

            foreach (DataRow _dataRow in dataTable.Select("colid =" + v, "colsort ASC"))
            {
                string _colname = _dataRow["colname"].ToString();
                if (!string.IsNullOrWhiteSpace(_colname))
                {
                    string _imgfile = PathHelper.GetFilePath(PathFileList.Resources, _colname);
                    ToolStripMenuItem _toolStripMenuItem = new ToolStripMenuItem(_colname) { Tag = _colname, Image = File.Exists(_imgfile) ? Image.FromFile(_imgfile) : null };
                    string _id = _dataRow["id"].ToString();
                    if (dataTable.Select("colid = " + _id, "colsort ASC").Length > 0)
                    {
                        SetMUB(dataTable, _id, _toolStripMenuItem);
                    }
                    string _guide = _dataRow["description"].ToString();
                    _toolStripMenuItem.ToolTipText = string.IsNullOrWhiteSpace(_guide) ? _colname : _guide;
                    toolStripMenuItem.DropDownItems.Add(_toolStripMenuItem);
                }
            }
        }

        /// <summary>
        /// 返回flowLayoutPanel中的按钮
        /// </summary>
        /// <param name="flowLayoutPanel"></param>
        /// <param name="toolTip"></param>
        public static void GetFlowLayoutPanel(FlowLayoutPanel flowLayoutPanel, ContextMenuStrip contextMenuStrip, ToolTip toolTip)
        {
            flowLayoutPanel.Controls.Clear();
            FlowLayoutPanelAddButton(flowLayoutPanel, null, toolTip, "代理人");
            DataTable _dataTable = DataBaseHelper.GetFlowLayoutPanel();
            foreach (DataRow dataRow in _dataTable.Rows)
            {
                FlowLayoutPanelAddButton(flowLayoutPanel, contextMenuStrip, toolTip, dataRow["name"].ToString());
            }
        }
        public static void FlowLayoutPanelAddButton(FlowLayoutPanel flowLayoutPanel, ContextMenuStrip contextMenuStrip, ToolTip toolTip, string name)
        {
            string _imgfile = PathHelper.GetFilePath(PathFileList.Resources, name);
            Button _newbutton = new Button() { Tag = name, Image = File.Exists(_imgfile) ? Image.FromFile(_imgfile) : null, FlatStyle = FlatStyle.Flat, Size = new System.Drawing.Size(40, 40), TextImageRelation = TextImageRelation.TextBeforeImage, UseVisualStyleBackColor = true, };
            _newbutton.Text = _newbutton.Image == null ? name : string.Empty;
            toolTip.SetToolTip(_newbutton, name);
            _newbutton.ContextMenuStrip = contextMenuStrip;
            flowLayoutPanel.Controls.Add(_newbutton);
        }
    }
}
