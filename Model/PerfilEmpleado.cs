using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class PerfilEmpleado
    {
        public PerfilEmpleado()
        {
            HoraTrabajada = new HashSet<HoraTrabajada>();
            Tarea = new HashSet<Tarea>();
        }

        public int Idempleado { get; set; }
        public int Idperfil { get; set; }

        public Empleado IdempleadoNavigation { get; set; }
        public Perfil IdperfilNavigation { get; set; }
        public ICollection<HoraTrabajada> HoraTrabajada { get; set; }
        public ICollection<Tarea> Tarea { get; set; }
    }
}
