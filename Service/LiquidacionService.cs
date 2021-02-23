using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface ILiquidacionService
    {
        void CrearLiquidacion(Liquidacion liquidacion);

        Liquidacion GetLiquidacion(int codLiquidacion);

        List<Liquidacion> GetLiquidacionesPeriodo(DateTime fechaDesde, DateTime fechaHasta);

        List<Liquidacion> GetLiquidacionesEmpleado(int idEmpleado);
    }

    public class LiquidacionService : ILiquidacionService
    {
        public IServiceScopeFactory _scopeFactory;

        public LiquidacionService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void CrearLiquidacion(Liquidacion liquidacion)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                if (dbContext.Liquidacion.Any(liq => liq.MesLiquidado == liquidacion.MesLiquidado
                    && liq.Idempleado == liquidacion.Idempleado))
                {
                    throw new Exception("Ya existe una liquidacion cargada para ese empleado en el mes seleccionado");
                }

                double importe = 0.0;

                // Datos básicos del empleado necesarios
                DateTime fechaIngreso = dbContext.Empleado.Find(liquidacion.Idempleado).FechaIngresoEmpleado;
                int antiguedad = DateTime.Today.Year - fechaIngreso.Year;

                // Traigo las adeudadas
                List<HoraTrabajada> horas = dbContext.HoraTrabajada
                                                        .Where(ht => ht.Idempleado == liquidacion.Idempleado
                                                                && ht.EstadoHoraTrabajada == "ADEUDADAS" 
                                                                && ht.FechaHoraTrabajada.Month == liquidacion.MesLiquidado)
                                                        .ToList();

                List<Tarea> tareasAdeudadas = new List<Tarea>(); ;

                foreach (var horaTrabajada in horas)
                {
                    // Por cada adeudada traigo la tarea de esa adeudada
                    tareasAdeudadas.Add(dbContext.Tarea.Where(t => t.Idtarea == horaTrabajada.Idtarea).First());
                }
                
                var importeHorasOverbudget = 0;

                foreach (var tarea in tareasAdeudadas)
                {
                    // Por cada tarea calculo aplico la formula overbudget.
                    var perfilTarea = dbContext.Perfil.Where(p => p.Idperfil == tarea.Idperfil).First();
                    importeHorasOverbudget = importeHorasOverbudget + (int) tarea.HorasOverbudget * (int) perfilTarea.ValorHora / 2;
                }

                if (horas.Count == 0)
                {
                    throw new Exception("No hay horas adeudadas en el mes seleccionado");
                }

                foreach (var item in horas)
                {
                    item.EstadoHoraTrabajada = EstadoHoras.PAGADAS.ToString();
                }

                // Si el empleado realizo más de 8 horas en un día, esas horas se tomar como horas extra y se abona un 50% más
                liquidacion.ImporteLiquidacion = horas.Sum(x => x.CantidadHoraTrabajada <= 8 ?
                                                           x.CantidadHoraTrabajada * dbContext.Perfil.Find(x.Idperfil).ValorHora
                                                           : (x.CantidadHoraTrabajada - 8) * dbContext.Perfil.Find(x.Idperfil).ValorHora * (float)1.5 + (8 * dbContext.Perfil.Find(x.Idperfil).ValorHora));

                liquidacion.ImporteLiquidacion = liquidacion.ImporteLiquidacion - importeHorasOverbudget;

                // Chequeo si realizo horas bajo más de un perfil
                var perfiles = horas.Select(x => x.Idperfil).Distinct().Count();
                if (perfiles > 1)
                {
                    importe = (double)liquidacion.ImporteLiquidacion * dbContext.EscalaPerfiles.Where(ep => ep.CantidadPerfiles == perfiles).FirstOrDefault().PorcentajeAumentoPerfil / 100;
                    liquidacion.IdescalaPerfil = dbContext.EscalaPerfiles.Where(ep => ep.CantidadPerfiles == perfiles).FirstOrDefault().IdescalaPerfil;
                }

                // Agrego porcentaje por antiguedad
                if (antiguedad != 0)
                {
                    if (antiguedad >= 5)
                    {
                        importe = importe + (double)(liquidacion.ImporteLiquidacion * (dbContext.EscalaAntiguedad.Find(2).PorcentajeAumentoAnt / 100));
                        liquidacion.IdescalaAntiguedad = 2;
                    }
                    else
                    {
                        importe = importe + (double)(liquidacion.ImporteLiquidacion * (dbContext.EscalaAntiguedad.Find(1).PorcentajeAumentoAnt / 100));
                        liquidacion.IdescalaAntiguedad = 2;
                    }
                }

                // Agrego porcentaje por cantidad de horas trabajadas
                var cantHoras = horas.Sum(h => h.CantidadHoraTrabajada);

                if (cantHoras >= 200)
                {
                    importe = importe + (double)liquidacion.ImporteLiquidacion * dbContext.EscalaHoras.Find(2).PorcentajeAumentoHoras / 100;
                    liquidacion.IdescalaHoras = dbContext.EscalaHoras.Find(2).IdescalaHoras;
                }
                else if (cantHoras >= 150)
                {
                    importe = importe + (double)liquidacion.ImporteLiquidacion * dbContext.EscalaHoras.Find(1).PorcentajeAumentoHoras / 100;
                    liquidacion.IdescalaHoras = dbContext.EscalaHoras.Find(1).IdescalaHoras;
                }

                liquidacion.ImporteLiquidacion = liquidacion.ImporteLiquidacion + importe;
                liquidacion.FechaLiquidacion = DateTime.Today;
                liquidacion.Estado = EstadoLiquidacion.EMITIDA.ToString();

                dbContext.Liquidacion.Add(liquidacion);

                dbContext.SaveChanges();
            }
        }

        public List <Liquidacion> GetLiquidacionesEmpleado (int idEmpleado)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var liquidaciones= dbContext.Liquidacion
                                    .Where(liq => liq.Idempleado == idEmpleado).ToList();

                return liquidaciones;
            }
        }

        public Liquidacion GetLiquidacion(int codLiquidacion)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var liquidacion = dbContext.Liquidacion.Find(codLiquidacion);

                return liquidacion;
            }

        }

        public List<Liquidacion> GetLiquidacionesPeriodo(DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var liquidaciones = dbContext.Liquidacion
                                                .Where(liq => fechaDesde < liq.FechaLiquidacion
                                                        && liq.FechaLiquidacion < fechaHasta)
                                                .ToList();

                return liquidaciones;
            }
        }
    }
}
