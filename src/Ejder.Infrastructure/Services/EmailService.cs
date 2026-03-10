using System.Threading.Tasks;
using Ejder.Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Ejder.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendNewTourNotificationAsync(string tourName, string tourUrl)
    {
        var host = _configuration["SmtpSettings:Host"];
        var portStr = _configuration["SmtpSettings:Port"];
        var port = int.TryParse(portStr, out int p) ? p : 587;
        var username = _configuration["SmtpSettings:Username"];
        var password = _configuration["SmtpSettings:Password"];
        var senderName = _configuration["SmtpSettings:SenderName"];
        var adminEmail = _configuration["SmtpSettings:AdminEmail"];

        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(adminEmail))
            return; // Config yoksa çık

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(senderName ?? "Ejder Backoffice", username));
        message.To.Add(new MailboxAddress("Admin", adminEmail));
        message.Subject = $"Yeni Tur Eklendi: {tourName}";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <h3>Sisteme yeni bir tur eklendi!</h3>
                <p><strong>Tur Adı:</strong> {tourName}</p>
                <p>İncelemek veya düzenlemek için <a href='{tourUrl}'>tıklayınız</a>.</p>"
        };

        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(username, password);
            await client.SendAsync(message);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}
