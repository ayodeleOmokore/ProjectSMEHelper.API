using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProjectSMEHelper.API.Models.Users;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string? Email { get; set; }
    public string? Password { get; set; }
    [Required]
    public string? Firstname { get; set; }
    public string? Middlename { get; set; }
    public string? Lastname { get; set; }
    public string? Phonenumber { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public int Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];
    public string? VerificationToken { get; set; }
    public bool Verified { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public string? OId { get; set; }
    public string? OIdProvider { get; set; }
    public string? PictureURL { get; set; }
    public string? CompanyId { get; set; }
    public string? Locale { get; set; }

}
