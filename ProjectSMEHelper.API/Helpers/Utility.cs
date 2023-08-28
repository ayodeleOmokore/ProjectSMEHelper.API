using Microsoft.EntityFrameworkCore;
using ProjectSMEHelper.API.Contracts.Email;
using ProjectSMEHelper.API.Contracts.Users.Responses;
using ProjectSMEHelper.API.Services.EmailServices;
using System.Security.Cryptography;
using System.Text;

namespace ProjectSMEHelper.API.Helpers;

public class Utility
{
    private readonly IConfiguration _configuration;
    private readonly ISendEmailService _sendEmailService;
    public Utility(IConfiguration configuration, ISendEmailService sendEmailService)
    {
        _configuration = configuration;
        _sendEmailService = sendEmailService;
    }
    public async Task<bool> VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computerHash.SequenceEqual(passwordHash);
        }
    }
    public async Task<string> GenerateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
    public async Task SendVerificationNotification(RegisterUserResponse user, string verificationCode)
    {
        string AppName = _configuration.GetValue<string>("AppName") ?? "Tranzkript";
        string RootUrl = _configuration.GetValue<string>("DefaultURL") ?? "https://app.tranzkript/";
        string VerificationPath = _configuration.GetValue<string>("VerificationPath") ?? "auth/verifyme";
        string body = @$"Hello {user.FirstName},<br/><br/>We are delighted to have you signup to {AppName}, please click the link below to complete your sign-up process.<br/><br/>{RootUrl}{VerificationPath}/{verificationCode}<br/><br/>Regards.";
        EmailDtos emailDtos = new()
        {
            Receiver = user.Email,
            Subject = $"Verify your account - {AppName}",
            Body = body,
        };
        await _sendEmailService.SendEmailAsync(emailDtos);
    }
}
