using System;
using System.Threading.Tasks;
using FlowerShop.Models;
using Product = FlowerShop.Models.Product;
using Category = FlowerShop.Models.Category;
using Manufacturer = FlowerShop.Models.Manufacturer;
using ProductAudit = FlowerShop.Models.ProductAudit;

namespace FlowerShop.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }

        IRepository<Category> Categories { get; }

        IRepository<Manufacturer> Manufacturers { get; }

        IRepository<ProductAudit> ProductAudit { get; }

        Task<int> SaveChangesAsync();
    }
}
