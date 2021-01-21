using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Dao
{
    public interface IProyectoDao
    {

        bool Guardar(Proyecto proyecto);

        void EliminarProyecto(int IdProyecto);

        List<Proyecto> GetProyectos();

        List<Proyecto> GetProyectosByCliente(int IdCliente);

        List<Proyecto> GetProyectosByNombre(string nombre);

    }

    public class ProyectoDao : IProyectoDao
    {

        private TpSeminarioContext _ctx = new TpSeminarioContext();

        public ProyectoDao(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public List<Proyecto> GetProyectos()
        {
            using (_ctx)
            {
                return _ctx.Proyecto.ToList();

            }
        }

        public List<Proyecto> GetProyectosByCliente(int IdCliente)
        {
            using (_ctx)
            {
                List<Proyecto> proyectos = _ctx.Proyecto
                                           .Where(p => p.Idcliente == IdCliente)
                                           .ToList();

                return proyectos;
            }
        }

        public List<Proyecto> GetProyectosByNombre(string nombre)
        {
            using (_ctx)
            {
                List<Proyecto> proyectos = _ctx.Proyecto
                                           .Where(p => p.NombreProyecto.ToLower().Contains(nombre.ToLower()))
                                           .ToList();
                return proyectos;
            }
        }

        public bool Guardar(Proyecto proyecto)
        {
            using (_ctx)
            {
                try
                {
                    _ctx.Proyecto.Add(proyecto);
                    _ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;

                }
            }
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
