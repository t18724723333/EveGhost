using System;
using System.Data;

namespace MODEL
{

    //public static class GetDT
    //{
    //    public static DataTable Account()
    //    {
    //        DataTable dt = new DataTable("Account");
    //        dt.Columns.Add("选择", typeof(Boolean));
    //        dt.Columns.Add("序号", typeof(String));
    //        dt.Columns.Add("角色名称", typeof(String));
    //        dt.Columns.Add("状态", typeof(String));
    //        return dt;
    //    }
    //}
    public class MainDataTable : DataTable
    {
        public MainDataTable() { Columns.Add("选择", typeof(Boolean)); Columns.Add("序号", typeof(Int32)); }

        public string GetString(string colname)
        {
            Console.WriteLine(this.Rows.Count);
            Console.WriteLine(Columns.IndexOf(colname));

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
    public class DTAccount : MainDataTable
    {
        public DTAccount()
        {
            Columns.Add("角色名称", typeof(String));
            Columns.Add("状态", typeof(String));
        }

    }



}