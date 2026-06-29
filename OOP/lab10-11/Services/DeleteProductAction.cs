using System.Collections.ObjectModel;
using FlowerShop.Models;
using Product = FlowerShop.Models.Product;

namespace FlowerShop.Services
{
    public class DeleteProductAction : IAction
    {
        private readonly ObservableCollection<Product> _products;
        private readonly Product _product;
        private int _index;

        public DeleteProductAction(ObservableCollection<Product> products, Product product)
        {
            _products = products;
            _product = product;
        }

        public void Do()
        {
            _index = _products.IndexOf(_product);
            _products.Remove(_product);
        }

        public void Undo()
        {
            _products.Insert(_index, _product);
        }
    }
}
