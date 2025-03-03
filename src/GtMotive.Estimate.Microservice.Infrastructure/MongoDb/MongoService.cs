using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public class MongoService
    {
        private readonly IMongoDatabase _database;

        public MongoService(IOptions<MongoDbSettings> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            MongoClient = new MongoClient(options.Value.ConnectionString);
            _database = MongoClient.GetDatabase(options.Value.MongoDbDatabaseName);
        }

        public MongoClient MongoClient { get; }

        // Método para obtener una colección
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        // Método para insertar un documento
        public async Task InsertDocumentAsync<T>(string collectionName, T document)
        {
            var collection = GetCollection<T>(collectionName);
            await collection.InsertOneAsync(document);
        }

        // Método para obtener documentos
        public async Task<List<T>> GetDocumentsAsync<T>(string collectionName)
        {
            var collection = GetCollection<T>(collectionName);
            return await collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        // Método para actualizar un documento
        public async Task UpdateDocumentAsync<T>(string collectionName, string id, T updatedDocument)
        {
            var collection = GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id); // Asegúrate de que tus documentos tengan un campo 'Id'
            await collection.ReplaceOneAsync(filter, updatedDocument);
        }

        // Método para eliminar un documento
        public async Task DeleteDocumentAsync<T>(string collectionName, string id)
        {
            var collection = GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id); // Asegúrate de que tus documentos tengan un campo 'Id'
            await collection.DeleteOneAsync(filter);
        }
    }
}
