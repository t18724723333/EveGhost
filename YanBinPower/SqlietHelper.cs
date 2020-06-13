using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Data.SQLite;

namespace YanBinPower
{
    public sealed class SQLiteHelper
    {
        private const string ConnDBName = "database.db";
        private static readonly Dictionary<String, ReaderWriterLock> keyvalueRW = new Dictionary<String, ReaderWriterLock>();
        private static readonly Dictionary<String, SQLiteConnection> keyValueConn = new Dictionary<string, SQLiteConnection>();
        private static readonly SQLiteHelper sqliteConn = new SQLiteHelper();

        private bool _looker;

        private SQLiteHelper()
        {
            keyvalueRW.Add("DataBase", new ReaderWriterLock());
            keyValueConn.Add("DataBase", CreateConn());
        }
        ~SQLiteHelper() { Dispose(false); }
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        protected void Dispose(bool disposing) { if (!_looker) { if (disposing) { CloseConn(); } _looker = true; } }
        private static SQLiteConnection CreateConn()
        {
            SQLiteConnection _conn = new SQLiteConnection();
            try
            {
                _conn.ConnectionString = new SQLiteConnectionStringBuilder { DataSource = ConnDBName }.ToString();
                _conn.Open();
                return _conn;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void CloseConn()
        {
            foreach (KeyValuePair<string, SQLiteConnection> item in keyValueConn)
            {
                SQLiteConnection _conn = item.Value;
                if (_conn != null && _conn.State != ConnectionState.Closed)
                {
                    try { _conn.Close(); _conn.Dispose(); _conn = null; }
                    catch (Exception exp) { exp.ToString(); }
                    finally { _conn = null; }
                }
            }
        }
        public static SQLiteHelper GetInstance() { return sqliteConn; }

        public SQLiteConnection GetConnection()
        {
            SQLiteConnection _conn = keyValueConn["DataBase"];
            try { if (_conn != null) { keyvalueRW["DataBase"].AcquireWriterLock(1000); return _conn; } }
            catch (Exception) { }
            return null;
        }
        public void ReleaseConn() { try { keyvalueRW["DataBase"].ReleaseLock(); } catch (Exception) { } }
        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句</param> 
        /// <param name="parameters">执行增删改语句的参数</param> 
        /// <returns></returns> 
        public int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            SQLiteConnection connection = GetConnection();
            int affectedRows = 0;
            using (System.Data.Common.DbTransaction transaction = connection.BeginTransaction())
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    affectedRows = command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            return affectedRows;
        }
        /// <summary> 
        /// 执行一个查询语句，返回SQLiteDataReader实例 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行查询语句的参数</param> 
        /// <returns></returns> 
        public SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)
        {
            SQLiteConnection connection = GetConnection();
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary> 
        /// 执行一个查询语句，返回一个包含查询结果的DataTable 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行查询语句的参数</param> 
        /// <returns></returns> 
        public DataTable ExecuteDataTable(string sql, params SQLiteParameter[] parameters)
        {

            SQLiteConnection connection = GetConnection();
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }

            }

        }
        /// <summary> 
        /// 执行一个查询语句，返回查询结果的第一行第一列 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            SQLiteConnection connection = GetConnection();
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable.Rows[0][0];
                }


            }
        }



    }

}
