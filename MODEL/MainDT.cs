using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    [Serializable]
    public class MainDT : DataTable
    {
        public MainDT() { Columns.Add("选择", typeof(Boolean));  }

        public string GetString(string colname)
        {
            if (this.Rows.Count < 1 || Columns.IndexOf(colname) == -1)
            {
                return string.Empty;
            }
            else
            {
                return Rows[0][colname].ToString();
            }
        }


    }



    [Serializable]
    public class DTAccount : MainDT
    {
        public DTAccount()
        {
            TableName = "Account";
            Columns.Add("角色名称", typeof(String));
            Columns.Add("脚本", typeof(String));
            Columns.Add("眼睛号序号", typeof(Int32));
            Columns.Add("预警状态", typeof(Int32));
            Columns.Add("状态", typeof(String));

        }
    }
}
