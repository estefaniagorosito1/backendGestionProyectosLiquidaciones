using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface ITareaService
    {
        List<Tarea> FindTareasByProyecto(int IdProyecto);

        List<Tarea> FindTareasEmpleado(int IdEmpleado);

        Tarea FindTarea(int IdTarea);

        void CrearTarea(Tarea tarea);

        bool ModificarTarea(Tarea tarea);

        void EliminarTarea(int IdTarea);

        int GetCantHorasOverbudgetProyecto(int idProyecto);
    }

    public class TareaService : ITareaService
    {
        public IServiceScopeFactory _scopeFactory;

        public TareaService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public List<Tarea> FindTareasByProyecto(int IdProyecto)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var tareas = from t in dbContext.Tarea
                             where t.Idproyecto == IdProyecto
                             select t;

                return tareas.ToList();
            }
        }

        public Tarea FindTarea(int IdTarea)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var tarea = from t in dbContext.Tarea
                            where t.Idtarea == IdTarea
                            select t;

                return tarea.FirstOrDefault();
            }
        }

        public void CrearTarea(Tarea tarea)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                dbContext.Tarea.Add(tarea);
                dbContext.SaveChanges();
            }
        }

        public bool ModificarTarea(Tarea tarea)
        {
            var tareaDB = FindTarea((int)tarea.Idtarea);

            if (tareaDB != null)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                    dbContext.Tarea.Update(tarea);
                    dbContext.SaveChanges();
                }

                return true;
            }

            return false;
        }

        public void EliminarTarea(int IdTarea)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var tarea = dbContext.Tarea.Where(t => t.Idtarea == IdTarea).First();
                tarea.Id = null;
                tarea.IdproyectoNavigation = null;

                var horas = dbContext.HoraTrabajada.Where(ht => ht.Idtarea == IdTarea).ToList();

                foreach (var item in horas)
                {
                    dbContext.HoraTrabajada.Remove(item);
                }

                dbContext.Tarea.Remove(tarea);
                dbContext.SaveChanges();
            }
        }

        public List<Tarea> FindTareasEmpleado(int IdEmpleado)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var tareas = dbContext.Tarea.Where(t => t.Idempleado == IdEmpleado).ToList();

                return tareas;
            }

        }

        public int GetCantHorasOverbudgetProyecto(int idProyecto)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var horas = dbContext.Tarea
                                     .Where(tarea => tarea.Idproyecto == idProyecto);

                return (int)horas.Sum(x => x.HorasOverbudget);
            }
        }
    }
}
