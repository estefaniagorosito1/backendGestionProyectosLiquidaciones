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
    public class PerfilController : ControllerBase
    {
        private IPerfilService _perfilService;

        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        //[Authorize(Roles = "Usuario")]
        public IActionResult FindPerfiles()
        {
            var perfiles = _perfilService.FindPerfiles();
            return Ok(perfiles);
        }

    }
}
