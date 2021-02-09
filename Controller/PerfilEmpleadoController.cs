using BackendGestionProyectosLiquidaciones.Model;
using BackendGestionProyectosLiquidaciones.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PerfilEmpleadoController : ControllerBase
    {
        private IPerfilEmpleadoService _perfilEmpleadoService;

        public PerfilEmpleadoController(IPerfilEmpleadoService perfilEmpleadoService)
        {
            _perfilEmpleadoService = perfilEmpleadoService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AsignarPerfilEmpleado(PerfilEmpleado perfilEmpleado)
        {
            _perfilEmpleadoService.AsignarPerfilEmpleado(perfilEmpleado);
            return Ok("Perfil " + perfilEmpleado.IdperfilNavigation.NombrePerfil + " asignado al empleado");
        }

        [HttpGet("{idPerfil}")]
        [Authorize]
        public IActionResult GetEmpleadosByPerfil([FromRoute] int idPerfil)
        {
            var empleados = _perfilEmpleadoService.GetEmpleadosByPerfil(idPerfil);

            if(empleados.Count != 0)
            {
                return Ok(empleados);
            }

            return BadRequest("No se encontraron empleados con el perfil seleccionado");
        }
    }
}
