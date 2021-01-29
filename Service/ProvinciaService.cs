using BackendGestionProyectosLiquidaciones.Dao;
using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IProvinciaService
    {
        List<Provincia> GetProvincias();
    }

    public class ProvinciaService : IProvinciaService
    {
        private TpSeminarioContext _ctx;

        public ProvinciaService(TpSeminarioContext ctx)
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
