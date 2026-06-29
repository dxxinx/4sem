using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FlowerShop.Models;

namespace FlowerShop.Data
{
    public class EntityProductService
    {
        public async Task EnsureDatabaseAsync()
        {
            using (var db = new FlowerShopContext())
            {
                db.Database.Initialize(false);

                if (await db.Categories.AnyAsync())
                    return;

                var bouquets = new Category { Name = "Букеты", Description = "Готовые цветочные композиции" };
                var plants = new Category { Name = "Комнатные растения", Description = "Растения в горшках" };
                var flowerLand = new Manufacturer { Name = "FlowerLand", Country = "Беларусь", Phone = "+375291112233" };
                var springFlowers = new Manufacturer { Name = "SpringFlowers", Country = "Нидерланды", Phone = "+31555111222" };

                db.Categories.AddRange(new[] { bouquets, plants });
                db.Manufacturers.AddRange(new[] { flowerLand, springFlowers });
                db.Products.AddRange(new[]
                {
                    CreateSeedProduct("Роза красная", "Букет красных роз (11 шт.)", "Классический букет для особых случаев.", bouquets, flowerLand, 4.8, 45, 10, "Красный", true, 10, 120),
                    CreateSeedProduct("Тюльпаны микс", "Весенний букет тюльпанов (15 шт.)", "Яркий весенний букет.", bouquets, springFlowers, 4.6, 35, 15, "Микс", false, 0, 80)
                });

                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetProductsAsync(
            string searchText = null,
            string categoryName = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string sortColumn = "ShortName",
            bool descending = false)
        {
            using (var db = new FlowerShopContext())
            {
                IQueryable<Product> query = db.Products
                    .Include(p => p.CategoryEntity)
                    .Include(p => p.ManufacturerEntity);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = ApplySearch(query, searchText);
                }

                if (!string.IsNullOrWhiteSpace(categoryName) && categoryName != "All")
                {
                    query = query.Where(p => p.CategoryEntity.Name == categoryName);
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice.Value);
                }

                query = ApplySort(query, sortColumn, descending);
                var products = await query.ToListAsync();

                foreach (var product in products)
                {
                    FillUiFields(product);
                }

                return products;
            }
        }

        public async Task<DataTable> GetProductsTableAsync(string searchText, string categoryName, string sortColumn, bool descending)
        {
            var products = await GetProductsAsync(searchText, categoryName, null, null, sortColumn, descending);
            var table = CreateProductsTable();

            foreach (var product in products)
            {
                table.Rows.Add(
                    product.ProductId,
                    product.Id,
                    product.ShortName,
                    product.FullName,
                    product.Price,
                    product.QuantityInStock,
                    product.Category,
                    product.Manufacturer,
                    product.Rating,
                    product.IsOnSale,
                    product.PurchasedCount);
            }

            return table;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            using (var db = new FlowerShopContext())
            {
                var category = await GetOrCreateCategoryAsync(db, product.Category);
                var manufacturer = await GetOrCreateManufacturerAsync(db, product.Manufacturer);
                product.CategoryEntity = category;
                product.ManufacturerEntity = manufacturer;
                product.CategoryId = category.CategoryId;
                product.ManufacturerId = manufacturer.ManufacturerId;

                db.Products.Add(product);
                await db.SaveChangesAsync();
                FillUiFields(product);
                return product;
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            using (var db = new FlowerShopContext())
            {
                var stored = await FindProductAsync(db, product);
                if (stored == null)
                    throw new InvalidOperationException("Товар не найден.");

                stored.ShortName = product.ShortName;
                stored.FullName = product.FullName;
                stored.Description = product.Description;
                stored.Rating = product.Rating;
                stored.Price = product.Price;
                stored.QuantityInStock = product.QuantityInStock;
                stored.Color = product.Color;
                stored.Size = product.Size;
                stored.DeliveryCountry = product.DeliveryCountry;
                stored.IsOnSale = product.IsOnSale;
                stored.DiscountPercent = product.DiscountPercent;
                stored.PurchasedCount = product.PurchasedCount;
                stored.CategoryEntity = await GetOrCreateCategoryAsync(db, product.Category);
                stored.ManufacturerEntity = await GetOrCreateManufacturerAsync(db, product.Manufacturer);

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(Product product)
        {
            using (var db = new FlowerShopContext())
            {
                var stored = await FindProductAsync(db, product);
                if (stored == null)
                    return;

                db.Products.Remove(stored);
                await db.SaveChangesAsync();
            }
        }

        public async Task SellProductInTransactionAsync(int productId, int quantity)
        {
            using (var db = new FlowerShopContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var product = await db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
                    if (product == null)
                        throw new InvalidOperationException("Товар не найден.");
                    if (product.QuantityInStock < quantity)
                        throw new InvalidOperationException("Недостаточно товара на складе.");

                    product.QuantityInStock -= quantity;
                    product.PurchasedCount += quantity;
                    db.ProductAudit.Add(new ProductAudit
                    {
                        ProductId = product.ProductId,
                        ActionName = "EF SALE",
                        ChangedAt = DateTime.Now,
                        OldPrice = product.Price,
                        NewPrice = product.Price
                    });

                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private static Product CreateSeedProduct(string shortName, string fullName, string description, Category category, Manufacturer manufacturer, double rating, decimal price, int quantity, string color, bool isOnSale, decimal discount, int purchased)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                ShortName = shortName,
                FullName = fullName,
                Description = description,
                CategoryEntity = category,
                ManufacturerEntity = manufacturer,
                Rating = rating,
                Price = price,
                QuantityInStock = quantity,
                Color = color,
                Size = "Средний",
                DeliveryCountry = "Беларусь",
                IsOnSale = isOnSale,
                DiscountPercent = discount,
                PurchasedCount = purchased,
                CreatedAt = DateTime.Now
            };
        }

        private static IQueryable<Product> ApplySearch(IQueryable<Product> query, string searchText)
        {
            return query.Where(p =>
                p.ShortName.Contains(searchText) ||
                p.FullName.Contains(searchText) ||
                p.Description.Contains(searchText) ||
                p.CategoryEntity.Name.Contains(searchText) ||
                p.ManufacturerEntity.Name.Contains(searchText));
        }

        private static IQueryable<Product> ApplySort(IQueryable<Product> query, string sortColumn, bool descending)
        {
            switch ((sortColumn ?? string.Empty).Trim())
            {
                case "Price":
                    return descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                case "QuantityInStock":
                    return descending ? query.OrderByDescending(p => p.QuantityInStock) : query.OrderBy(p => p.QuantityInStock);
                case "Rating":
                    return descending ? query.OrderByDescending(p => p.Rating) : query.OrderBy(p => p.Rating);
                case "Category":
                case "CategoryName":
                    return descending ? query.OrderByDescending(p => p.CategoryEntity.Name) : query.OrderBy(p => p.CategoryEntity.Name);
                case "Manufacturer":
                case "ManufacturerName":
                    return descending ? query.OrderByDescending(p => p.ManufacturerEntity.Name) : query.OrderBy(p => p.ManufacturerEntity.Name);
                default:
                    return descending ? query.OrderByDescending(p => p.ShortName) : query.OrderBy(p => p.ShortName);
            }
        }

        private static void FillUiFields(Product product)
        {
            product.Category = product.CategoryEntity?.Name ?? string.Empty;
            product.Manufacturer = product.ManufacturerEntity?.Name ?? string.Empty;
            product.ImagePaths = new List<string>
            {
                product.ShortName.IndexOf("тюльпан", StringComparison.OrdinalIgnoreCase) >= 0
                    ? "Assets/tulip1.jpg"
                    : "Assets/rose2.jpg"
            };
        }

        private static DataTable CreateProductsTable()
        {
            var table = new DataTable("EF Products");
            table.Columns.Add("ProductId", typeof(int));
            table.Columns.Add("ProductUid", typeof(Guid));
            table.Columns.Add("ShortName", typeof(string));
            table.Columns.Add("FullName", typeof(string));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("QuantityInStock", typeof(int));
            table.Columns.Add("CategoryName", typeof(string));
            table.Columns.Add("ManufacturerName", typeof(string));
            table.Columns.Add("Rating", typeof(double));
            table.Columns.Add("IsOnSale", typeof(bool));
            table.Columns.Add("PurchasedCount", typeof(int));
            return table;
        }

        private static async Task<Category> GetOrCreateCategoryAsync(FlowerShopContext db, string name)
        {
            name = string.IsNullOrWhiteSpace(name) ? "Разное" : name.Trim();
            var category = await db.Categories.FirstOrDefaultAsync(c => c.Name == name);
            return category ?? db.Categories.Add(new Category { Name = name, Description = "Добавлено через EF Code First" });
        }

        private static async Task<Manufacturer> GetOrCreateManufacturerAsync(FlowerShopContext db, string name)
        {
            name = string.IsNullOrWhiteSpace(name) ? "Новый поставщик" : name.Trim();
            var manufacturer = await db.Manufacturers.FirstOrDefaultAsync(m => m.Name == name);
            return manufacturer ?? db.Manufacturers.Add(new Manufacturer { Name = name, Country = "Беларусь", Phone = "+375" });
        }

        private static Task<Product> FindProductAsync(FlowerShopContext db, Product product)
        {
            return product.ProductId > 0
                ? db.Products.FirstOrDefaultAsync(p => p.ProductId == product.ProductId)
                : db.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        }
    }
}
