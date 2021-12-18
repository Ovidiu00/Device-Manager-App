using DeviceManager.Busniess.Dtos;
using MediatR;

namespace DeviceManager.Busniess.Commands.UsersCommands
{
    public record LoginUserCommand(LoginUserDTO userToBeLoggedIn) : IRequest<ResponseDTO>;
}
