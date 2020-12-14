using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopBookApi.Services.DTO
{
    public class MongoCRUD
    {
        private readonly IMongoDatabase db;

        public MongoCRUD(string database, IMongoClient client)
        {
            db = client.GetDatabase(database);
        }

        // CREATE
        public async Task CreateDocument<T>(string table, T record)
        {
            IMongoCollection<T> collection = db.GetCollection<T>(table);
            await collection.InsertOneAsync(record);
        }

        // READ
        public async Task<T> GetDocumentById<T>(string collection, Guid id)
        {
            IMongoCollection<T> collectionRef = db.GetCollection<T>(collection);
            return await collectionRef.Find(Builders<T>.Filter.Eq("_id", id)).FirstAsync();
        }

        public async Task<List<T>> GetDocuments<T>(string collection)
        {
            IMongoCollection<T> collectionRef = db.GetCollection<T>(collection);
            return await collectionRef.Find(new BsonDocument()).ToListAsync();
        }

        // UPDATE
        public async Task<T> ReplaceDocument<T>(string collection, Guid id, T document)
        {
            IMongoCollection<T> collectionRef = db.GetCollection<T>(collection);
            return await collectionRef.FindOneAndReplaceAsync<T>(Builders<T>.Filter.Eq("_id", id), document);
        }

        public async Task<T> UpdateSingleFieldOnDocument<T>(string collection, Guid id, string fieldToUpdate, T newValue)
        {
            IMongoCollection<T> collectionRef = db.GetCollection<T>(collection);
            return await collectionRef.FindOneAndUpdateAsync<T>(Builders<T>.Filter.Eq("_id", id), Builders<T>.Update.Set(fieldToUpdate, newValue));
        }

        // DELETE
        public async Task DeleteDocument<T>(string collection, Guid id)
        {
            IMongoCollection<T> collectionRef = db.GetCollection<T>(collection);
            await collectionRef.FindOneAndDeleteAsync<T>(Builders<T>.Filter.Eq("_id", id));
        }
    }
}
