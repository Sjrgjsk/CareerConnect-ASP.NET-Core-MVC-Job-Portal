using System.Net;
using System.Net.Mail;

namespace CareerConnect.Helpers
{
    public class EmailHelper
    {
        public static async Task SendEmailAsync(
            string toEmail,
            string subject,
            string body)
        {
            var message = new MailMessage();

            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            message.From = new MailAddress("your_email@gmail.com");

            using var smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential(
                "your_email@gmail.com",
                "your_app_password");

            smtp.EnableSsl = true;

            await smtp.SendMailAsync(message);
        }
    }
}