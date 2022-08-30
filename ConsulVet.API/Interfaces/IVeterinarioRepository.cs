using ConsulVet.API.Models;
using System.Collections.Generic;

namespace ConsulVet.API.Interfaces
{
    public interface IVeterinarioRepository
    {
        // READ
        ICollection<Veterinario> GetAll();
        Veterinario GetById(int id);

        // CREATE
        Veterinario Insert(Veterinario veterinario);

        // UPDATE
        Veterinario Update(int id, Veterinario veterinario);

        // DELETE
        bool Delete(int id);
    }
}
