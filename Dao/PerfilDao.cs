using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Dao
{
    public interface IPerfilDao
    {
        List<Perfil> FindPerfiles();
    }


    public class PerfilDao : IPerfilDao
    {
        private TpSeminarioContext _ctx = new TpSeminarioContext();

        public PerfilDao(TpSeminarioContext ctx)
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
