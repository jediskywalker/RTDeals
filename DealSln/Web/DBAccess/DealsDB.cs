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
using Util;
using Web.Models;

namespace Web.DBAccess
{
    public class DealsDB
    {

        public static List<DealsSourceModel> GetDealsSourceByName(string SourceName)
        {
            SqlCommand mysql = new SqlCommand();
            mysql.CommandText = "Select * from DealsSource where SourceName='" + SourceName + "'";
            mysql.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<DealsSourceModel>(mysql);
        }


        public static List<RegexPatternModel> GetDealPetternByName(string SourceName)
        {
            SqlCommand mysql = new SqlCommand();
            mysql.CommandText = "Select * from regexpattern where SourceName='" + SourceName +"'";
            mysql.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<RegexPatternModel>(mysql);

        }

        public static List<DealsSourceModel> GetAllDealSource()
        {
            SqlCommand mysql = new SqlCommand();
            mysql.CommandText = "Select * from DealsSource";
            mysql.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<DealsSourceModel>(mysql);
        }




    }
}