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
        Usuario FindUsuario(string username, string password);

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

        public Usuario FindUsuario(string username, string password)
        {
            using (_ctx)
            {
                return _ctx.Usuario.FirstOrDefault(user => user.NombreUsuario == username
                                                           & user.PasswordUsuario == password);
            }

        }

        public Usuario FindUsuarioById(int IdUsuario)
        {
            using (_ctx)
            {
                return _ctx.Usuario.FirstOrDefault(user => user.Idusuario == IdUsuario);
            }
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

        }
    }
}
