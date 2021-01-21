using BackendGestionProyectosLiquidaciones.Dao;
using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IProvinciaService
    {
        List<Provincia> GetProvincias();
    }

    public class ProvinciaService : IProvinciaService
    {
        private ProvinciaDao _provinciaDao;

        public ProvinciaService(ProvinciaDao provinciaDao)
        {
            _provinciaDao = provinciaDao;
        }

        public List<Provincia> GetProvincias()
        {
            return _provinciaDao.GetProvincias();
        }
    }
}
