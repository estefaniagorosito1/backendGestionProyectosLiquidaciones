using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Dao
{
    public interface ILocalidadDao
    {
        List<Localidad> GetLocalidades(int IdProvincia, string param);
    }

    public class LocalidadDao : ILocalidadDao
    {
        private TpSeminarioContext _ctx;

        public LocalidadDao(TpSeminarioContext ctx)
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
