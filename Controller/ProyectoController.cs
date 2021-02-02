﻿using BackendGestionProyectosLiquidaciones.Model;
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
    public class ProyectoController : ControllerBase
    {
        private IProyectoService _proyectoService;

        public ProyectoController(IProyectoService proyectoService)
        {
            _proyectoService = proyectoService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetProyectos()
        {
            var proyectos = _proyectoService.FindProyectos();

            if (proyectos.Count == 0)
            {
                return NotFound("No se encontraron proyectos");
            }

            return Ok(proyectos);
        }

        [HttpGet("/cliente/{idcliente}")]
        [Authorize]
        public IActionResult GetProyectosByCliente([FromRoute] int IdCliente)
        {
            var proyectosCliente = _proyectoService.FindProyectosByCliente(IdCliente);

            if (proyectosCliente.Count == 0)
            {
                return NotFound("El cliente no posee proyectos cargados");
            }

            return Ok(proyectosCliente);
        }

        [HttpGet("/bynombre/{nombre}")]
        [Authorize]
        public IActionResult GetProyectosByNombre([FromRoute] string nombre)
        {
            var proyectos = _proyectoService.FindProyectosByNombre(nombre);

            if (proyectos.Count == 0)
            {
                return NotFound("No se encontraron proyectos");
            }

            return Ok(proyectos);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CrearProyecto([FromBody] Proyecto proyecto)
        {
            _proyectoService.CrearProyecto(proyecto);
            return Ok("Proyecto creado correctamente");
        }

        [HttpPut]
        [Authorize]
        public IActionResult ModificarProyecto([FromBody] Proyecto proyecto)
        {
            bool respuesta = _proyectoService.ModificarProyecto(proyecto);

            if (respuesta)
            {
                return Ok("Proyecto modificado correctamente");
            }

            return BadRequest("No se pudo modificar el proyecto");
        }

        [HttpDelete("{IdProyecto}")]
        [Authorize]
        public IActionResult EliminarProyecto([FromRoute] int IdProyecto)
        {
            _proyectoService.EliminarProyecto(IdProyecto);
            return Ok("Proyecto eliminado");
        }

    }
}