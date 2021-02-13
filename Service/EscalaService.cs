using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IEscalaService
    {
        List<EscalaAntiguedad> GetEscalaAntiguedad();

        List<EscalaHoras> GetEscalaHoras();

        List<EscalaPerfiles> GetEscalaPerfiles();
    }


    public class EscalaService : IEscalaService
    {
        public IServiceScopeFactory _scopeFactory;

        public EscalaService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public List<EscalaAntiguedad> GetEscalaAntiguedad()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                return dbContext.EscalaAntiguedad.ToList();
            }
        }

        public List<EscalaHoras> GetEscalaHoras()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                return dbContext.EscalaHoras.ToList();
            }
        }

        public List<EscalaPerfiles> GetEscalaPerfiles()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                return dbContext.EscalaPerfiles.ToList();
            }
        }
    }
}
