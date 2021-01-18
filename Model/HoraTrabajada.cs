using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class HoraTrabajada
    {
        public int IdhoraTrabajada { get; set; }
        public int Idproyecto { get; set; }
        public int Idtarea { get; set; }
        public int CantidadHoraTrabajada { get; set; }
        public DateTime FechaHoraTrabajada { get; set; }
        public string EstadoHoraTrabajada { get; set; }
        public int Idempleado { get; set; }
        public int Idperfil { get; set; }

        public PerfilEmpleado Id { get; set; }
        public Tarea IdNavigation { get; set; }
    }
}
