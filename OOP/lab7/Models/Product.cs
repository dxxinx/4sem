using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlowerShop.Models
{
    public class Product : INotifyPropertyChanged
    {
        private Guid _id = Guid.NewGuid();
        private string _shortName = string.Empty;
        private string _fullName = string.Empty;
        private string _description = string.Empty;
        private List<string> _imagePaths = new();
        private string _category = string.Empty;
        private double _rating;
        private decimal _price;
        private int _quantityInStock;
        private string _color = string.Empty;
        private string _size = string.Empty;
        private string _deliveryCountry = string.Empty;
        private bool _isOnSale;
        private decimal _discountPercent;
        private int _purchasedCount;
        private string _manufacturer = string.Empty;
        private List<Guid> _relatedProductIds = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public Guid Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        public string ShortName
        {
            get => _shortName;
            set => SetField(ref _shortName, value);
        }

        public string FullName
        {
            get => _fullName;
            set => SetField(ref _fullName, value);
        }

        public string Description
        {
            get => _description;
            set => SetField(ref _description, value);
        }

        public List<string> ImagePaths
        {
            get => _imagePaths;
            set => SetField(ref _imagePaths, value);
        }

        public string Category
        {
            get => _category;
            set => SetField(ref _category, value);
        }

        public double Rating
        {
            get => _rating;
            set => SetField(ref _rating, value);
        }

        public decimal Price
        {
            get => _price;
            set => SetField(ref _price, value);
        }

        public int QuantityInStock
        {
            get => _quantityInStock;
            set
            {
                if (SetField(ref _quantityInStock, value))
                {
                    OnPropertyChanged(nameof(IsAvailable));
                }
            }
        }

        public string Color
        {
            get => _color;
            set => SetField(ref _color, value);
        }

        public string Size
        {
            get => _size;
            set => SetField(ref _size, value);
        }

        public string DeliveryCountry
        {
            get => _deliveryCountry;
            set => SetField(ref _deliveryCountry, value);
        }

        public bool IsOnSale
        {
            get => _isOnSale;
            set => SetField(ref _isOnSale, value);
        }

        public decimal DiscountPercent
        {
            get => _discountPercent;
            set => SetField(ref _discountPercent, value);
        }

        public bool IsAvailable => QuantityInStock > 0;

        public int PurchasedCount
        {
            get => _purchasedCount;
            set => SetField(ref _purchasedCount, value);
        }

        public string Manufacturer
        {
            get => _manufacturer;
            set => SetField(ref _manufacturer, value);
        }

        public List<Guid> RelatedProductIds
        {
            get => _relatedProductIds;
            set => SetField(ref _relatedProductIds, value);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
