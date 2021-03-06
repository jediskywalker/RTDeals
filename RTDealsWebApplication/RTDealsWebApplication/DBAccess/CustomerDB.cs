﻿using System;
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
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using RTDealsWebApplication.DBAccess;


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
        public static CustomerModel getCustomerByUsername(string Username)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from Customer where Username=@username";
            cmd.Parameters.AddWithValue("@username", Username);
            List<CustomerModel> customer = DB.GetListFromDataReader<CustomerModel>(cmd);
            if (customer.Count == 1)
                return customer[0];
            else if(customer.Count>1)
                Logging.Log(LoggingLevel.ERROR, "SelectUserByUsername", "Multiple record returned for username=" + Username, null);
            return null;
            
        }
        public static void CreateCustomer(CustomerModel Customer)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Insert into Customer(Email,Password,LastIPAddress,LastLongitude,LastLatitude,LastCity,LastZipCode,LastCountryName,LastTimeZone,status,SignUpdate,IsNew)" +
                    "values('" + Customer.Email + "','" + Customer.Password + "','" + Customer.LastIPAddress + "','" + Customer.LastLongitude + "','" + Customer.LastLatitude + "','" + Customer.LastCity + "','" + Customer.LastZipCode + "','" + Customer.LastCountryName + "','" + Customer.LastTimeZone + "','" + 'I' + "' , " + "NOW()" + " , '" + 1 + "')";
                cmd.CommandType = CommandType.Text;
                DB.ExecuteNonQuery(cmd);
            }
            catch (Exception e)
            {
                string s = e.Message;

            }

        }

    }
}