using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Slider:BaseEntity
    {
        public string? Offer {  get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? RedirectUrl { get; set; }
        [StringLength(100)]
        public string? ImageUrl { get; set; }
        
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
