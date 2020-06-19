using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YanBinPower
{
    public static class MdiHelper
    {
        public static bool IsOpen(string fText, Form fr) { foreach (Form form in fr.MdiChildren) { if (form.Name.Equals(fText)) { form.Activate(); return true; } } return false; }
        public static void OpenClientForm(Form fr, Form mdiForm) { fr.Icon = mdiForm.Icon; fr.MdiParent = mdiForm; fr.Show(); }
        public static void ColseAllForm(string fText, Form fr) { foreach (Form form in fr.MdiChildren) { if (form.Name.Equals(fText)) fr.Close(); } }

    }
}