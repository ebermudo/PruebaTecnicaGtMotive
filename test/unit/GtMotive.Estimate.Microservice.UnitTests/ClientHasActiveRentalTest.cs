using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositorys.Interfaces;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests
{
    public class ClientHasActiveRentalTest
    {
        [Fact]
        public async Task ClientHasActiveRentals()
        {
            // Arrange: Crear un mock del repositorio
            var mockVehicleRentalService = new Mock<IRentalRepository>();

            mockVehicleRentalService
            .Setup(service => service.HasActiveRental(1))
            .Returns(Task.FromResult(true));

            var result = await mockVehicleRentalService.Object.HasActiveRental(1);

            Assert.True(result);
        }

        [Fact]
        public async Task ClientHasntActiveRentals()
        {
            // Arrange: Crear un mock del repositorio
            var mockVehicleRentalService = new Mock<IRentalRepository>();

            mockVehicleRentalService
            .Setup(service => service.HasActiveRental(6))
            .Returns(Task.FromResult(false));

            var result = await mockVehicleRentalService.Object.HasActiveRental(6);

            Assert.False(result);
        }
    }
}
