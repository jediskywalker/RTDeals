using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;


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

            foreach (ScheduledDelivery current in allscheduled) // foreach customer...
            {
                emailcontent="";
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
                               
                    emailcontent += MakeUp(tmpdeal,current.Keywords);
                }

                SendEmail(emailcontent, current.Email, current.customerID, dealids.Length);
                Move2Delivered(current.scheduleID);
            }        
        }

        private string MakeUp(Deals tmpdeal,string keywords)   // improve 1. sequence, 2. timestamp after link
        {
            // grab the deal, and format it...
            string tmptitle = tmpdeal.Title.ToLower();
            string[] keys = keywords.ToLower().Split(',');
            foreach(string key in keys)
            {               
               tmptitle = tmptitle.Replace(key, "<b><u>" + key + "</u></b>");            
            }

            string content = "<div ><a href='" + tmpdeal.URL + "'>" + tmptitle + "</a></div><i> (" + tmpdeal.PubDate + ")</i><br/><br/>";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(tmpdeal.Title.ToLower());
            return content;
        }

        protected void SendEmail(string content, string to, int cid, int dealcnt)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("*** sending email");
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                
                string subject = cid.ToString()+ ": you got "+dealcnt+" deals alert from sDeals";
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
