using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class userMSMQ
    {
        MessageQueue queue = new MessageQueue();
        
        private string receiverName;
        private string receiverEmailID;

        public void SendMail(string token, string emailID, string name )
        {
            this.receiverEmailID = emailID;
            this.receiverName = name;
            queue.Path = @".\Private$\Token";

            try
            {
                if(!MessageQueue.Exists(queue.Path))
                {
                    MessageQueue.Create(queue.Path);
                }
                queue.Formatter = new XmlMessageFormatter(new Type[] {typeof(string) });
                queue.ReceiveCompleted += Queue_RecieveCompleted;
                queue.Send(token);
                queue.BeginReceive();
                queue.Close();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            
        }
        private void Queue_RecieveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var message = queue.EndReceive(e.AsyncResult);
                string token = message.Body.ToString();
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("surveshrathore98@gmail.com", "eondshuodywiynas"),
                };
                mailMessage.From = new MailAddress("surveshrathore98@gmail.com");
                mailMessage.To.Add(new MailAddress(receiverEmailID));

                string mailBody = $"<!DOCTYPE html>" +
                                  $"<html>" +
                                    $"<body style = \"background-color:#DBFF73;text-align:center;padding:5px;\">" +
                                    $"<h1 style = \"color:#6A8D02; border-bottom: 3px solid #84AF08; margin-top: 5px;\"> Dear <b>{receiverName}</b> </h1>\n" +
                                    $"<a href = \'http://localhost:4200/ResetPassword/{token}'>Click me </a>"+
                                    $"</body>" +
                                    $"</html>";
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Fundoo Notes Passwork Reset Link";
                smtpClient.Send(mailMessage);

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


    }
}
