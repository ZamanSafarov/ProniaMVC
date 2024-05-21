using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class MemberRegisterVM
    {
        [Required]
        [MaxLength(40)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(40)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
