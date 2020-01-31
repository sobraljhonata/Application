using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Infra.CrossCuting.Identity.Interfaces;

namespace Application.Infra.CrossCuting.Identity.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            if(!ValidarEmailPattern(email))
                throw new ArgumentException("Email inválido");

            const string usuario = "email_sender";
            const string remetente = "name_remtente";
            var _mailMessage = new MailMessage()
            {
                From = new MailAddress(usuario, remetente)
            };

            _mailMessage.To.Add(new MailAddress(email));
            _mailMessage.Subject = subject;
            _mailMessage.Body = message;

            using (var smtp = new SmtpClient("mail_server, mail_port"))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("email_sender", "email_password");
                smtp.EnableSsl = true;
                smtp.SendMailAsync(_mailMessage);
            }

            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        private static bool ValidarEmailPattern(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            const string MatchEmailPattern =
                @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

            return Regex.IsMatch(email, MatchEmailPattern);
        }
        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}