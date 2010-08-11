using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.OleDb;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Utilities
{
    public class FileUtility
    {
        private FileUtility()
        { 
        }


        //Eric 6.18.2010
        // to check if a given file text file or binary file.
        // by checking first 50 characters of the file
        public static bool IsTextFile(String fileName)
        {
            bool isTextFile = true;
            FileStream fs = null;

            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                fs.Position = 0;
                
                int current = fs.ReadByte();
                int count = 0;

                while (current != -1 && count < 50) // -1: eof; 13,10 cr;
                {
                    if ((current <= 31) && current != 9 && current != 13 && current != 10 && current != 0)  // 9: tab; 
                    {
                        isTextFile = false;
                        break;
                    }

                    current = fs.ReadByte();
                    count++;
                }
            }
            finally
            {
                fs.Close();
                if (fs != null) ((IDisposable)fs).Dispose();
            }

            return isTextFile;

        }
        
        // Eric 6.24.2010
        // to check if a binary file is Excel file or nto
        public static bool CheckXLS(String fileName)
        {

            bool pass = true;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open);
                byte[] fileHeader = new byte[4];

                fs.Position = 0;
                fs.Read(fileHeader, 0, 4); //read(array, offset, count)
                uint bitHdr = (uint)((fileHeader[0] << 24) | (fileHeader[1] << 16) | (fileHeader[2] << 8) | fileHeader[3]);

                if (bitHdr == 3503231456 || bitHdr == 151127552 || bitHdr == 151258624 || bitHdr == 1347093252)
                    pass = true;
                else
                    pass = false;

            }
            finally
            {
                fs.Close();
                if (fs != null) ((IDisposable)fs).Dispose();

            }

            return pass;

            /*Console.WriteLine(fileHeader[0]);
            Console.WriteLine(fileHeader[1]);
            Console.WriteLine(fileHeader[2]);
            Console.WriteLine(fileHeader[3]);
                    
            first 4 bytes of xls files are:  D0: 11010000            CF: 11001111            11: 00010001            E0: 11100000 
                                        or:  09 02 06 00  00001001 00000010 00000110 00000000  xls V2
                                        or:  09 04 06 00  00001001 00000100 00000110 00000000  xls V4
             ******* NEED DO THE RESEARCH ON 2007******
             * 
             * 2007 : 50 4B 03 04  : 01010000   01001011 00000011  00000100
             * 01010000010010110000001100000100
            */


        }

        //Eric 6.25
        public static DataTable getXLSLeadsRecordsTable(string file, bool hasHeader)
        {
            DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
            // builder.Add("Provider", "Microsoft.Jet.OleDb.4.0");
            builder.Add("Provider", "Microsoft.ACE.OLEDB.12.0");

            builder.Add("Data Source", file);
            if (hasHeader) // if the file contains a header
                // builder.Add("Extended Properties", "Excel 8.0;HDR=Yes");
                builder.Add("Extended Properties", "Excel 12.0;HDR=Yes;IMEX=1;");
            else
                //builder.Add("Extended Properties", "Excel 8.0;HDR=No");
                builder.Add("Extended Properties", "Excel 12.0;HDR=No;IMEX=1;");

            string conStr = builder.ToString();
            OleDbConnection dbConn = new OleDbConnection(conStr);
            DataTable dt = new DataTable();

            try
            {
                dbConn.Open();

                DataTable dtsheet = dbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                String[] sWorksheet = new String[dtsheet.Rows.Count];
                int i = 0;

                foreach (DataRow row in dtsheet.Rows)
                {
                    sWorksheet[i++] += row["TABLE_NAME"].ToString();
                }

                //char chSplit = '$';

                //sWorksheet = sWorksheet.Split(chSplit).GetValue(0).ToString() + "$";


                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "Select * FROM [" + sWorksheet[0].Replace("''", "'") + "]";
                cmd.Connection = dbConn;
                OleDbDataAdapter objAd = new OleDbDataAdapter();
                objAd.SelectCommand = cmd;
                objAd.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConn.Close();
            }
            return dt;
        }
   
        // Eric 6.24.2010
        // combine the "xxx,xxx" format strings split by delimiter ','
        public static string[] ConstructCSVLine(string[] cols)
        {

            string[] finalcols = new string[cols.Length];
            int newIndex = 0;

            for (int i = 0; i < cols.Length; )
            {
                int j = 0;
                if (cols[i].Trim().StartsWith("\"") && !cols[i].Trim().EndsWith("\""))
                {
                    j = 1;
                    if (i != cols.Length - 1)
                    {
                        do
                        {
                            cols[i] = cols[i] + "," + cols[i + j];
                            j++;

                        } while ((!cols[i + j - 1].Trim().EndsWith("\"")) && ((i + j) < cols.Length));
                    }

                    j = ((i + j) == cols.Length) ? 0 : j;
                }

                cols[i] = cols[i].Trim().Trim('\"');
                cols[i] = cols[i].Replace("\"\"", "\"");

                finalcols[newIndex] = cols[i];
                newIndex++;

                i = (j != 0) ? (i + j) : (i + 1);
            }

            Array.Resize(ref finalcols, newIndex);

            return finalcols;
        }
    }
}