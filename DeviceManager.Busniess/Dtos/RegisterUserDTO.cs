using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Busniess.Dtos
{
    public class RegisterUserDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        //TODO : IMAGE FILE
    }
}
