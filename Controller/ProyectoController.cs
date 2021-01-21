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
    [Route("controller")]
    public class ProyectoController : ControllerBase
    {
        private ProyectoService _proyectoService;

        public ProyectoController(ProyectoService proyectoService)
        {
            _proyectoService = proyectoService;
        }

        [HttpGet]
        //[Authorize(Roles = "Usuario")]
        public IActionResult GetProyectos()
        {
            var proyectos = _proyectoService.GetProyectos();

            if (proyectos.Count == 0)
            {
                return NotFound("No se encontraron proyectos");
            }

            return Ok(proyectos);
        }

        [HttpGet("/cliente/{idcliente}")]
        //[Authorize(Roles = "Usuario")]
        public IActionResult GetProyectosByCliente([FromRoute] int IdCliente)
        {
            var proyectosCliente = _proyectoService.GetProyectosByCliente(IdCliente);

            if (proyectosCliente.Count == 0)
            {
                return NotFound("El cliente no posee proyectos cargados");
            }

            return Ok(proyectosCliente);
        }

        [HttpGet("/bynombre/{nombre}")]
        //[Authorize(Roles = "Usuario")]
        public IActionResult GetProyectosByNombre([FromRoute] string nombre)
        {
            var proyectos = _proyectoService.GetProyectosByNombre(nombre);

            if (proyectos.Count == 0)
            {
                return NotFound("No se encontraron proyectos");
            }

            return Ok(proyectos);
        }

        [HttpPost]
        //[Authorize(Roles = "Usuario")]
        public IActionResult CrearProyecto([FromBody] Proyecto proyecto)
        {
            bool respuesta = _proyectoService.CrearProyecto(proyecto);

            if (!respuesta)
            {
                return NotFound("No se pudo crear el proyecto");
            }

            return Ok("Proyecto creado correctamente");
        }

        [HttpPut]
        //[Authorize(Roles = "Usuario")]
        public IActionResult ModificarProyecto([FromBody] Proyecto proyecto)
        {
            bool respuesta = _proyectoService.ModificarProyecto(proyecto);

            if (!respuesta)
            {
                return BadRequest("No se pudo modificar el proyecto");
            }

            return Ok("Proyecto modificado correctamente");
        }

        [HttpDelete("{IdProyecto}")]
        //[Authorize(Roles = "Usuario")]
        public IActionResult EliminarProyecto([FromRoute] int IdProyecto)
        {
            _proyectoService.EliminarProyecto(IdProyecto);
            return Ok("Proyecto eliminado");
        }

    }
}