using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using RTDealsWebApplication.Models;
using MySql.Data.MySqlClient;


namespace RTDealsWebApplication.DBAccess
{
    public class CategoryDB
    {

        public static List<CategoryModel> GetCategory()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from category";
            cmd.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<CategoryModel>(cmd);
        }

        public static List<CategoryKeywordsModel> GetCategoryKeywordsByID(int CategoryID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from categorykeywords where categoryid=" + CategoryID + " Order by keyword";
            cmd.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<CategoryKeywordsModel>(cmd);
        }

        public static List<CategoryKeywordsModel> GetCategoryKeywordsByName(string CategoryName)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "sp_getCategoryKeywordsByName";
                cmd.Parameters.AddWithValue("CategoryName", CategoryName);
                cmd.CommandType = CommandType.StoredProcedure;
                return DB.GetListFromDataReader<CategoryKeywordsModel>(cmd);
            }
            catch (Exception e)
            {

                string s = e.Message;
                return null;
            }

        }



        public static void InsertCategory(CategoryModel cm)
        {
            if (cm.CategoryID > 0)
                throw (new Exception("CategoryDB.Update: Category already set"));
              cm.CategoryID = DB.Insert<CategoryModel>(cm, "category","categoryid");
                   
        }


        public static void InsertCategoryKeywords(CategoryKeywordsModel ckm)
        {

            if (ckm.CategoryKeywordID >0)
                throw (new Exception("CategoryDB.Update: Category already set"));

            ckm.CategoryKeywordID = DB.Insert<CategoryKeywordsModel>(ckm, "categorykeywords", "GetCategoryKeywordsByID");
        }

        public static void UpdateCustomerCategoryKeywords(int CustomerID, int CategoryKeywordID,bool isIn)
        {
             try
                {
                    if (isIn)
                    {

                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandText = "insert into customercategorykeywords(customerid,categorykeywordid) values(" + CustomerID + "," + CategoryKeywordID + ")";
                        cmd.CommandType = CommandType.Text;
                        DB.ExecuteNonQuery(cmd);

                    }
                    else
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandText = "delete from customercategorykeywords where customerid=" + CustomerID + " and categorykeywordid=" + CategoryKeywordID;
                        cmd.CommandType = CommandType.Text;
                        DB.ExecuteNonQuery(cmd);
                    }
                }
             catch (Exception e)
             {
                 throw (new Exception(e.Message));

             }

              
        }

        public static bool IsExsitCustomerCategoryKeywords(int CustomerID, int CategoryKeywordID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from customercategorykeywords where CustomerID=" + CustomerID + " and CategoryKeywordID= " + CategoryKeywordID;
            cmd.CommandType=CommandType.Text;
            List<CustomerCategoryKeywordsModel> lcckm=DB.GetListFromDataReader<CustomerCategoryKeywordsModel>(cmd);
            if (lcckm.Count>0)
                return true;
            else
                return false;



        }

    }
}