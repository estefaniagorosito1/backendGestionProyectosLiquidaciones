using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class Usuario
    {
        public int Idusuario { get; set; }
        public string NombreUsuario { get; set; }
        public string PasswordUsuario { get; set; }
        public int Idrol { get; set; }
        public int Idempleado { get; set; }

        public Empleado IdempleadoNavigation { get; set; }
        public Rol IdrolNavigation { get; set; }
    }
}
