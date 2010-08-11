using System;
using System.Text;
using System.Text.RegularExpressions;


namespace Utilities
{
    public class StrUtil
    {
        public static string Dummy(string input)
        {
            return "Dummy";
        }

        public static string CleanPhoneNo(string ph)
        {
            if (ph == null || ph.Length == 0)
                return "";

            string strPh = "";
            string pattern = @"(\d+?\d*|\d+)";
            MatchCollection result = Regex.Matches(ph, pattern);

            for (int i = 0; i < result.Count; i++)
                strPh += result[i];

            // remove leading '1'
            if (strPh.Length == 11 && strPh.Substring(0, 1) == "1")
                strPh = strPh.Substring(1);

            return strPh;
        }

        public static string CleanFileDescription(string desc)
        {
            // \/:*?"<>|
            desc = desc.Replace('\\', '_');
            desc = desc.Replace(':', '_');
            desc = desc.Replace('/', '_');
            desc = desc.Replace('*', '_');
            desc = desc.Replace('<', '_');
            desc = desc.Replace('>', '_');
            desc = desc.Replace('|', '_');
            desc = desc.Replace('?', '_');
            desc = desc.Replace('"', '_');
            desc = desc.Replace(" ", "");
            desc = desc.Replace(",", "");

            return desc;
        }
      
    }


}