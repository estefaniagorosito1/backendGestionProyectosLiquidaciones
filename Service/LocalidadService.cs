using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface ILocalidadService
    {
        List<Localidad> GetLocalidades(int IdProvincia);

        Localidad GetLocalidadById(int IdLocalidad);

    }

    public class LocalidadService : ILocalidadService
    {
        private IServiceScopeFactory _scopeFactory;

        public LocalidadService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public List<Localidad> GetLocalidades(int IdProvincia)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                var localidades = from l in dbContext.Localidad
                                  where l.Idprovincia == IdProvincia
                                  select l;

                return localidades.ToList();
            }
        }

        public Localidad GetLocalidadById(int IdLocalidad)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                var localidad = dbContext.Localidad.Where(lo => lo.Idlocalidad== IdLocalidad).First();

                return localidad;
            }
        }
    }
}
