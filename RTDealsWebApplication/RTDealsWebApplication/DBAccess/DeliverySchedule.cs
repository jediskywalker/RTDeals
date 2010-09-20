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
        public static void InsertUpdateDeliveryPlan(DeliveryPlan tmpplan )
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "sp_UpdateInsertCustomerDeliveryPlan";

                cmd.Parameters.AddWithValue("@inputCustID", tmpplan.CustomerID);
                cmd.Parameters.AddWithValue("@inputMonday", tmpplan.Monday);
                cmd.Parameters.AddWithValue("@inputTuesday", tmpplan.Tuesday);
                cmd.Parameters.AddWithValue("@inputWednesday", tmpplan.Wednesday);
                cmd.Parameters.AddWithValue("@inputThursday", tmpplan.Thursday);
                cmd.Parameters.AddWithValue("@inputFriday", tmpplan.Friday);
                cmd.Parameters.AddWithValue("@inputSaturday", tmpplan.Saturday);
                cmd.Parameters.AddWithValue("@inputSunday", tmpplan.Sunday);
                cmd.Parameters.AddWithValue("@inputAllWeekDay", tmpplan.AllWeekDay);
                cmd.Parameters.AddWithValue("@inputFirstTime", tmpplan.FirstTime);
                cmd.Parameters.AddWithValue("@inputSecondTime", tmpplan.SecondTime);
                cmd.Parameters.AddWithValue("@inputThirdTime", tmpplan.ThirdTime);
                cmd.Parameters.AddWithValue("@inputFourthTime", tmpplan.FourthTime);
                cmd.Parameters.AddWithValue("@inputFifthTime", tmpplan.FifthTime);
                cmd.Parameters.AddWithValue("@inputRealTime", tmpplan.RealTime);
                cmd.Parameters.AddWithValue("@inputInterval", tmpplan.Interval);
                cmd.Parameters.AddWithValue("@inputNightPause", tmpplan.NightPause);
                cmd.Parameters.AddWithValue("@inputLastDeliveryTime", tmpplan.LastDeliveryTime);
                
                cmd.CommandType = CommandType.StoredProcedure;


                DB.ExecuteNonQuery(cmd);


                                
            }
            catch (Exception e)
            {
                string s = e.Message;
               // return null;
            }
        }

    }
}