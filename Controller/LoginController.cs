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

namespace BackendGestionProyectosLiquidaciones.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IUsuarioService _usuarioService;
        private readonly Settings _settings;

        public LoginController(IUsuarioService usuarioService, IOptions<Settings> settings)
        {
            _usuarioService = usuarioService;
            _settings = settings.Value;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Test()
        {
            return Ok("Se pudo conectar");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult authenticate([FromBody] Usuario usuario)
        {
            var signkey = _settings.Secret;
            Usuario user = _usuarioService.FindUsuario(usuario);

            if(user == null)
            {
                return BadRequest("Usuario y/o contraseña incorrectos.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(signkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Idusuario.ToString()),
                    new Claim(ClaimTypes.Role, user.IdrolNavigation.DescripcionRol),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {
                Idusuario = user.Idusuario,
                Username = user.NombreUsuario,
                IdRol = user.Idrol,
                Rol = user.IdrolNavigation,
                IdEmpleado = user.Idempleado,
                Token = tokenString
            });
        }
    }

}
