using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Pattern
    {
        const string AND = "A";
        const string OR = "O";

        public int PatternID { get; set; }
        public string Description { get; set; }
        public string Patterns { get; set; }
        public string MultiItemRelation { get; set; }

        private string[] _splittedPatterns;
        public string[] SplittedPatterns
        {
            get
            {
                if (_splittedPatterns != null) return _splittedPatterns;

                if (string.IsNullOrEmpty(Patterns))
                {
                    _splittedPatterns = new string[] { };
                    return _splittedPatterns;
                }

                char[] delimitors = new char[] { ',' };
                _splittedPatterns = Patterns.Split(delimitors, StringSplitOptions.RemoveEmptyEntries);
                return _splittedPatterns;
            }
        }

        public override string ToString()
        {
            return string.Format("<Pattern id:{0} Desc:{1} Ptns:{2} Rel:{3}>; ", PatternID, Description, Patterns, MultiItemRelation);
        }
    }
}