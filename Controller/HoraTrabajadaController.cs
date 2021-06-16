using BackendGestionProyectosLiquidaciones.Service;
using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HoraTrabajadaController : ControllerBase
    {
        private IHoraTrabajadaService _horaTrabajadaService;

        public HoraTrabajadaController(IHoraTrabajadaService horaTrabajadaService)
        {
            _horaTrabajadaService = horaTrabajadaService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CrearHoraTrabajada([FromBody] HoraTrabajada horaTrabajada)
        {
            _horaTrabajadaService.CrearHoraTrabajada(horaTrabajada);
            return Ok("Hora trabajada creada correctamente");
        }

        [HttpGet("{IdEmpleado}/{fechaInicio}/{fechaFin}")]
        [Authorize]
        public IActionResult GetCantHorasTrabajadasEmpleado([FromRoute] int IdEmpleado, [FromRoute] DateTime fechaInicio, [FromRoute] DateTime fechaFin)
        {
            var horas = _horaTrabajadaService.GetCantHorasTrabajadasEmpleado(IdEmpleado, fechaInicio, fechaFin);
            return Ok(horas);
        }

        [HttpGet("{IdProyecto}/{IdPerfil}")]
        [Authorize]
        public IActionResult GetCantHorasTrabajadasProyectoPerfil([FromRoute] int IdProyecto, [FromRoute] int IdPerfil)
        {
            var horas = _horaTrabajadaService.GetCantHorasTrabajadasProyectoPerfil(IdProyecto, IdPerfil);
            return Ok(horas);
        }

        [HttpGet("{idProyecto}")]
        [Authorize]
        public IActionResult GetCantHorasAdeudadasProyecto([FromRoute] int idProyecto)
        {
            var horas = _horaTrabajadaService.GetCantHorasAdeudadasProyecto(idProyecto);
            return Ok(horas);
        }
    }
}
