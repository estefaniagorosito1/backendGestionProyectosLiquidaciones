﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IHoraTrabajadaService
    {
        int GetCantHorasTrabajadasEmpleado(int IdEmpleado, DateTime fechaInicio, DateTime fechaFin);

        int GetCantHorasTrabajadasProyectoPerfil(int IdProyecto, int IdPerfil);

        int GetCantHorasAdeudadasProyecto(int IdProyecto);

        void CrearHoraTrabajada(HoraTrabajada horaTrabajada);
    }


    public class HoraTrabajadaService : IHoraTrabajadaService
    {
        public IServiceScopeFactory _scopeFactory;

        public HoraTrabajadaService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }


        public void CrearHoraTrabajada (HoraTrabajada horaTrabajada)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                dbContext.HoraTrabajada.Add(horaTrabajada);
                dbContext.SaveChanges();
            }
        }

        public int GetCantHorasAdeudadasProyecto(int IdProyecto)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var horas = dbContext.HoraTrabajada
                                     .Where(ht => ht.Idproyecto == IdProyecto && ht.EstadoHoraTrabajada == "ADEUDADAS");

                return horas.Sum(x => x.CantidadHoraTrabajada);
            }
        }

        public int GetCantHorasTrabajadasEmpleado(int IdEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var horas = dbContext.HoraTrabajada.Where(ht => ht.Idempleado == IdEmpleado
                                                           && fechaInicio < ht.FechaHoraTrabajada
                                                           && ht.FechaHoraTrabajada < fechaFin);

                return horas.Sum(x => x.CantidadHoraTrabajada);
            }
        }

        public int GetCantHorasTrabajadasProyectoPerfil(int IdProyecto, int IdPerfil)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var horas = dbContext.HoraTrabajada.Where(ht => ht.Idproyecto == IdProyecto
                                                           && ht.Idperfil == IdPerfil);

                return horas.Sum(x => x.CantidadHoraTrabajada);
            }
        }
    }
}
