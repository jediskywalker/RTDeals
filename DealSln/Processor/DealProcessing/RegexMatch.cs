using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DealProcessing
{
    public class RegexMatch
    {
        public static bool Match(string input, string regexs, bool IsAnd, ref string matchInfo)
        {
            if (string.IsNullOrEmpty(regexs) ||
                string.IsNullOrEmpty(input))
                return false;

            char[] delimitors = new char[1] { ',' };

            string[] patterns = regexs.Split(delimitors, StringSplitOptions.RemoveEmptyEntries);

            matchInfo = "";
            foreach (string ptn in patterns)
            {
                Match match = Regex.Match(input, ptn, RegexOptions.IgnoreCase);
                matchInfo += string.Format("regex:[{0}] match?{1} matched:[{2}]", ptn, (match.Success ? "Yes" : "No"), match);

                if (match.Success)
                {
                    if (patterns.Length == 1) return true;
                    if (!IsAnd) return true;
                }
                else
                {
                    if (patterns.Length == 1) return false;
                    if (IsAnd) return false;
                }
            }
            if (IsAnd) return true;
            else return false;
        }
    }
}
