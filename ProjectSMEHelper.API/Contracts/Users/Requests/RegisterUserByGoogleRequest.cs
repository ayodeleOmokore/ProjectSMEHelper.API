using System.ComponentModel.DataAnnotations;

namespace ProjectSMEHelper.API.Contracts.Users.Requests;

public class RegisterUserByGoogleRequest
{
    [Required] public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? OId { get; set; }
    public string? OIdProvider { get; set; }
    public string? PictureURL { get; set; }
    public bool VerifiedEmail { get; set; }
    public string? Locale { get; set; }

}
