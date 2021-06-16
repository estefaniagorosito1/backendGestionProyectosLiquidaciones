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
        private IEmpleadoService _empleadoService;
        private IUsuarioService _usuarioService;

        public EmpleadoController(IEmpleadoService empleadoService, IUsuarioService usuarioService)
        {
            _empleadoService = empleadoService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult FindEmpleados()
        {
            var empleados = _empleadoService.FindEmpleados();
            return Ok(empleados);
        }

        [HttpGet("rol")]
        [Authorize]
        public IActionResult FindEmpleadosRolEmpleado()
        {
            var empleados = _empleadoService.FindEmpleadosRolEmpleado();
            return Ok(empleados);
        }

        [HttpGet("usuario/{IdEmpleado}")]
        [Authorize]
        public IActionResult FindUsuarioById([FromRoute] int IdEmpleado)
        {
            var usuario = _usuarioService.FindUsuarioByIdEmpleado(IdEmpleado);
            return Ok(usuario);
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

        [HttpGet("empleado/{IdEmpleado}")]
        [Authorize]
        public IActionResult FindEmpleado([FromRoute] int IdEmpleado)
        {
            var empleado = _empleadoService.FindEmpleadoById(IdEmpleado);
            return Ok(empleado);
        }

        [HttpGet("empleadosDesocupados/{IdProyecto}")]
        [Authorize]
        public IActionResult FindEmpleadosDesocupados([FromRoute] int IdProyecto)
        {
            var empleados = _empleadoService.FindEmpleadosSinTareas(IdProyecto);
            return Ok(empleados);
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
        public IActionResult ModificarEmpleado([FromBody] Empleado empleado)
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
