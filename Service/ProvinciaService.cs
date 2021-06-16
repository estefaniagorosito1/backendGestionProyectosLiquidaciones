using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IProvinciaService
    {
        List<Provincia> GetProvincias();

        Provincia GetProvinciaById(int IdProvincia);
    }

    public class ProvinciaService : IProvinciaService
    {
        private IServiceScopeFactory _scopeFactory;

        public ProvinciaService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public List<Provincia> GetProvincias()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                return dbContext.Provincia.ToList();
            }
        }

        public Provincia GetProvinciaById(int IdProvincia)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                var provincia = dbContext.Provincia.Where(pro => pro.Idprovincia == IdProvincia).First();

                return provincia;
            }
        }
    }
}
