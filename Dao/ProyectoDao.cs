using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Dao
{
    public interface IProyectoDao
    {

        List<Proyecto> FindProyectos();

        List<Proyecto> FindProyectosByCliente(int IdCliente);

        List<Proyecto> FindProyectosByNombre(string nombre);

        Proyecto FindProyectoByID(int IdProyecto);

        void CrearProyecto(Proyecto proyecto);

        bool ModificarProyecto(Proyecto proyecto);

        void EliminarProyecto(int IdProyecto);

    }

    public class ProyectoDao : IProyectoDao
    {

        private TpSeminarioContext _ctx = new TpSeminarioContext();

        public ProyectoDao(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public List<Proyecto> FindProyectos()
        {
            using (_ctx)
            {
                return _ctx.Proyecto.ToList();

            }
        }

        public List<Proyecto> FindProyectosByCliente(int IdCliente)
        {
            using (_ctx)
            {
                List<Proyecto> proyectos = _ctx.Proyecto
                                           .Where(p => p.Idcliente == IdCliente)
                                           .ToList();

                return proyectos;
            }
        }

        public List<Proyecto> FindProyectosByNombre(string nombre)
        {
            using (_ctx)
            {
                List<Proyecto> proyectos = _ctx.Proyecto
                                           .Where(p => p.NombreProyecto.ToLower().Contains(nombre.ToLower()))
                                           .ToList();
                return proyectos;
            }
        }

        public Proyecto FindProyectoByID(int IdProyecto)
        {
            using (_ctx)
            {
                var proyecto = _ctx.Proyecto
                                   .Where(p => p.Idproyecto.Equals(IdProyecto))
                                   .FirstOrDefault();

                return proyecto;
            }
        }

        public void CrearProyecto(Proyecto proyecto)
        {
            using (_ctx)
            {
                _ctx.Proyecto.Add(proyecto);
                _ctx.SaveChanges();
            }
        }

        public bool ModificarProyecto(Proyecto proyecto)
        {
            Proyecto proyectoDB = FindProyectoByID(proyecto.Idproyecto);

            if (proyectoDB != null)
            {
                _ctx.Proyecto.Update(proyecto);
                return true;
            }

            return false;
        }

        public void EliminarProyecto(int IdProyecto)
        {
            using (_ctx)
            {
                Proyecto proyecto = _ctx.Proyecto.FirstOrDefault(p => p.Idproyecto.Equals(IdProyecto));
                _ctx.Proyecto.Remove(proyecto);
                _ctx.SaveChanges();
            }
        }

    }
}
