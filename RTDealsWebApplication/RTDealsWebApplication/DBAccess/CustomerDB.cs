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
    public class CustomerDB
    {
        //public static string getConfigValue(string ConfigName)
        //{

        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "Select * from config where ConfigName='" + ConfigName +"'";
        //    cmd.CommandType = CommandType.Text;
        //    List<ConfigModel> config= DB.GetListFromDataReader<ConfigModel>(cmd);

        //    return config[0].configValue;


        //}


        public static List<CustomerModel> getCustomer()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from Customer";
            cmd.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<CustomerModel>(cmd);

        }

    }
}