using FlowerShop.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FlowerShop.Services
{
    public class ProductService
    {
        public static List<Product> LoadProducts()
        {
            string json = File.ReadAllText("products.json");
            return JsonConvert.DeserializeObject<List<Product>>(json);
        }
    }
}