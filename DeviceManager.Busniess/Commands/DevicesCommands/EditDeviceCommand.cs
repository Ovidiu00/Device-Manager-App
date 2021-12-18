using DeviceManager.Busniess.Dtos;
using MediatR;

namespace DeviceManager.Busniess.Commands.DevicesCommands
{
    public record EditDeviceCommand(int id,EditDeviceDTO deviceToBeEdited) : IRequest<DeviceDTO>;
}
