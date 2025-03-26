using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebApiIdentityJwt.Entities;
using WebApiIdentityJwt.Models;

namespace WebApiIdentityJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _sigInManager;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> sigInManager)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] AddUserRequest login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha)
                || string.IsNullOrWhiteSpace(login.rg))
                return Ok("Faltam alguns dados");

            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                Rg = login.rg
            };

            var resultado = await _userManager.CreateAsync(user, login.senha);

            if (resultado.Errors.Any())
                return Ok(resultado.Errors);

            // Geração de configuração caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //retorno Email
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

            if (resultado2.Succeeded)
                return Ok("Usuario Adicionado com sucesso");
            else
                return Ok("Erro ao confirmar usuario");
        }



    }
}
