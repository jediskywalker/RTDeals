using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace RTDealsScanerEngine.RTDealsDataAccess
{
    class clsSQLControl
    {

        public clsSQLControl()
        {
        }

        public static MySqlConnection CreateConnection()
        {
            MySqlConnection conn = null;
            //create connection string
            try
            {
                string connstr = "server=192.168.1.107;user id=jediskywalker;password=19810408;database=rtdeals";  
                //string connstr = ConfigurationManager.AppSettings["DialerDBConStr"];
                conn = new MySqlConnection(connstr);
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
