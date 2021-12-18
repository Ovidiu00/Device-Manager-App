using DeviceManager.Busniess.Dtos;
using DeviceManager.DataAcess.EF.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Services
{
    public interface IUserService
    {
        Task<ResponseDTO> LoginUserAsync(LoginUserDTO model);
        Task<ResponseDTO> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<User> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(User user,string password);
        Task<IdentityResult> CreateAsync(User user,string password);
    }
}
