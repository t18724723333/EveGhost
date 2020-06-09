using System;
using System.Windows.Forms;

namespace UI
{
    public partial class Form2 : Form
    {
        public Form2() { InitializeComponent(); }

        private void Button1_Click(object sender, EventArgs e) => label1.Text = MODEL.Model.Abc + "     " + MODEL.Model.AAdfadfaa;

        private void Form2_Load(object sender, EventArgs e) { }
    }
}
