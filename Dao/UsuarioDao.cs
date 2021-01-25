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

        void CrearUsuario(Usuario usuario);

        void ModificarUsuario(Usuario usuario);

        void EliminarUsuario(int IdUsuario);
    }


    public class UsuarioDao : IUsuarioDao
    {
        private TpSeminarioContext _ctx;

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

        public void CrearUsuario(Usuario usuario)
        {
            using (_ctx)
            {
                _ctx.Usuario.Add(usuario);
                _ctx.SaveChanges();

            }
        }

        public void ModificarUsuario(Usuario usuario)
        {
            using (_ctx)
            {
                _ctx.Update(usuario);
                _ctx.SaveChanges();
            }
        }

        public void EliminarUsuario(int IdUsuario)
        {
            using (_ctx)
            {
                var user = FindUsuarioByID(IdUsuario);
                _ctx.Usuario.Remove(user);
                _ctx.SaveChanges();
            }
        }

    }
}
