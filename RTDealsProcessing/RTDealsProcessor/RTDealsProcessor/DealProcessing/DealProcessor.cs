using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;
using Libraries;


namespace DealProcessing
{

    public class DealProcessor
    {
        // constant
        public const int AccuracyThreshold = 50;


        #region lockup list

        static public object LockLookupLists = new object();
        static public Hashtable AllPatterns = new Hashtable();
        static public List<PatternMatch> AllPatternMatches = new List<PatternMatch>();
        static public List<PatternToIgnore> AllPatternsToIgnore = new List<PatternToIgnore>();
        static public List<Brand> AllBrands = new List<Brand>();
        static public List<Product> AllProducts = new List<Product>();
        static public List<ProductModel> AllProductModels = new List<ProductModel>();
        static public List<Category> AllCategories = new List<Category>();
        static public Hashtable AllSubCategories = new Hashtable();
        static public List<Store> AllStores = new List<Store>();
        static public List<DealSource> AllDealSources = new List<DealSource>();
   
        #endregion lockup list

        #region lookup list search

        static public Pattern GetPatternByID(int id)
        {
            lock (AllPatterns)
            {
                if (AllPatterns.Contains(id))
                    return (Pattern)AllPatterns[id];
            }
            return null;
        }

        static public PatternMatch GetPatternMatchByID(int id)
        {
            lock (AllPatternMatches)
            {
                foreach (PatternMatch item in AllPatternMatches)
                {
                    if (item.MatchID == id)
                        return item;
                }
            }
            return null;
        }

        static public PatternToIgnore GetPatternToIgnoreByID(int id)
        {
            lock (AllPatternsToIgnore)
            {
                foreach (PatternToIgnore item in AllPatternsToIgnore)
                {
                    if (item.IgnoreID == id)
                        return item;
                }
            }
            return null;
        }

        static public Brand GetBrandByID(int id)
        {
            lock (AllBrands)
            {
                foreach (Brand item in AllBrands)
                {
                    if (item.BrandID == id)
                        return item;
                }
            }
            return null;
        }

        static public Product GetProductByID(int id)
        {
            lock (AllProducts)
            {
                foreach (Product item in AllProducts)
                {
                    if (item.ProductID == id)
                        return item;
                }
            }
            return null;
        }

        static public ProductModel GetProductModelByID(int id)
        {
            lock (AllProductModels)
            {
                foreach (ProductModel item in AllProductModels)
                {
                    if (item.ModelID == id)
                        return item;
                }
            }
            return null;
        }

        static public SubCategory GetSubCategoryByID(int id)
        {
            lock (AllSubCategories)
            {
                if (AllSubCategories.Contains(id))
                    return (SubCategory)AllSubCategories[id];
            }
            return null;
        }

        static public Category GetCategoryByID(int id)
        {
            lock (AllCategories)
            {
                foreach (Category item in AllCategories)
                {
                    if (item.CategoryID == id)
                        return item;
                }
            }
            return null;
        }

        static public Store GetStoreByID(int id)
        {
            lock (AllStores)
            {
                foreach (Store item in AllStores)
                {
                    if (item.StoreID == id)
                        return item;
                }
            }
            return null;
        }

        static public DealSource GetDealSourceByID(int id)
        {
            lock (AllDealSources)
            {
                foreach (DealSource item in AllDealSources)
                {
                    if (item.SourceID == id)
                        return item;
                }
            }
            return null;
        }

        #endregion lookup list search


        static public void ProcessRawDeals()
        {

            // load newly loaded recent deals
            List<Deal> rawdeals = DealProcessingDB.GetNewRawDeals();

            int cnt = rawdeals.Count;

            foreach (Deal rawdeal in rawdeals)
            {
                ProcessOneRawDeal(rawdeal);
            }

        }

        //
        //  dealsource { categories, store { categories } }
        //  product { categories }
        //  pattern { categories, product { categories }, store { categories }, dealtype }
        //
        static public void ProcessOneRawDeal(Deal deal)
        {
            DealSource source = GetDealSourceByID(deal.SourceID);
            List<SubCategory> subcategoriesFromSource = source.MySubCategories;

            string cleanTitle = CleanDealTitle(deal.Title);
            deal.CleanTitle = cleanTitle;

            // check brand/product/model to get product
            ProductMatch productMatched = MatchProduct(cleanTitle);

            // check patterns for category/store/dealtype/product
            SubCategory subcategoryFromPtn = null;
            Store storeFromPtn = null;
            Product productFromPtn = null;
            string dealTypeFromPtn = null;
            int scAccuracy = 0, stAccuracy = 0, prdAccuracy = 0, dtAccuracy = 0;
            bool freeshipping = false;

            TitlePattenMatching(cleanTitle, ref subcategoryFromPtn, ref scAccuracy, ref storeFromPtn, ref stAccuracy, ref productFromPtn, ref prdAccuracy, ref dealTypeFromPtn, ref dtAccuracy, ref freeshipping);

            // process all the info
            deal.MySubCategories.Clear();

            // get category info
            if (subcategoryFromPtn != null)
            {
                deal.MySubCategories.Add(subcategoryFromPtn);
            }
            else if (productMatched != null)
            {
                Product prod = GetProductByID(productMatched.ProductID);
                if (prod.MySubcategory != null)
                    deal.MySubCategories.Add(prod.MySubcategory);
            }
            else if (subcategoriesFromSource != null && subcategoriesFromSource.Count > 0)
            {
                deal.MySubCategories = subcategoriesFromSource;
            }

            // get product info
            if (productMatched != null)
            {
                deal.ProductID = productMatched.ProductID;
            }

            // get store info
            if (source.StoreID > 0)
                deal.StoreID = source.StoreID;
            else if (storeFromPtn != null)
                deal.StoreID = storeFromPtn.StoreID;
            
            // get dealtype
            if (!string.IsNullOrEmpty(dealTypeFromPtn))
                deal.DealType = dealTypeFromPtn;

            // get free shipping
            deal.FreeShipping = freeshipping;

            DealProcessingDB.InsertUpdateProcessedDeal(deal);
        }

        static public void TitlePattenMatching(string title, ref SubCategory subcategory, ref int scAccuracy, ref Store store, ref int stAccuracy, ref Product product, ref int prdAccuray, ref string dealType, ref int dtAccuracy, ref bool freeshippping)
        {
            subcategory = null;
            store = null;
            product = null;
            dealType = null;

            foreach (PatternMatch pm in AllPatternMatches)
            {
                // match pattern
                bool And = (pm.MatchPattern.MultiItemRelation == Pattern.AND);
                bool match = false;
                foreach (string ptn in pm.MatchPattern.SplittedPatterns)
                {
                    if (Regex.IsMatch(title, ptn, RegexOptions.IgnoreCase))
                    {
                        match = true;
                        if (!And) break;
                    }
                    else
                    {
                        match = false;
                        if (And) break;
                    }
                }

                if (!match) continue;

                if (pm.ExcludePattern != null)
                {
                    And = (pm.ExcludePattern.MultiItemRelation == Pattern.AND);
                    match = false;
                    foreach (string ptn in pm.ExcludePattern.SplittedPatterns)
                    {
                        if (Regex.IsMatch(title, ptn, RegexOptions.IgnoreCase))
                        {
                            match = true;
                            if (!And) break;
                        }
                        else
                        {
                            match = false;
                            if (And) break;
                        }
                    }

                    if (!match) continue;
                }

                if (subcategory == null && pm.SubCategoryID > 0)
                {
                    subcategory = GetSubCategoryByID(pm.SubCategoryID);
                    scAccuracy = pm.Accuracy;
                }
                if (product == null && pm.ProductID > 0)
                {
                    product = GetProductByID(pm.ProductID);
                    prdAccuray = pm.Accuracy;
                }
                if (store == null && pm.StoreID > 0)
                {
                    store = GetStoreByID(pm.StoreID);
                    stAccuracy = pm.Accuracy;
                }
                if (dealType == null && !string.IsNullOrEmpty(pm.DealType))
                {
                    dealType = pm.DealType;
                    dtAccuracy = pm.Accuracy;
                }
                if (subcategory != null && product != null && store != null && dealType != null)
                {
                    // get everything, break out the loop
                    break;
                }
            }
        }

        static public bool MatchKeywords(string[] titleKeywords, string name, string aliases)
        {
            char[] comma = new char[] { ',' };
            List<string> keywords = new List<string>();
            keywords.Add(name.ToLower());
            if (!string.IsNullOrEmpty(aliases))
            {
                string[] cols = aliases.Split(comma, StringSplitOptions.RemoveEmptyEntries);
                foreach (string col in cols)
                    keywords.Add(col.ToLower());
            }
            foreach (string t in titleKeywords)
            {
                string tl = t.ToLower();
                foreach (string k in keywords)
                {
                    if (tl == k)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static public ProductMatch MatchProduct(string title)
        {
            title = title.ToLower();
            char[] space = new char[] { ' ', '\t' };
            string[] titleWords = title.Split(space, StringSplitOptions.RemoveEmptyEntries);

            List<ProductMatch> products = new List<ProductMatch>();
            ProductMatch prodMatchRst = null;

            ProductModel modelMatched = null;
            foreach (ProductModel mdl in AllProductModels)
            {
                if (mdl.Name.ToLower().StartsWith("other") || mdl.Accuracy <= 0) continue; // skip special ones: Others, 0 accuracy

                if (MatchKeywords(titleWords, mdl.Name, mdl.Aliases))
                {
                    if (prodMatchRst == null || prodMatchRst.Accuracy < mdl.Accuracy)
                    {
                        if (prodMatchRst == null) prodMatchRst = new ProductMatch();
                        prodMatchRst.ProductID = mdl.ProductID;
                        prodMatchRst.ProductMatched = mdl.MyProduct;
                        prodMatchRst.MatchLevel = "M";
                        prodMatchRst.Accuracy = mdl.Accuracy;
                        modelMatched = mdl;
                    }
                }
            }

            if (modelMatched != null)
            {
                // adjust for product / brand level match
                bool prodAlsoMatched = MatchKeywords(titleWords, modelMatched.MyProduct.Name, modelMatched.MyProduct.Aliases);
                bool brandAlsoMatched = MatchKeywords(titleWords, modelMatched.MyProduct.MyBrand.Name, modelMatched.MyProduct.MyBrand.Aliases);

                if (prodAlsoMatched)
                    prodMatchRst.Accuracy += (100 - prodMatchRst.Accuracy) * modelMatched.MyProduct.Accuracy / 100;

                if (brandAlsoMatched)
                    prodMatchRst.Accuracy += (100 - prodMatchRst.Accuracy) * modelMatched.MyProduct.MyBrand.Accuracy / 100;

                return prodMatchRst;
            }


            Product productMatched = null;
            foreach (Product prod in AllProducts)
            {
                if (prod.Name.ToLower().StartsWith("other") || prod.Accuracy <= 0) continue; // skip special ones: Others, 0 accuracy

                if (MatchKeywords(titleWords, prod.Name, prod.Aliases))
                {
                    if (prodMatchRst == null || prodMatchRst.Accuracy < prod.Accuracy)
                    {
                        if (prodMatchRst == null) prodMatchRst = new ProductMatch();
                        prodMatchRst.ProductID = prod.ProductID;
                        prodMatchRst.ProductMatched = prod;
                        prodMatchRst.MatchLevel = "M";
                        prodMatchRst.Accuracy = prod.Accuracy;
                        productMatched = prod;
                    }
                }
            }

            if (productMatched != null)
            {
                bool brandAlsoMatched = MatchKeywords(titleWords, productMatched.MyBrand.Name, productMatched.MyBrand.Aliases);

                if (brandAlsoMatched)
                    prodMatchRst.Accuracy += (100 - prodMatchRst.Accuracy) * productMatched.MyBrand.Accuracy / 100;
                return prodMatchRst;
            }

            foreach (Brand brd in AllBrands)
            {
                if (brd.Name.ToLower().StartsWith("other") || brd.Accuracy <= 0) continue; // skip special ones: Others, 0 accuracy

                if (MatchKeywords(titleWords, brd.Name, brd.Aliases))
                {
                    if (prodMatchRst == null || prodMatchRst.Accuracy < brd.Accuracy)
                    {
                        if (prodMatchRst == null) prodMatchRst = new ProductMatch();
                        foreach (Product prod in brd.MyProducts)
                        {
                            if (prod.Name.ToLower() == "others")
                            {
                                prodMatchRst.ProductID = prod.ProductID;
                                prodMatchRst.ProductMatched = prod;
                                prodMatchRst.MatchLevel = "B";
                                prodMatchRst.Accuracy = brd.Accuracy;
                            }
                        }
                    }
                }
            }


            return prodMatchRst;
        }

        static public string CleanDealTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) return "";

            string cleanTitle = title;

            foreach (PatternToIgnore pti in AllPatternsToIgnore)
            {
                foreach (string ptn in pti.IgnorePattern.SplittedPatterns)
                {
                    cleanTitle = Regex.Replace(cleanTitle, ptn, "", RegexOptions.IgnoreCase);
                }
            }

            return cleanTitle;
        }

        
    }

}
