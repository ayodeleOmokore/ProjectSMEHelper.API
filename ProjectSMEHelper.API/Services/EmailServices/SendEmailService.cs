using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using ProjectSMEHelper.API.Contracts.Email;
using ProjectSMEHelper.API.Helpers;



namespace ProjectSMEHelper.API.Services.EmailServices;

public class SendEmailService : ISendEmailService
{
    private IConfiguration _config;
    private DAL _dAL;
    public SendEmailService(IConfiguration configuration, DAL dAL)
    {
        _config = configuration;
        _dAL = dAL;
    }
    public async Task<string> SendEmailAsync(EmailDtos emailDtos)
    {

        string resp = string.Empty;
        string emailFrom = _config.GetValue<string>("CommunicationConfiguration:EmailConfiguration:SenderName");
        int emailPort = int.Parse(_config.GetValue<string>("CommunicationConfiguration:EmailConfiguration:Port"));
        string emailHost = _config.GetValue<string>("CommunicationConfiguration:EmailConfiguration:Host"); ;
        string emailSender = _config.GetValue<string>("CommunicationConfiguration:EmailConfiguration:Username"); ;
        string emailSenderPassword = _config.GetValue<string>("CommunicationConfiguration:EmailConfiguration:Password");
        string blindCopy = _config.GetValue<string>("CommunicationConfiguration:EmailConfiguration:BlindCopy") ?? "";// "omokore.ayodele@gmail.com";

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(emailFrom));
        email.To.Add(MailboxAddress.Parse(emailDtos.Receiver));//"info@tranzkript.com"
        if (blindCopy != "")
        {
            email.Bcc.Add(MailboxAddress.Parse(blindCopy));
        }
        email.Subject = emailDtos.Subject;
        string formatedBodyTemplate = await _dAL.GetEmailContainer();
        emailDtos.Body = formatedBodyTemplate.Replace("#body#", emailDtos.Body).Replace("#subject#", emailDtos.Subject);
        email.Body = new TextPart(TextFormat.Html) { Text = emailDtos.Body };

        using var smtp = new SmtpClient();
        smtp.Connect(emailHost, emailPort, SecureSocketOptions.StartTls);
        smtp.Authenticate(emailSender, emailSenderPassword);
        resp = smtp.Send(email);
        smtp.Disconnect(true);
        return resp;
    }

}
