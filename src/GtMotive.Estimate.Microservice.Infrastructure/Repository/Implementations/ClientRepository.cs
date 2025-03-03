using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositorys.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repository.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _clientCollection;

        // Inyectamos MongoService y obtenemos la colección de clientes
        public ClientRepository(MongoService mongoService)
        {
            if (mongoService == null)
            {
                throw new ArgumentNullException(nameof(mongoService), "El parámetro mongoService no puede ser nulo.");
            }

            _clientCollection = mongoService.GetCollection<Client>("Clients");
        }

        /// <summary>
        /// Gets a client by ID.
        /// </summary>
        /// <param name="clientId">Client Id.</param>
        /// <returns>A client object.</returns>
        public async Task<Client> GetById(int clientId)
        {
            var filter = Builders<Client>.Filter.Eq(c => c.ClientId, clientId);
            return await _clientCollection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the client information.
        /// </summary>
        /// <param name="client">Client entity.</param>
        /// <returns>void task.</returns>
        public async Task Update(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "El parámetro client no puede ser nulo.");
            }

            var filter = Builders<Client>.Filter.Eq(c => c.ClientId, client.ClientId);
            var update = Builders<Client>.Update
                .Set(c => c.Dni, client.Dni)
                .Set(c => c.Name, client.Name);

            await _clientCollection.UpdateOneAsync(filter, update);
        }
    }
}
