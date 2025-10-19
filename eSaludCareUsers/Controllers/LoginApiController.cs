using CapaEntidad;
using CapaNegocio;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;



namespace eSaludCareUsers.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1")]

    public class LoginApiController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("login")]
        public IHttpActionResult Login([FromBody] Usuarios usuario)
        {
            if (usuario == null)
                return BadRequest("El cuerpo de la solicitud está vacío o mal formado.");

            CN_Usuarios cnUsuarios = new CN_Usuarios();
            var user = cnUsuarios.Login(usuario.correo, usuario.contrasena);
            if (user == null)
            {
                return Unauthorized();
            }
            // Generar token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("s6QyD3rFlJpL7gX98N2rVqz4RccK90F2y6sTgY12e8M=");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.id_usuario.ToString()),
                    new Claim(ClaimTypes.Role, user.rol)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwt = tokenHandler.WriteToken(token);

            cnUsuarios.GuardarToken(user.id_usuario, jwt);

            return Ok(new
            {

                success = true,
                token = jwt,
                role = user.rol,
                nombre = user.nombre
            });

        }

    }
}
