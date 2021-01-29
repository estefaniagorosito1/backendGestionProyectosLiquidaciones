using BackendGestionProyectosLiquidaciones.Dao;
using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface ILocalidadService
    {
        List<Localidad> GetLocalidades(int IdProvincia, string param);
    }

    public class LocalidadService : ILocalidadService
    {
        private TpSeminarioContext _ctx;

        public LocalidadService(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public List<Localidad> GetLocalidades(int IdProvincia, string param)
        {
            using (_ctx)
            {
                var localidades = from l in _ctx.Localidad
                                  where l.Idprovincia == IdProvincia
                                  && l.Descripcion.ToLower().Contains(param.ToLower())
                                  select l;

                return localidades.ToList();
            }
        }
    }
}
