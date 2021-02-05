using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
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

        List<Proyecto> FindProyectos();

        List<Proyecto> FindProyectosByCliente(int IdCliente);

        List<Proyecto> FindProyectosByNombre(string nombre);
    }

    public class ProyectoService : IProyectoService
    {
        public IServiceScopeFactory _scopeFactory;

        public ProyectoService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public List<Proyecto> FindProyectos()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                return dbContext.Proyecto.ToList();

            }
        }

        public List<Proyecto> FindProyectosByCliente(int IdCliente)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                List<Proyecto> proyectos = dbContext.Proyecto
                                           .Where(p => p.Idcliente == IdCliente)
                                           .ToList();

                return proyectos;
            }
        }

        public List<Proyecto> FindProyectosByNombre(string nombre)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                List<Proyecto> proyectos = dbContext.Proyecto
                                           .Where(p => p.NombreProyecto.ToLower().Contains(nombre.ToLower()))
                                           .ToList();
                return proyectos;
            }
        }

        public Proyecto FindProyectoByID(int IdProyecto)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                var proyecto = dbContext.Proyecto.Find(IdProyecto);
                return proyecto;
            }
        }

        public void CrearProyecto(Proyecto proyecto)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                dbContext.Proyecto.Add(proyecto);
                dbContext.SaveChanges();
            }
        }

        public bool ModificarProyecto(Proyecto proyecto)
        {
            Proyecto proyectoDB = FindProyectoByID(proyecto.Idproyecto);

            if (proyectoDB != null)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                    dbContext.Proyecto.Update(proyecto);
                    dbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public void EliminarProyecto(int IdProyecto)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                Proyecto proyecto = dbContext.Proyecto.FirstOrDefault(p => p.Idproyecto.Equals(IdProyecto));
                dbContext.Proyecto.Remove(proyecto);
                dbContext.SaveChanges();
            }
        }

    }
}
