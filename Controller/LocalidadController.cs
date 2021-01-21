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
    public class LocalidadController : ControllerBase
    {
        private LocalidadService _localidadService;

        public LocalidadController(LocalidadService localidadService)
        {
            _localidadService = localidadService;
        }

        [HttpGet("{IdProvincia}/{param}")]
        [Authorize]
        public IActionResult GetLocalidades([FromRoute] int IdProvincia, [FromRoute] string param)
        {
            List<Localidad> localidades = _localidadService.GetLocalidades(IdProvincia, param);
            return Ok(localidades);
        }

    }
}
