using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace OSNB.Helpers
{
    public class SendMailHelper
    {
        public bool SendEmail(string to, string subject, string body)
        {
            bool isSended = false;

            try
            {
                //sending mail
                var message = new System.Net.Mail.MailMessage();
                message.From = new MailAddress("osnbfrommail@gmail.com");
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;

                SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.UseDefaultCredentials = false;
                var credentials = new NetworkCredential("osnbfrommail@gmail.com", "@@123456");
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(message);

                isSended = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return isSended;
        }
    }
}