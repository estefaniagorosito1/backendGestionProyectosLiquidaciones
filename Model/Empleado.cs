using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class Empleado
    {
        public Empleado()
        {
            EmpleadoProyecto = new HashSet<EmpleadoProyecto>();
            Liquidacion = new HashSet<Liquidacion>();
            PerfilEmpleado = new HashSet<PerfilEmpleado>();
            Usuario = new HashSet<Usuario>();
        }

        public int? Idempleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public long? DniEmpleado { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public long? Localidad { get; set; }
        public DateTime FechaIngresoEmpleado { get; set; }

        public Localidad LocalidadNavigation { get; set; }
        public ICollection<EmpleadoProyecto> EmpleadoProyecto { get; set; }
        public ICollection<Liquidacion> Liquidacion { get; set; }
        public ICollection<PerfilEmpleado> PerfilEmpleado { get; set; }
        public ICollection<Usuario> Usuario { get; set; }
    }
}
