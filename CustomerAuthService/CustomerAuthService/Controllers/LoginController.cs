using CustomerAuthService.BLL;
using CustomerAuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAuthService.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class LoginController : ControllerBase
{
    private readonly AuthService _auth;

    public LoginController(AuthService auth)
    {
        _auth = auth;
    }

    //[HttpPost("register")]
    //public async Task<IActionResult> Register(string name, string email, string password)
    //{
    //    //await _auth.Register(name, email, password);
    //    return Ok("Registered successfully");
    //}

    public class User
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }

    [HttpPost]
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _auth.Login(
            request.Email,
            request.Password
        );


        if (!result.success)
        {

            if (result.message == "User not found")
            {
                return NotFound(new
                {
                    message = result.message
                });
            }


            return Unauthorized(new
            {
                message = result.message
            });

        }


        return Ok(new
        {
            accessToken = result.jwt,
            refreshToken = result.refreshToken,
            user = result.user
        });
    }

}