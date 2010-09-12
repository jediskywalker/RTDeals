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
               
                Console.WriteLine("***delivery");
                Thread.Sleep(5000);
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
                    emailcontent += MakeUp(tmpdeal);
                }

                SendEmail(emailcontent, current.Email, current.customerID);
                Move2Delivered(current.scheduleID);
            }        
        }

        private string MakeUp(Deals tmpdeal)
        {
            // grab the deal, and format it...
            string content = "<div ><a href='" + tmpdeal.URL + "'>" + tmpdeal.Title + "</a></div><br/><br/>";

            Console.WriteLine(content);
            return content;
        }

        protected void SendEmail(string content, string to, int cid)
        {
            Console.WriteLine("*** sending email");
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                
                string subject = cid.ToString()+ ": Your Deals Alert from intime.deals";
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
                Console.WriteLine(y);
            }

        }

        private void Move2Delivered(int scheduleID)
        {
            //todo
            // copy to history table, so won't delivery multiple times
            DBQuerys.MoveScheduled2Delivered(scheduleID);
            Console.WriteLine("*** move to delivered");
        }

    }
}
