namespace ProjectSMEHelper.API.AuthenticationServices;

public class APIAuthResponse
{
    public Guid Id { get; set; }
    public string? Role { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
}
