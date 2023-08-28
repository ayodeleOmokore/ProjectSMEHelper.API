using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSMEHelper.API.Contracts.Users.Requests;
using ProjectSMEHelper.API.Services.UserServices.Interfaces;
using System.Reflection.Emit;

namespace ProjectSMEHelper.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;   
        }
        [Authorize]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Password and email cannot be empty");
            }
            return Ok(await _loginService.Login(request));
        }
        [Authorize]
        [HttpGet("CheckIfEmailExists")]
        public async Task<IActionResult> CheckIfEmailExists(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (String.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Provide valid email");
            }
            return Ok( await _loginService.CheckIfEmailExists(email));
        }
    }
}
