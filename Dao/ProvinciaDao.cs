using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Dao
{
    public interface IProvinciaDao
    {
        List<Provincia> GetProvincias();
    }

    public class ProvinciaDao : IProvinciaDao
    {
        private TpSeminarioContext _ctx;

        public ProvinciaDao(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public List<Provincia> GetProvincias()
        {
            using (_ctx)
            {
                return _ctx.Provincia.ToList();
            }

        }
    }
}
