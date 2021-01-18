using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class Localidad
    {
        public Localidad()
        {
            Cliente = new HashSet<Cliente>();
            Empleado = new HashSet<Empleado>();
        }

        public long Idlocalidad { get; set; }
        public string Descripcion { get; set; }
        public long? Idprovincia { get; set; }

        public Provincia IdprovinciaNavigation { get; set; }
        public ICollection<Cliente> Cliente { get; set; }
        public ICollection<Empleado> Empleado { get; set; }
    }
}
