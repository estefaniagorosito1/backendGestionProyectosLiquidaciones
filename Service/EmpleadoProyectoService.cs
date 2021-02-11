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
        void AsignarEmpleadosProyecto(List<EmpleadoProyecto> empleadoProyecto);

        List<Empleado> GetEmpleadosProyecto(int idProyecto);
    }


    public class EmpleadoProyectoService : IEmpleadoProyectoService
    {
        private IServiceScopeFactory _scopeFactory;

        public EmpleadoProyectoService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void AsignarEmpleadosProyecto(List<EmpleadoProyecto> empleadosProyecto)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var lista = dbContext.EmpleadoProyecto
                    .Where(ep => ep.Idproyecto.Equals(empleadosProyecto[0].Idproyecto))
                    .ToList();

                foreach (var item in lista)
                {
                    dbContext.EmpleadoProyecto.Remove(item);
                }

                foreach (var item in empleadosProyecto)
                {
                    dbContext.EmpleadoProyecto.Add(item);
                }

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
