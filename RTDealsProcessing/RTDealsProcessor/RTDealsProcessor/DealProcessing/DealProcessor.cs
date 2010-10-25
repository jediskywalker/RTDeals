using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Libraries;


namespace DealProcessing
{

    public class DealProcessor
    {
        static public Hashtable AllPatterns = null;
        static public List<PatternMatch> AllPatternMatches = null;
        static public List<PatternToIgnore> AllPatternsToIgnore = null;
        static public List<Brand> AllBrands = null;
        static public List<Product> AllProducts = null;
        static public List<ProductModel> AllProductModels = null;
        static public List<Category> AllCategories = null;
        static public Hashtable AllSubCategories = null;

        private void LoadPatterns()
        {
            List<Pattern> _patternList = DealProcessingDB.GetAllPatterns();

            foreach (Pattern p in _patternList)
            {
                AllPatterns.Add(p.PatternID, p);
            }
        }

        private void LoadPatternMatches()
        {
            AllPatternMatches = DealProcessingDB.GetAllPatternMatches();

            foreach (PatternMatch m in AllPatternMatches)
            {
                // set pattern reference
                if (m.MatchPatternID > 0)
                {
                    if (AllPatterns.Contains(m.MatchPatternID))
                    {
                        m.MatchPattern = (Pattern)AllPatterns[m.MatchPatternID];
                    }
                    else
                    {
                        LogUtil.Log(LogLevel.ERROR, "LoadPatternMatches", "Missing pattern for id:" + m.MatchPatternID, null);
                    }
                }
                if (m.ExcludePatternID > 0)
                {
                    if (AllPatterns.Contains(m.ExcludePatternID))
                    {
                        m.ExcludePattern = (Pattern)AllPatterns[m.ExcludePatternID];
                    }
                    else
                    {
                        LogUtil.Log(LogLevel.ERROR, "LoadPatternMatches", "Missing pattern for id:" + m.ExcludePatternID, null);
                    }
                }
            }
        }

        private void LoadPatternsToIgnore()
        {
            AllPatternsToIgnore = DealProcessingDB.GetAllPatternsToIgnore();

            foreach (PatternToIgnore m in AllPatternsToIgnore)
            {
                // set pattern reference
                if (m.PatternID > 0)
                {
                    if (AllPatterns.Contains(m.PatternID))
                    {
                        m.IgnorePattern = (Pattern)AllPatterns[m.PatternID];
                    }
                    else
                    {
                        LogUtil.Log(LogLevel.ERROR, "LoadPatternsToIgnore", "Missing pattern for id:" + m.PatternID, null);
                    }
                }
            }
        }

        private void LoadAllLookUpLists()
        {
            LoadPatterns();
            LoadPatternMatches();
            LoadPatternsToIgnore();

        }
    }

}
