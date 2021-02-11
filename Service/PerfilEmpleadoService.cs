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
        void AsignarPerfilEmpleado(List<PerfilEmpleado> perfilesEmpleado);

        List<Empleado> GetEmpleadosByPerfil(int idPerfil);
    }

    public class PerfilEmpleadoService : IPerfilEmpleadoService
    {
        private IServiceScopeFactory _scopeFactory;

        public PerfilEmpleadoService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void AsignarPerfilEmpleado(List<PerfilEmpleado> perfilesEmpleado)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                foreach (var item in perfilesEmpleado)
                {
                    dbContext.PerfilEmpleado.Add(item);
                }

                dbContext.SaveChanges();
            }
        }

        public List<Empleado> GetEmpleadosByPerfil(int idPerfil)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var empleados = dbContext.PerfilEmpleado
                                    .Where(pe => pe.Idperfil.Equals(idPerfil))
                                    .Select(pe => pe.IdempleadoNavigation)
                                    .ToList();

                return empleados;
            }
        }
    }
}
