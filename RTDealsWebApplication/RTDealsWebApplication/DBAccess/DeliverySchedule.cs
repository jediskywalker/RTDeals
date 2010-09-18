using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using RTDealsWebApplication.Models;
using MySql.Data.MySqlClient;


namespace RTDealsWebApplication.DBAccess
{
    public class DeliverySchedule
    {
        public static void InsertUpdateDeliveryPlan()
        {

            try
            {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "insert into customercategorykeywords(customerid,categorykeywordid) values()";
                    cmd.CommandType = CommandType.Text;
                    DB.ExecuteNonQuery(cmd);

                                
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));

            }
        }

    }
}