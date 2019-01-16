using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Urlaub
{
    public class EmailSender : IEmailSender
    {
        private IConfiguration _iconfiguration;

        public EmailSender(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public Task SendEmailAsync(string To_Email_Address, string subject, string message)
        {
            try
            {
                /* vars */
                string sFrom_Email_Address = _iconfiguration.GetSection("Domain").Value;
                string sFrom_Email_DisplayName = _iconfiguration.GetSection("DisplayName").Value;
                string sSenderAppname = _iconfiguration.GetSection("SenderAppname").Value;
                string sHost = _iconfiguration.GetSection("Host").Value;
                int intPort = Convert.ToInt32(_iconfiguration.GetSection("Port").Value);
                string sEmail_Login = _iconfiguration.GetSection("Login").Value;
                string sEmail_Passwort = _iconfiguration.GetSection("Passwort").Value;

                /* MailMessage */
                MailMessage email = new MailMessage();
                email.To.Add(new MailAddress(To_Email_Address));
                email.From = new MailAddress(sFrom_Email_Address, sFrom_Email_DisplayName);
                email.Subject = sSenderAppname + " " + subject;
                email.Body = message;
                email.IsBodyHtml = true;

                /* EmailClient */
                SmtpClient smtp = new SmtpClient();
                smtp.Host = sHost;
                smtp.Port = intPort;
                smtp.Credentials = new NetworkCredential(sEmail_Login, sEmail_Passwort);
                smtp.EnableSsl = true;

                /* send */
                try
                {
                    smtp.Send(email);
                     return Task.FromResult<object>(true);
                }
                catch (System.Exception ex)
                {
                     return Task.FromResult<object>(ex.Message);
                }
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error EmailSender.cs error:" + ex.Message);
                return Task.FromResult<object>(false);
            }
        }
    }
}