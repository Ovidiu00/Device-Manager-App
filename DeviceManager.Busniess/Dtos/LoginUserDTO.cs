﻿using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Busniess.Dtos
{
    public class LoginUserDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
