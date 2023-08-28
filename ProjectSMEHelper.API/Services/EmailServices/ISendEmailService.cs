using ProjectSMEHelper.API.Contracts.Email;

namespace ProjectSMEHelper.API.Services.EmailServices;

public interface ISendEmailService
{
    Task<string> SendEmailAsync(EmailDtos emailDto);
}
