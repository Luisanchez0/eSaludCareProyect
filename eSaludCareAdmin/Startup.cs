using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Data;
using System.Text;

[assembly: OwinStartup(typeof(eSaludCareAdmin.Startup))]

namespace eSaludCareAdmin
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var issuer = "https://eSaludCare.local";
            var audience = "eSaludCareAdmin";
            var secret = Encoding.UTF8.GetBytes("s6QyD3rFlJpL7gX98N2rVqz4RccK90F2y6sTgY12e8M=");

            var key = new SymmetricSecurityKey(secret);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = key
                }
            });
        }
    }
}