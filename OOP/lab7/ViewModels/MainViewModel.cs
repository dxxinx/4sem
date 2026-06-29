using FlowerShop.Models;
using FlowerShop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace FlowerShop.ViewModels
{
    public enum UserRole
    {
        Client,
        Admin
    }

    public class MainViewModel : BaseViewModel
    {
        private readonly ProductRepository _repository;

        public ObservableCollection<Product> Products { get; } = new();
        public ObservableCollection<CartItem> CartItems { get; } = new();
        // Undo/Redo
        private readonly Stack<IAction> _undoStack = new();
        private readonly Stack<IAction> _redoStack = new();

        private ICollectionView _productsView;
        public ICollectionView ProductsView
        {
            get => _productsView;
            private set => SetField(ref _productsView, value);
        }

        private Product? _selectedProduct;
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set => SetField(ref _selectedProduct, value);
        }

        private UserRole _currentRole = UserRole.Client;
        public UserRole CurrentRole
        {
            get => _currentRole;
            set
            {
                if (SetField(ref _currentRole, value))
                {
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }

        public bool IsAdmin => CurrentRole == UserRole.Admin;

        // Фильтры
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetField(ref _searchText, value))
                    ProductsView.Refresh();
            }
        }

        private string _selectedCategory = "All";
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (SetField(ref _selectedCategory, value))
                    ProductsView.Refresh();
            }
        }

        private decimal _minPrice = 0;
        public decimal MinPrice
        {
            get => _minPrice;
            set
            {
                if (SetField(ref _minPrice, value))
                    ProductsView.Refresh();
            }
        }

        private decimal _maxPrice = 100000;
        public decimal MaxPrice
        {
            get => _maxPrice;
            set
            {
                if (SetField(ref _maxPrice, value))
                    ProductsView.Refresh();
            }
        }

        public ObservableCollection<string> Categories { get; } = new();

        // Команды
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand AddToCartCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand ClearCartCommand { get; }
        public ICommand SaveProductsCommand { get; }
        public ICommand LoadProductsCommand { get; }
        public ICommand SwitchToAdminCommand { get; }
        public ICommand SwitchToClientCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        public MainViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
            var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "products.json");
            var dir = Path.GetDirectoryName(dataPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            _repository = new ProductRepository(dataPath);


            LoadProducts();

            ProductsView = CollectionViewSource.GetDefaultView(Products);
            ProductsView.Filter = FilterProduct;

            AddProductCommand = new RelayCommand(_ => AddProduct(), _ => IsAdmin);
            EditProductCommand = new RelayCommand(_ => EditProduct(), _ => IsAdmin && SelectedProduct != null);
            DeleteProductCommand = new RelayCommand(_ => DeleteProduct(), _ => IsAdmin && SelectedProduct != null);

            AddToCartCommand = new RelayCommand(_ => AddToCart(), _ => SelectedProduct != null && SelectedProduct.IsAvailable);
            RemoveFromCartCommand = new RelayCommand(item => RemoveFromCart(item as CartItem), item => item is CartItem);
            ClearCartCommand = new RelayCommand(_ => ClearCart(), _ => CartItems.Any());

            SaveProductsCommand = new RelayCommand(_ => SaveProducts());
            LoadProductsCommand = new RelayCommand(_ => LoadProducts());

            SwitchToAdminCommand = new RelayCommand(_ => CurrentRole = UserRole.Admin);
            SwitchToClientCommand = new RelayCommand(_ => CurrentRole = UserRole.Client);
            UndoCommand = new RelayCommand(_ => Undo(), _ => _undoStack.Any());
            RedoCommand = new RelayCommand(_ => Redo(), _ => _redoStack.Any());

        }
        private void ExecuteAction(IAction action)
        {
            action.Do();
            _undoStack.Push(action);
            _redoStack.Clear();
        }

        private void Undo()
        {
            if (!_undoStack.Any()) return;

            var action = _undoStack.Pop();
            action.Undo();
            _redoStack.Push(action);
        }

        private void Redo()
        {
            if (!_redoStack.Any()) return;

            var action = _redoStack.Pop();
            action.Do();
            _undoStack.Push(action);
        }

        private void LoadProducts()
        {
            Products.Clear();
            Categories.Clear();
            Categories.Add("All");

            var loaded = _repository.Load();
            foreach (var p in loaded)
            {
                Products.Add(p);
                if (!string.IsNullOrWhiteSpace(p.Category) && !Categories.Contains(p.Category))
                    Categories.Add(p.Category);
            }

            if (!Products.Any())
            {
                SeedSampleData();
            }
        }

        private void SaveProducts()
        {
            _repository.Save(Products.ToList());
        }

        private void SeedSampleData()
        {
            var rose = new Product
            {
                ShortName = "Роза красная",
                FullName = "Букет красных роз (11 шт.)",
                Description = "Классический букет красных роз для особых случаев.",
                Category = "Букеты",
                Rating = 4.8,
                Price = 45,
                QuantityInStock = 10,
                Color = "Красный",
                Size = "Средний",
                DeliveryCountry = "Беларусь",
                IsOnSale = true,
                DiscountPercent = 10,
                PurchasedCount = 120,
                Manufacturer = "FlowerLand",
                ImagePaths = { "Assets/rose2.jpg" }
            };

            var tulip = new Product
            {
                ShortName = "Тюльпаны микс",
                FullName = "Весенний букет тюльпанов (15 шт.)",
                Description = "Яркий весенний букет разноцветных тюльпанов.",
                Category = "Букеты",
                Rating = 4.6,
                Price = 35,
                QuantityInStock = 15,
                Color = "Микс",
                Size = "Средний",
                DeliveryCountry = "Беларусь",
                IsOnSale = false,
                DiscountPercent = 0,
                PurchasedCount = 80,
                Manufacturer = "SpringFlowers",
                ImagePaths = { "Assets/tulip1.jpg" }
            };

            Products.Add(rose);
            Products.Add(tulip);

            foreach (var c in Products.Select(p => p.Category).Distinct())
                Categories.Add(c);

            SaveProducts();
        }

        private bool FilterProduct(object obj)
        {
            if (obj is not Product p) return false;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var text = SearchText.ToLower();
                if (!(p.ShortName.ToLower().Contains(text) ||
                      p.FullName.ToLower().Contains(text) ||
                      p.Description.ToLower().Contains(text)))
                    return false;
            }

            if (SelectedCategory != "All" && !string.Equals(p.Category, SelectedCategory, StringComparison.OrdinalIgnoreCase))
                return false;

            if (p.Price < MinPrice || p.Price > MaxPrice)
                return false;

            return true;
        }

        private void AddProduct()
        {
            var newProduct = new Product
            {
                ShortName = "Новый товар",
                FullName = "Новый цветочный товар",
                Category = "Разное",
                Price = 0,
                QuantityInStock = 0,
                DeliveryCountry = "Беларусь"
            };

            ExecuteAction(new AddProductAction(Products, newProduct));

            if (!Categories.Contains(newProduct.Category))
                Categories.Add(newProduct.Category);

            SelectedProduct = newProduct;
        }

        private void EditProduct()
        {
            //редактирование идёт прямо через биндинги в UI.
            SaveProducts();
        }

        private void DeleteProduct()
        {
            if (SelectedProduct == null) return;

            ExecuteAction(new DeleteProductAction(Products, SelectedProduct));
            SelectedProduct = null;
        }

        private void AddToCart()
        {
            if (SelectedProduct == null) return;

            var existing = CartItems.FirstOrDefault(c => c.Product.Id == SelectedProduct.Id);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                CartItems.Add(new CartItem(SelectedProduct, 1));
            }

            SelectedProduct.PurchasedCount++;
            SelectedProduct.QuantityInStock = Math.Max(0, SelectedProduct.QuantityInStock - 1);

            OnPropertyChanged(nameof(CartTotal));
        }

        private void RemoveFromCart(CartItem? item)
        {
            if (item == null) return;

            CartItems.Remove(item);
            OnPropertyChanged(nameof(CartTotal));
        }

        private void ClearCart()
        {
            CartItems.Clear();
            OnPropertyChanged(nameof(CartTotal));
        }

        public decimal CartTotal => CartItems.Sum(c => c.TotalPrice);
    }
}
