using ProjectSMEHelper.API.Contracts.Users.Requests;
using ProjectSMEHelper.API.Contracts.Users.Responses;

namespace ProjectSMEHelper.API.Services.UserServices.Interfaces;

public interface IManageUsers
{
    Task<RegisterUserResponse> RegisterUser(RegisterUserRequest registerUserRequest);
    Task<bool> VerifyToken(string token);
    Task<RegisterUserResponse> RegisterUserByGoogle(RegisterUserByGoogleRequest registerUserRequest);
}
