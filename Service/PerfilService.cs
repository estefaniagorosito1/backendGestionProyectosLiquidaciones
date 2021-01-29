using BackendGestionProyectosLiquidaciones.Dao;
using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IPerfilService
    {
        List<Perfil> FindPerfiles();
    }

    public class PerfilService : IPerfilService
    {
        private TpSeminarioContext _ctx;

        public PerfilService(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public List<Perfil> FindPerfiles()
        {
            using (_ctx)
            {
                return _ctx.Perfil.ToList();
            }
        }
    }
}
