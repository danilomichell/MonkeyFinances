namespace MonkeyFinances.Identidade.Api.Models
{
    public class UserModel
    {
        public class UserLoginResponse
        {
            public string AccesToken { get; set; } = null!;
            public double ExpiresIn { get; set; }
            public UserToken UserToken { get; set; } = null!;
        }

        public class UserToken
        {
            public string Id { get; set; } = null!;
            public string Email { get; set; } = null!;
            public IEnumerable<UserClaim> Claims { get; set; } = null!;
        }

        public class UserClaim
        {
            public string Value { get; set; } = null!;
            public string Type { get; set; } = null!;
        }
    }
}
