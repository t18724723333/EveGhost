using System;
using System.Data;
using System.Data.SQLite;

namespace YanBinPower
{
    public static class DataBaseHelper
    {

        public static DataTable GetMenuStrip(string permission)
        {
            try
            {
                DataTable _dataTable = new DataTable("MenuStrip");
                _dataTable = SQLiteHelper.GetInstance().ExecuteDataTable(string.Format("SELECT * FROM menustrip  WHERE perid = {0} ORDER BY colid ASC", permission));
                return _dataTable;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                SQLiteHelper.GetInstance().ReleaseConn();
            }
        }

    }
}
