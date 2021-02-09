using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IEmpleadoProyectoService
    {
        void AsignarEmpleadosProyecto(EmpleadoProyecto empleadoProyecto);

        List<Empleado> GetEmpleadosProyecto(int idProyecto);
    }


    public class EmpleadoProyectoService : IEmpleadoProyectoService
    {
        private IServiceScopeFactory _scopeFactory;

        public EmpleadoProyectoService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void AsignarEmpleadosProyecto(EmpleadoProyecto empleadoProyecto)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                dbContext.EmpleadoProyecto.Add(empleadoProyecto);
                dbContext.SaveChanges();
            }
        }

        public List<Empleado> GetEmpleadosProyecto(int idProyecto)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var empleados = dbContext.EmpleadoProyecto
                                    .Where(ep => ep.Idproyecto.Equals(idProyecto))
                                    .Select(ep => ep.IdempleadoNavigation).ToList();

                return empleados;
            }
        }
    }
}
