using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhoneBook.DataLayer.Entities;
using PhoneBook.Services.AppSettingProperty;
using PhoneBook.Services.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhoneBook.Services.Services
{
    public class JwtTokenBuilder : IJwtTokenBuilder
    {
        // use app setting properties for set some descriptor property and key value 
        private readonly AppSetting _siteSetting;
        public JwtTokenBuilder(IOptionsSnapshot<AppSetting> settings)
        {
            _siteSetting = settings.Value;
        }

        public TokenViewModel JwtGenerations(User user)
        {
            // security Key 
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSetting.Key);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature);

            var claim = _getClaim(user);

            // Token Descriptor
            var description = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.JwtSetting.Issued,
                Audience =_siteSetting.JwtSetting.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claim)
            };

            //make token 
            var jwtToken = new JwtSecurityTokenHandler();
            var securityToken = jwtToken.CreateJwtSecurityToken(description);
            return new TokenViewModel(securityToken); // return Token
        }

        // the detail that we want to add them in our token
        private IEnumerable<Claim> _getClaim(User user)
        {
            var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
            };
            return list;
        }
    }
}
