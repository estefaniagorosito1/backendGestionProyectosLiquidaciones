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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private EmpleadoService _empleadoService;

        public EmpleadoController(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet("/lista")]
        [Authorize]
        public IActionResult FindEmpleados()
        {
            var empleados = _empleadoService.FindEmpleados();

            if (empleados.Count != 0)
            {
                return Ok(empleados);
            }

            return NotFound("No se encontraron empleados");
        }

        [HttpGet("{param}")]
        [Authorize]
        public IActionResult FindEmpleadosByNombreApellido([FromRoute] string param)
        {
            var empleados = _empleadoService.FindEmpleadosByNombreApellido(param);

            if (empleados.Count != 0)
            {
                return Ok(empleados);
            }

            return NotFound("No se encontraron empleados");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CrearEmpleado([FromBody] Empleado empleado)
        {
            _empleadoService.CrearEmpleado(empleado);
            return Ok("Empleado creado correctamente");
        }

        [HttpPut]
        [Authorize]
        public IActionResult ModificarEmpleado(Empleado empleado)
        {
            bool respuesta = _empleadoService.ModificarEmpleado(empleado);

            if (respuesta)
            {
                return Ok("Empleado modificado correctamente");
            }

            return BadRequest("No se pudo modificar el empleado");
        }

        [HttpDelete("{IdEmpleado}")]
        [Authorize]
        public IActionResult EliminarEmpleado([FromRoute] int IdEmpleado)
        {
            _empleadoService.EliminarEmpleado(IdEmpleado);
            return Ok("Empleado eliminado");
        }
    }
}
