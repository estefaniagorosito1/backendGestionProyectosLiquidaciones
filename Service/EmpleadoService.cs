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
        private EmpleadoDao _empleadoDao;

        public EmpleadoService(EmpleadoDao empleadoDao)
        {
            _empleadoDao = empleadoDao;
        }

        public List<Empleado> FindEmpleados()
        {
            return _empleadoDao.FindEmpleados();
        }

        public List<Empleado> FindEmpleadosByNombreApellido(string param)
        {
            return _empleadoDao.FindEmpleadosByNombreApellido(param);
        }

        public void CrearEmpleado(Empleado empleado)
        {
            _empleadoDao.CrearEmpleado(empleado);
        }

        public bool ModificarEmpleado(Empleado empleado)
        {
            var empleadoDB = _empleadoDao.FindEmpleadoByID(empleado.Idempleado);

            if (empleadoDB != null)
            {
                _empleadoDao.ModificarEmpleado(empleado);
                return true;
            }

            return false;
            
        }

        public void EliminarEmpleado(int IdEmpleado)
        {
            Empleado empleado = _empleadoDao.FindEmpleadoByID(IdEmpleado);
            _empleadoDao.EliminarEmpleado(empleado);
        }

    }
}
