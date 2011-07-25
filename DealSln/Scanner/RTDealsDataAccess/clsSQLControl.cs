using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace RTDealsScanerEngine.RTDealsDataAccess
{
    class clsSQLControl
    {

        public clsSQLControl()
        {
        }

        public static SqlConnection CreateConnection()
        {
            SqlConnection conn = null;
            //create connection string
            try
            {
                string connstr = "server=192.168.1.107;user id=jediskywalker;password=12345678;database=rtdeals";  
                //string connstr = ConfigurationManager.AppSettings["DialerDBConStr"];
                conn = new SqlConnection(connstr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return conn;
        }
    }
}
