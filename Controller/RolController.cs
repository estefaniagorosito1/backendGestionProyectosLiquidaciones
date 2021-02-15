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
    
    public class RolController: ControllerBase
    {
        private IRolService _RolService;

        public RolController(IRolService RolService)
        {
            _RolService = RolService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetRoles()
        {
            var roles = _RolService.GetRoles();
            return Ok(roles);
        }
    }
}
