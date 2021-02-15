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

        Tarea FindTarea(int IdTarea);

        void CrearTarea(Tarea tarea);

        bool ModificarTarea(Tarea tarea);

        void EliminarTarea(int IdTarea);
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

                var tarea = dbContext.Tarea.Find(IdTarea);
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
    }
}
