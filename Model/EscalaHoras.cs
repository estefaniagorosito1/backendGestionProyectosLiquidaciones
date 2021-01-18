using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class EscalaHoras
    {
        public EscalaHoras()
        {
            Liquidacion = new HashSet<Liquidacion>();
        }

        public int IdescalaHoras { get; set; }
        public int CantidadHorasTrabajadas { get; set; }
        public double PorcentajeAumentoHoras { get; set; }

        public ICollection<Liquidacion> Liquidacion { get; set; }
    }
}
