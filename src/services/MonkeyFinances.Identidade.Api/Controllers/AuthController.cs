using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonkeyFinances.Identidade.Api.Models;

namespace MonkeyFinances.Identidade.Api.Controllers
{
    [Route("api/identidade")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpPost("register-user")]
        public async Task<ActionResult> CadastrarUsuario([FromBody] RegisterUserModel registerUser)
        {
            if (!ModelState.IsValid) return BadRequest("Informações inválidas");

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }

            return BadRequest();
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {
            if (!ModelState.IsValid) return BadRequest("Informações inválidas");

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password,
                false, true);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }
    }
}
