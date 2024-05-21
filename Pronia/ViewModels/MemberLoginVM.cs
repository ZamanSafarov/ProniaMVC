using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class MemberLoginVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
