using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Utilities;
using MySql.Data.MySqlClient;

namespace RTDealsWebApplication.DBAccess
{
    public class DB
    {
        protected static string ConnectionString()
        {
            string connstr = ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString;
            if (string.IsNullOrEmpty(connstr))
            {
                Logging.Log(LoggingLevel.ERROR, "DB.OpenConnection", "Missing MySQLConnStr", null);
                throw (new Exception("Missing connection string MySQLConnStr"));
            }
            return connstr;
        }

        protected static MySqlConnection OpenConnection()
        {
            string connstr = ConnectionString();
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connstr);
                conn.Open();
            }
            catch (Exception ex)
            {
                Logging.Log(LoggingLevel.ERROR, "DB.OpenConnection", "Failed to open connection for " + connstr, ex);
                throw (new Exception("DB: Failed to open connection"));
            }
            finally
            {
            }

            return conn;
        }

        protected static void CloseConnection(MySqlConnection conn)
        {
            conn.Close();
        }



        static public int ExecuteNonQuery(string sql, int expectAffected)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.CommandText = sql;
            sqlcmd.CommandType = CommandType.Text;
            return ExecuteNonQuery(sqlcmd, expectAffected);
        }

        static public int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, -1);
        }

        static public int ExecuteNonQuery(MySqlCommand sqlcmd, int expectAffected)
        {
            int rowAffected = -1;

            MySqlConnection conn = OpenConnection();
            sqlcmd.Connection = conn;

            MySqlTransaction sqltrans = null;
            sqltrans = conn.BeginTransaction();
            sqlcmd.Transaction = sqltrans;

            try
            {
                rowAffected = sqlcmd.ExecuteNonQuery();

                // if affected count not expected, throw exceptio and rollback
                if (expectAffected != -1 && rowAffected != expectAffected)
                {
                    throw (new Exception("Expect " + expectAffected + " but got " + rowAffected));
                }
                sqltrans.Commit();
            }
            catch (Exception ex)
            {
                sqltrans.Rollback();
                throw (ex);
            }
            finally
            {
                sqltrans.Dispose();
                CloseConnection(conn);
            }
            return rowAffected;
        }

        static public int ExecuteNonQuery(MySqlCommand sqlcmd)
        {
            return ExecuteNonQuery(sqlcmd, -1);
        }

        static public void FillDataSet(string sql, ref DataSet ds)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.CommandText = sql;
            sqlcmd.CommandType = CommandType.Text;
            FillDataSet(sqlcmd, ref ds);
        }

        static public void FillDataSet(MySqlCommand sqlcmd, ref DataSet ds)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlConnection conn = OpenConnection();
            sqlcmd.Connection = conn;
            da.SelectCommand = sqlcmd;

            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception("DB.FillDataSet(MySqlCommand):" + ex.Message);
            }
            finally
            {
                da.Dispose();
                CloseConnection(conn);
            }
        }

        static public DataTable GetDataTable(string table, string sql)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand sqlcmd = new MySqlCommand();
            MySqlConnection conn = OpenConnection();
            sqlcmd.Connection = conn;
            sqlcmd.CommandText = sql;
            da.SelectCommand = sqlcmd;
            DataTable dt = new DataTable(table);

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("DBUtils.GetDataTable():" + ex.Message + " " + ex.StackTrace);
            }
            finally
            {
                sqlcmd.Dispose();
                da.Dispose();
                CloseConnection(conn);
            }
            return dt;
        }

        //Execute sql to return a MySqlDataReader
        static public MySqlDataReader ExecuteReader(string sql)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.CommandText = sql;
            sqlcmd.CommandType = CommandType.Text;

            return ExecuteReader(sqlcmd);
        }

        //Execute sqlcommand to return a MySqlDataReader
        static public MySqlDataReader ExecuteReader(MySqlCommand sqlcmd)
        {
            MySqlDataReader reader = null;
            MySqlConnection conn = OpenConnection();
            sqlcmd.Connection = conn;

            try
            {
                reader = sqlcmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception("DBUtils.ExecuteReader(sqlcmd):" + ex.Message);
            }
            finally
            {
            }

            return reader;
        }


        static public object ExecuteScalar(string sql)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.CommandText = sql;
            sqlcmd.CommandType = CommandType.Text;

            return ExecuteScalar(sqlcmd);
        }

        static public object ExecuteScalar(MySqlCommand sqlcmd)
        {
            object obj = null;
            MySqlConnection conn = OpenConnection();
            sqlcmd.Connection = conn;

            try
            {
                obj = sqlcmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("DBUtils.ExecuteScalar(sqlcmd):" + ex.Message);
            }
            finally
            {
                CloseConnection(conn);
            }
            return obj;
        }


        private static List<T> mapList<T>(MySqlDataReader dr)
        {
            List<T> list = new List<T>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            while (dr.Read())
            {
                T t = Activator.CreateInstance<T>();

                for (int i = 0; i < dr.FieldCount; i++)
                {
                    string column = dr.GetName(i);

                    foreach (PropertyInfo pi in properties)
                    {
                        if (pi.Name.Equals(column, StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (!(dr[pi.Name] is System.DBNull))
                                pi.SetValue(t, dr[pi.Name], null);
                        }
                    }

                }
                list.Add(t);
            }

            return list;
        }

        public static List<T> GetListFromDataReader<T>(MySqlCommand sqlcmd)
        {
            List<T> result = new List<T>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString()))
            {
                conn.Open();
                sqlcmd.Connection = conn;
                using (MySqlDataReader sqldr = sqlcmd.ExecuteReader())
                {
                    result = mapList<T>(sqldr);
                }
            }
            return result;
        }

        public static List<TableColumnInfo> GetTableColumnInfo(string tablename)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select COLUMN_NAME,IS_NULLABLE,DATA_TYPE from information_schema.columns where table_name = @table";
            cmd.Parameters.AddWithValue("@table", tablename);

            return GetListFromDataReader<TableColumnInfo>(cmd);
        }

        public static int Insert<T>(T model, string tablename, string identityColumn)
        {
            if (identityColumn == null) identityColumn = "";

            // get column info
            string cacheKey = "TableColumnInfo-" + tablename;
            List<TableColumnInfo> columns = (List<TableColumnInfo>)Utilities.CacheUtil.GetCacheItem(cacheKey);
            if (columns == null)
            {
                columns = GetTableColumnInfo(tablename);
                Utilities.CacheUtil.SetCacheItem("TableColumnInfo-" + tablename, columns, -1);
            }

            // get type T properties
            cacheKey = "PropertyInfo-" + typeof(T).ToString();
            PropertyInfo[] properties = (PropertyInfo[])Utilities.CacheUtil.GetCacheItem(cacheKey);
            if (properties == null)
            {
                properties = typeof(T).GetProperties();
                Utilities.CacheUtil.SetCacheItem(cacheKey, properties, -1);
            }

            MySqlCommand sqlcmd = new MySqlCommand();
            string colToInsert = "", colvalues = "";

            foreach (TableColumnInfo col in columns)
            {
                if (col.COLUMN_NAME.Equals(identityColumn, StringComparison.InvariantCultureIgnoreCase)) continue;

                PropertyInfo p = null;

                foreach (PropertyInfo pi in properties)
                {
                    if (pi.Name.Equals(col.COLUMN_NAME, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (col.IS_NULLABLE == "YES")
                        {
                            if (pi.GetValue(model, null) == null) break;
                        }
                        p = pi;
                        break;
                    }
                }

                if (p != null)
                {
                    if (colToInsert != "")
                    {
                        colToInsert += ",";
                        colvalues += ",";
                    }
                    colToInsert += col.COLUMN_NAME;
                    colvalues += "@" + col.COLUMN_NAME;
                    sqlcmd.Parameters.AddWithValue("@" + col.COLUMN_NAME, p.GetValue(model, null));
                }
            }
            sqlcmd.CommandText = "insert [" + tablename + "] (" + colToInsert + ") values (" + colvalues + ")";

            int identity = 0;
            if (identityColumn == "")
                ExecuteNonQuery(sqlcmd);
            else
            {
                // get the new identity value back
                sqlcmd.CommandText += "; select scope_identity()";
                object obj = ExecuteScalar(sqlcmd);
                identity = Convert.ToInt32(obj);
            }

            return identity;
        }
     
        public static int Update<T>(T model, string tablename, string keyColumns, int expectAffected)
        {
            if (keyColumns == null) throw(new Exception("DB.Update: missing keyColumns"));
            keyColumns = "," + keyColumns + ",";

            // get column info
            string cacheKey = "TableColumnInfo-" + tablename;
            List<TableColumnInfo> columns = (List<TableColumnInfo>)Utilities.CacheUtil.GetCacheItem(cacheKey);
            if (columns == null)
            {
                columns = GetTableColumnInfo(tablename);
                Utilities.CacheUtil.SetCacheItem("TableColumnInfo-" + tablename, columns, -1);
            }


            // get type T properties
            cacheKey = "PropertyInfo-" + typeof(T).ToString();
            PropertyInfo[] properties = (PropertyInfo[])Utilities.CacheUtil.GetCacheItem(cacheKey);
            if (properties == null)
            {
                properties = typeof(T).GetProperties();
                Utilities.CacheUtil.SetCacheItem(cacheKey, properties, -1);
            }

            MySqlCommand sqlcmd = new MySqlCommand();
            string colToUpdate = "", where = "";

            foreach (TableColumnInfo col in columns)
            {
                PropertyInfo p = null;
                bool key = false;
                foreach (PropertyInfo pi in properties)
                {
                    if (pi.Name.Equals(col.COLUMN_NAME, StringComparison.InvariantCultureIgnoreCase))
                    {
                        p = pi;

                        // check if key column
                        if (keyColumns.ToUpper().Contains(pi.Name.ToUpper()))
                        {
                            key = true;
                            p = pi;
                            break;
                        }
                        break;
                    }
                }

                if (p != null)
                {
                    if (key)
                    {
                        if (where != "") where += " and ";
                        where += " " + col.COLUMN_NAME + "=@" + col.COLUMN_NAME;
                        sqlcmd.Parameters.AddWithValue("@" + col.COLUMN_NAME, p.GetValue(model, null));
                    }
                    else
                    {
                        if (colToUpdate != "") colToUpdate += ",";
                        if (p.GetValue(model, null) == null)
                            colToUpdate += col.COLUMN_NAME + "=null";
                        else
                        {
                            colToUpdate += col.COLUMN_NAME + "=@" + col.COLUMN_NAME;
                            sqlcmd.Parameters.AddWithValue("@" + col.COLUMN_NAME, p.GetValue(model, null));
                        }
                    }
                    
                }
            }
            sqlcmd.CommandText = "update [" + tablename + "] set " + colToUpdate + " where " + where;

            int cnt = ExecuteNonQuery(sqlcmd, expectAffected);
            return cnt;
        }


        public static int Delete<T>(T model, string tablename, string keyColumns, int expectAffected)
        {
            if (keyColumns == null) throw (new Exception("DB.Update: missing keyColumns"));
              keyColumns = "," + keyColumns + ",";

            // get column info
            string cacheKey = "TableColumnInfo-" + tablename;
            List<TableColumnInfo> columns = (List<TableColumnInfo>)Utilities.CacheUtil.GetCacheItem(cacheKey);
            if (columns == null)
            {
                columns = GetTableColumnInfo(tablename);
                Utilities.CacheUtil.SetCacheItem("TableColumnInfo-" + tablename, columns, -1);
            }


            // get type T properties
            cacheKey = "PropertyInfo-" + typeof(T).ToString();
            PropertyInfo[] properties = (PropertyInfo[])Utilities.CacheUtil.GetCacheItem(cacheKey);
            if (properties == null)
            {
                properties = typeof(T).GetProperties();
                Utilities.CacheUtil.SetCacheItem(cacheKey, properties, -1);
            }

            MySqlCommand sqlcmd = new MySqlCommand();
            string colToUpdate = "", where = "";

            foreach (TableColumnInfo col in columns)
            {
                PropertyInfo p = null;
                bool key = false;
                foreach (PropertyInfo pi in properties)
                {
                    if (pi.Name.Equals(col.COLUMN_NAME, StringComparison.InvariantCultureIgnoreCase))
                    {
                        p = pi;

                        // check if key column
                        if (keyColumns.ToUpper().Contains(pi.Name.ToUpper()))
                        {
                            key = true;
                            p = pi;
                            break;
                        }
                        break;
                    }
                }

                if (p != null)
                {
                    if (key)
                    {
                        if (where != "") where += " and ";
                        where += " " + col.COLUMN_NAME + "=@" + col.COLUMN_NAME;
                        sqlcmd.Parameters.AddWithValue("@" + col.COLUMN_NAME, p.GetValue(model, null));
                    }
                    else
                    {
                        if (colToUpdate != "") colToUpdate += ",";
                        if (p.GetValue(model, null) == null)
                            colToUpdate += col.COLUMN_NAME + "=null";
                        else
                        {
                            colToUpdate += col.COLUMN_NAME + "=@" + col.COLUMN_NAME;
                            sqlcmd.Parameters.AddWithValue("@" + col.COLUMN_NAME, p.GetValue(model, null));
                        }
                    }

                }
            }
            sqlcmd.CommandText = "delete from [" + tablename + "] "  + " where " + where;

            int cnt = ExecuteNonQuery(sqlcmd, expectAffected);
            return cnt;
        }

        public static int Delete<T>(T model, string tablename, string keyColumns)
        {
            return Update<T>(model, tablename, keyColumns, -1);
        }


        public static int Update<T>(T model, string tablename, string keyColumns)
        {
            return Update<T>(model, tablename, keyColumns, -1);
        }
    }

    public class TableColumnInfo
    {
        public string COLUMN_NAME { get; set; }
        public string IS_NULLABLE { get; set; }
        public string DATA_TYPE { get; set; }
    }
}