using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendGestionProyectosLiquidaciones.Dao;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IHoraTrabajadaService
    {
        int GetCantHorasTrabajadasEmpleado(int IdEmpleado, DateTime fechaInicio, DateTime fechaFin);

        int GetCantHorasTrabajadasProyectoPerfil(int IdProyecto, int IdPerfil);
    }


    public class HoraTrabajadaService : IHoraTrabajadaService
    {
        private HoraTrabajadaDao _horaTrabajadaDao;

        public HoraTrabajadaService(HoraTrabajadaDao horaTrabajadaDao)
        {
            _horaTrabajadaDao = horaTrabajadaDao;
        }

        public int GetCantHorasTrabajadasEmpleado(int IdEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            return _horaTrabajadaDao.GetCantHorasTrabajadasEmpleado(IdEmpleado, fechaInicio, fechaFin);
        }

        public int GetCantHorasTrabajadasProyectoPerfil(int IdProyecto, int IdPerfil)
        {
            return _horaTrabajadaDao.GetCantHorasTrabajadasProyectoPerfil(IdProyecto, IdPerfil);
        }
    }
}
