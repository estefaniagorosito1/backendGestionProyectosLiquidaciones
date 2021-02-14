using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IEmpleadoService
    {
        List<Empleado> FindEmpleados();

        List<Empleado> FindEmpleadosByNombreApellido(string param);

        void CrearEmpleado(Empleado empleado);

        bool ModificarEmpleado(Empleado empleado);

        void EliminarEmpleado(int IdEmpleado);
    }

    public class EmpleadoService : IEmpleadoService
    {
        private IServiceScopeFactory _scopeFactory;

        public EmpleadoService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public List<Empleado> FindEmpleados()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                return dbContext.Empleado.ToList();
            }
        }

        public void EliminarEmpleado(int IdEmpleado)
        {
            Empleado empleado = FindEmpleadoByID(IdEmpleado);

            if (empleado != null)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                    // Borro perfiles
                    var perfiles = dbContext.PerfilEmpleado.Where(pe => pe.Idempleado == empleado.Idempleado).ToList();

                    foreach (var item in perfiles)
                    {
                        dbContext.PerfilEmpleado.Remove(item);
                    }

                    // Borro usuario
                    var usuario = dbContext.Usuario.Where(us => us.Idempleado== empleado.Idempleado).First();
                    dbContext.Usuario.Remove(usuario);

                    dbContext.Empleado.Remove(empleado);
                    dbContext.SaveChanges();
                }
            }
        }

        public List<Empleado> FindEmpleadosByNombreApellido(string param)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var empleados = from e in dbContext.Empleado
                                where e.NombreEmpleado.ToLower().Contains(param.ToLower())
                                      || e.ApellidoEmpleado.ToLower().Contains(param.ToLower())
                                select e;

                return empleados.ToList();
            }
        }

        public Empleado FindEmpleadoByID(int IdEmpleado)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var empleado = from e in dbContext.Empleado
                               where e.Idempleado == IdEmpleado
                               select e;

                return empleado.FirstOrDefault();
            }
        }

        public void CrearEmpleado(Empleado empleado)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                dbContext.Empleado.Add(empleado);
                dbContext.SaveChanges();
            }
        }

        public bool ModificarEmpleado(Empleado empleado)
        {
            var empleadoDB = FindEmpleadoByID((int)empleado.Idempleado);

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                if (dbContext.Empleado.Any(c => c.Idempleado == empleado.Idempleado))
                {
                    dbContext.Update(empleado);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }



    public void EliminarEmpleaasdasddobackup(int IdEmpleado)
        {
            Empleado empleado = FindEmpleadoByID(IdEmpleado);

            if (empleado != null)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                    dbContext.Empleado.Remove(empleado);
                    dbContext.SaveChanges();
                }
            }
        }

    }
}
