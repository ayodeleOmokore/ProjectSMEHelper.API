using Microsoft.EntityFrameworkCore;
using ProjectSMEHelper.API.Models.Configurations;
using ProjectSMEHelper.API.Models.Users;
using System.Text.RegularExpressions;

namespace ProjectSMEHelper.API.DBContext.PostgreDBContext;

public class PostgreDbContext: DbContext
{
    public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options) { }
    public DbSet<User> User { get; set; }
    public DbSet<UserAddress> UserAddress { get; set; }
    public DbSet<EmailContainerConfiguration> EmailContainerConfiguration { get;set; }

}
