using System.Net.Mail;
using System.Net;

using Microsoft.Extensions.Logging;

namespace OtoTamir.WEBUI.Services.MailHelper
{
    public interface IMailHelper
    {
        bool SendMail(string body, string to, string subject, bool isHtml = true);
        bool SendMail(string body, List<string> to, string subject, bool isHtml = true);
    }
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailHelper> _logger;

        public MailHelper(IConfiguration configuration, ILogger<MailHelper> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public bool SendMail(string body, string to, string subject, bool isHtml = true)
        {
            return SendMail(body, new List<string> { to }, subject, isHtml);
        }

        public bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
        {
            bool result = false;
            try
            {
                var message = new MailMessage();
                message.Subject = subject;

                to.ForEach(x => message.To.Add(new MailAddress(x)));

               
                var senderEmail = _configuration["MailSettings:SenderEmail"];
                var senderPassword = _configuration["MailSettings:SenderPassword"];
                var host = _configuration["MailSettings:Host"];
                var port = int.Parse(_configuration["MailSettings:Port"]);

                message.From = new MailAddress(senderEmail);
                message.Body = body;
                message.IsBodyHtml = isHtml;

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = host;
                    smtp.EnableSsl = true;
                    smtp.Port = port;
                    smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);

                    smtp.Send(message);
                    result = true;
                    _logger.LogInformation("E-posta başarıyla gönderildi. Konu: {Subject}, Alıcılar: {To}", subject, string.Join(",", to));
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "E-posta gönderimi başarısız oldu! Konu: {Subject}, Alıcılar: {To}", subject, string.Join(",", to));
            }

            return result;
        }
    }
}