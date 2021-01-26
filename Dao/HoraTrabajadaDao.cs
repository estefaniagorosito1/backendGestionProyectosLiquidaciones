using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendGestionProyectosLiquidaciones.Model;

namespace BackendGestionProyectosLiquidaciones.Dao
{
    public interface IHoraTrabajadaDao
    {
        int GetCantHorasTrabajadasEmpleado(int IdEmpleado, DateTime fechaInicio, DateTime fechaFin);

        int GetCantHorasTrabajadasProyectoPerfil(int IdProyecto, int IdPerfil);
    }

    public class HoraTrabajadaDao : IHoraTrabajadaDao
    {
        private TpSeminarioContext _ctx;

        public HoraTrabajadaDao(TpSeminarioContext ctx)
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
            using(_ctx)
            {
                var horas = _ctx.HoraTrabajada.Where(ht => ht.Idproyecto == IdProyecto
                                                           && ht.Idperfil == IdPerfil);

                return horas.Sum(x => x.CantidadHoraTrabajada);
            }
        }
    }
}
