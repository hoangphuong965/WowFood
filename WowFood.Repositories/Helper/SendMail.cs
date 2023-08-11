using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace WowFood.Repositories.Helper
{
    public class SendMail
    {
        private static readonly string EmailAdmin = WebConfigurationManager.AppSettings["EmailAdmin"];
        private static readonly string Password = WebConfigurationManager.AppSettings["PasswordEmail"];

        public static bool Send(string name, string subject, string content, string toMail)
        {
            bool rs;
            try
            {
                MailMessage message = new MailMessage();
                var smtp = new SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com"; //host name
                    smtp.Port = 587; //port number
                    smtp.EnableSsl = true; //whether your smtp server requires SSL
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential()
                    {
                        UserName = EmailAdmin,
                        Password = Password
                    };
                }
                MailAddress fromAddress = new MailAddress("wowfood@gmail.com", name);
                message.From = fromAddress;
                message.To.Add(toMail);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = content;
                smtp.Send(message);
                rs = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rs = false;
            }
            return rs;
        }
    }
}