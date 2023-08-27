using System.ComponentModel.DataAnnotations;

namespace ProjectSMEHelper.API.Models.Users;

public class UserAddress
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required] public string? CustomerId { get; set; }
    public string? Address { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public int Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime Timestamp { get; set; }
    public bool PrimaryAddress { get; set; }

}
