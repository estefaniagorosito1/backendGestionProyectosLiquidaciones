using System;
using System.Collections.Generic;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class Cliente
    {
        public Cliente()
        {
            Proyecto = new HashSet<Proyecto>();
        }

        public int Idcliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string DireccionCliente { get; set; }
        public long? LocalidadCliente { get; set; }
        public string EmailCliente { get; set; }

        public Localidad LocalidadClienteNavigation { get; set; }
        public ICollection<Proyecto> Proyecto { get; set; }
    }
}
