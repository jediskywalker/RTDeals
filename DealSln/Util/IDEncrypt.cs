using System;
using System.Collections.Generic;

namespace Util
{
    public class IDEncrypt
    {
        private static int _NumOfChars = 62; // max 62
        private static List<byte> _CharSet = InitCharSet();
        private static Random _RandomShift = new Random();

        private static List<byte> InitOrigCharSet()
        {
            // 0-9, A-Z, a-z
            List<byte> set = new List<byte>();
            for (byte i = (byte)'0'; i <= (byte)'9'; i++)
                set.Add(i);
            for (byte i = (byte)'A'; i <= (byte)'Z'; i++)
                set.Add(i);
            for (byte i = (byte)'a'; i <= (byte)'z'; i++)
                set.Add(i);

            return set;
        }

        private static List<byte> InitCharSet()
        {
            List<byte> _OrigCharSet = InitOrigCharSet();

            List<byte> set = new List<byte>();
            for (int i = 0; i < _NumOfChars; i++) set.Add(0);

            Random rand = new Random();
            for (int i = 0; i < _NumOfChars; i++)
            {
                int loc = rand.Next(_NumOfChars);
                while (set[loc] != 0)
                    loc = rand.Next(_NumOfChars);
                set[loc] = _OrigCharSet[i];
            }
            return set;
        }

        private static char NumToByte(int num)
        {
            return (char)_CharSet[num % _NumOfChars];
        }

        private static int ByteToNum(char bt)
        {
            for (int i = 0; i < _NumOfChars; i++)
                if (_CharSet[i] == (byte)bt) return i;
            return -1;
        }

        public static void Init(int numberOfChars)
        {
            _NumOfChars = numberOfChars;
            _CharSet = InitCharSet();
        }

        public static string Encrypt(int id)
        {
            List<int> digits = new List<int>();
            int r, q = id;
            while ((q = Math.DivRem(q, _NumOfChars, out r)) > 0)
                digits.Insert(0, r);
            digits.Insert(0, r);

            Random rand = new Random();
            int shift = _RandomShift.Next(_NumOfChars);
            int chksum = shift;
            string ret = "";
            for (int i = 0; i < digits.Count; i++)
            {
                ret += NumToByte(digits[i] + shift);
                chksum += digits[i] * (2 + i);
            }
            ret += NumToByte(shift);
            ret += NumToByte(chksum);

            return ret;
        }

        public static int Decrypt(string encrypted)
        {
            if (encrypted.Length < 3) goto WrongID;

            int chksumIn = ByteToNum(encrypted[encrypted.Length - 1]);
            if (chksumIn == -1) goto WrongID;

            int shift = ByteToNum(encrypted[encrypted.Length - 2]);
            int rst = 0;
            int chksum = shift;
            for (int i = 0; i < encrypted.Length - 2; i++)
            {
                int num = ByteToNum(encrypted[i]);
                if (num == -1) goto WrongID;

                num -= shift;
                if (num < 0)
                    num = _NumOfChars + num;
                chksum += num * (2 + i);

                rst = rst * _NumOfChars + num;
            }
            if (chksum % _NumOfChars != chksumIn) goto WrongID;

            return rst;

        WrongID:

            return -1;
        }
    }

}