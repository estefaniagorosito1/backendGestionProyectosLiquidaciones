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
    [Authorize]
    [Route("[controller]")]
    public class TareaController : ControllerBase
    {
        private ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpGet("lista/{IdProyecto}")]
        [Authorize]
        public IActionResult FindTareasByProyecto([FromRoute] int IdProyecto)
        {
            var tareas = _tareaService.FindTareasByProyecto(IdProyecto);
            return Ok(tareas);
        }

        [HttpGet("empleado/{IdEmpleado}")]
        [Authorize]
        public IActionResult FindTareasEmpleado([FromRoute] int IdEmpleado)
        {
            var tareas = _tareaService.FindTareasEmpleado(IdEmpleado);
            return Ok(tareas);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CrearTarea([FromBody] Tarea tarea)
        {
            _tareaService.CrearTarea(tarea);
            return Ok("Tarea creada correctamente");
        }

        [HttpPut]
        [Authorize]
        public IActionResult ModificarTarea([FromBody] Tarea tarea)
        {
            var respuesta = _tareaService.ModificarTarea(tarea);

            if (respuesta)
            {
                return Ok("Tarea modificada correctamente");
            }

            return BadRequest("No se pudo modifcar la tarea");
        }

        [HttpDelete("{IdTarea}")]
        [Authorize]
        public IActionResult EliminarTarea([FromRoute] int IdTarea)
        {
            _tareaService.EliminarTarea(IdTarea);
            return Ok();
        }
        
    }
}
