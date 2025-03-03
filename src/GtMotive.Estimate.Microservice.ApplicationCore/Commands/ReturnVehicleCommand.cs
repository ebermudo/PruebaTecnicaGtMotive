using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Commands
{
    public record ReturnVehicleCommand(int ClientId, int VehicleId) : IRequest<bool>;
}
