using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Service
{
    public interface IClienteService
    {
        List<Cliente> FindClientes();

        List<Cliente> FindClienteByNombreApellido(string param);

        Cliente FindCliente(int IdCliente);

        void CrearCliente(Cliente cliente);

        bool ModificarCliente(Cliente cliente);

        bool EliminarCliente(int IdCliente);

    }

    public class ClienteService : IClienteService
    {
        public IServiceScopeFactory _scopeFactory;

        public ClienteService(IServiceScopeFactory scopeFactory) {
            _scopeFactory = scopeFactory;
        }

        public List<Cliente> FindClientes()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();
                return dbContext.Cliente.ToList();
            }

        }

        public List<Cliente> FindClienteByNombreApellido(string param)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var clientes = from c in dbContext.Cliente
                               where c.NombreCliente.ToLower().Contains(param.ToLower())
                                     || c.ApellidoCliente.ToLower().Contains(param.ToLower())
                               select c;

                return clientes.ToList();
            }
        }

        public Cliente FindCliente(int IdCliente)
        {
            using (var scope = _scopeFactory.CreateScope()) {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var cliente = from c in dbContext.Cliente
                              where c.Idcliente == IdCliente
                              select c;

                    return cliente.FirstOrDefault();
            }
        }

        public void CrearCliente(Cliente cliente)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                dbContext.Cliente.Add(cliente);
                dbContext.SaveChanges();
            }
        }

        public bool ModificarCliente(Cliente cliente)
        {
            Cliente clienteDB = FindCliente((int)cliente.Idcliente);

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                if (clienteDB != null)
                {
                    dbContext.Update(cliente);
                    return true;
                }

                return false;
            }
        }

        public bool EliminarCliente(int IdCliente)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TpSeminarioContext>();

                var cliente = from c in dbContext.Cliente
                              where c.Idcliente == IdCliente
                              select c;

                Cliente clienteDB = cliente.FirstOrDefault();

                if (clienteDB != null)
                {
                    dbContext.Cliente.Remove(clienteDB);
                    dbContext.SaveChanges();
                return true;
                }
            return false;
            }

        }


    }
}
