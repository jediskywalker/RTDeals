using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace RTDealsScanerEngine
{
    class SendEmail
    {
        public static void SendDealsEmail(string MailTo, string MailFrom, string Subject, string MailBody)
        {
            try
            {

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(MailFrom, "Scaner Engine Error!");
                mail.To.Add(new MailAddress(MailTo));
                mail.IsBodyHtml = true;
                mail.Body = MailBody;
                mail.Subject = Subject;
                SmtpClient smtp = new SmtpClient("smtp.live.com");
                NetworkCredential Credential = new NetworkCredential("rtdeals@hotmail.com", "xuhao123456");
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = Credential;
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw (ex);
            }
        }
    }
}
