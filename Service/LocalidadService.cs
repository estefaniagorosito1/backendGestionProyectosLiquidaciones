using BackendGestionProyectosLiquidaciones.Dao;
using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface ILocalidadService
    {
        List<Localidad> GetLocalidades(int IdProvincia, string param);
    }

    public class LocalidadService : ILocalidadService
    {
        private LocalidadDao _localidadDao;

        public LocalidadService(LocalidadDao localidadDao)
        {
            _localidadDao = localidadDao;
        }

        public List<Localidad> GetLocalidades(int IdProvincia, string param)
        {
            return _localidadDao.GetLocalidades(IdProvincia, param);
        }
    }
}
