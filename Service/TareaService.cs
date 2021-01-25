using BackendGestionProyectosLiquidaciones.Dao;
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

        void CrearTarea(Tarea tarea);

        bool ModificarTarea(Tarea tarea);
    }

    public class TareaService : ITareaService
    {
        private TareaDao _tareaDao;

        public TareaService(TareaDao tareaDao)
        {
            _tareaDao = tareaDao;
        }

        public List<Tarea> FindTareasByProyecto(int IdProyecto)
        {
            return _tareaDao.FindTareasByProyecto(IdProyecto);
        }

        public void CrearTarea(Tarea tarea)
        {
            _tareaDao.CrearTarea(tarea);
        }

        public bool ModificarTarea(Tarea tarea)
        {
            var tareaDB = _tareaDao.FindTarea(tarea.Idtarea);

            if (tareaDB != null)
            {
                _tareaDao.ModificarTarea(tarea);
                return true;
            }

            return false;
        }
    }
}
