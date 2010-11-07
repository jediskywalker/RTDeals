using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;


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





        public static string EncryptPassCode = "Wewillchangeyourlife"; //CorrectLengthofPasscode(System.Configuration.ConfigurationManager.AppSettings["EncryptPassCode"]);

        //AES Encrypt
        public static string Encrypt(string toEncrypt)
        {
            if (string.IsNullOrEmpty(EncryptPassCode))
                return toEncrypt;

            if (toEncrypt == "")
                return "";


            EncryptPassCode = CorrectLengthofPasscode(EncryptPassCode);
            try
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(EncryptPassCode);
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception exp)
            {
                //throw (exp);
                return toEncrypt;
            }
        }

        //AES Decrypt
        public static string Decrypt(string toDecrypt)
        {

            if (string.IsNullOrEmpty(EncryptPassCode))
                return toDecrypt;

            if (toDecrypt == "")
                return "";

            try
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(EncryptPassCode);
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception exp)
            {
                // throw (exp);
                return toDecrypt;
            }
        }

        public static string CorrectLengthofPasscode(string Passcode)
        {
            if (Passcode == null)
                return null;
            if (Passcode.Length < 16)
                Passcode = Passcode.PadRight(16, '0');
            else if (Passcode.Length > 16 && Passcode.Length < 24)
                Passcode = Passcode.PadRight(24, '0');
            else if (Passcode.Length > 24 && Passcode.Length < 32)
                Passcode = Passcode.PadRight(32, '0');
            else if (Passcode.Length > 32)
                Passcode = Passcode.Substring(0, 32);



            return Passcode;



        }


    }


}