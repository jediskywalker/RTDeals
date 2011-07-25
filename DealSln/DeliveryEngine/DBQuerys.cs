using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

// 9.12.2010 Li
namespace DeliveryEngine
{
    class DBQuerys
    {

        public static string connStr = "server=192.168.1.107;database=rtdeals;uid=jediskywalker;password=12345678;";
        
        public static Deals GetOneDeal(int dealid)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            SqlDataReader dr = null;
            Deals tmpModel = new Deals();
            try
            {
                cmd.CommandText = "select * from rssdeals where dealsid =" + dealid;
                conn.Open();
                cmd.CommandTimeout = 60; // increase timeout to 60, just in case of busy or locking
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    tmpModel.PubDate = (string)dr["PubDate"];
                    tmpModel.dealsID = (int)dr["dealsID"];
                    tmpModel.URL = (string)dr["URL"];
                    tmpModel.Title = (string)dr["Title"];
                    tmpModel.InDate = (DateTime)dr["inTime"];
                    tmpModel.IsDrug = (bool)dr["IsDrug"];
                    tmpModel.IsFinance = (bool)dr["IsFinance"];
                    //tmpModel.IsFree = (bool)dr["IsFree"];
                    tmpModel.Ishot = (bool)dr["Ishot"];
                    tmpModel.IsTravel = (bool)dr["IsTravel"];
                    tmpModel.IsElectronic = (bool)dr["IsElectronic"];
                    tmpModel.IsAppeal = (bool)dr["IsAppeal"];
                    tmpModel.IsAppliances = (bool)dr["IsAppliances"];
                    tmpModel.IsBeauty = (bool)dr["IsBeauty"];
                    tmpModel.IsJewelry = (bool)dr["IsJewelry"];
                    tmpModel.IsOfficeSupplies = (bool)dr["IsOfficeSupplies"];
                    tmpModel.IsRestaurant = (bool)dr["IsRestaurant"];                    
                    tmpModel.IsOthers = (bool)dr["IsOthers"];                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
                if (dr != null) dr.Dispose();
                cmd.Dispose();
            }

            return tmpModel;

        
        }
                // 
        public static List<ScheduledDelivery> GetAllScheduled2Deliver()
        {
             DBisNULLUtility isNull = new DBisNULLUtility();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataReader dr = null;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            List<ScheduledDelivery> tmpList = new List<ScheduledDelivery>();

            try
            {
                cmd.CommandText = "sp_getScheduledDelivery";
                conn.Open();
                cmd.CommandTimeout = 60; // increase timeout to 60, just in case of busy or locking
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ScheduledDelivery tmpModel = new ScheduledDelivery();

                    tmpModel.scheduleID = (int)dr["scheduleID"];
                    tmpModel.Email = (string) dr["Email"];
                    tmpModel.dealsID= (string)dr["dealsID"];
                    tmpModel.customerID = (int)dr["customerID"];
                    tmpModel.FirstName= (string)dr["FirstName"];
                    tmpModel.LastName = (string)dr["LastName"];
                    tmpModel.Keywords = isNull.isDBNull(dr["keywords"], "");

                    tmpList.Add(tmpModel);

                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
                if (dr != null) dr.Dispose();
                cmd.Dispose();
            }

            return tmpList;

        }

        public static void MoveScheduled2Delivered(int scheduleID)
        { 
            //sp_movedelivedtohistory
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                cmd.CommandText = "sp_movedelivedtohistory";
                cmd.Parameters.AddWithValue("@deliveryedScheduleID", scheduleID);
                
                conn.Open();
                cmd.CommandTimeout = 60; // increase timeout to 60, just in case of busy or locking
                int y = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
     
        }

        public static List<Deals> GrabLatestDeals(bool newdealsonly)
        {
            // sp_getnewdeals
            
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataReader dr = null;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            List<Deals> tmpList = new List<Deals>();

            try
            {
                cmd.CommandText = "sp_getnewdeals";
                cmd.Parameters.AddWithValue("@newdealsonly",newdealsonly);
                conn.Open();
                cmd.CommandTimeout = 60; // increase timeout to 60, just in case of busy or locking
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Deals tmpModel = new Deals();
                                       
                    tmpModel.dealsID = (int)dr["dealsID"];
                    tmpModel.Title = (string)dr["Title"];
                    tmpModel.URL = (string)dr["URL"];
                    tmpModel.Ishot = (bool)dr["Ishot"];
                   
                    tmpList.Add(tmpModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
                if (dr != null) dr.Dispose();
                cmd.Dispose();
            }

            return tmpList;
     
        }

        public static List<Customers> GetAllCustomers(bool isNew)
        {
            //sp_getactivecustomerstomatchnewdeal
            
             DBisNULLUtility isNull = new DBisNULLUtility();
            
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataReader dr = null;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            List<Customers> tmpList = new List<Customers>();

            try
            {
                cmd.CommandText = "sp_getactivecustomerstomatchnewdeal";
                cmd.Parameters.AddWithValue("@isNewInput", isNew);
                conn.Open();
                cmd.CommandTimeout = 60; // increase timeout to 60, just in case of busy or locking
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Customers tmpModel = new Customers();

                    tmpModel.CustomerID = (int)dr["CustomerID"];
                    tmpModel.Email = isNull.isDBNull(dr["Email"],"");
                    tmpModel.FirstName = isNull.isDBNull(dr["FirstName"],"");
                    tmpModel.LastName = isNull.isDBNull(dr["LastName"],"");
                    tmpModel.Catekeywords = isNull.isDBNull(dr["catekeywords"],"");
                    tmpModel.Custkeywords = isNull.isDBNull(dr["custkeywords"],"");
                    
                    tmpModel.KeyWords = (tmpModel.Catekeywords.Trim(',') + "," + tmpModel.Custkeywords.Trim(',')).Trim(',');

                    tmpList.Add(tmpModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
                if (dr != null) dr.Dispose();
                cmd.Dispose();
            }

            return tmpList;
        }

        public static void UpdateScheduleTable(int customerID, string dealIDs, bool newcust,string keywords)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.CommandText = "sp_updateScheduled";
                cmd.Parameters.AddWithValue("@customerIDinput", customerID);
                cmd.Parameters.AddWithValue("@dealIDinput", dealIDs);
                cmd.Parameters.AddWithValue("@newcust",newcust);
                cmd.Parameters.AddWithValue("@keywordsin",keywords);
                conn.Open();
                cmd.CommandTimeout = 60; // increase timeout to 60, just in case of busy or locking
                int y = cmd.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();                
                cmd.Dispose();
            }
     
        }

        private void testDB()
        {
            
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataReader dr = null;
            cmd.CommandType = System.Data.CommandType.Text;

            try
            {
                cmd.CommandText = "select * from rssdeals";

                conn.Open();

                cmd.CommandTimeout = 60; // increase timeout to 60, just in case of busy or locking

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    string y = (string)dr["Title"];
                    Console.WriteLine(y);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // LogMsg(INFO, "[ARE] got:" + todo.Count.ToString());
                conn.Close();
                if (dr != null) dr.Dispose();
                cmd.Dispose();

            }
        }
    }
}
