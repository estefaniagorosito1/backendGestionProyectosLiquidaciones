using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendGestionProyectosLiquidaciones.Service;
using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using BackendGestionProyectosLiquidaciones;
using Microsoft.Extensions.Options;
using System.IO;
using Newtonsoft.Json;

namespace BackendGestionProyectosLiquidaciones.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IUsuarioService _usuarioService;
        private readonly string signkey = "$3M1N@R10PUN70N3T";

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate()
        {
            StreamReader sr = new StreamReader(Request.Body);
            var bodyString = sr.ReadToEnd();
            User body = new User();

            try
            {
                body = JsonConvert.DeserializeObject<User>(bodyString);
            }
            catch (Exception)
            {
                throw;
            }

            Usuario user = _usuarioService.FindUsuario(body.user, body.password);
            

            if (user == null)
            {
                return BadRequest("Usuario y/o contraseña incorrectos.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(signkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Name, user.Idusuario.ToString()),
                    new Claim(ClaimTypes.Role, user.Idrol.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Idusuario = user.Idusuario,
                Username = user.NombreUsuario,
                IdRol = user.Idrol,
                Rol = user.IdrolNavigation,
                IdEmpleado = user.Idempleado,
                Token = tokenString
            });
        }
    }

    // Clase usada únicamente en el login para recuperar los parámetros del body
    public class User
    {
        public string user { get; set; }
        public string password { get; set; }
    }

}
