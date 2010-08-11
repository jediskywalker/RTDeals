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
using RTDealsWebApplication.Models;

namespace RTDealsWebApplication.DBAccess
{
    public class DealsDB
    {

        public static List<DealsSourceModel> GetDealsSourceByName(string SourceName)
        {
            MySqlCommand mysql = new MySqlCommand();
            mysql.CommandText = "Select * from DealsSource where SourceName='" + SourceName + "'";
            mysql.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<DealsSourceModel>(mysql);
        }


        public static List<RegexPatternModel> GetDealPetternByName(string SourceName)
        {
            MySqlCommand mysql = new MySqlCommand();
            mysql.CommandText = "Select * from regexpattern where SourceName='" + SourceName +"'";
            mysql.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<RegexPatternModel>(mysql);

        }

        public static List<DealsSourceModel> GetAllDealSource()
        {
            MySqlCommand mysql = new MySqlCommand();
            mysql.CommandText = "Select * from DealsSource";
            mysql.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<DealsSourceModel>(mysql);
        }




    }
}