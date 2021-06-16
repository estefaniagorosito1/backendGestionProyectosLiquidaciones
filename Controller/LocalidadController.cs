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
    public class LocalidadController : ControllerBase
    {
        private ILocalidadService _localidadService;

        public LocalidadController(ILocalidadService localidadService)
        {
            _localidadService = localidadService;
        }

        [HttpGet("{IdProvincia}")]
        [Authorize]
        public IActionResult GetLocalidades([FromRoute] int IdProvincia)
        {
            List<Localidad> localidades = _localidadService.GetLocalidades(IdProvincia);
            return Ok(localidades);
        }

        [HttpGet("findOne/{IdLocalidad}")]
        [Authorize]
        public IActionResult GetLocalidadById([FromRoute] int IdLocalidad)
        {
            Localidad localidad = _localidadService.GetLocalidadById(IdLocalidad);
            return Ok(localidad);
        }
    }
}
