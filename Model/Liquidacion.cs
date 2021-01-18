using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class Liquidacion
    {
        public int CodLiquidacion { get; set; }
        public DateTime? FechaLiquidacion { get; set; }
        public int? MesLiquidado { get; set; }
        public string Estado { get; set; }
        public double? ImporteLiquidacion { get; set; }
        public int Idempleado { get; set; }
        public int? IdescalaPerfil { get; set; }
        public int? IdescalaHoras { get; set; }
        public int? IdescalaAntiguedad { get; set; }

        public Empleado IdempleadoNavigation { get; set; }
        public EscalaAntiguedad IdescalaAntiguedadNavigation { get; set; }
        public EscalaHoras IdescalaHorasNavigation { get; set; }
        public EscalaPerfiles IdescalaPerfilNavigation { get; set; }
    }
}
