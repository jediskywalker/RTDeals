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

        public static List<CategoryKeywordsModel> GetCategoryKeywords()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from categorykeywords";
            cmd.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<CategoryKeywordsModel>(cmd);
        }


        public static void InsertCategory(CategoryModel cm)
        {
            if (cm.ID > 0)
                throw (new Exception("CategoryDB.Update: Category already set"));


              cm.ID = DB.Insert<CategoryModel>(cm, "category","id");
            
                
        }







    }
}