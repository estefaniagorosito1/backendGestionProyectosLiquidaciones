using BackendGestionProyectosLiquidaciones.Dao;
using BackendGestionProyectosLiquidaciones.Model;
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
        private TpSeminarioContext _ctx;

        public EmpleadoService(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public List<Empleado> FindEmpleados()
        {
            using (_ctx)
            {
                return _ctx.Empleado.ToList();
            }
        }

        public List<Empleado> FindEmpleadosByNombreApellido(string param)
        {
            using (_ctx)
            {
                var empleados = from e in _ctx.Empleado
                                where e.NombreEmpleado.ToLower().Contains(param.ToLower())
                                      || e.ApellidoEmpleado.ToLower().Contains(param.ToLower())
                                select e;

                return empleados.ToList();

            }
        }

        public Empleado FindEmpleadoByID(int IdEmpleado)
        {
            using (_ctx)
            {
                var empleado = from e in _ctx.Empleado
                               where e.Idempleado == IdEmpleado
                               select e;

                return empleado.FirstOrDefault();
            }
        }

        public void CrearEmpleado(Empleado empleado)
        {
            using (_ctx)
            {
                _ctx.Empleado.Add(empleado);
                _ctx.SaveChanges();
            }
        }

        public bool ModificarEmpleado(Empleado empleado)
        {
            var empleadoDB = FindEmpleadoByID(empleado.Idempleado);

            if (empleadoDB != null)
            {
                using (_ctx)
                {
                    _ctx.Empleado.Update(empleado);
                    _ctx.SaveChanges();

                }
                return true;
            }

            return false;

        }

        public void EliminarEmpleado(int IdEmpleado)
        {
            Empleado empleado = FindEmpleadoByID(IdEmpleado);

            if (empleado != null)
            {
                using (_ctx)
                {
                    _ctx.Empleado.Remove(empleado);
                    _ctx.SaveChanges();
                }

            }
        }

    }
}
