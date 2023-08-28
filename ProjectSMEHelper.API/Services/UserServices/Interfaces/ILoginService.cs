using ProjectSMEHelper.API.Contracts.Users.Requests;
using ProjectSMEHelper.API.Contracts.Users.Responses;

namespace ProjectSMEHelper.API.Services.UserServices.Interfaces;

public interface ILoginService
{
    Task<LoginResponse?> Login(LoginRequest loginRequest);
    Task<LoginResponse> CheckIfEmailExists(string email);
    
}
