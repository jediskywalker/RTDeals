using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Configuration; 

namespace Util
{
    public class StrUtil
    {
        public static string EncryptPassCode = CorrectLengthofPasscode(System.Configuration.ConfigurationManager.AppSettings["EncryptPassCode"]);

        //AES Encrypt
        public static string AESEncrypt(string toEncrypt)
        {
            if (string.IsNullOrEmpty(EncryptPassCode))
                return toEncrypt;

            if (string.IsNullOrEmpty(toEncrypt))
                return toEncrypt;


            // EncryptPassCode = CorrectLengthofPasscode(EncryptPassCode);
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
            catch (Exception)
            {
                return toEncrypt;
            }
        }

        //AES Decrypt
        public static string AESDecrypt(string toDecrypt)
        {
            if (string.IsNullOrEmpty(EncryptPassCode))
                return toDecrypt;

            if (string.IsNullOrEmpty(toDecrypt))
                return toDecrypt;

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
            catch (Exception)
            {
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

        public static  string isDBNull(object val, string val2)
        {
            string returnVal = "";
            if (val == null || val is System.DBNull)
            {
                returnVal = val2;
            }
            else
            {
                returnVal = (string)val;
            }
            return returnVal;
        }
        public static int isDBNull(object val, int val2)
        {
            int returnVal = 0;
            if (val == null || val is System.DBNull)
            {
                returnVal = val2;
            }
            else
            {
                returnVal = (int)val;
            }
            return returnVal;
        }
        public static DateTime isDBNull(object val, DateTime val2)
        {
            DateTime returnVal = DateTime.Now;
            if (val == null || val is System.DBNull)
            {
                returnVal = val2;
            }
            else
            {
                returnVal = (DateTime)val;
            }
            return returnVal;
        }

        public static bool isDBNull(object val, bool val2)
        {
            bool returnVal = false;
            if (val == null || val is System.DBNull)
            {
                returnVal = val2;
            }
            else
            {
                returnVal = (bool)val;
            }
            return returnVal;
        }

        public static bool IsValidEmail(string email)
        {
            string strRegex = @"^[a-zA-Z0-9_\-\.]+@([a-zA-Z0-9_\-]+\.)+([a-zA-Z]{2,4})$";
            Regex re = new Regex(strRegex);
            bool valid = re.IsMatch(email.Trim());
            return valid;
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return false;

            string strRegex = @"^\s*1?[\(-]?\s*\d{3}[-\s\.\)]?\s*\d{3}[-\.]?\s*\d{4}\s*$";
            Regex re = new Regex(strRegex);
            bool valid = re.IsMatch(phone);
            return valid;
        }

    }


}