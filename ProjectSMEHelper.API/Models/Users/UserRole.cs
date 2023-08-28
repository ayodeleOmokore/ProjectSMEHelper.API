using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ProjectSMEHelper.API.Models.Users;
public class UserRole
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public User User { get; set; }
    public Roles Role { get; set; }
}
