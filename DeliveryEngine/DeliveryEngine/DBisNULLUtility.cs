using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Text;
using System.Security.Cryptography;

// 9.12.2010 Li
/// <summary>
/// Summary description for StrUtility
/// provides various string utility functions
/// </summary>
public class DBisNULLUtility
{
    public DBisNULLUtility()
	{
	}
   
    public string isDBNull(object val, string val2)
    {
        string returnVal = "";
        if (val == null || val is System.DBNull)
        {
            returnVal = val2;
        }
        else
        {
            returnVal = (string) val;
        }
        return returnVal;
    }
    public int isDBNull(object val, int val2)
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
    public DateTime isDBNull(object val, DateTime val2)
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
    public int isDBNull(object val, short val2)
    {
        int returnVal = 0;
        if (val == null || val is System.DBNull)
        {
            returnVal = val2;
        }
        else
        {
            returnVal = (short)val;
        }
        return returnVal;
    }
    public int isDBNull(object val, byte val2)
    {
        int returnVal = 0;
        if (val == null || val is System.DBNull)
        {
            returnVal = val2;
        }
        else
        {
            returnVal = (byte)val;
        }
        return returnVal;
    }
    public bool isDBNull(object val, bool val2)
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
    public int convertBoolToInt(object val, int val2)
    {
        int returnVal = 0;
        if (val == null || val is System.DBNull)
        {
            returnVal = val2;
        }
        else if ((bool)val == true)
        {
            returnVal = 1;
        }
        else
        {
            returnVal = 0;
        }
        return returnVal;
    }

  

}



