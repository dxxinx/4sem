using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int ManufacturerId { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(30)]
        public string Phone { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; }
    }
}
