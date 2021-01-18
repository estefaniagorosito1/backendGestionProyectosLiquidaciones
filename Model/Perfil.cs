using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class Perfil
    {
        public Perfil()
        {
            PerfilEmpleado = new HashSet<PerfilEmpleado>();
        }

        public int Idperfil { get; set; }
        public string NombrePerfil { get; set; }
        public int ValorHora { get; set; }

        public ICollection<PerfilEmpleado> PerfilEmpleado { get; set; }
    }
}
