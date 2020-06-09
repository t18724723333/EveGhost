using System;
using System.Windows.Forms;

namespace UI
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            Button2_Click(null, null);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MODEL.Config.RoleNumber = Convert.ToInt32(textBox1.Text);
            MODEL.TempConfig tc = MODEL.Config.GetTempConfig();
            YanBinPower.Serializer.ObjectToFile(tc, "config.config");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = MODEL.Config.RoleNumber + string.Empty;




        }
    }
}
