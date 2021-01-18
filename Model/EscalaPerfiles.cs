using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class EscalaPerfiles
    {
        public EscalaPerfiles()
        {
            Liquidacion = new HashSet<Liquidacion>();
        }

        public int IdescalaPerfil { get; set; }
        public int CantidadPerfiles { get; set; }
        public double PorcentajeAumentoPerfil { get; set; }

        public ICollection<Liquidacion> Liquidacion { get; set; }
    }
}
