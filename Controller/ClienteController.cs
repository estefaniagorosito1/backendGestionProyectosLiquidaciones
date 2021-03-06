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
    public class ClienteController : ControllerBase
    {
        private IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult FindClientes()
        {
            var clientes = _clienteService.FindClientes();
            return Ok(clientes);
        }

        [HttpGet("find/{param}")]
        [Authorize]
        public IActionResult FindClienteById([FromRoute] int param)
        {
            var cliente = _clienteService.FindCliente(param);

            if (cliente != null)
            {
                return Ok(cliente);
            }

            return NotFound("No se encontro un cliente con el id ingresado");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            _clienteService.CrearCliente(cliente);
            return Ok("Cliente creado correctamente");
        }

        [HttpPut]
        [Authorize]
        public IActionResult ModificarCliente(Cliente cliente)
        {
            var respuesta = _clienteService.ModificarCliente(cliente);

            if (respuesta)
            {
                return Ok("Cliente modificado");
            }

            return BadRequest("No se pudo modificar el cliente");
        }

        [HttpDelete("{IdCliente}")]
        [Authorize]
        public IActionResult EliminarCliente([FromRoute] int IdCliente)
        {
            var respuesta = _clienteService.EliminarCliente(IdCliente);

            if(respuesta)
            {
                return Ok("Cliente eliminado");
            }

            return BadRequest("No se pudo eliminar el cliente.");
        }

    }
}
