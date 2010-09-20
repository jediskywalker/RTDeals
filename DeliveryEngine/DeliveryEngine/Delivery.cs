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
            string divFree = "<div> <span style='font-size:large;'>Free:</span><br/><br/>     </div>";
            string divFinance = "<div> <span style='font-size:large;'>Finance:</span><br/><br/>     </div>";
            string divHot = "<div> <span style='font-size:large;'>Hot: </span><br/><br/>     </div>";
            string divMix = "<div> <span style='font-size:large;'>Others:</span><br/><br/>     </div>";

            bool bElec = false;
            bool bDrug = false;
            bool bTravel = false;
            bool bFree = false;
            bool bFinance = false;
            bool bHot = false;
            bool bMix = false;

            foreach (ScheduledDelivery current in allscheduled) // foreach customer...
            {
                emailcontent="";

                divElec = "<div> <span style='font-size:large;'>Electronic:</span><br/><br/>     </div>";
                divDrug = "<div> <span style='font-size:large;'>Drugs:</span><br/><br/>     </div>";
                divTravel = "<div> <span style='font-size:large;'>Travel:</span><br/><br/>     </div>";
                divFree = "<div> <span style='font-size:large;'>Free:</span><br/><br/>     </div>";
                divFinance = "<div> <span style='font-size:large;'>Finance:</span><br/><br/>     </div>";
                divHot = "<div> <span style='font-size:large;'>Hot: </span><br/><br/>     </div>";
                divMix = "<div> <span style='font-size:large;'>Others:</span><br/><br/>     </div>";

                bElec = false;
                bDrug = false;
                bTravel = false;
                bFree = false;
                bFinance = false;
                bHot = false;
                bMix = false;


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
                    if (tmpdeal.IsFree)
                    {
                        int tmpindex = divFree.Length - 7;
                        divFree = divFree.Insert(tmpindex, tmpLine);
                        bFree = true;
                    }
                    if (tmpdeal.Ishot)
                    {
                        int tmpindex = divHot.Length - 7;
                        divHot = divHot.Insert(tmpindex, tmpLine);
                        bHot = true;
                    }
                    if (tmpdeal.IsTravel)
                    {
                        int tmpindex = divTravel.Length - 7;
                       divTravel =  divTravel.Insert(tmpindex, tmpLine);
                        bTravel = true;
                    }
                    if (!tmpdeal.IsTravel && !tmpdeal.Ishot && !tmpdeal.IsFree && !tmpdeal.IsFinance && !tmpdeal.IsElectronic && !tmpdeal.IsDrug)
                    {
                        int tmpindex = divMix.Length - 7;
                        divMix = divMix.Insert(tmpindex, tmpLine);
                        bMix = true;
                    }
                }
                emailcontent = (bHot ? divHot : "") +
                    (bElec ? divElec : "") +
                    (bFree ? divFree : "") +
                    (bFinance ? divFinance : "") +
                    (bDrug ? divDrug : "") +
                    (bTravel ? divTravel : "") +
                    (bMix ? divMix : "");
                    
                SendEmail(emailcontent, current.Email, current.FirstName+" "+current.LastName, dealids.Length);
                Move2Delivered(current.scheduleID);
            }        
        }

        private string MakeUp(Deals tmpdeal,string keywords)   // improve 1. sequence, 2. timestamp after link
        {
            string andsplit = "~^~";
            string exlcudesplit = "!^!";
            string[] splitarray = { andsplit, exlcudesplit };

            // grab the deal, and format it...
            string tmptitle = tmpdeal.Title.ToLower();
            string[] keys = keywords.ToLower().Split(',');

            foreach(string key in keys)   // todo: && || keyword, need split again.....
            {
                if (key.Contains(andsplit) || key.Contains(exlcudesplit))
                {
                    string[] subkeys = key.Split(splitarray, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string subkey in subkeys)
                    {
                        if (!key.Contains(exlcudesplit + subkey))
                        {
                            tmptitle = tmptitle.Replace(subkey, "<span style='background-color:Yellow'><b><u>" + subkey + "</u></b></span>");  
                        }
                    }
                }
                else                
                    tmptitle = tmptitle.Replace(key, "<span style='background-color:Yellow'><b><u>" + key + "</u></b></span>");            
            }

            string content = "<div ><a href='" + tmpdeal.URL + "'>" + tmptitle + "</a></div><i> (" + tmpdeal.InDate.ToString() + ")</i><br/><br/>";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(tmpdeal.Title.ToLower());
            return content;
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
