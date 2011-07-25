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
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using Web.DBAccess;


namespace Web.DBAccess
{
    public class CustomerDB
    {
        //public static string getConfigValue(string ConfigName)
        //{

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "Select * from config where ConfigName='" + ConfigName +"'";
        //    cmd.CommandType = CommandType.Text;
        //    List<ConfigModel> config= DB.GetListFromDataReader<ConfigModel>(cmd);

        //    return config[0].configValue;


        //}


        public static List<CustomerModel> getCustomer()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Customer";
            cmd.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<CustomerModel>(cmd);

        }
        public static CustomerModel getCustomerByUsername(string Username)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Customer where Username=@username";
            cmd.Parameters.AddWithValue("@username", Username);
            List<CustomerModel> customer = DB.GetListFromDataReader<CustomerModel>(cmd);
            if (customer.Count == 1)
                return customer[0];
            else if(customer.Count>1)
                Logger.Log(LogLevel.ERROR, "SelectUserByUsername", "Multiple record returned for username=" + Username, null);
            return null;
            
        }
        public static void CreateCustomer(CustomerModel Customer)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
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