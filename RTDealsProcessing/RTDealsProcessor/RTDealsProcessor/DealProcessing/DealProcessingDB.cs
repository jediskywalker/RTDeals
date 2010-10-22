using System;
using System.Collections.Generic;
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

    }

}