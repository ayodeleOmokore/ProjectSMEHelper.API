using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectSMEHelper.API.AuthenticationServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectSMEHelper.API.Controllers.APIAuthentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIAuthentication : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public APIAuthentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] APILoginModel model)
        {
            //var user = await userManager.FindByNameAsync(model.Username);
            APIAuthResponse authResponse = await APIUserAuthenticationService.AuthenticateAPIUser(model.email, model.password);
            if (model != null && authResponse != null)
            {
                

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.email),
                    new Claim(JwtRegisteredClaimNames.Jti, authResponse.Id.ToString()),
                     new Claim(ClaimTypes.Role, authResponse.Role),
                };

                //foreach (var userRole in userRoles)
                //{
                //    authClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
                //}

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}
