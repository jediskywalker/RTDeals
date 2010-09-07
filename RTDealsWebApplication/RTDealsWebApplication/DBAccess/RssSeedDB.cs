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
    public class RssSeedDB
    {
        public static SourceRssSeedModel GetSourceRssSeedByID(int SourceID)
        {
            MySqlCommand mysql = new MySqlCommand();
            mysql.CommandText = "Select * from Sourcerssseed where SourceID=" + SourceID;
            mysql.CommandType = CommandType.Text;
            if (DB.GetListFromDataReader<SourceRssSeedModel>(mysql).Count == 1)
                return DB.GetListFromDataReader<SourceRssSeedModel>(mysql)[0];
            else
                return null;
        }


    }
}