using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiIdentityJwt.Entities;
using WebApiIdentityJwt.Models;
using WebApiIdentityJwt.Token;

namespace WebApiIdentityJwt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public TokenController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> sigInManager)
    {
        _userManager = userManager;
        _signInManager = sigInManager;
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("/api/CreateToken")]
    public async Task<IActionResult> CreateToken([FromBody] InputLoginRequest input)
    {
        if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password))
            return Unauthorized();

        var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var token = new TokenJwtBuilder()
                .AddSecuritykey(JwtSecurityKey.Create("fUYbOnJU1d_4q360HnMT3ksRo-f3QT7X42dY1jLiRv10LgEnpD4IwEe3I5T8XnkiW04G_6F6pUq1Il3Ae6z0Jg"))
                .AddSubject("jwt token + identity")
                .AddIssuer("Teste.Security.Bearer")
                .AddAudience("Teste.Security.Bearer")
                .AddExpiry(5)
                .Builder();

            return Ok(token.value);
        }
        else
        {
            return Unauthorized();
        }
    }
}
