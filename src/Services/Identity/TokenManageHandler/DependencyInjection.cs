using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TokenManageHandler
{
    public static class DependencyInjection
    {
        public static void AddCustomTokenManage(this IServiceCollection services)
        {

            var securityKey = Environment.GetEnvironmentVariable("PETPROJECT_JWT_SECURITY_KEY") ?? string.Empty;
            if (string.IsNullOrEmpty(securityKey))
            {
                throw new Exception("The secret key cound not be found.");
            }
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey))
                };
            });
        }
    }
}
