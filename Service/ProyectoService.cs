using BackendGestionProyectosLiquidaciones.Dao;
using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IProyectoService
    {
        void CrearProyecto(Proyecto proyecto);

        bool ModificarProyecto(Proyecto proyecto);

        void EliminarProyecto(int IdProyecto);

        List<Proyecto> GetProyectos();

        List<Proyecto> GetProyectosByCliente(int IdCliente);

        List<Proyecto> GetProyectosByNombre(string nombre);
    }

    public class ProyectoService : IProyectoService
    {
        private ProyectoDao _proyectoDao;

        public ProyectoService(ProyectoDao proyectoDao)
        {
            this._proyectoDao = proyectoDao;
        }

        public void CrearProyecto(Proyecto proyecto)
        {
            _proyectoDao.CrearProyecto(proyecto);
        }

        public bool ModificarProyecto(Proyecto proyecto)
        {
            return _proyectoDao.ModificarProyecto(proyecto);
        }

        public void EliminarProyecto(int IdProyecto)
        {
            _proyectoDao.EliminarProyecto(IdProyecto);
        }

        public List<Proyecto> GetProyectos()
        {
            return _proyectoDao.FindProyectos();
        }

        public List<Proyecto> GetProyectosByCliente(int IdCliente)
        {
            return _proyectoDao.FindProyectosByCliente(IdCliente);
        }

        public List<Proyecto> GetProyectosByNombre(string nombre)
        {
            return _proyectoDao.FindProyectosByNombre(nombre);
        }


    }
}
