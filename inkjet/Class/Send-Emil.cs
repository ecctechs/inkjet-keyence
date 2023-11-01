using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace send_email
{
    public class Send_Email
    {
        public void send(string name, string subject, string detail)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("theerasak789900@gmail.com");
            mailMessage.To.Add(name);
            mailMessage.Subject = subject;
            mailMessage.Body = detail;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("theerasak789900@gmail.com", "nabqxphxunwfkltb");
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email Sent Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
