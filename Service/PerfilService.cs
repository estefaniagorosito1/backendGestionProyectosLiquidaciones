using BackendGestionProyectosLiquidaciones.Dao;
using BackendGestionProyectosLiquidaciones.Model;
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
        private PerfilDao _perfilDao;

        public PerfilService(PerfilDao perfilDao)
        {
            _perfilDao = perfilDao;
        }

        public List<Perfil> FindPerfiles()
        {
            return _perfilDao.FindPerfiles();
        }
    }
}
