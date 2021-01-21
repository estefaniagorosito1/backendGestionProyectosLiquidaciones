using BackendGestionProyectosLiquidaciones.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGestionProyectosLiquidaciones.Dao
{
    public interface IClienteDao
    {
        List<Cliente> FindClientes();

        List<Cliente> FindClienteByNombreApellido(string param);

        Cliente FindCliente(int IdCliente);

        void EliminarCliente(Cliente cliente);

        void Guardar(Cliente cliente);
    }


    public class ClienteDao : IClienteDao
    {
        private TpSeminarioContext _ctx = new TpSeminarioContext();

        public ClienteDao(TpSeminarioContext ctx)
        {
            _ctx = ctx;
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

        public List<Cliente> FindClientes()
        {
            using (_ctx)
            {
                return _ctx.Cliente.ToList();
            }
        }

        public Cliente FindCliente(int IdCliente)
        {
            using(_ctx)
            {
                var cliente = from c in _ctx.Cliente
                              where c.Idcliente == IdCliente 
                              select c;

                return cliente.FirstOrDefault();
            }
        }

        public void EliminarCliente(Cliente cliente)
        {
            using (_ctx)
            {
                _ctx.Cliente.Remove(cliente);
                _ctx.SaveChanges();
            }
        }

        public void Guardar(Cliente cliente)
        {
            using (_ctx)
            {
                _ctx.Cliente.Add(cliente);
                _ctx.SaveChanges();
            }
        }


    }
}
