using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class RabbitMQ
    {
        public String SendMail(string emailTo, string token)
        {
			try
			{
				string emailFrom = "sarveshrathour2002@gmail.com";
				MailMessage message = new MailMessage(emailFrom, emailTo);
				string mailBody = "Token Generated: " + token;

				message.Subject = "Token will be expired in 15 minutes. "; 
				message.Body = mailBody.ToString();
				message.BodyEncoding = Encoding.UTF8;

				message.IsBodyHtml = true;

				SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",587); // gmail smtp System.Net.NetworkCredential 
				
				System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential("surveshrathore98@gmail.com", "eondshuodywiynas");

				smtpClient.EnableSsl = true;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = networkCredential;
				smtpClient.Send(message);

				return emailTo;
				
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
