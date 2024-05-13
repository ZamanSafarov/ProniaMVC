using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Tag : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }

    }
}
