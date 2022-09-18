using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MonkeyFinances.Identidade.Api.Entensions;
using MonkeyFinances.Identidade.Api.Models;

namespace MonkeyFinances.Identidade.Api.Services
{
    public class TokenService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public TokenService(UserManager<IdentityUser> userManager,
                                    IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<UserModel.UserLoginResponse> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.ValidAt,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationtHour),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return new UserModel.UserLoginResponse
            {
                AccesToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_jwtSettings.ExpirationtHour).TotalSeconds,
                UserToken = new UserModel.UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserModel.UserClaim
                    {
                        Type = c.Type,
                        Value = c.Value
                    })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}
