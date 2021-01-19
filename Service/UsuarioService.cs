using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendGestionProyectosLiquidaciones.Model;
using BackendGestionProyectosLiquidaciones.Dao;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public class UsuarioService
    {
        private UsuarioDao _usuarioDao;

        public UsuarioService(UsuarioDao usuarioDao)
        {
            _usuarioDao = usuarioDao;
        }

        public Usuario FindUsuario(Usuario usuario)
        {
            return _usuarioDao.FindUsuario(usuario);
        }

        /* ----- ABM ----- */
        public bool CrearUsuario(Usuario usuario)
        {
            return _usuarioDao.CrearUsuario(usuario);
        }

        public bool ModificarUsuario(Usuario usuario)
        {
            return _usuarioDao.ModificarUsuario(usuario);
        }

        public void EliminarUsuario(int IdUsuario)
        {
            _usuarioDao.EliminarUsuario(IdUsuario);
        }
    }
}
