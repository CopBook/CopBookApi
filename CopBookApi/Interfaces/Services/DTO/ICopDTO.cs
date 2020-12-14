using CopBookApi.Interfaces.Api.Cop;
using CopBookApi.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopBookApi.Interfaces.Services.DTO
{
    public interface ICopDTO
    {
        // CREATE
        Task CreateCop(Cop cop);

        // READ
        Task<Cop> GetCopById(Guid id);

        Task<List<Cop>> GetAllCops();

        // UDPATE
        Task UpdateCop<T>(Guid id, string fieldToUpdate, T updatedValue);

        // DELETE
        Task DeleteCop(Guid id);
    }
}
