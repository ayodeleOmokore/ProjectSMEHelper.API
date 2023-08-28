namespace ProjectSMEHelper.API.Contracts.Users.Responses;

public class LoginResponse
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Status { get; set; }
    public DateTime Timestamp { get; set; }
    public string? OIdProvider { get; set; }
    public string? OId { get; set; }
    public bool Verified { get; set; }
    public DateTime? VerifiedAt { get; set; }

}
