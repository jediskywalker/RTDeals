using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using RTDealsScanerEngine.RTDealsDataAccess;

namespace RTDealsScanerEngine.RTDealsDataAccess
{
    class DBUtils
    {
        private SqlConnection con;

        public DBUtils()
        {
            //attempt to pull sql connection from cache
            try
            {
                //create new connection to db
                con = clsSQLControl.CreateConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void OpenConnection()
        {
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                con.Open();
        }

        public void CloseConnection()
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }

        public int ExecuteNonQuery(string sql)
        {
            int rowAffected = -1;
            OpenConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlTransaction sqltrans = con.BeginTransaction();
            cmd.Transaction = sqltrans;
            cmd.CommandText = sql;

            try
            {
                rowAffected = cmd.ExecuteNonQuery();
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
                cmd.Dispose();
                CloseConnection();
            }
            return rowAffected;
        }
        public int ExecuteNonQuery(SqlCommand sqlCommand)
        {
            int rowAffected = -1;
            OpenConnection();

            sqlCommand.Connection = con;
            SqlTransaction sqltrans = con.BeginTransaction();
            sqlCommand.Transaction = sqltrans;

            try
            {
                rowAffected = sqlCommand.ExecuteNonQuery();
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
                sqlCommand.Dispose();
                CloseConnection();
            }
            return rowAffected;
        }
        /*
		private void CommitTransaction()
		{
			sqltrans.Commit();
			sqltrans=null;
		}

		private void RollbackTransaction()
		{
			sqltrans.Rollback();
			sqltrans=null;
		}
        */

        public void FillDataSet(SqlCommand cmdin, ref DataSet ds)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            OpenConnection();
            cmdin.Connection = con;
            da.SelectCommand = cmdin;

            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception("DBUtils.FillDataSet(SqlCommand):" + ex.Message);
            }
            finally
            {
                da.Dispose();
                CloseConnection();
            }
        }

        public void FillDataSet(string sql, ref DataSet ds)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception("DBUtils.FillDataSet():" + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                da.Dispose();
                CloseConnection();
            }
        }

        public DataTable GetDataTable(string table, string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            OpenConnection();
            cmd.Connection = con;
            cmd.CommandText = sql;
            da.SelectCommand = cmd;
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
                cmd.Dispose();
                da.Dispose();
                CloseConnection();
            }
            return dt;
        }
        //Execute sqlcommand to return a SqlDataReader
        //Caller needs to close connection after using the returned SqlDataReader
        public SqlDataReader ExecuteReader(SqlCommand sqlcmd)
        {
            SqlDataReader reader = null;
            OpenConnection();
            sqlcmd.Connection = con;

            try
            {
                reader = sqlcmd.ExecuteReader();//CommandBehavior.CloseConnection);
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
        //Execute sql to return a SqlDataReader
        //Caller needs to close connection after using the returned SqlDataReader
        public SqlDataReader ExecuteReader(string sql)
        {
            SqlDataReader reader = null;
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                throw new Exception("DBUtils.ExecuteReader():" + ex.Message);
            }
            finally
            {
                cmd.Dispose();
            }
            return reader;
        }

        public object ExecuteScalar(string sql)
        {
            object obj = null;
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;

            try
            {
                obj = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("DBUtils.ExecuteScalar():" + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                CloseConnection();
            }
            return obj;
        }

        public object ExecuteScalar(SqlCommand sqlcmd)
        {
            object obj = null;
            OpenConnection();
            sqlcmd.Connection = con;

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
                //sqlcmd.Dispose();
                CloseConnection();
            }
            return obj;
        }

        public int ExecuteCommand(SqlCommand pcmd)
        {
            int rowsAffected = -1;

            OpenConnection();
            pcmd.Connection = con;
            SqlTransaction sqltrans = con.BeginTransaction();
            pcmd.Transaction = sqltrans;

            try
            {
                rowsAffected = pcmd.ExecuteNonQuery();
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
                CloseConnection();
            }
            return rowsAffected;
        }

        internal void FillDataSet(string p, ref ScanerDataSet ds)
        {
            throw new NotImplementedException();
        }
    }
}
