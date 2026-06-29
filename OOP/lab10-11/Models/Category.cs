using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; }
    }
}
