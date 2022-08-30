using ConsulVet.API.Models;
using System.Collections.Generic;

namespace ConsulVet.API.Interfaces
{
    public interface IClienteRepository
    {
        // READ
        ICollection<Cliente> GetAll();
        Cliente GetById(int id);

        // CREATE
        Cliente Insert(Cliente cliente);

        // UPDATE
        Cliente Update(int id, Cliente cliente);

        // DELETE
        bool Delete(int id);
    }
}
