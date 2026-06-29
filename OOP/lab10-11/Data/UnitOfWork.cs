using System.Threading.Tasks;
using FlowerShop.Models;
using Product = FlowerShop.Models.Product;
using Category = FlowerShop.Models.Category;
using Manufacturer = FlowerShop.Models.Manufacturer;
using ProductAudit = FlowerShop.Models.ProductAudit;

namespace FlowerShop.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FlowerShopContext _context;

        public UnitOfWork()
            : this(new FlowerShopContext())
        {
        }

        public UnitOfWork(FlowerShopContext context)
        {
            _context = context;
            Products = new Repository<Product>(_context);
            Categories = new Repository<Category>(_context);
            Manufacturers = new Repository<Manufacturer>(_context);
            ProductAudit = new Repository<ProductAudit>(_context);
        }

        public IRepository<Product> Products { get; }

        public IRepository<Category> Categories { get; }

        public IRepository<Manufacturer> Manufacturers { get; }

        public IRepository<ProductAudit> ProductAudit { get; }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
