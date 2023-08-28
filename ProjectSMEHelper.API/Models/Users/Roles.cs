namespace ProjectSMEHelper.API.Models.Users;

public class Roles
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

}
