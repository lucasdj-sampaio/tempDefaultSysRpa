#region - Imports
using System.Net;
using System.Net.Mail;
#pragma warning disable IDE1006, S4830
#endregion

namespace COE000.Portal.NomeProjeto.Util
{
    public class EmailService
    {
        private string _host { get; set; }
        private int _port { get; set; }

        private dynamic HostCredential { get; set; }

        private readonly string _template = @"<!DOCTYPE html>
			<html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
				<head>
					<meta charset='utf-8' />
					<title>Email</title>
				</head>
				<body style='margin:20px;'>
					<p style='font-family:Calibri; font-size:16px; color:#666666;'>
						#[CONTENT]
					</p>
					<p style='font-family:Calibri; font-size:14px; color:#1F1589;'>
						COE (Centro de Excelência em Automação)
						<br />
						<span style='color:#F87C46'>Atento Interfile<span>
					</p>
				</body>
			</html>";

        public EmailService(IConfiguration configuration)
        {
#if DEBUG
            _host = configuration["HomologEmailSetting:Host"];
            _port = int.Parse(configuration["HomologEmailSetting:Port"]);

            HostCredential = new { 
                User = configuration["HomologEmailSetting:User"],
                Password = configuration["HomologEmailSetting:Password"]
            };
#else
            _host = configuration["EmailSetting:Host"];
            _port = int.Parse(configuration["EmailSetting:Port"]);

            HostCredential = new { 
                User = configuration["EmailSetting:User"],
            };
#endif
        }

        public async Task<bool> SendAsync(string adressToSend, string subject, string body)
        {
            try
            {
                var content = _template.Replace("#[CONTENT]", body);

                using var message = new MailMessage
                {
                    From = new MailAddress(HostCredential.User),
                    Subject = subject,
                    Body = content,
                    Priority = MailPriority.High,
                    IsBodyHtml = true,
                };

                message.To.Add(adressToSend);
                
                using var client = new SmtpClient(_host, _port);
#if DEBUG
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(HostCredential.User, HostCredential.Password);
#else
                client.EnableSsl = false;
                client.Credentials = new NetworkCredential(HostCredential.User, "");
#endif
                ServicePointManager.ServerCertificateValidationCallback += (s, c, h, e) => true;
                await client.SendMailAsync(message);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}