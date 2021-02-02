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
    [Authorize]
    [Route("[controller]")]
    public class HoraTrabajadaController : ControllerBase
    {
        private IHoraTrabajadaService _horaTrabajadaService;

        public HoraTrabajadaController(IHoraTrabajadaService horaTrabajadaService)
        {
            _horaTrabajadaService = horaTrabajadaService;
        }

        [HttpGet("{IdEmpleado}/{fechaInicio}/{fechaFin}")]
        [Authorize]
        public IActionResult GetCantHorasTrabajadasEmpleado([FromRoute] int IdEmpleado, [FromRoute] DateTime fechaInicio, [FromRoute] DateTime fechaFin)
        {
            var horas = _horaTrabajadaService.GetCantHorasTrabajadasEmpleado(IdEmpleado, fechaInicio, fechaFin);

            if (horas != 0)
            {
                return Ok(horas);
            }

            return BadRequest("El empleado no tiene horas cargadas en ese período de tiempo");
        }

        [HttpGet("{IdProyecto}/{IdPerfil}")]
        [Authorize]
        public IActionResult GetCantHorasTrabajadasProyectoPerfil([FromRoute] int IdProyecto, [FromRoute] int IdPerfil)
        {
            var horas = _horaTrabajadaService.GetCantHorasTrabajadasProyectoPerfil(IdProyecto, IdPerfil);

            if (horas != 0)
            {
                return Ok(horas);
            }

            return BadRequest("No hay horas cargadas con este perfil en el proyecto seleccionado");
        }

    }
}
