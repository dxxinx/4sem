using System;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class ProductAudit
    {
        [Key]
        public int AuditId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [MaxLength(20)]
        public string ActionName { get; set; } = string.Empty;

        public DateTime ChangedAt { get; set; } = DateTime.Now;

        public decimal? OldPrice { get; set; }

        public decimal? NewPrice { get; set; }
    }
}
