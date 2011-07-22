using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealProcessing
{
    public class Synonym
    {
        public int SynonymID { get; set; }
        public string Word { get; set; }
        public string Synonyms { get; set; }

        private string[] _splittedSynonyms;
        public string[] SplittedSynonyms
        {
            get
            {
                if (_splittedSynonyms != null) return _splittedSynonyms;

                if (string.IsNullOrEmpty(Word))
                {
                    _splittedSynonyms = new string[] { };
                    return _splittedSynonyms;
                }

                char[] delimitors = new char[] { ',', ' ' };
                _splittedSynonyms = Synonyms.Split(delimitors, StringSplitOptions.RemoveEmptyEntries);
                return _splittedSynonyms;
            }
        }
    }
}
