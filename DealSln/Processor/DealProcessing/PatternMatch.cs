using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class PatternMatch
    {
        public int MatchID { get; set; }
        public string Description { get; set; }
        public int MatchPatternID { get; set; }
        public Pattern MatchPattern { get; set; }
        public int ExcludePatternID { get; set; }
        public Pattern ExcludePattern { get; set; }
        public int Accuracy { get; set; }
        public int SubCategoryID { get; set; }
        public int ProductID { get; set; }
        public int StoreID { get; set; }
        public string DealType { get; set; }
        public string FreeShipping { get; set; }

        public override string ToString()
        {
            return string.Format("<PatternMatch id:{0} Desc:{1} match:{2} exclude:{3} acc:{4} catg:{5} prod:{6} store:{7} type:{8}> f/s:{9}>; ", MatchID, Description, MatchPattern.ToString(), ExcludePattern.ToString(), Accuracy, SubCategoryID, ProductID, StoreID, DealType, FreeShipping);
        }
    }
}