using Identity.API.Models;
using Identity.Service.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API
{
    public class JwtTokenHandler()
    {
        public AuthenticationResponse GenerateJwtToken(UserAccount userAccountAuthenticated)
        {
            var JWT_SECURITY_KEY = Environment.GetEnvironmentVariable("PETPROJECT_JWT_SECURITY_KEY") ?? "";
            var JWT_TOKEN_VALIDITY_MINS = int.Parse(Environment.GetEnvironmentVariable("PET_PROJECT_JWT_TOKEN_VALIDITY_MINS") ?? "0");
            
            var tokenExpiryTimestamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Name, userAccountAuthenticated.UserName),
                new Claim(ClaimTypes.GivenName, userAccountAuthenticated.DisplayName),
                new Claim(ClaimTypes.StreetAddress, userAccountAuthenticated.Address??""),
                new Claim(ClaimTypes.MobilePhone, userAccountAuthenticated.Phone??""),
                new Claim(ClaimTypes.Email, userAccountAuthenticated.Email??""),
                new Claim(ClaimTypes.Role, userAccountAuthenticated.Role),
                new Claim(ClaimTypes.Uri, userAccountAuthenticated.Avatar??""),
            });

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
            var securityJwtTokenDescritor = new SecurityTokenDescriptor()
            {
                SigningCredentials = signingCredentials,
                Subject = claimsIdentity,
                Expires = tokenExpiryTimestamp
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityJwtTokenDescritor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse() { AuthType = "Bearer", ExpireIns = (int)tokenExpiryTimestamp.Subtract(DateTime.Now).TotalSeconds, Token = token };

        }
    }
}
