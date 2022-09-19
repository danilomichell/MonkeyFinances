using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MonkeyFinances.Identidade.Api.Entensions;
using MonkeyFinances.Identidade.Api.Filters;
using MonkeyFinances.Identidade.Api.Models;
using MonkeyFinances.Identidade.Api.Services;

namespace MonkeyFinances.Identidade.Api.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [ApiController]
    [Route("api/identidade")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenService _tokenService;

        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _tokenService = tokenService;
        }

        ///  <summary>
        ///  Endpoint responsável por cadastrar o usuário 
        /// </summary>
        ///  <param name = "registerUser" />
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register-user")]
        public async Task<ActionResult> CadastrarUsuario([FromBody] RegisterUserModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                return CustomResponse(await _tokenService.GenerateJwt(registerUser.Email));
            }

            foreach (var error in result.Errors)
            {
                AddErrors(error.Description);
            }
            return CustomResponse();
        }
        /// <summary>
        /// Endpoint responsável por logar
        /// </summary>
        /// <param name="login"></param> 
        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password,
                false, true);

            if (result.Succeeded) return CustomResponse(await _tokenService.GenerateJwt(login.Email));
            if (result.IsLockedOut)
            {
                AddErrors("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }
            AddErrors("Usuário ou senha incorretos");
            return CustomResponse();
        }
    }
}
