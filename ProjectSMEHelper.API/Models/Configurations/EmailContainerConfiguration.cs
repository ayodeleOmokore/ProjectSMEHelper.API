using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ProjectSMEHelper.API.Models.Configurations;

public class EmailContainerConfiguration
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? BodyTemplate { get; set; }
    public int Status { get; set; } = 1;
}
