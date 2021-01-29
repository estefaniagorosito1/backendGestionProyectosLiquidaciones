using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendGestionProyectosLiquidaciones.Dao;
using BackendGestionProyectosLiquidaciones.Model;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IHoraTrabajadaService
    {
        int GetCantHorasTrabajadasEmpleado(int IdEmpleado, DateTime fechaInicio, DateTime fechaFin);

        int GetCantHorasTrabajadasProyectoPerfil(int IdProyecto, int IdPerfil);
    }


    public class HoraTrabajadaService : IHoraTrabajadaService
    {
        private TpSeminarioContext _ctx;

        public HoraTrabajadaService(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public int GetCantHorasTrabajadasEmpleado(int IdEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            using (_ctx)
            {
                var horas = _ctx.HoraTrabajada.Where(ht => ht.Idempleado == IdEmpleado
                                                           && fechaInicio < ht.FechaHoraTrabajada
                                                           && ht.FechaHoraTrabajada < fechaFin);

                return horas.Sum(x => x.CantidadHoraTrabajada);
            }
        }

        public int GetCantHorasTrabajadasProyectoPerfil(int IdProyecto, int IdPerfil)
        {
            using (_ctx)
            {
                var horas = _ctx.HoraTrabajada.Where(ht => ht.Idproyecto == IdProyecto
                                                           && ht.Idperfil == IdPerfil);

                return horas.Sum(x => x.CantidadHoraTrabajada);
            }
        }
    }
}
