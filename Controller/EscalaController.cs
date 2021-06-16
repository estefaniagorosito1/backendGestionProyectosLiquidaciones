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
    public class EscalaController : ControllerBase
    {
        private IEscalaService _escalaService;

        public EscalaController(IEscalaService escalaService)
        {
            _escalaService = escalaService;
        }

        [HttpGet("ant")]
        [Authorize]
        public IActionResult GetEscalaAntiguedad()
        {
            var escalaAnt = _escalaService.GetEscalaAntiguedad();

            return Ok(escalaAnt);
        }

        [HttpGet("hora")]
        [Authorize]
        public IActionResult GetEscalaHoras()
        {
            var escalaHoras = _escalaService.GetEscalaHoras();

            return Ok(escalaHoras);
        }

        [HttpGet("perf")]
        [Authorize]
        public IActionResult GetEscalaPerfiles()
        {
            var escalaPerfiles = _escalaService.GetEscalaPerfiles();

            return Ok(escalaPerfiles);
        }
    }
}
