using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Pattern
    {
        public int PatternID { get; set; }
        public string Description { get; set; }
        public string Patterns { get; set; }
        public string MultiItemRelation { get; set; }

        public override string ToString()
        {
            return string.Format("<Pattern id:{0} Desc:{1} Ptns:{2} Rel:{3}>; ", PatternID, Description, Patterns, MultiItemRelation);
        }
    }
}