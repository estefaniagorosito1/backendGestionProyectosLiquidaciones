using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Dao
{
    public interface ITareaDao
    {
        List<Tarea> FindTareasByProyecto(int IdProyecto);

        Tarea FindTarea(int IdTarea);

        void CrearTarea(Tarea tarea);

        void ModificarTarea(Tarea tarea);

    }

    public class TareaDao : ITareaDao
    {
        private TpSeminarioContext _ctx;

        public TareaDao(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public List<Tarea> FindTareasByProyecto(int IdProyecto)
        {
            using(_ctx)
            {
                var tareas = from t in _ctx.Tarea
                             where t.Idproyecto == IdProyecto
                             select t;

                return tareas.ToList();
            }
        }

        public Tarea FindTarea(int IdTarea)
        {
            using(_ctx)
            {
                var tarea = from t in _ctx.Tarea
                            where t.Idtarea == IdTarea
                            select t;

                return tarea.FirstOrDefault();
            }
        }

        public void CrearTarea(Tarea tarea)
        {
            using(_ctx)
            {
                _ctx.Tarea.Add(tarea);
                _ctx.SaveChanges();
            }
        }


        public void ModificarTarea(Tarea tarea)
        {
            using(_ctx)
            {
                _ctx.Tarea.Update(tarea);
                _ctx.SaveChanges();
            }
        }

    }
}
