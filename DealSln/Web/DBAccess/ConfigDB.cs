using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Util;
using Web.Models;

namespace DBAccess
{
    public class ConfigDB
    {
        //public static string getConfigValue(string ConfigName)
        //{

            //    SqlCommand cmd = new SqlCommand();
            //    cmd.CommandText = "Select * from config where ConfigName='" + ConfigName +"'";
            //    cmd.CommandType = CommandType.Text;
            //    List<ConfigModel> config= DB.GetListFromDataReader<ConfigModel>(cmd);

            //    return config[0].configValue;


            //}
            //public static  List<ConfigModel> GettConfigAll()
            //{
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.CommandText = "Select * from config";
            //    cmd.CommandType = CommandType.Text;
            //    return DB.GetListFromDataReader<ConfigModel>(cmd);
            //}

            //public static string getConfigNote(string ConfigName)
            //{

            //    SqlCommand cmd = new SqlCommand();
            //    cmd.CommandText = "Select * from config where ConfigName='" + ConfigName + "'";
            //    cmd.CommandType = CommandType.Text;
            //    List<ConfigModel> config = DB.GetListFromDataReader<ConfigModel>(cmd);

            //    return config[0].Note;


            //}




            //static public string Update(ConfigModel cm)
            //{

            //    if (cm.configValue == getConfigValue(cm.configName) && cm.Note == getConfigNote(cm.configName))
            //    {
            //        return "";

            //    }
            //    else
            //    {
            //        DB.Update<ConfigModel>(cm,"Config","ConfigName",1);
            //        return "Updated!";
            //    }

            //}
       // }

    }
}