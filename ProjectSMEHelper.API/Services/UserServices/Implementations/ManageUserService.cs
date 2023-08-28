using Azure.Core;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectSMEHelper.API.Contracts.Users.Requests;
using ProjectSMEHelper.API.Contracts.Users.Responses;
using ProjectSMEHelper.API.DBContext.PostgreDBContext;
using ProjectSMEHelper.API.Helpers;
using ProjectSMEHelper.API.Models.Users;
using ProjectSMEHelper.API.Services.UserServices.Interfaces;
using User = ProjectSMEHelper.API.Models.Users.User;

namespace ProjectSMEHelper.API.Services.UserServices.Implementations
{
    public class ManageUserService : IManageUserService
    {
        private readonly PostgreDbContext _postgreDB;
        private readonly Helpers.Utility _utility;
        public ManageUserService(PostgreDbContext postgreDbContext, Helpers.Utility utility)
        {
            _postgreDB = postgreDbContext;
            _utility = utility;

        }
        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            User user = _postgreDB.User.FirstOrDefault(x => x.Email.Trim() == registerUserRequest.Email.Trim());
            if (user != null)
            {
                return new RegisterUserResponse
                {
                    FirstName = "Email exists",
                };
            }
            if (!user.Verified)
            {
                return new RegisterUserResponse
                {
                    FirstName = "Email exists, but awaits verification",
                };
            }
            _utility.CreatePasswordHash(registerUserRequest.Password.Trim(), out byte[] passwordHash, out byte[] passwordSalt);
            string randomToken = await _utility.GenerateRandomToken();
            User newUser = new()
            {
                Email = registerUserRequest.Email.Trim(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = randomToken,
                Firstname = registerUserRequest.FirstName.Trim(),
                Lastname = registerUserRequest.LastName.Trim(),
                Phonenumber = registerUserRequest.PhoneNumber.Trim(),
                Status = 1,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Verified = false,
                OId = Guid.NewGuid().ToString(),
                OIdProvider = "Self",
            };
            _postgreDB.User.Add(newUser);
            await _postgreDB.SaveChangesAsync();
            User dbUser = await _postgreDB.User.AsNoTracking().FirstOrDefaultAsync(x => x.Email == registerUserRequest.Email);
            if (dbUser != null)
            {
                RegisterUserResponse registerUserResponse = new()
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.Firstname,
                    LastName = dbUser.Lastname,
                    PhoneNumber = dbUser.Phonenumber,
                    Status = dbUser.Status,
                };
                await _utility.SendVerificationNotification(registerUserResponse, randomToken);
                return registerUserResponse;
            }
            return null;
        }
        //
       //Code Smell: Optimise the condition where user already exists either by system authentication or other providers
        public async Task<RegisterUserResponse> RegisterUserByGoogle(RegisterUserByGoogleRequest request)
        {
            if (String.IsNullOrWhiteSpace(request.Email))
            {
                return new RegisterUserResponse
                {
                    FirstName = "Invalid Email",
                };
            }
            User user = await _postgreDB.User.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Trim() == request.Email.Trim());
            if (user != null)
            {
                return new RegisterUserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.Firstname,
                    LastName = user.Lastname,
                    PhoneNumber = user.Phonenumber,
                    FirstTimeLogin = false,
                    Status = user.Status,
                    OId = user.OId,
                    OIdProvider = user.OIdProvider,
                    LastModifiedDate = user.LastModifiedDate
                };
            }
            User newUser = new()
            {
                Email = request.Email.Trim(),
                Firstname = request.FirstName.Trim(),
                Lastname = request.LastName.Trim(),
                Verified = true,
                CreationDate = DateTime.UtcNow,
                OId = request.OId.Trim(),
                OIdProvider = request.OIdProvider,
                LastModifiedDate = DateTime.UtcNow,
                PictureURL = request.PictureURL,
                Phonenumber = request.PhoneNumber.Trim(),
                Locale = request.Locale,
                Status = 2,
            };
            await _postgreDB.User.AddAsync(newUser);
            await _postgreDB.SaveChangesAsync();
            User responseUser = await _postgreDB.User.AsNoTracking().FirstOrDefaultAsync(x=> x.Email == request.Email.Trim());
            if(responseUser != null)
            {
                return new RegisterUserResponse
                {
                    Id = responseUser.Id,
                    Email = responseUser.Email,
                    FirstName = responseUser.Firstname,
                    LastName = responseUser.Lastname,
                    PhoneNumber = responseUser.Phonenumber,
                    FirstTimeLogin = true,
                    Status = responseUser.Status,
                    OId = responseUser.OId,
                    OIdProvider = responseUser.OIdProvider,
                    LastModifiedDate = responseUser.LastModifiedDate
                };
            }
            return null;
        }
        public async Task<bool> VerifyToken(string token)
        {
            if (String.IsNullOrWhiteSpace(token))
                return await Task.FromResult(false);

            var user = _postgreDB.User.FirstOrDefault(user => user.VerificationToken == token);
            if (user == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                user.VerifiedAt = DateTime.UtcNow;
                user.VerificationToken = null;
                user.Status = 2;
                user.LastModifiedDate = DateTime.UtcNow;
                await _postgreDB.SaveChangesAsync();
                return await Task.FromResult(true);
            }
        }
    }
}
