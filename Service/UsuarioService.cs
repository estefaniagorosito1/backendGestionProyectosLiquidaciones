using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;

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
        public IServiceScopeFactory _scopeFactory;

        public UsuarioService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Usuario FindUsuario(string username, string password)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                return dbContext.Usuario.FirstOrDefault(user => user.NombreUsuario == username
                                           & user.PasswordUsuario == password);
            }
        }

        public Usuario FindUsuarioById(int IdUsuario)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                return dbContext.Usuario.FirstOrDefault(user => user.Idusuario == IdUsuario);
            }
        }

        public bool CrearUsuario(Usuario usuario)
        {
            var user = FindUsuarioById(usuario.Idusuario);

            if (user == null)
            {

                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                    dbContext.Usuario.Add(usuario);
                    dbContext.SaveChanges();
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

                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                    dbContext.Update(usuario);
                    dbContext.SaveChanges();
                }

                return true;
            }

            return false;
        }

        public void EliminarUsuario(int IdUsuario)
        {
                var user = FindUsuarioById(IdUsuario);

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                dbContext.Usuario.Remove(user);
                dbContext.SaveChanges();
            }

        }
    }
}
