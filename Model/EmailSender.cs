using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Model
{
    internal class EmailSender
    {
        public EmailSender()
        {

        }

        public void sendMail(string userMail, string subject, string body)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            var cred = new NetworkCredential("MapsZone.contact@gmail.com",
                "wfomwqwogwngihct");
            var mail = new MailMessage();

            mail.From = new MailAddress(cred.UserName);
            mail.To.Add(userMail);
            mail.Subject = subject;
            mail.Body = body;

            client.UseDefaultCredentials = false;
            client.Credentials = cred;
            client.EnableSsl = true;
            
            client.Send(mail);
        }
    }
}
