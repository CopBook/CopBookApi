using CopBookApi.Models.Api;
using CopBookApi.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopBookApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CopController : Controller
    {
        readonly CopDTO copDTO;
        public CopController(IMongoClient client)
        {
            copDTO = new CopDTO(client);
        }

        public async Task<List<Cop>> GetAllCops()
        {
            return await copDTO.GetAllCops();
        }

        [HttpGet]
        [Route("{id}")]
        public Task<Cop> GetCop(Guid id)
        {
            return copDTO.GetCopById(id);
        }

        public async Task CreateCop(Cop cop)
        {
            await copDTO.CreateCop(cop);
        }

    }
}
