using DeviceManager.Busniess.Dtos;
using MediatR;

namespace DeviceManager.Busniess.Queries.DevicesQueries
{
    public record GetDeviceByIdQuery(int id) : IRequest<DeviceDTO>;
}
