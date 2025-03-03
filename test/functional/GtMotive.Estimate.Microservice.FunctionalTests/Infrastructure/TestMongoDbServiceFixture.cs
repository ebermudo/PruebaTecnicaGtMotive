using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;

namespace GtMotive.Estimate.Microservice.FunctionalTests
{
    public class TestMongoDbServiceFixture
    {
        public TestMongoDbServiceFixture()
        {
            // Aquí usamos la cadena de conexión a la base de datos del contenedor de MongoDB (el contenedor de Docker)
            var connectionString = "mongodb://localhost:27017";  // Usamos el contenedor mongo
            var databaseName = "TestDatabase"; // Nombre de la base de datos de prueba

            // Inicializamos el servicio de MongoDB para pruebas
            MongoDbTestService = new MongoDbTestService(connectionString, databaseName);
        }

        public MongoDbTestService MongoDbTestService { get; private set; }
    }
}
