using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;
using Libraries;

namespace DealProcessing
{

    public class DealProcessingDB
    {
        static public List<Brand> GetAllBrands()
        {
            List<Brand> brands = new List<Brand>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from brand";

            brands = DBUtil.GetListFromDataReader<Brand>(cmd);

            return brands;
        }

        static public List<Pattern> GetAllPatterns()
        {
            List<Pattern> list = new List<Pattern>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from Pattern";

            list = DBUtil.GetListFromDataReader<Pattern>(cmd);

            return list;
        }

        static public List<PatternMatch> GetAllPatternMatches()
        {
            List<PatternMatch> list = new List<PatternMatch>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from pattern_match order by Accuracy Desc";

            list = DBUtil.GetListFromDataReader<PatternMatch>(cmd);

            return list;
        }

        static public List<PatternToIgnore> GetAllPatternsToIgnore()
        {
            List<PatternToIgnore> list = new List<PatternToIgnore>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from pattern_to_ignore order by Priority desc";

            list = DBUtil.GetListFromDataReader<PatternToIgnore>(cmd);

            return list;
        }

        static public List<Store> GetAllStores()
        {
            List<Store> list = new List<Store>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from Store";

            list = DBUtil.GetListFromDataReader<Store>(cmd);

            return list;
        }
    }

}