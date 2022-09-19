using MonkeyFinances.Identidade.Api.Models;

namespace MonkeyFinances.Identidade.Api.Services
{
    public interface ITokenService
    {
        Task<UserLoginResponse> GenerateJwt(string email);
    }
}
