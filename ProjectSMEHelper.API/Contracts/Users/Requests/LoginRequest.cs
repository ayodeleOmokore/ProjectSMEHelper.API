using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace ProjectSMEHelper.API.Contracts.Users.Requests;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Display(Name = "Remember me")]
    public bool RemeberMe { get; set; }
    //public string ReturnUrl { get; set; }
    //public IList<AuthenticationScheme> ExternalLogins { get; set; }
}
