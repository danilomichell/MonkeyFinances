namespace MonkeyFinances.Financas.Api.Extensions
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;
        public double ExpirationtHour { get; set; }  
        public string Issuer { get; set; } = null!;
        public string ValidAt { get; set; } = null!;
    }
}
