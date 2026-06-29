using System;
using System.Collections.Generic;

namespace FlowerShop.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string ShortName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<string> ImagePaths { get; set; } = new();

        public string Category { get; set; } = string.Empty;

        public double Rating { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string DeliveryCountry { get; set; } = string.Empty;

        public bool IsOnSale { get; set; }
        public decimal DiscountPercent { get; set; }

        public bool IsAvailable => QuantityInStock > 0;

        public int PurchasedCount { get; set; }

        public string Manufacturer { get; set; } = string.Empty;

        public List<Guid> RelatedProductIds { get; set; } = new();
    }
}
