using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class EmpleadoProyecto
    {
        public int Idempleado { get; set; }
        public int Idproyecto { get; set; }

        public Empleado IdempleadoNavigation { get; set; }
        public Proyecto IdproyectoNavigation { get; set; }
    }
}
