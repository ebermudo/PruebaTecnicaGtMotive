using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Linq;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup>
    where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder), "El servicio builder no puede ser nulo.");
            }

            builder.ConfigureServices(services =>
            {
                // Remueve la configuración original de MongoDB
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IMongoClient));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Agrega la nueva configuración para usar la base de datos de test
                services.AddSingleton<IMongoClient>(provider =>
                {
                    var mongoClient = new MongoClient("mongodb://localhost:27017");
                    return mongoClient;
                });

                services.AddSingleton(provider =>
                {
                    var client = provider.GetRequiredService<IMongoClient>();
                    return client.GetDatabase("TestDatabase");
                });
            });
        }
    }
}
