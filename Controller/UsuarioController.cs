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
    public class UsuarioController : ControllerBase
    {
        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CrearUsuario([FromBody] Usuario usuario)
        {
            var respuesta = _usuarioService.CrearUsuario(usuario);

            if (respuesta)
            {
                return Ok();
            }

            return BadRequest("Error al crear el usuario");
        }

        [HttpPut]
        [Authorize]
        public IActionResult ModificarUsuario([FromBody] Usuario usuario)
        {
            var respuesta = _usuarioService.ModificarUsuario(usuario);

            if (respuesta)
            {
                return Ok();
            }

            return BadRequest("Error al modificar el usuario.");
        }

        [HttpDelete("{IdUsuario}")]
        [Authorize]
        public IActionResult EliminarUsuario([FromRoute] int IdUsuario)
        {
            _usuarioService.EliminarUsuario(IdUsuario);
            return Ok();

        }

        [HttpGet("{IdEmpleado}")]
        [Authorize]
        public IActionResult GetUsuarioById([FromRoute] int IdEmpleado)
        {
            var usuario = _usuarioService.FindUsuarioByIdEmpleado(IdEmpleado);
            return Ok(usuario);
        }
    }
}
