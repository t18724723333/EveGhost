using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace UI
{
    public partial class MainForm : Form
    {
        public MainForm() { InitializeComponent(); }


        private void Button2_Click(object sender, EventArgs e)
        {

            //取flowLayoutPanel控件数
            //textBox1.AppendText(flowLayoutPanel1.Controls.Count + "\r\n");

            //foreach (Control item in flowLayoutPanel1.Controls)
            //{
            //    textBox1.AppendText(flowLayoutPanel1.Controls.GetChildIndex(item) + "----");
            //    if (item as Button != null)
            //    {
            //        Button bt = item as Button;
            //        textBox1.AppendText(bt.Tag + "\r\n");
            //    }
            //}
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            SetupToolStripMenuItemHandler();
            RealConfig();
            MainInit();
            RegDM();
        }
        /// <summary>
        /// 系统初始化
        /// </summary>
        void MainInit()
        {
            Model.MainAccount = new MODEL.DTAccount();
            Model.MainAccount.Rows.Add();
        }
        /// <summary>
        /// 注册大漠
        /// </summary>
        void RegDM()
        {
            //throw new NotImplementedException();
        }


        /// <summary>
        /// 读取系统设置
        /// </summary>
        void RealConfig()
        {
            string _cfg = YanBinPower.PathHelper.GetFilePath(YanBinPower.PathFileList.Config, "Config");
            Config.GetConfig((File.Exists(_cfg) && YanBinPower.Serializer.FileToObject<TempConfig>(_cfg) is TempConfig _tc) ? _tc : new TempConfig());
        }


        #region 导航相关
        Control TempControl;
        /// <summary>
        /// 左导航右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            TempControl = (sender as ContextMenuStrip).SourceControl;
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Remove(TempControl);
        }
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TempControl != null)
            {
                int controlindex = flowLayoutPanel1.Controls.GetChildIndex(TempControl);
                if (controlindex > 0)
                {
                    flowLayoutPanel1.Controls.SetChildIndex(TempControl, flowLayoutPanel1.Controls.GetChildIndex(TempControl) - 1);
                }
            }
        }
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TempControl != null)
            {
                int controlindex = flowLayoutPanel1.Controls.GetChildIndex(TempControl);
                if (controlindex < flowLayoutPanel1.Controls.Count - 1)
                {
                    flowLayoutPanel1.Controls.SetChildIndex(TempControl, flowLayoutPanel1.Controls.GetChildIndex(TempControl) + 1);
                }
            }
        }

        /// <summary>
        /// 设置导航菜单单击事件
        /// </summary>
        private void SetupToolStripMenuItemHandler()
        {
            foreach (ToolStripMenuItem item in menuStrip1.Items) //添加三个事件
            {
                item.MouseDown += new MouseEventHandler(ALLToolStripMenuItem_MouseDown);
                item.MouseUp += new MouseEventHandler(ALLToolStripMenuItem_MouseUP);
                item.MouseMove += new MouseEventHandler(ALLToolStripMenuItem_MouseMove);
            }
        }

        /// <summary>
        /// 左导航拖放结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        /// <summary>
        /// 左导航拖放中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            //Button btnInstance = sender as Button; 数据在e不在sender 不能这样用
            Button btnInstance = null;
            if ((e.Data.GetDataPresent(typeof(Button))))
            {
                FieldInfo info, info2, info3;
                object obj, obj2, obj3;
                info = e.Data.GetType().GetField("innerData", BindingFlags.NonPublic | BindingFlags.Instance);
                obj = info.GetValue(e.Data);
                info2 = obj.GetType().GetField("data", BindingFlags.NonPublic | BindingFlags.Instance);
                obj2 = info2.GetValue(obj);
                System.Collections.Hashtable dataItems = (obj2 as System.Collections.Hashtable);
                foreach (var dataItem in dataItems)
                {
                    System.Collections.DictionaryEntry dictEntry = (System.Collections.DictionaryEntry)dataItem;
                    object key = dictEntry.Key;
                    object value = dictEntry.Value;
                    info3 = value.GetType().GetField("data", BindingFlags.Public | BindingFlags.Instance);
                    obj3 = info3.GetValue(value);
                    btnInstance = obj3 as Button;
                }

                if (btnInstance != null)
                {
                    Button tbu;
                    foreach (var item in flowLayoutPanel1.Controls)
                    {
                        tbu = item as Button;
                        if (tbu != null && tbu.Name == btnInstance.Name)
                        {
                            return;
                        }
                    }
                    btnInstance.Text = string.Empty;
                    flowLayoutPanel1.Controls.Add(btnInstance);
                }
            }
        }

        /// <summary>
        /// 左导航单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllflowLayoutPanelButton_Click(object sender, EventArgs e)
        {
            string temp = string.Empty;
            if (sender as Button != null)
            {
                Button temptsmi = sender as Button;
                temp = temptsmi.Tag.ToString();
            }

            ALL_Click(temp);
        }

        /// <summary>
        /// 菜单鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ALLToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        { Model.MousePoint = e.Location; }

        /// <summary>
        /// 菜单鼠标松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ALLToolStripMenuItem_MouseUP(object sender, MouseEventArgs e)
        {
            if (e.Location == Model.MousePoint)
            {
                string temp = string.Empty;
                if (sender as ToolStripMenuItem != null)
                {
                    ToolStripMenuItem temptsmi = sender as ToolStripMenuItem;
                    temp = temptsmi.Text;
                }

                ALL_Click(temp);
            }
        }

        /// <summary>
        /// 菜单鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ALLToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && sender as ToolStripMenuItem != null)
            {
                ToolStripMenuItem TSMT = sender as ToolStripMenuItem;
                Button tmpbutton = new Button
                {
                    Name = "Button" + TSMT.Name,
                    Size = new System.Drawing.Size(40, 40),
                    Image = TSMT.Image,
                    Text = TSMT.Text,
                    Tag = TSMT.Text,
                    ContextMenuStrip = this.contextMenuStrip1,
                    FlatStyle = System.Windows.Forms.FlatStyle.Flat
                };

                toolTip1.SetToolTip(tmpbutton, TSMT.Text);
                tmpbutton.Click += new EventHandler(AllflowLayoutPanelButton_Click);

                tmpbutton.DoDragDrop(tmpbutton, DragDropEffects.Copy);
            }
        }

        /// <summary>
        /// 导航单击事件处理
        /// </summary>
        /// <param name="str"></param>
        private void ALL_Click(string str)
        {
            switch (str)
            {
                case "系统设置":
                    this.Add_TabPage(str + "  ", new ConfigForm());
                    break;
                case "监控中心":
                    this.Add_TabPage(str + "  ", new Form2());
                    break;
                case "账号管理":
                    this.Add_TabPage(str + "  ", new AccountForm());
                    break;
                default:
                    Console.WriteLine(str);
                    break;
            }
        }
        #endregion

        #region MainTabControl相关操作

        /// <summary>
        /// 将标题添加进tabpage中
        /// </summary>
        /// <param name="str"></param>
        /// <param name="myForm"></param>
        public void Add_TabPage(string str, Form myForm) //将标题添加进tabpage中
        {
            if (!this.TabControlCheckHave(this.MainTabControl, str))
            {
                this.MainTabControl.TabPages.Add(str);
                this.MainTabControl.SelectTab((this.MainTabControl.TabPages.Count - 1));

                myForm.FormBorderStyle = FormBorderStyle.None;
                myForm.TopLevel = false;

                myForm.Dock = DockStyle.Fill;
                myForm.Show();
                myForm.Parent = this.MainTabControl.SelectedTab;
            }
        }

        /// <summary>
        /// 看tabpage中是否已有窗体
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public bool TabControlCheckHave(TabControl tab, string tabName) //看tabpage中是否已有窗体
        {
            for (int i = 0; i < tab.TabCount; i++)
            {
                if (tab.TabPages[i].Text == tabName)
                {
                    tab.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 绘制TabPage关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)// 绘制TabPage关闭按钮
        {
            /*如果将 DrawMode 属性设置为 OwnerDrawFixed， 
 则每当 TabControl 需要绘制它的一个选项卡时，它就会引发 DrawItem 事件*/
            try
            {
                //Rectangle rec = MainTabControl.ClientRectangle;
                //Image backImage = UI.Properties.Resources.black;
                //e.Graphics.DrawImage(backImage, 0, 0, MainTabControl.Width+65, MainTabControl.Height+40);
                ////新建一个StringFormat对象，用于对标签文字的布局设置
                //StringFormat StrFormat = new StringFormat
                //{
                //    LineAlignment = StringAlignment.Center,// 设置文字垂直方向居中
                //    Alignment = StringAlignment.Center// 设置文字水平方向居中          
                //};
                //// 标签背景填充颜色，也可以是图片
                ////SolidBrush bru = new SolidBrush(Color.FromArgb(72, 181, 250));
                ////SolidBrush bruFont = new SolidBrush(Color.FromArgb(217, 54, 26));// 标签字体颜色
                ////Font font = new System.Drawing.Font("微软雅黑", 12F);//设置标签字体样式
                ////绘制标签样式
                //for (int i = 0; i < MainTabControl.TabPages.Count; i++)
                //{
                //    e.Graphics.FillRectangle(new SolidBrush(Color.Black), MainTabControl.GetTabRect(i)); //绘制标签头背景颜色

                //    e.Graphics.DrawString(MainTabControl.TabPages[i].Text, new System.Drawing.Font("微软雅黑", 12F), new SolidBrush(Color.White), MainTabControl.GetTabRect(i), StrFormat);   //绘制标签头的文字
                //}
                //this.MainTabControl.TabPages[e.Index].BackColor = Color.Black;
                Rectangle tabRect = this.MainTabControl.GetTabRect(e.Index);
                e.Graphics
                    .DrawString(this.MainTabControl.TabPages[e.Index].Text,
                                this.Font,
                                SystemBrushes.ActiveCaptionText,
                                (float)(tabRect.X + 2),
                                (float)(tabRect.Y + 2));

                #region 绘制关闭正方形底色
                //using (Pen pen = new Pen(Color.White))
                //{
                //    tabRect.Offset(tabRect.Width - 15, 2);
                //    tabRect.Width = 15;
                //    tabRect.Height = 15;
                //    e.Graphics.DrawRectangle(pen, tabRect);
                //}

                //Color color = (e.State == DrawItemState.Selected) ? Color.LightBlue : Color.White;
                //using (Brush brush = new SolidBrush(color))
                //{
                //    e.Graphics.FillRectangle(brush, tabRect);
                //} 
                #endregion

                using (Pen pen2 = new Pen(Color.DimGray, 2))//绘制关闭X
                {
                    tabRect.Offset(tabRect.Width - 17, 1);
                    tabRect.Width = 15;
                    tabRect.Height = 15;
                    Point point = new Point(tabRect.X + 3, tabRect.Y + 3);
                    Point point2 = new Point((tabRect.X + tabRect.Width) - 3, (tabRect.Y + tabRect.Height) - 3);
                    e.Graphics.DrawLine(pen2, point, point2);
                    Point point3 = new Point(tabRect.X + 3, (tabRect.Y + tabRect.Height) - 3);
                    Point point4 = new Point((tabRect.X + tabRect.Width) - 3, tabRect.Y + 3);
                    e.Graphics.DrawLine(pen2, point3, point4);
                }
                e.Graphics.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                int y = e.Y;

                Rectangle tabRect = this.MainTabControl.GetTabRect(this.MainTabControl.SelectedIndex);
                tabRect.Offset(tabRect.Width - 0x12, 2);
                tabRect.Width = 15;
                tabRect.Height = 15;
                if ((((x > tabRect.X) && (x < tabRect.Right)) && (y > tabRect.Y)) && (y < tabRect.Bottom))
                {

                    this.MainTabControl.TabPages.Remove(this.MainTabControl.SelectedTab);
                }
            }
        }


        #endregion

        private void button6_Click(object sender, EventArgs e)
        {
            Model.MainAccount.Rows.Add(true, 0, ":123", "aaaaaa");
            Console.WriteLine(Model.MainAccount.GetString("选择"));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Model.MainAccount;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //string manage = "SELECT * From Win32_NetworkAdapter";
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(manage);
            //ManagementObjectCollection collection = searcher.Get();
            //List<string> netWorkList = new List<string>();

            //foreach (ManagementObject obj in collection)
            //{
            //    netWorkList.Add(obj["Name"].ToString());

            //}
            //this.dataGridView1.DataSource = netWorkList;

        }
    }
}