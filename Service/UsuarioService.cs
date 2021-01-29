using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendGestionProyectosLiquidaciones.Model;
using BackendGestionProyectosLiquidaciones.Dao;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IUsuarioService
    {
        Usuario FindUsuario(Usuario usuario);

        Usuario FindUsuarioById(int IdUsuario);

        bool CrearUsuario(Usuario usuario);

        bool ModificarUsuario(Usuario usuario);

        void EliminarUsuario(int IdUsuario);

    }


    public class UsuarioService : IUsuarioService
    {
        private TpSeminarioContext _ctx;

        public UsuarioService(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }


        /*private UsuarioDao _usuarioDao;

        public UsuarioService(UsuarioDao usuarioDao)
        {
            _usuarioDao = usuarioDao;
        }*/

        public Usuario FindUsuario(Usuario usuario)
        {
            using (_ctx)
            {
                return _ctx.Usuario.FirstOrDefault(user => user.NombreUsuario == usuario.NombreUsuario
                                                           & user.PasswordUsuario == usuario.PasswordUsuario);
            }
            //return _usuarioDao.FindUsuario(usuario);
        }

        public Usuario FindUsuarioById(int IdUsuario)
        {
            using (_ctx)
            {
                return _ctx.Usuario.FirstOrDefault(user => user.Idusuario == IdUsuario);
            }
            //return _usuarioDao.FindUsuarioByID(IdUsuario);
        }

        public bool CrearUsuario(Usuario usuario)
        {
            var user = FindUsuarioById(usuario.Idusuario);

            if (user == null)
            {
                using (_ctx)
                {
                    _ctx.Usuario.Add(usuario);
                    _ctx.SaveChanges();

                }
                //_usuarioDao.CrearUsuario(usuario);
                return true;
            }

            return false;
        }

        public bool ModificarUsuario(Usuario usuario)
        {
            var user = FindUsuarioById(usuario.Idusuario);
            if (user != null)
            {
                using (_ctx)
                {
                    _ctx.Update(usuario);
                    _ctx.SaveChanges();
                }
                //_usuarioDao.ModificarUsuario(usuario);
                return true;
            }

            return false;
        }

        public void EliminarUsuario(int IdUsuario)
        {
            using (_ctx)
            {
                var user = FindUsuarioById(IdUsuario);
                _ctx.Usuario.Remove(user);
                _ctx.SaveChanges();
            }
            //_usuarioDao.EliminarUsuario(IdUsuario);
        }
    }
}
