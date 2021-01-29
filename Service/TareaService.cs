using BackendGestionProyectosLiquidaciones.Model;
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
    }

    public class TareaService : ITareaService
    {
        private TpSeminarioContext _ctx;

        public TareaService(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public List<Tarea> FindTareasByProyecto(int IdProyecto)
        {
            using (_ctx)
            {
                var tareas = from t in _ctx.Tarea
                             where t.Idproyecto == IdProyecto
                             select t;

                return tareas.ToList();
            }
        }

        public Tarea FindTarea(int IdTarea)
        {
            using (_ctx)
            {
                var tarea = from t in _ctx.Tarea
                            where t.Idtarea == IdTarea
                            select t;

                return tarea.FirstOrDefault();
            }
        }

        public void CrearTarea(Tarea tarea)
        {
            using (_ctx)
            {
                _ctx.Tarea.Add(tarea);
                _ctx.SaveChanges();
            }
        }

        public bool ModificarTarea(Tarea tarea)
        {
            var tareaDB = FindTarea(tarea.Idtarea);

            if (tareaDB != null)
            {
                using (_ctx)
                {
                    _ctx.Tarea.Update(tarea);
                    _ctx.SaveChanges();
                }

                return true;
            }

            return false;
        }
    }
}
