using DeviceManager.Busniess.Dtos;
using MediatR;

namespace DeviceManager.Busniess.Commands.DevicesCommands
{
    public record AddDeviceCommand(AddDeviceDTO deviceToBeAdded) : IRequest<DeviceDTO>;
}
