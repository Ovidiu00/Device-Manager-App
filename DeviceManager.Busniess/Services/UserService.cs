using DeviceManager.Busniess.Dtos;
using DeviceManager.DataAcess.EF.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IAuthenticationService authenticationService;

        public UserService(UserManager<User> userManager,
            IAuthenticationService authenticationService)
        {
            this.userManager = userManager;
            this.authenticationService = authenticationService;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<ResponseDTO> LoginUserAsync(LoginUserDTO model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new ResponseDTO
                {
                    Message = "There is no user with that Email address",
                    Success = false,
                };
            }

            var result = await userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new ResponseDTO
                {
                    Message = "Invalid password",
                    Success = false,
                };

            var tokenAsString = authenticationService.Authenticate(model.Email, model.Password);
            return new ResponseDTO()
            {
                Success = true,
                Result = tokenAsString
            };
        }

        public async Task<ResponseDTO> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            if (registerUserDTO == null)
                throw new NullReferenceException("Reigster Model is null");

            if (registerUserDTO.Password != registerUserDTO.ConfirmPassword)
            {
                return new ResponseDTO
                {
                    Message = "Confirm password doesn't match the password",
                    Success = false,
                };
            }

            var user = new User
            {
                Email = registerUserDTO.Email,
                UserName = registerUserDTO.Email

            };
            var result = await userManager.CreateAsync(user, registerUserDTO.Password);

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
