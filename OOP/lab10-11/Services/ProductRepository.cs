using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using FlowerShop.Models;
using Product = FlowerShop.Models.Product;

namespace FlowerShop.Services
{
    public class ProductRepository
    {
        private readonly string _filePath;

        public ProductRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Product> Load()
        {
            if (!File.Exists(_filePath))
                return new List<Product>();

            var json = File.ReadAllText(_filePath);

            var products = JsonConvert.DeserializeObject<List<Product>>(json);

            return products ?? new List<Product>();
        }

        public void Save(List<Product> products)
        {
            var json = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText(_filePath, json);
        }
    }
}
