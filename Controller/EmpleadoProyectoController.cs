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
    public class EmpleadoProyectoController : ControllerBase
    {
        private IEmpleadoProyectoService _empleadoProyectoService;

        public EmpleadoProyectoController(IEmpleadoProyectoService empleadoProyectoService)
        {
            _empleadoProyectoService = empleadoProyectoService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AsignarEmpleadosProyecto([FromBody] EmpleadoProyecto empleadoProyecto)
        {
            _empleadoProyectoService.AsignarEmpleadosProyecto(empleadoProyecto);

            return Ok("Empleado asignado al proyecto " + empleadoProyecto.IdproyectoNavigation.Descripcion);
        }
    }
}
