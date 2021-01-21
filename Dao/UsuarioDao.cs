using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendGestionProyectosLiquidaciones.Model;

namespace BackendGestionProyectosLiquidaciones.Dao
{
    public interface IUsuarioDao
    {
        Usuario FindUsuario(Usuario usuario);

        Usuario FindUsuarioByID(int IdUsuario);

        bool CrearUsuario(Usuario usuario);

        bool ModificarUsuario(Usuario usuario);

        void EliminarUsuario(int IdUsuario);
    }


    public class UsuarioDao : IUsuarioDao
    {
        private TpSeminarioContext _ctx = new TpSeminarioContext();

        public UsuarioDao(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public Usuario FindUsuario(Usuario usuario)
        {
            using (_ctx)
            {
                return _ctx.Usuario.FirstOrDefault(user => user.NombreUsuario == usuario.NombreUsuario
                                                           & user.PasswordUsuario == usuario.PasswordUsuario);
            }
        }

        public Usuario FindUsuarioByID(int IdUsuario)
        {
            using (_ctx)
            {
                return _ctx.Usuario.FirstOrDefault(user => user.Idusuario == IdUsuario);
            }
        }

        public bool CrearUsuario(Usuario usuario)
        {
            using (_ctx)
            {
                var user = FindUsuario(usuario);

                if (user == null)
                {
                    _ctx.Usuario.Add(usuario);
                    _ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public bool ModificarUsuario(Usuario usuario)
        {
            using (_ctx)
            {
                var user = _ctx.Usuario.First(us => us.Idusuario == usuario.Idusuario);
                if (user != null)
                {
                    user = usuario;
                    _ctx.SaveChanges();
                    return true;
                }

                return false;

            }
        }

        public void EliminarUsuario(int IdUsuario)
        {
            using(_ctx)
            {
                var user = _ctx.Usuario.FirstOrDefault(u => u.Idusuario == IdUsuario);
                _ctx.Usuario.Remove(user);
                _ctx.SaveChanges();
            }
        }


    }
}
