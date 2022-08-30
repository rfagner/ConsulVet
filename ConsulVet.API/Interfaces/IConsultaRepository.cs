using ConsulVet.API.Models;
using System.Collections.Generic;

namespace ConsulVet.API.Interfaces
{
    public interface IConsultaRepository
    {
        // READ
        ICollection<Consulta> GetAll();
        Consulta GetById(int id);

        // CREATE
        Consulta Insert(Consulta consulta);

        // UPDATE
        Consulta Update(int id, Consulta consulta);

        // DELETE
        bool Delete(int id);
    }
}
