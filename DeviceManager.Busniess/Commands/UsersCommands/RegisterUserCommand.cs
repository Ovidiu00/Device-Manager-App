using DeviceManager.Busniess.Dtos;
using MediatR;

namespace DeviceManager.Busniess.Commands.UsersCommands
{
    public record RegisterUserCommand(RegisterUserDTO userTryingToRegister) : IRequest<ResponseDTO>;
}
