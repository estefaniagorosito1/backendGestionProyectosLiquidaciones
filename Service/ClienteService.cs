using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private TpSeminarioContext _ctx;

        public ClienteService(TpSeminarioContext ctx)
        {
            _ctx = ctx;
        }

        public List<Cliente> FindClientes()
        {
            using (_ctx)
            {
                return _ctx.Cliente.ToList();
            }

        }

        public List<Cliente> FindClienteByNombreApellido(string param)
        {
            using (_ctx)
            {
                var clientes = from c in _ctx.Cliente
                               where c.NombreCliente.ToLower().Contains(param.ToLower())
                                     || c.ApellidoCliente.ToLower().Contains(param.ToLower())
                               select c;

                return clientes.ToList();

            }
        }

        public Cliente FindCliente(int IdCliente)
        {
            using (_ctx)
            {
                var cliente = from c in _ctx.Cliente
                              where c.Idcliente == IdCliente
                              select c;

                return cliente.FirstOrDefault();
            }
        }

        public void CrearCliente(Cliente cliente)
        {
            using (_ctx)
            {
                _ctx.Cliente.Add(cliente);
                _ctx.SaveChanges();
            }
        }

        public bool ModificarCliente(Cliente cliente)
        {
            Cliente clienteDB = FindCliente(cliente.Idcliente);

            if (clienteDB != null)
            {
                using (_ctx)
                {
                    _ctx.Update(cliente);
                    return true;
                }
            }

            return false;

        }

        public bool EliminarCliente(int IdCliente)
        {
            Cliente clienteDB = FindCliente(IdCliente);

            if (clienteDB != null)
            {
                using (_ctx)
                {
                    _ctx.Cliente.Remove(clienteDB);
                    _ctx.SaveChanges();
                    return true;
                }

            }

            return false;

        }


    }
}
