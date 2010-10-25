using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class PatternToIgnore
    {
        public int IgnoreID { get; set; }
        public string Description { get; set; }
        public int PatternID { get; set; }
        public Pattern IgnorePattern { get; set; }
        public int Priority { get; set; }

        public override string ToString()
        {
            return string.Format("<PatternToIgnore id:{0} Desc:{1} PtnID:{2} Pri:{3}>; ", IgnoreID, Description, PatternID, Priority);
        }
    }
}