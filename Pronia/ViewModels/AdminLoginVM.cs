﻿using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class AdminLoginVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
