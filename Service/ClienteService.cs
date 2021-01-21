using BackendGestionProyectosLiquidaciones.Dao;
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

        void CrearCliente(Cliente cliente);

        bool ModificarCliente(Cliente cliente);

        bool EliminarCliente(int IdCliente);

    }

    public class ClienteService : IClienteService
    {

        private ClienteDao _clienteDao;

        public ClienteService(ClienteDao clienteDao)
        {
            _clienteDao = clienteDao;
        }

        public List<Cliente> FindClientes()
        {
            return _clienteDao.FindClientes();
        }

        public List<Cliente> FindClienteByNombreApellido(string param)
        {
            return _clienteDao.FindClienteByNombreApellido(param);
        }

        public void CrearCliente(Cliente cliente)
        {
            _clienteDao.CrearCliente(cliente);
        }

        public bool ModificarCliente(Cliente cliente)
        {
            Cliente clienteDB = _clienteDao.FindCliente(cliente.Idcliente);

            if (clienteDB != null && cliente.Idcliente == clienteDB.Idcliente)
            {
                _clienteDao.ModificarCliente(cliente);
                return true;
            }

            return false;

        }

        public bool EliminarCliente(int IdCliente)
        {
            Cliente clienteDB = _clienteDao.FindCliente(IdCliente);

            if (clienteDB != null)
            {
                _clienteDao.EliminarCliente(clienteDB);
                return true;
            }

            return false;
        }


    }
}
