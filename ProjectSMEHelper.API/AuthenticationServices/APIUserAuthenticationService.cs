namespace ProjectSMEHelper.API.AuthenticationServices;

public class APIUserAuthenticationService
{
    public static async Task<APIAuthResponse> AuthenticateAPIUser(string username, string password)
    {
        if(username == "WebAPIUser" || password == "TestPassword")
        {
            return new APIAuthResponse
            {
                Id = Guid.NewGuid(),
                Email = username,
                Role = "Admin",
                Name = username,
            };
        }
        else
        {
            return null;
        }
    }
}
