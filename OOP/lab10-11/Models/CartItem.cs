using FlowerShop.ViewModels;
using System;

namespace FlowerShop.Models
{
    public class CartItem : BaseViewModel
    {
        public Product Product { get; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetField(ref _quantity, value);
        }

        public decimal TotalPrice => Product.Price * Quantity;

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }

}
