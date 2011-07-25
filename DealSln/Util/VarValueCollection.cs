using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class StringVarValuePairs
    {
        private string _delimitor;
        private string _equal;

        private Hashtable _pairs;

        private StringVarValuePairs() { }
        public StringVarValuePairs(string delimitor, string equal)
        {
            _delimitor = delimitor;
            _equal = equal;
            _pairs = new Hashtable();
        }

        public StringVarValuePairs(string delimitor, string equal, string content)
        {
            _delimitor = delimitor;
            _equal = equal;
            _pairs = new Hashtable();
            Parse(content);
        }

        public void Parse(string content)
        {
            if (string.IsNullOrEmpty(content)) return;

            string[] delimitors = new string[1];
            delimitors[0] = _delimitor;
            string[] pairs = content.Split(delimitors, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < pairs.Length; i++)
            {
                int idx = pairs[i].IndexOf(_equal);
                if (idx < 0) continue;
                string var = pairs[i].Substring(0, idx);
                string value = pairs[i].Substring(idx + _equal.Length);
                _pairs[var] = value;
            }
        }

        public string GetValue(string var)
        {
            if (_pairs.Contains(var))
                return (string)_pairs[var];
            else
                return null;
        }

        public void SetValue(string var, string value)
        {
            _pairs[var] = value;
        }
    }

}