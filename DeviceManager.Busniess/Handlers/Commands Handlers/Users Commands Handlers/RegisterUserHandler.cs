using DeviceManager.Busniess.Commands.UsersCommands;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Services;
using DeviceManager.DataAcess.EF.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Handlers.Commands_Handlers.Users_Commands_Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ResponseDTO>
    {
        private readonly IUserService userService;

        public RegisterUserHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<ResponseDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userTryingToRegister = request.userTryingToRegister;

            if (userTryingToRegister == null)
                throw new NullReferenceException("Reigster Model is null");

            if (userTryingToRegister.Password != userTryingToRegister.ConfirmPassword)
            {
                return new ResponseDTO
                {
                    Message = "Confirm password doesn't match the password",
                    Success = false,
                };
            }

            var user = new User
            {
                Email = userTryingToRegister.Email,
                UserName = userTryingToRegister.Email

            };
            var result = await userService.CreateAsync(user, userTryingToRegister.Password);

            if (!result.Succeeded)
            {
                return new ResponseDTO()
                {
                    Message = " ERROR CANT CREATE USER", //to do vezi cum le afisezi , cv cu select many
                    Success = false
                };
            }


            return new ResponseDTO()
            {
                Success = true,
                Result = user
            };
        }
    }
}
