namespace ProjectSMEHelper.API.Contracts.Users.Responses;

public class RegisterUserResponse
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public int Status { get; set; }
}
