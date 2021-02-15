using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class Tarea
    {
        public Tarea()
        {
            HoraTrabajada = new HashSet<HoraTrabajada>();
        }

        public int? Idtarea { get; set; }
        public int Idproyecto { get; set; }
        public int Idempleado { get; set; }
        public int Idperfil { get; set; }
        public string DescripcionTarea { get; set; }
        public int HorasEstimadasTarea { get; set; }
        public int? HorasOverbudget { get; set; }
        public int HorasTrabajadas { get; set; }

        public PerfilEmpleado Id { get; set; }
        public Proyecto IdproyectoNavigation { get; set; }
        public ICollection<HoraTrabajada> HoraTrabajada { get; set; }
    }
}
