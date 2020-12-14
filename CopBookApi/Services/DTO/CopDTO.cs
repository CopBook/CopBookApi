using CopBookApi.Interfaces.Api.Cop;
using CopBookApi.Interfaces.Services.DTO;
using CopBookApi.Models.Api;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopBookApi.Services.DTO
{
    [BsonIgnoreExtraElements]
    public class CopDTO : ICopDTO
    {
        MongoCRUD MongoCRUD;
        readonly string COP_COLLECTION = "cops";
        public CopDTO(IMongoClient client)
        {
            MongoCRUD = new MongoCRUD("main", client);
        }

        public async Task CreateCop(Cop cop)
        {
            await MongoCRUD.CreateDocument(COP_COLLECTION, cop);
        }

        public Task<List<Cop>> GetAllCops()
        {
            return MongoCRUD.GetDocuments<Cop>(COP_COLLECTION);
        }

        public Task<Cop> GetCopById(Guid id)
        {
            return MongoCRUD.GetDocumentById<Cop>(COP_COLLECTION, id);
        }

        public Task UpdateCop<T>(Guid id, string fieldToUpdate, T updatedValue)
        {
            return MongoCRUD.UpdateSingleFieldOnDocument(COP_COLLECTION, id, fieldToUpdate, updatedValue);
        }

        public Task DeleteCop(Guid id)
        {
            return MongoCRUD.DeleteDocument<ICop>(COP_COLLECTION, id);
        }
    }
}
