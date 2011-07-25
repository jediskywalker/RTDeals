using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

// 9.12.2010 Li
namespace DeliveryEngine
{
    class Delivery
    {

        public void Start()
        {
            while (true)
            {
                DoDelivery();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("***delivery     "+ DateTime.Now.ToLongTimeString());
                Thread.Sleep(30000);
            }

        }

        private void DoDelivery()
        {
            List<ScheduledDelivery> allscheduled = new List<ScheduledDelivery>();            
            allscheduled = DBQuerys.GetAllScheduled2Deliver();
            
            Hashtable deals = new Hashtable();
            
            string emailcontent="";
            string divElec = "<div> <span style='font-size:large;'>Electronic:</span><br/><br/>     </div>";
            string divDrug = "<div> <span style='font-size:large;'>Drugs:</span><br/><br/>     </div>";
            string divTravel = "<div> <span style='font-size:large;'>Travel:</span><br/><br/>     </div>";
           // string divFree = "<div> <span style='font-size:large;'>Free:</span><br/><br/>     </div>";
            string divFinance = "<div> <span style='font-size:large;'>Finance:</span><br/><br/>     </div>";
            string divHot = "<div> <span style='font-size:large;'>Hot: </span><br/><br/>     </div>";
            string divApp = "<div> <span style='font-size:large;'>Appliances: </span><br/><br/>     </div>";
            string divBeauty = "<div> <span style='font-size:large;'>Beauty: </span><br/><br/>     </div>";
            string divOS = "<div> <span style='font-size:large;'>OfficeSupplies: </span><br/><br/>     </div>";
            string divJewelry = "<div> <span style='font-size:large;'>Jewelry: </span><br/><br/>     </div>";
            string divAppeal = "<div> <span style='font-size:large;'>Appeal: </span><br/><br/>     </div>";
            string divRestaurant = "<div> <span style='font-size:large;'>Restaurant: </span><br/><br/>     </div>";
            string divMix = "<div> <span style='font-size:large;'>Others:</span><br/><br/>     </div>";

            bool bElec = false;
            bool bDrug = false;
            bool bTravel = false;
           // bool bFree = false;
            bool bFinance = false;
            bool bHot = false;
            bool bMix = false;
            bool bAppl = false;
            bool bAlle = false;
            bool bBea = false;
            bool bOS = false;
            bool bJew = false;
            bool bRes = false;

            foreach (ScheduledDelivery current in allscheduled) // foreach customer...
            {
                emailcontent="";

                divElec = "<div> <span style='font-size:large;'>Electronic:</span><br/><br/>     </div>";
                divDrug = "<div> <span style='font-size:large;'>Drugs:</span><br/><br/>     </div>";
                divTravel = "<div> <span style='font-size:large;'>Travel:</span><br/><br/>     </div>";
               // divFree = "<div> <span style='font-size:large;'>Free:</span><br/><br/>     </div>";
                divFinance = "<div> <span style='font-size:large;'>Finance:</span><br/><br/>     </div>";
                divHot = "<div> <span style='font-size:large;'>Hot: </span><br/><br/>     </div>";

                divApp = "<div> <span style='font-size:large;'>Appliances: </span><br/><br/>     </div>";
                divBeauty = "<div> <span style='font-size:large;'>Beauty: </span><br/><br/>     </div>";
                divOS = "<div> <span style='font-size:large;'>OfficeSupplies: </span><br/><br/>     </div>";
                divJewelry = "<div> <span style='font-size:large;'>Jewelry: </span><br/><br/>     </div>";
                divAppeal = "<div> <span style='font-size:large;'>Appeal: </span><br/><br/>     </div>";
                divRestaurant = "<div> <span style='font-size:large;'>Restaurant: </span><br/><br/>     </div>";


                divMix = "<div> <span style='font-size:large;'>Others:</span><br/><br/>     </div>";

                bElec = false;
                bDrug = false;
                bTravel = false;
               // bFree = false;
                bFinance = false;
                bHot = false;
                bMix = false;
                bAppl = false;
                bAlle = false;
                bBea = false;
                bOS = false;
                bJew = false;
                bRes = false;


                string[] dealids = current.dealsID.Split(',');
                foreach (string dealid in dealids)
                {
                    int intdealid = Convert.ToInt32(dealid);

                    if (!deals.ContainsKey(intdealid))
                    {
                        Deals newdeal = DBQuerys.GetOneDeal(intdealid);
                        deals.Add(intdealid, newdeal);                    
                    }

                    Deals tmpdeal = (Deals)deals[intdealid];   
                    string tmpLine= MakeUp(tmpdeal,current.Keywords);

                    #region

                    if (tmpdeal.Ishot)
                    {
                        int tmpindex = divHot.Length - 7;
                        divHot = divHot.Insert(tmpindex, tmpLine);
                        bHot = true;
                        continue;   // hot flag could duplicate with other flags.

                    }
                    else
                    {

                        if (tmpdeal.IsDrug)
                        {
                            int tmpindex = divDrug.Length - 7;
                            divDrug = divDrug.Insert(tmpindex, tmpLine);
                            bDrug = true;
                        }
                        if (tmpdeal.IsElectronic)
                        {
                            int tmpindex = divElec.Length - 7;
                            divElec = divElec.Insert(tmpindex, tmpLine);
                            bElec = true;
                        }
                        if (tmpdeal.IsFinance)
                        {
                            int tmpindex = divFinance.Length - 7;
                            divFinance = divFinance.Insert(tmpindex, tmpLine);
                            bFinance = true;
                        }
                        /*
                        if (tmpdeal.IsFree)
                        {
                            int tmpindex = divFree.Length - 7;
                            divFree = divFree.Insert(tmpindex, tmpLine);
                            bFree = true;
                        }
                         */

                        if (tmpdeal.IsTravel)
                        {
                            int tmpindex = divTravel.Length - 7;
                            divTravel = divTravel.Insert(tmpindex, tmpLine);
                            bTravel = true;
                        }

                        if (tmpdeal.IsRestaurant)
                        {
                            int tmpindex = divRestaurant.Length - 7;
                            divRestaurant = divRestaurant.Insert(tmpindex, tmpLine);
                            bRes = true;
                        }
                        if (tmpdeal.IsOfficeSupplies)
                        {
                            int tmpindex = divOS.Length - 7;
                            divOS = divOS.Insert(tmpindex, tmpLine);
                            bOS = true;
                        }
                        if (tmpdeal.IsJewelry)
                        {
                            int tmpindex = divJewelry.Length - 7;
                            divJewelry = divJewelry.Insert(tmpindex, tmpLine);
                            bJew = true;
                        }
                        if (tmpdeal.IsAppeal)
                        {
                            int tmpindex = divAppeal.Length - 7;
                            divAppeal = divAppeal.Insert(tmpindex, tmpLine);
                            bAlle = true;
                        }
                        if (tmpdeal.IsAppliances)
                        {
                            int tmpindex = divApp.Length - 7;
                            divApp = divApp.Insert(tmpindex, tmpLine);
                            bAppl = true;
                        }
                        if (tmpdeal.IsBeauty)
                        {
                            int tmpindex = divBeauty.Length - 7;
                            divBeauty = divBeauty.Insert(tmpindex, tmpLine);
                            bBea = true;
                        }

                        if ((!tmpdeal.IsTravel && !tmpdeal.IsFinance && !tmpdeal.IsElectronic && !tmpdeal.IsDrug && !tmpdeal.IsRestaurant
                            && !tmpdeal.IsBeauty && !tmpdeal.IsAppeal && !tmpdeal.IsAppliances && !tmpdeal.IsJewelry && !tmpdeal.IsOfficeSupplies)
                            || tmpdeal.IsOthers)
                        {
                            int tmpindex = divMix.Length - 7;
                            divMix = divMix.Insert(tmpindex, tmpLine);
                            bMix = true;
                        }
                    }
                    #endregion
                }
                emailcontent = (bHot ? divHot : "") +
                    (bElec ? divElec : "") +
                    //(bFree ? divFree : "") +
                    (bFinance ? divFinance : "") +
                    (bDrug ? divDrug : "") +
                    (bTravel ? divTravel : "") +
                    (bAppl ? divApp:"")+
                    (bAlle? divAppeal:"")+
                    (bBea? divBeauty:"")+
                    (bRes? divRestaurant:"")+
                    (bOS?divOS:"")+
                    (bJew?divJewelry:"")+
                    (bMix ? divMix : "");
                    
                SendEmail(emailcontent, current.Email, current.FirstName+" "+current.LastName, dealids.Length);
                Move2Delivered(current.scheduleID);
            }        
        }

        // the hot deal icon for now...
        // http://freebiesdealsandrewards.com/forum/images/icons/hot-deal.png
        //

        private string MakeUp(Deals tmpdeal,string keywords)   // improve 1. sequence, 2. timestamp after link
        {
            string andsplit = "~^~";
            string exlcudesplit = "!^!";
            string[] splitarray = { andsplit, exlcudesplit };

            // grab the deal, and format it...
            string tmptitle = tmpdeal.Title;
            string[] keys = keywords.Split(',');

            foreach(string key in keys)   // todo: && || keyword, need split again.....
            {
                if (key.Contains(andsplit) || key.Contains(exlcudesplit))
                {
                    string[] subkeys = key.Split(splitarray, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string subkey in subkeys)
                    {
                        if (!key.Contains(exlcudesplit + subkey))
                        {
                            //tmptitle = tmptitle.Replace(subkey, "<span style='background-color:Yellow'><b><u>" + subkey + "</u></b></span>");  
                            tmptitle = ReplaceEx(tmptitle, subkey, "<span style='background-color:Yellow'><b><u>" + subkey.ToUpper() + "</u></b></span>");
                        }
                    }
                }
                else
                    tmptitle = ReplaceEx(tmptitle, key, "<span style='background-color:Yellow'><b><u>" + key.ToUpper() + "</u></b></span>");
                    //tmptitle = tmptitle.Replace(key, "<span style='background-color:Yellow'><b><u>" + key + "</u></b></span>");            
            }

            string ishot = tmpdeal.Ishot ? "<img src='http://freebiesdealsandrewards.com/forum/images/icons/hot-deal.png' alt='Hot Deal' border='0' />" : "";

            string content = "<div ><a href='" + tmpdeal.URL + "'>" + tmptitle + "</a>" + ishot + "</div><i> (" + tmpdeal.InDate.ToString() + ")</i><br/><br/>";

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(tmpdeal.Title.ToLower());
            return content;
        }

        /* 
         * Ignore case when replacing...
         * http://www.codeproject.com/KB/string/fastestcscaseinsstringrep.aspx 
         * 
        */
        private static string ReplaceEx(string original, string pattern, string replacement)
        {
            int count, position0, position1;
            count = position0 = position1 = 0;
            string upperString = original.ToUpper();
            string upperPattern = pattern.ToUpper();
            int inc = (original.Length / pattern.Length) *
                      (replacement.Length - pattern.Length);
            char[] chars = new char[original.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern,
                                              position0)) != -1)
            {
                for (int i = position0; i < position1; ++i)
                    chars[count++] = original[i];
                for (int i = 0; i < replacement.Length; ++i)
                    chars[count++] = replacement[i];
                position0 = position1 + pattern.Length;
            }
            if (position0 == 0) return original;
            for (int i = position0; i < original.Length; ++i)
                chars[count++] = original[i];
            return new string(chars, 0, count);
        }

        protected void SendEmail(string content, string to, string cName, int dealcnt)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("*** sending email");
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                string subject = cName + ": sDeals " + dealcnt + " deals alert! @" + DateTime.Now.ToLongTimeString();
                string[] receiver = { to };
              
                foreach (string emailadd in receiver)
                {
                    message.To.Add(emailadd);
                }


                message.Subject = subject;
               
                message.From = new System.Net.Mail.MailAddress("gooddeal.2u@gmail.com");
                message.Body = content;
                message.IsBodyHtml = true;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                System.Net.NetworkCredential y = new System.Net.NetworkCredential("intime.deals@gmail.com", "fa2010dacai"); ;
                smtp.Credentials = y;

                smtp.Send(message);

            }
            catch (Exception ex)
            {
                string y = ex.Message;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(y);
            }

        }

        private void Move2Delivered(int scheduleID)
        {
            //todo
            // copy to history table, so won't delivery multiple times
            DBQuerys.MoveScheduled2Delivered(scheduleID);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("*** move to delivered");
        }

    }
}
