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
    [ApiController]
    [Route("[controller]")]
    public class LiquidacionController : ControllerBase
    {
        private ILiquidacionService _liquidacionService;

        public LiquidacionController(ILiquidacionService liquidacionService)
        {
            _liquidacionService = liquidacionService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CrearLiquidacion([FromBody] Liquidacion liquidacion)
        {
            try
            {
                _liquidacionService.CrearLiquidacion(liquidacion);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{codLiquidacion}")]
        [Authorize]
        public IActionResult GetLiquidacion([FromRoute] int codLiquidacion)
        {
            var liq = _liquidacionService.GetLiquidacion(codLiquidacion);
            return Ok(liq);

        }

        [HttpGet("fechas/{fechaDesde}/{fechaHasta}")]
        [Authorize]
        public IActionResult GetLiquidacionesPeriodo([FromRoute] string fechaDesde, [FromRoute] string fechaHasta)
        {
            var liqs = _liquidacionService.GetLiquidacionesPeriodo(DateTime.Parse(fechaDesde), DateTime.Parse(fechaHasta));
            return Ok(liqs);
        }

    }
}
