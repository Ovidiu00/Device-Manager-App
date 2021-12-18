using DeviceManager.Busniess.Commands.UsersCommands;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Services;
using DeviceManager.DataAcess.EF.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Handlers.Commands_Handlers.Users_Commands_Handlers
{
    class LoginUserHandler : IRequestHandler<LoginUserCommand, ResponseDTO>
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;

        public LoginUserHandler(IAuthenticationService authenticationService,IUserService userService)
        {
            this.authenticationService = authenticationService;
            this.userService = userService;
        }
        public async Task<ResponseDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userTryingToLogin = request.userToBeLoggedIn;

            User user = await userService.FindByEmailAsync(userTryingToLogin.Email);
            if (user == null)
            {
                return new ResponseDTO
                {
                    Message = "There is no user with that Email address",
                    Success = false,
                };
            }

            var result = await userService.CheckPasswordAsync(user, userTryingToLogin.Password);

            if (!result)
                return new ResponseDTO
                {
                    Message = "Invalid password",
                    Success = false,
                };

            var tokenAsString = authenticationService.Authenticate(userTryingToLogin.Email, userTryingToLogin.Password);
            return new ResponseDTO()
            {
                Success = true,
                Result = tokenAsString
            };
        }
    }
}
