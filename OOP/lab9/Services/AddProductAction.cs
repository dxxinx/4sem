using System.Collections.ObjectModel;
using FlowerShop.Models;

namespace FlowerShop.Services
{
    public class AddProductAction : IAction
    {
        private readonly ObservableCollection<Product> _products;
        private readonly Product _product;

        public AddProductAction(ObservableCollection<Product> products, Product product)
        {
            _products = products;
            _product = product;
        }

        public void Do()
        {
            _products.Add(_product);
        }

        public void Undo()
        {
            _products.Remove(_product);
        }
    }
}
