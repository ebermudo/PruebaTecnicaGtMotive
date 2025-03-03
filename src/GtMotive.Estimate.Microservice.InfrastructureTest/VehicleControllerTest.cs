using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    public class VehicleControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public VehicleControllerTest(CustomWebApplicationFactory<Program> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory), "El servicio factory no puede ser nulo.");
            }

            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateVehicleWithInvalidModelReturnsBadRequest()
        {
            // Arrange: JSON con datos inválidos (faltan campos requeridos)
            var invalidVehicle = new { Model = "Corolla" }; // Falta "Make" y "Year"

            using var content = new StringContent(JsonSerializer.Serialize(invalidVehicle), Encoding.UTF8, "application/json");

            // Realiza la solicitud POST al endpoint de vehículos
            var response = await _client.PostAsync(new Uri("/api/vehicles", UriKind.Relative), content);

            // Assert: Debe devolver un error 400 (BadRequest)
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            // Obtiene y verifica el contenido de la respuesta
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("The Make field is required", responseContent, StringComparison.Ordinal);
        }
    }
}
