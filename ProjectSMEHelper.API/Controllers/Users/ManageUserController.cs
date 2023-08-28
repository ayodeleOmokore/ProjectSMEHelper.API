using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSMEHelper.API.Contracts.Users.Requests;
using ProjectSMEHelper.API.Services.UserServices.Interfaces;

namespace ProjectSMEHelper.API.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class ManageUserController : ControllerBase
{
    private readonly IManageUserService _manageUserService;
    public ManageUserController(IManageUserService manageUserService)
    {
        _manageUserService = manageUserService;
    }
    [Authorize]
    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Password and email cannot be empty");
        }
        return Ok(await _manageUserService.RegisterUser(request));
    }
    [Authorize]
    [HttpPost("RegisterUserGoogle")]
    public async Task<IActionResult> RegisterUserWithGoogle([FromBody] RegisterUserByGoogleRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (String.IsNullOrEmpty(request.Email))
        {
            return BadRequest("Email cannot be empty");
        }
        return Ok(await _manageUserService.RegisterUserByGoogle(request));
    }
    [Authorize]
    [HttpGet("VerifyToken")]
    public async Task<IActionResult> VerifyToken(string token)
    {
        if (String.IsNullOrWhiteSpace(token))
        {
            return BadRequest("Token cannot be empty");
        }
        return Ok(await _manageUserService.VerifyToken(token));
    }
}
