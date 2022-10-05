using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonkeyFinances.Core.Controller;
using MonkeyFinances.Identidade.Api.Models;
using MonkeyFinances.Identidade.Api.Services;
using MonkeyFinances.Core.Exception;

namespace MonkeyFinances.Identidade.Api.Controllers
{
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Route("api/identidade")]
    public class AuthController : MainController
    {
        private readonly ITokenService _tokenService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;

        public AuthController(ITokenService tokenService,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserService userService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

            if (result.Succeeded)
            {
                var userCreated = await _userManager.FindByEmailAsync(usuarioRegistro.Email);
                var creteUser = await _userService.CreateUser(
                    new UsuarioRegistroApiFinancas
                    {
                        Id = userCreated.Id,
                        Email = usuarioRegistro.Email,
                        Nome = usuarioRegistro.Nome
                    });
                if (creteUser.Errors is null)
                    return CustomResponse(false,await _tokenService.GerarJwt(usuarioRegistro.Email));
                await _userManager.DeleteAsync(userCreated);
                foreach (var error in creteUser.Errors.Mensagens)
                {
                    AdicionarErroProcessamento(error);
                }

                return CustomResponse(false);
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.Description);
            }

            return CustomResponse(false);
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha,
                false, true);

            if (result.Succeeded)
            {
                return CustomResponse(false,await _tokenService.GerarJwt(usuarioLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(false);
            }

            AdicionarErroProcessamento("Usuário ou Senha incorretos");
            return CustomResponse(false);
        }
    }
}
