using System.IdentityModel.Tokens.Jwt;

namespace PhoneBook.Services.Models
{
    public class TokenViewModel
    {
        public string access_token { get; init; }
        public int expires_in { get; init; }
        public string token_type { get; } = "Bearer";

        public TokenViewModel(JwtSecurityToken securityToken)
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            expires_in = (int)(securityToken.ValidTo - DateTime.Now).TotalSeconds;
        }
    }
}
