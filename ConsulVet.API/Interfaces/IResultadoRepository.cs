using ConsulVet.API.Models;
using System.Collections.Generic;

namespace ConsulVet.API.Interfaces
{
    public interface IResultadoRepository
    {
        // READ
        ICollection<Resultado> GetAll();
        Resultado GetById(int id);

        // CREATE
        Resultado Insert(Resultado resultado);

        // UPDATE
        Resultado Update(int id, Resultado resultado);

        // DELETE
        bool Delete(int id);
    }
}
