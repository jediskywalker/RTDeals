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
    public class RssSeedDB
    {
        public static SourceRssSeedModel GetSourceRssSeedByID(int SourceID)
        {
            SqlCommand mysql = new SqlCommand();
            mysql.CommandText = "Select * from Sourcerssseed where SourceID=" + SourceID;
            mysql.CommandType = CommandType.Text;
            if (DB.GetListFromDataReader<SourceRssSeedModel>(mysql).Count == 1)
                return DB.GetListFromDataReader<SourceRssSeedModel>(mysql)[0];
            else
                return null;
        }

        public static List<HomePageSearchModel> GetHomeSearchResult(string keyword)
        {

            SqlCommand mysql = new SqlCommand();
            mysql.CommandText = "Select * from rssdeals where title like '%" + keyword + "%' order by dealsid desc";
            mysql.CommandType = CommandType.Text;
            return DB.GetListFromDataReader<HomePageSearchModel>(mysql);
         
        }

        public static void GetList(string keyword)
        {
            SqlCommand mysql = new SqlCommand();
            mysql.CommandText="select count(*) from rssdeals where title like '%" + keyword + "%' order by dealsid desc";
            mysql.CommandType=CommandType.Text;
            MyPaperControls.MyPaper MyPaper1 = new MyPaperControls.MyPaper();
            MyPaper1.RecordCount = Convert.ToInt32(DB.ExecuteScalar(mysql));
            //MyPaper1.DataSet_StartIndex = 50;
            MyPaper1.PageSize = 100;
            // MyPaper1.RecordCount = (int)mysql.

        }


    }
}