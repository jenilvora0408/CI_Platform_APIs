using Common.Constants;
using Common.Utils.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MimeKit;
using Services.Interfaces;

namespace Services.Implementations;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _env;

    public EmailService(IConfiguration configuration, IHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public async Task SendEmailAsync(EmailMessage message)
    {
        EmailSetting setting = GetEmailSetting();

        MimeMessage mimeMessage = CreateEmailMessage(setting,message);

        await SendAsync(setting, mimeMessage);
    }

    private EmailSetting GetEmailSetting()
    {
        EmailSetting setting = new ();
        _configuration.GetSection(SystemConstant.EMAIL_SETTING).Bind(setting);
        return setting;
    }

    private MimeMessage CreateEmailMessage(EmailSetting setting, EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email",setting.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;

        // Read the template file
        string templatePath = _env.ContentRootPath + SystemConstant.EMAIL_TEMPLATES_PATH + "Reset_Password.html";
        var templateContent = File.ReadAllText(templatePath);

        // Replace placeholders in the template content
        templateContent = templateContent.Replace("{{resetLink}}", message.Content);
        templateContent = templateContent.Replace("{{minutes}}", SystemConstant.RESET_PASSWORD_TOKEN_EXPIRY_MINUTES.ToString());

        var bodyBuilder = new BodyBuilder { HtmlBody = templateContent };

        if (message.Attachments != null && message.Attachments.Any())
        {
            byte[] fileBytes;
            foreach (var attachment in message.Attachments)
            {
                using (var ms = new MemoryStream())
                {
                    attachment.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
            }
        }

        emailMessage.Body = bodyBuilder.ToMessageBody();
        return emailMessage;
    }

    private static async Task SendAsync(EmailSetting setting, MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(setting.SmtpServer, setting.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(setting.From, setting.Password);

                await client.SendAsync(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception, or both.
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
