using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            EmpleadoProyecto = new HashSet<EmpleadoProyecto>();
            Tarea = new HashSet<Tarea>();
        }

        public int Idproyecto { get; set; }
        public int Idcliente { get; set; }
        public string NombreProyecto { get; set; }
        public string Descripcion { get; set; }
        public string EstadoProyecto { get; set; }
        public DateTime? FechaInicioProyecto { get; set; }
        public DateTime? FechaFinProyecto { get; set; }

        public Cliente IdclienteNavigation { get; set; }
        public ICollection<EmpleadoProyecto> EmpleadoProyecto { get; set; }
        public ICollection<Tarea> Tarea { get; set; }
    }
}
