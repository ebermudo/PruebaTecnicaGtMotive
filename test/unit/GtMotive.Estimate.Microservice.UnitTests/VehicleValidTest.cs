using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests
{
    public class VehicleValidTest
    {
        [Fact]
        public void IsVehicleValidReturnsTrueWhenVehicleCanBeCreated()
        {
            // Arrange: Crear un mock del repositorio
            var mockVehicleRentalService = new Mock<IVehicleRentalService>();

            // Crear un objeto de vehículo ficticio que será devuelto por el mock
            var activeVehicle = new Vehicle("02/03/2025", 3, "ford fiesta", true, null);

            mockVehicleRentalService
            .Setup(service => service.CanCreateVehicle(activeVehicle))
            .Returns(true);

            var result = mockVehicleRentalService.Object.CanCreateVehicle(activeVehicle);

            Assert.True(result);
        }

        [Fact]
        public void IsVehicleValidReturnsTrueWhenVehicleCantBeCreated()
        {
            // Arrange: Crear un mock del repositorio
            var mockVehicleRentalService = new Mock<IVehicleRentalService>();

            // Crear un objeto de vehículo ficticio que será devuelto por el mock
            var activeVehicle = new Vehicle("02/03/2019", 3, "ford fiesta", true, null);

            mockVehicleRentalService
            .Setup(service => service.CanCreateVehicle(activeVehicle))
            .Returns(false);

            var result = mockVehicleRentalService.Object.CanCreateVehicle(activeVehicle);

            Assert.False(result);
        }
    }
}
