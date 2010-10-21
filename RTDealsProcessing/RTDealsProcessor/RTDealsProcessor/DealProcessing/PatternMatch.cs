﻿using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class PatternMatch
    {
        public int MatchID { get; set; }
        public string Description { get; set; }
        public string MatchPatternID { get; set; }
        public Pattern MatchPattern { get; set; }
        public string ExcludePatternID { get; set; }
        public Pattern ExcludePattern { get; set; }
        public int Accuracy { get; set; }
        public int? CategoryID { get; set; }
        public int? ProductID { get; set; }
        public int? StoreID { get; set; }
        public string DealType { get; set; }

        public override string ToString()
        {
            return string.Format("<PatternMatch id:{0} Desc:{1} match:{2} exclude:{3} acc:{4} catg:{5} prod:{6} store:{7} type:{8}>; ", MatchID, Description, MatchPattern.ToString(), ExcludePattern.ToString(), Accuracy, CategoryID, ProductID, StoreID, DealType);
        }
    }
}