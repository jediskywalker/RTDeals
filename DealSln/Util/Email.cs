using System;
using System.Net.Mail;

namespace Util
{
    public class Email
    {
        #region email sending functions
        public static void SendHtmlEmail(string senderEmail, string receipant, string subject, string content)
        {
            SendHtmlEmail(senderEmail, receipant, null, null, subject, content);
        }

        public static void SendHtmlEmail(string senderEmail, string receipant, string CC, string BCC, string subject, string content)
        {
            if (string.IsNullOrEmpty(receipant))
            {
                Logger.LogError("Email.SendHtmlEmail", "Missing receipant address");
                return;
            }
            if (string.IsNullOrEmpty(subject))
            {
                Logger.LogError("Email.SendHtmlEmail", "Missing subject");
                return;
            }

            if (string.IsNullOrEmpty(senderEmail))
            {
                Logger.LogError("Email.SendHtmlEmail", "Missing from address");
                return;
            }

            SmtpClient client = new SmtpClient();
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = content;

            // addresses
            MailAddressCollection mas = getMailAddressCollectionFromString(receipant);
            foreach (MailAddress ma in mas)
            {
                mail.To.Add(ma);
            }
            if (!string.IsNullOrEmpty(CC))
            {
                mas = getMailAddressCollectionFromString(CC);

                foreach (MailAddress ma in mas)
                {
                    mail.CC.Add(ma);
                }
            }
            if (!string.IsNullOrEmpty(BCC))
            {
                mas = getMailAddressCollectionFromString(BCC);
                foreach (MailAddress ma in mas)
                {
                    mail.Bcc.Add(ma);
                }
            }

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Logger.LogError("Email.SendHtmlEmail", "Fail to send to " + receipant + " with subj:" + subject + " ex:" + ex.Message);
            }
        }

        public static MailAddressCollection getMailAddressCollectionFromString(string emailsInString)
        {
            string[] emails = emailsInString.Split(new char[] { ',', ';' });

            MailAddressCollection returnCollection = new MailAddressCollection();

            foreach (string s in emails)
            {
                int length = s.IndexOf('>') - s.IndexOf('<');
                //sometimes when the name is empty the customeremail is just the emial (without "<"  ">")
                string thisEmail = "";
                if (length == 0)
                    thisEmail = s;
                else
                    thisEmail = s.Substring(s.IndexOf('<') + 1, length - 1);

                returnCollection.Add(thisEmail);

            }
            return returnCollection;


        }

        #endregion email sending functions
    }
}