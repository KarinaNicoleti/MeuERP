using ERPWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace ERPWeb.Services
{
    public class ClienteService
    {
        private readonly List<Cliente> _clientes = new();

        public IEnumerable<Cliente> Listar()
        {
            return _clientes;
        }

        public Cliente Criar(Cliente cliente)
        {
            cliente.Id = _clientes.Count + 1;
            _clientes.Add(cliente);
            return cliente;
        }

        public Cliente? BuscarPorId(int id)
        {
            return _clientes.FirstOrDefault(c => c.Id == id);
        }

        public Cliente? Atualizar(int id, Cliente cliente)
        {
            var existente = BuscarPorId(id);
            if (existente == null) return null;

            existente.Nome = cliente.Nome;
            existente.Email = cliente.Email;
            return existente;
        }

        public bool Deletar(int id)
        {
            var cliente = BuscarPorId(id);
            if (cliente == null) return false;

            _clientes.Remove(cliente);
            return true;
        }
    }
}