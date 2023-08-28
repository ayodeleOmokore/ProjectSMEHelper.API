using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectSMEHelper.API.DBContext.PostgreDBContext;
using ProjectSMEHelper.API.Helpers;
using ProjectSMEHelper.API.Services.EmailServices;
using ProjectSMEHelper.API.Services.UserServices.Implementations;
using ProjectSMEHelper.API.Services.UserServices.Interfaces;
using Serilog;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
    // Read from your appsettings.json.
    .AddJsonFile("appsettings.json")
    // Read from your secrets.
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables()
    .Build();
//builder.Services.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"));
//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(configuration)
//.CreateLogger();
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PostgreDbContext>(opt =>
       opt.UseNpgsql(configuration.GetConnectionString("PostgreDBConnectionString")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidAudience = configuration["JWT:ValidAudience"],
                   ValidIssuer = configuration["JWT:ValidIssuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
               };
           });
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IManageUserService, ManageUserService>();
builder.Services.AddTransient<DAL>();
builder.Services.AddTransient<ProjectSMEHelper.API.Helpers.Utility>();
builder.Services.AddTransient<ISendEmailService, SendEmailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
