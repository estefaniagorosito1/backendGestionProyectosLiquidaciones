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
   // [Authorize]
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
       // [Authorize]
        public IActionResult FindClientes()
        {
            var clientes = _clienteService.FindClientes();

            if (clientes.Count != 0)
            {
                return Ok(clientes);
            }

            return NotFound("No se encontraron clientes");
        }

        [HttpGet("{param}")]
        [Authorize]
        public IActionResult FindClientesByNombreApellido([FromRoute] string param)
        {
            var clientes = _clienteService.FindClienteByNombreApellido(param);

            if (clientes.Count != 0)
            {
                return Ok(clientes);
            }

            return NotFound("No se encontraron clientes");
        }

        [HttpPost]
       // [Authorize]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            _clienteService.CrearCliente(cliente);
            return Ok("Cliente creado correctamente");
        }

        [HttpPut]
        // [Authorize]
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
