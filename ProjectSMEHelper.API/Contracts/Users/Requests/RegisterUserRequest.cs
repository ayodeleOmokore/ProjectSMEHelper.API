using System.ComponentModel.DataAnnotations;

namespace ProjectSMEHelper.API.Contracts.Users.Requests;

public class RegisterUserRequest
{
   [Required] public string? Email { get; set; } 
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 
    public string? PhoneNumber { get; set; }
}
