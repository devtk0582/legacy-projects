using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLib.Smtp
{
    public class SmtpHelper
    {
        public void SendEmail(string fromAddress, string toAddress, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.ReplyToList.Add(new MailAddress(fromAddress));

            SmtpClient client = new SmtpClient();
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                //Log Error
            }
        }
    }
}
