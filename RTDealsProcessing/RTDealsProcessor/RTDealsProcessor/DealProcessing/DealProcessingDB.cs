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
        #region load all lock up tables

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
            cmd.CommandText = "select * from pattern_to_ignore order by Accuracy desc";

            list = DBUtil.GetListFromDataReader<PatternToIgnore>(cmd);

            return list;
        }


        static public List<Category> GetAllCategories()
        {
            List<Category> list = new List<Category>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from Category";

            list = DBUtil.GetListFromDataReader<Category>(cmd);

            return list;
        }

        static public List<SubCategory> GetAllSubCategories()
        {
            List<SubCategory> list = new List<SubCategory>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from SubCategory";

            list = DBUtil.GetListFromDataReader<SubCategory>(cmd);

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

        static public List<DealSource> GetAllDealSources()
        {
            List<DealSource> list = new List<DealSource>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from dealssource";

            list = DBUtil.GetListFromDataReader<DealSource>(cmd);

            return list;
        }

        static public List<Brand> GetAllBrands()
        {
            List<Brand> list = new List<Brand>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from Brand";

            list = DBUtil.GetListFromDataReader<Brand>(cmd);

            return list;
        }

        static public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from Product";

            list = DBUtil.GetListFromDataReader<Product>(cmd);

            return list;
        }

        static public List<ProductModel> GetAllProductModels()
        {
            List<ProductModel> list = new List<ProductModel>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from product_model";

            list = DBUtil.GetListFromDataReader<ProductModel>(cmd);

            return list;
        }




        /// <summary>
        /// Load all look up list
        /// </summary>
        static public void LoadAllLookUpLists()
        {
            // -----------------------------------------------------------
            // load patterns

            List<Pattern> _patternList = GetAllPatterns();

            lock (DealProcessor.AllPatterns)
            {
                DealProcessor.AllPatterns.Clear();
                foreach (Pattern p in _patternList)
                {
                    DealProcessor.AllPatterns.Add(p.PatternID, p);
                }
            }

            // -----------------------------------------------------------
            // load pattern_matches

            List<PatternMatch> _patternMatches = GetAllPatternMatches();

            lock (DealProcessor.AllPatternMatches)
            {
                DealProcessor.AllPatternMatches.Clear();
                foreach (PatternMatch m in _patternMatches)
                {
                    DealProcessor.AllPatternMatches.Add(m);

                    // set pattern reference
                    if (m.MatchPatternID > 0)
                    {
                        if (DealProcessor.AllPatterns.Contains(m.MatchPatternID))
                        {
                            m.MatchPattern = (Pattern)DealProcessor.AllPatterns[m.MatchPatternID];
                        }
                        else
                        {
                            LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "PatternMatch missing pattern for id:" + m.MatchPatternID, null);
                        }
                    }
                    if (m.ExcludePatternID > 0)
                    {
                        if (DealProcessor.AllPatterns.Contains(m.ExcludePatternID))
                        {
                            m.ExcludePattern = (Pattern)DealProcessor.AllPatterns[m.ExcludePatternID];
                        }
                        else
                        {
                            LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "PatternMatch missing pattern for id:" + m.ExcludePatternID, null);
                        }
                    }
                }
            }

            // -----------------------------------------------------------
            // load pattern_to_ignore

            List<PatternToIgnore> _patternsToIgnore = GetAllPatternsToIgnore();

            lock (DealProcessor.AllPatternsToIgnore)
            {
                DealProcessor.AllPatternsToIgnore.Clear();

                foreach (PatternToIgnore m in _patternsToIgnore)
                {
                    DealProcessor.AllPatternsToIgnore.Add(m);
                    if (m.PatternID > 0)
                    {
                        if (DealProcessor.AllPatterns.Contains(m.PatternID))
                        {
                            m.IgnorePattern = (Pattern)DealProcessor.AllPatterns[m.PatternID];
                        }
                        else
                        {
                            LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "patterntoignore missing pattern for id:" + m.PatternID, null);
                        }
                    }
                }
            }

            // -----------------------------------------------------------
            // load category and subcategory

            List<Category> _categoryList = GetAllCategories();
            List<SubCategory> _subcategoryList = GetAllSubCategories();

            lock (DealProcessor.AllCategories)
            {
                DealProcessor.AllCategories = _categoryList;
                DealProcessor.AllSubCategories.Clear();
                foreach (SubCategory sub in _subcategoryList)
                {
                    Category catLookup = null;
                    foreach (Category cat in _categoryList)
                    {
                        if (cat.CategoryID == sub.CategoryID)
                        {
                            catLookup = cat;
                            break;
                        }
                    }
                    if (catLookup != null)
                    {
                        DealProcessor.AllSubCategories.Add(sub.CategoryID, sub);
                        sub.Category = catLookup;
                        catLookup.MySubCategories.Add(sub);
                    }
                    else
                    {
                        LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "subcategory missing category for id:" + sub.CategoryID, null);
                    }
                }
            }



            // -----------------------------------------------------------
            // load store

            List<Store> _storeList = GetAllStores();

            lock (DealProcessor.AllStores)
            {
                DealProcessor.AllStores = _storeList;

                MySqlDataReader sdr = DBUtil.ExecuteReader("select * from store_subcategory");
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        int storeid = (int)sdr["StoreID"];
                        int subid = (int)sdr["SubCategoryID"];

                        Store store = DealProcessor.GetStoreByID(storeid);
                        SubCategory sub = DealProcessor.GetSubCategoryByID(subid);

                        if (store != null && sub != null)
                        {
                            sub = DealProcessor.GetSubCategoryByID(subid);
                        }
                        else if (sub == null)
                        {
                            LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "store subcategory: missing subcategory for id:" + subid, null);
                        }
                        else
                        {
                            LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "store subcategory: missing store for id:" + storeid, null);
                        }
                    }
                }

            }

            // -----------------------------------------------------------
            // load dealsource

            List<DealSource> _dealsrcList = GetAllDealSources();

            lock (DealProcessor.AllDealSources)
            {
                DealProcessor.AllDealSources = _dealsrcList;

                MySqlDataReader sdr = DBUtil.ExecuteReader("select * from dealsource_subcategory");
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        int srcid = (int)sdr["SourceID"];
                        int subid = (int)sdr["SubCategoryID"];

                        DealSource source = DealProcessor.GetDealSourceByID(srcid);
                        SubCategory sub = DealProcessor.GetSubCategoryByID(subid);

                        if (source != null && sub != null)
                        {
                            sub = DealProcessor.GetSubCategoryByID(subid);
                        }
                        else if (sub == null)
                        {
                            LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "dealsrc subcategory: missing subcategory for id:" + subid, null);
                        }
                        else
                        {
                            LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "dealsrc subcategory: missing dealsource for id:" + srcid, null);
                        }
                    }
                }

            }


            // -----------------------------------------------------------
            // load brand, product, model

            List<Brand> _brandList = GetAllBrands();
            lock (DealProcessor.AllBrands)
            {
                DealProcessor.AllBrands = _brandList;
            }

            List<Product> _productList = GetAllProducts();
            lock (DealProcessor.AllProducts)
            {
                DealProcessor.AllProducts.Clear();
                foreach (Product prod in _productList)
                {
                    DealProcessor.AllProducts.Add(prod);
                    if (prod.BrandID > 0)
                    {
                        Brand brd = DealProcessor.GetBrandByID(prod.BrandID);
                        if (brd != null)
                        {
                            prod.MyBrand = brd;
                            brd.MyProducts.Add(prod);
                        }
                        else
                            LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "product missing brand for id:" + prod.BrandID, null);
                    }
                    if (prod.SubCategoryID > 0)
                    {
                        SubCategory sub = DealProcessor.GetSubCategoryByID(prod.SubCategoryID);
                        if (sub != null)
                            prod.MySubcategory = sub;
                        else
                            LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "product missing subcategory for id:" + prod.SubCategoryID, null);
                    }
                }
            }

            List<ProductModel> _modelList = GetAllProductModels();
            lock (DealProcessor.AllProductModels)
            {
                DealProcessor.AllProductModels.Clear();
                foreach (ProductModel mdl in _modelList)
                {
                    Product prod = DealProcessor.GetProductByID(mdl.ProductID);
                    if (prod != null)
                    {
                        mdl.MyProduct = prod;
                        prod.MyModels.Add(mdl);
                    }
                    else
                        LogUtil.Log(LogLevel.ERROR, "LoadAllLookUpLists", "model missing product for id:" + mdl.ProductID, null);
                }
            }
        }

        #endregion load all lock up tables


        static DateTime lastRawDealFetchTime;
        static public List<Deal> GetNewRawDeals()
        {
            List<Deal> deals = new List<Deal>();

            object lastFetch = DBUtil.ExecuteScalar("select max(inTime) from deals_processed");
            if (lastFetch == DBNull.Value)
                lastRawDealFetchTime = DateTime.Now.AddYears(-1);
            else
                lastRawDealFetchTime = (DateTime)lastFetch;

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select dealsID as RawDealID,SourceID,Title,InTime from rssdeals where inTime>=date_add(@lastfetch,interval -1 minute) and not EXISTS (select 1 from deals_processed where RawDealID=dealsID) limit 2";
            cmd.Parameters.AddWithValue("@lastfetch", lastRawDealFetchTime);

            deals = DBUtil.GetListFromDataReader<Deal>(cmd);

            return deals;
        }

        static public void InsertUpdateProcessedDeal(Deal deal)
        {
            MySqlCommand cmd = new MySqlCommand();
            if (deal.DealID == 0) // new deal
            {
                cmd.CommandText = "insert deals_processed (SourceID,ProductID,DealType,StoreID,FreeShipping,Title,URL,Detail,InTime,RawDealID,CleanTitle) " +
                    "select SourceID,@prodid,@dealtype,@storeid,@freeship,Title,URL,SendContent,InTime,DealsID,@cleanTitle " +
                    "from rssdeals where dealsID = @rawdealid;";
                cmd.Parameters.AddWithValue("@prodid", (deal.ProductID > 0 ? (object)deal.ProductID : null));
                cmd.Parameters.AddWithValue("@dealtype", (string.IsNullOrEmpty(deal.DealType) ? null : deal.DealType));
                cmd.Parameters.AddWithValue("@storeid", (deal.StoreID > 0 ? (object)deal.StoreID : null));
                cmd.Parameters.AddWithValue("@freeship", deal.FreeShipping);
                cmd.Parameters.AddWithValue("@cleanTitle", deal.CleanTitle);
                cmd.Parameters.AddWithValue("@rawdealid", deal.RawDealID);

                DBUtil.ExecuteNonQuery(cmd);

                cmd = new MySqlCommand();
                cmd.CommandText = "select DealID from deals_processed where RawDealID = @rawdealid";
                cmd.Parameters.AddWithValue("@rawdealid", deal.RawDealID);

                object rst = DBUtil.ExecuteScalar(cmd);
                if (rst != DBNull.Value)
                {
                    // insert subcategories
                    deal.DealID = (int)rst;
                    foreach (SubCategory sub in deal.MySubCategories)
                    {
                        cmd = new MySqlCommand();
                        cmd.CommandText = "insert deal_subcategory (DealID,SubCategoryID) values (@dealid,@subid);";
                        cmd.Parameters.AddWithValue("@dealid", deal.DealID);
                        cmd.Parameters.AddWithValue("@subid", sub.SubCategoryID);

                        DBUtil.ExecuteNonQuery(cmd);
                    }
                }
            }
            else // update deal
            {
                cmd.CommandText = "update deals_processed set ProductID=@prodid, DealType=@dealtype, StoreID=@storeid,FreeShipping=@freeship,CleanTitle=@cleanTitle where DealID=@dealid ";
                cmd.Parameters.AddWithValue("@prodid", (deal.ProductID > 0 ? (object)deal.ProductID : null));
                cmd.Parameters.AddWithValue("@dealtype", (string.IsNullOrEmpty(deal.DealType) ? null : deal.DealType));
                cmd.Parameters.AddWithValue("@freeship", deal.FreeShipping);
                cmd.Parameters.AddWithValue("@cleanTitle", deal.CleanTitle);
                cmd.Parameters.AddWithValue("@dealid", deal.DealID);

                DBUtil.ExecuteNonQuery(cmd);

                // remove existing ones
                cmd = new MySqlCommand();
                cmd.CommandText = "delete deal_subcategory where DealID = @dealid;";
                cmd.Parameters.AddWithValue("@dealid", deal.DealID);
                DBUtil.ExecuteNonQuery(cmd);

                // insert new subcategories
                foreach (SubCategory sub in deal.MySubCategories)
                {
                    cmd = new MySqlCommand();
                    cmd.CommandText = "insert deal_subcategory (DealID,SubCategoryID) values (@dealid,@subid);";
                    cmd.Parameters.Clear();

                    DBUtil.ExecuteNonQuery(cmd);
                }
            }
        }
    }

}