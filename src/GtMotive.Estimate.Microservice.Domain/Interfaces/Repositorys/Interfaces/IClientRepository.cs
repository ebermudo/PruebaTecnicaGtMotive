using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces.Repositorys.Interfaces
{
    /// <summary>
    /// Interface of client repository.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Get client by Id.
        /// </summary>
        /// <param name="clientId">client Id.</param>
        /// <returns>the client getted.</returns>
        public Task<Client> GetById(int clientId);

        /// <summary>
        /// Update the client.
        /// </summary>
        /// <param name="client">client for update.</param>
        /// <returns>a task void.</returns>
        public Task Update(Client client);
    }
}
