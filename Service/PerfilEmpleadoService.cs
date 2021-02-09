using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IPerfilEmpleadoService
    {
        void AsignarPerfilEmpleado(PerfilEmpleado perfilEmpleado);
    }

    public class PerfilEmpleadoService : IPerfilEmpleadoService
    {
        private IServiceScopeFactory _scopeFactory;

        public PerfilEmpleadoService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void AsignarPerfilEmpleado(PerfilEmpleado perfilEmpleado)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                dbContext.PerfilEmpleado.Add(perfilEmpleado);
                dbContext.SaveChanges();
            }
        }
    }
}
