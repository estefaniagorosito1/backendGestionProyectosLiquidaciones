using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class Provincia
    {
        public Provincia()
        {
            Localidad = new HashSet<Localidad>();
        }

        public long Idprovincia { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Localidad> Localidad { get; set; }
    }
}
