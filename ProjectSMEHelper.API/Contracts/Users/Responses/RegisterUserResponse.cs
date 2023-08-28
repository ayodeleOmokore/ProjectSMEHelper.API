namespace ProjectSMEHelper.API.Contracts.Users.Responses;

public class RegisterUserResponse
{
    public Guid? Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public int Status { get; set; }
    public bool FirstTimeLogin { get; set; } = false;
    public string? OId { get; set; }
    public string? OIdProvider { get; set; }
    public DateTime LastModifiedDate { get; set; }
}
