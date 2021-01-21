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
    public class ProvinciaController : ControllerBase
    {
        private ProvinciaService _provinciaService;

        public ProvinciaController(ProvinciaService provinciaService)
        {
            _provinciaService = provinciaService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetProvincias()
        {
            List<Provincia> provincias = _provinciaService.GetProvincias();
            return Ok(provincias);
        }
    }
}
