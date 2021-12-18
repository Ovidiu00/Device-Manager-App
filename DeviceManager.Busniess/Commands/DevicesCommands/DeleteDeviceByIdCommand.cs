using MediatR;

namespace DeviceManager.Busniess.Commands.DevicesCommands
{
    public record DeleteDeviceByIdCommand(int id) : IRequest<bool>;
    
}
