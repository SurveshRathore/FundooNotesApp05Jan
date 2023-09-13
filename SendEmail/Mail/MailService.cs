using MassTransit;
using System.Net.Mail;
using System.Net;
using CommonLayer.Model;

namespace SendEmail.Mail
{
    public class MailService: IConsumer<ConsumerEmail>
    {
        public async Task Consume(ConsumeContext<ConsumerEmail> context)
        {
            var data = context.Message; // containg data which is consume
            string email = data.Email;
            string receiverName = "hello";
            string token = data.Token;
            //string token = data.Token.ToString();
            string subject = "Fundoo password Reset link";

            string mailBody = $"<!DOCTYPE html>" +
                             $"<html>" +
                               $"<body style = \"background-color:#DBFF73;text-align:center;padding:5px;\">" +
                               $"<h1 style = \"color:#6A8D02; border-bottom: 3px solid #84AF08; margin-top: 5px;\"> Dear <b>{receiverName}</b> </h1>\n" +
                               $"<a href = \'http://localhost:4200/ResetPassword/{token}'>Click me </a>" +
                               $"</body>" +
                               $"</html>";

            
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")  // SMTP Configur gmail Smtp server setting
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("surveshrathore98@gmail.com", "eondshuodywiynas"),
            };

            smtpClient.Send("surveshrathore98@gmail.com", email, subject, mailBody);

        }
    }
}
