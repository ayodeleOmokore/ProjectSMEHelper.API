using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ProjectSMEHelper.API.Contracts.Users.Requests;
using ProjectSMEHelper.API.Contracts.Users.Responses;
using ProjectSMEHelper.API.DBContext.PostgreDBContext;
using ProjectSMEHelper.API.Helpers;
using ProjectSMEHelper.API.Models.Users;
using ProjectSMEHelper.API.Services.UserServices.Interfaces;

namespace ProjectSMEHelper.API.Services.UserServices.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly PostgreDbContext _postgreDB;
        private readonly Utility _utility;
        public LoginService(PostgreDbContext postgreDbContext, Utility utility)
        {
            _postgreDB = postgreDbContext;
            _utility = utility;

        }
        public async Task<LoginResponse?> CheckIfEmailExists(string email)
        {
            User user = await _postgreDB.User.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
            if (user != null)
            {
                return new LoginResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.Firstname,
                    LastName = user.Lastname,
                    Status = user.Status,
                    Timestamp = user.LastModifiedDate,
                    OId = user.OId,
                    OIdProvider = user.OIdProvider,
                    Verified = user.Verified,
                    CompanyId = user.CompanyId,
                };
            }
            else
            {
                return null;
            }
        }
        public async Task<LoginResponse?> Login(LoginRequest loginRequest)
        {
            User? user = await _postgreDB.User.AsNoTracking().FirstOrDefaultAsync(x => x.Email == loginRequest.Email);
            if (user == null)
            {
                return new LoginResponse
                {
                    FirstName = "User does not exist",
                };
            }
            var resp = await _utility.VerifyPasswordHash(loginRequest.Password??"", user.PasswordHash, user.PasswordSalt);
            if (!resp)
            {
                return new LoginResponse
                {
                    FirstName = "Incorrect username and password",
                };
            }
            if (!user.Verified)
            {
                return new LoginResponse
                {
                    FirstName = "User has not been verified, please check your email for onboarding email",
                };
            }
            return new LoginResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                Status = user.Status,
                Timestamp = user.LastModifiedDate,
                OIdProvider = user?.OIdProvider,
                OId = user?.OId,
                Verified = user.Verified,
                VerifiedAt = user.VerifiedAt,
                CompanyId = user.CompanyId,
            };
        }
    }
}
