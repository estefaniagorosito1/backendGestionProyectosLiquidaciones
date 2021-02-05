﻿using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IPerfilService
    {
        List<Perfil> FindPerfiles();
    }

    public class PerfilService : IPerfilService
    {
        private IServiceScopeFactory _scopeFactory;

        public PerfilService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public List<Perfil> FindPerfiles()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                return dbContext.Perfil.ToList();
            }
        }
    }
}
