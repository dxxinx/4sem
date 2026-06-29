using FlowerShop.Models;
using FlowerShop.Data;
using FlowerShop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Product = FlowerShop.Models.Product;

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
        private readonly DatabaseService _database = new DatabaseService();
        private readonly EntityProductService _efDatabase = new EntityProductService();
        public DatabaseViewModel Database { get; }

        public ObservableCollection<Product> Products { get; } = new();
        public ObservableCollection<CartItem> CartItems { get; } = new();
        public ObservableCollection<string> DatabaseTables { get; } = new()
        {
            "Products",
            "Categories",
            "Manufacturers",
            "ProductAudit"
        };
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

        private string _selectedDatabaseTable = "Products";
        public string SelectedDatabaseTable
        {
            get => _selectedDatabaseTable;
            set
            {
                if (SetField(ref _selectedDatabaseTable, value))
                    _ = LoadDatabaseTableAsync();
            }
        }

        private DataTable _currentDatabaseTable;
        public DataTable CurrentDatabaseTable
        {
            get => _currentDatabaseTable;
            private set
            {
                if (SetField(ref _currentDatabaseTable, value))
                    OnPropertyChanged(nameof(CurrentDatabaseView));
            }
        }

        public DataView CurrentDatabaseView => CurrentDatabaseTable?.DefaultView;

        private string _databaseSortColumn = "ShortName";
        public string DatabaseSortColumn
        {
            get => _databaseSortColumn;
            set => SetField(ref _databaseSortColumn, value);
        }

        private string _databaseSearchText = string.Empty;
        public string DatabaseSearchText
        {
            get => _databaseSearchText;
            set => SetField(ref _databaseSearchText, value);
        }

        private string _databaseProcedureCategory = "Букеты";
        public string DatabaseProcedureCategory
        {
            get => _databaseProcedureCategory;
            set => SetField(ref _databaseProcedureCategory, value);
        }

        private string _databaseStatus = "SQL Server: ожидание подключения.";
        public string DatabaseStatus
        {
            get => _databaseStatus;
            private set => SetField(ref _databaseStatus, value);
        }

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
        public ICommand LoadDatabaseTableCommand { get; }
        public ICommand AddDatabaseRowCommand { get; }
        public ICommand DeleteDatabaseRowCommand { get; }
        public ICommand SaveDatabaseTableCommand { get; }
        public ICommand SortDatabaseAscCommand { get; }
        public ICommand SortDatabaseDescCommand { get; }
        public ICommand SearchProductsAsyncCommand { get; }
        public ICommand ExecuteProcedureAsyncCommand { get; }
        public ICommand LoadEfProductsCommand { get; }
        public ICommand AddEfProductCommand { get; }
        public ICommand UpdateEfProductCommand { get; }
        public ICommand DeleteEfProductCommand { get; }
        public ICommand SellEfProductCommand { get; }

        public MainViewModel()
        {
            Database = new DatabaseViewModel();

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
            var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "products.json");
            var dir = Path.GetDirectoryName(dataPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            _repository = new ProductRepository(dataPath);
            Database.ProductCatalogChanged += LoadProductsFromDatabaseAsync;


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
            LoadDatabaseTableCommand = new RelayCommand(async _ => await LoadDatabaseTableAsync(), _ => IsAdmin);
            AddDatabaseRowCommand = new RelayCommand(_ => AddDatabaseRow(), _ => IsAdmin && SelectedDatabaseTable != "ProductAudit");
            DeleteDatabaseRowCommand = new RelayCommand(row => DeleteDatabaseRow(row as DataRowView), _ => IsAdmin && SelectedDatabaseTable != "ProductAudit");
            SaveDatabaseTableCommand = new RelayCommand(async _ => await SaveDatabaseTableAsync(), _ => IsAdmin && SelectedDatabaseTable != "ProductAudit");
            SortDatabaseAscCommand = new RelayCommand(async _ => await SortDatabaseTableAsync(false), _ => IsAdmin);
            SortDatabaseDescCommand = new RelayCommand(async _ => await SortDatabaseTableAsync(true), _ => IsAdmin);
            SearchProductsAsyncCommand = new RelayCommand(async _ => await SearchProductsAsync(), _ => IsAdmin);
            ExecuteProcedureAsyncCommand = new RelayCommand(async _ => await ExecuteProcedureAsync(), _ => IsAdmin);
            LoadEfProductsCommand = new RelayCommand(async _ => await LoadEfProductsTableAsync(false), _ => IsAdmin);
            AddEfProductCommand = new RelayCommand(async _ => await AddEfProductAsync(), _ => IsAdmin);
            UpdateEfProductCommand = new RelayCommand(async row => await UpdateEfProductAsync(row as DataRowView), _ => IsAdmin);
            DeleteEfProductCommand = new RelayCommand(async row => await DeleteEfProductAsync(row as DataRowView), _ => IsAdmin);
            SellEfProductCommand = new RelayCommand(async row => await SellEfProductAsync(row as DataRowView), _ => IsAdmin);

        }

        private async Task InitializeDatabaseAsync()
        {
            try
            {
                DatabaseStatus = "SQL Server: проверка базы данных...";
                await _database.EnsureDatabaseAsync();
                await _efDatabase.EnsureDatabaseAsync();
                await RunRequiredDatabaseQueriesAsync();
                await LoadProductsFromDatabaseAsync();
                await LoadDatabaseTableAsync();
                DatabaseStatus = "SQL Server + Entity Framework: база FlowerShopLab8 готова.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "SQL Server: " + ex.Message;
            }
        }

        public async Task CommitDatabaseChangesAsync()
        {
            await SaveDatabaseTableAsync();
        }

        private async Task RunRequiredDatabaseQueriesAsync()
        {
            await _database.SearchProductsAsync(string.Empty, string.Empty);
            await _database.ExecuteProductsByCategoryProcedureAsync(string.Empty);
        }

        private async Task LoadProductsFromDatabaseAsync()
        {
            var loaded = await _database.LoadProductsForShopAsync();
            Products.Clear();
            Categories.Clear();
            Categories.Add("All");

            foreach (var product in loaded)
            {
                Products.Add(product);
                if (!string.IsNullOrWhiteSpace(product.Category) && !Categories.Contains(product.Category))
                    Categories.Add(product.Category);
            }

            SelectedCategory = "All";
            ProductsView?.Refresh();
        }

        private async Task LoadDatabaseTableAsync()
        {
            try
            {
                CurrentDatabaseTable = await _database.LoadTableAsync(SelectedDatabaseTable, DatabaseSortColumn);
                DatabaseStatus = "Загружена таблица " + SelectedDatabaseTable + ".";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка загрузки: " + ex.Message;
            }
        }

        private async Task SortDatabaseTableAsync(bool descending)
        {
            try
            {
                CurrentDatabaseTable = await _database.LoadTableAsync(SelectedDatabaseTable, DatabaseSortColumn, descending);
                DatabaseStatus = "Сортировка выполнена по полю " + DatabaseSortColumn + ".";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка сортировки: " + ex.Message;
            }
        }

        private async Task SearchProductsAsync()
        {
            try
            {
                CurrentDatabaseTable = await _database.SearchProductsAsync(DatabaseSearchText, DatabaseProcedureCategory);
                DatabaseStatus = "Асинхронный параметризованный запрос выполнен.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка запроса: " + ex.Message;
            }
        }

        private async Task ExecuteProcedureAsync()
        {
            try
            {
                CurrentDatabaseTable = await _database.ExecuteProductsByCategoryProcedureAsync(DatabaseProcedureCategory);
                DatabaseStatus = "Хранимая процедура usp_GetProductsByCategory выполнена.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка процедуры: " + ex.Message;
            }
        }

        private async Task LoadEfProductsTableAsync(bool descending)
        {
            try
            {
                CurrentDatabaseTable = await _efDatabase.GetProductsTableAsync(
                    DatabaseSearchText,
                    DatabaseProcedureCategory,
                    DatabaseSortColumn,
                    descending);
                DatabaseStatus = "EF Code First: LINQ to Entities загрузил товары с фильтрацией, поиском и сортировкой.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка EF-запроса: " + ex.Message;
            }
        }

        private async Task AddEfProductAsync()
        {
            try
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    ShortName = "EF товар " + DateTime.Now.ToString("HHmmss"),
                    FullName = "Товар, добавленный через Entity Framework Code First",
                    Description = "CRUD: добавление сущности через DbContext.",
                    Category = string.IsNullOrWhiteSpace(DatabaseProcedureCategory) ? "Букеты" : DatabaseProcedureCategory,
                    Manufacturer = "EF Supplier",
                    Rating = 5,
                    Price = 25,
                    QuantityInStock = 7,
                    Color = "Микс",
                    Size = "Средний",
                    DeliveryCountry = "Беларусь",
                    IsOnSale = false,
                    DiscountPercent = 0,
                    PurchasedCount = 0
                };

                await _efDatabase.AddProductAsync(product);
                await LoadEfProductsTableAsync(false);
                await LoadProductsFromDatabaseAsync();
                DatabaseStatus = "EF CRUD: товар добавлен.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка EF-добавления: " + ex.Message;
            }
        }

        private async Task UpdateEfProductAsync(DataRowView row)
        {
            try
            {
                var product = ProductFromDataRow(row);
                if (product == null)
                    return;

                product.Description = "CRUD: запись отредактирована через Entity Framework.";
                product.Price += 1;
                await _efDatabase.UpdateProductAsync(product);
                await LoadEfProductsTableAsync(false);
                await LoadProductsFromDatabaseAsync();
                DatabaseStatus = "EF CRUD: товар обновлён, цена увеличена на 1.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка EF-редактирования: " + ex.Message;
            }
        }

        private async Task DeleteEfProductAsync(DataRowView row)
        {
            try
            {
                var product = ProductFromDataRow(row);
                if (product == null)
                    return;

                await _efDatabase.DeleteProductAsync(product);
                await LoadEfProductsTableAsync(false);
                await LoadProductsFromDatabaseAsync();
                DatabaseStatus = "EF CRUD: товар удалён.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка EF-удаления: " + ex.Message;
            }
        }

        private async Task SellEfProductAsync(DataRowView row)
        {
            try
            {
                var productId = GetProductId(row);
                if (productId == null)
                    return;

                await _efDatabase.SellProductInTransactionAsync(productId.Value, 1);
                await LoadEfProductsTableAsync(false);
                await LoadProductsFromDatabaseAsync();
                DatabaseStatus = "EF: асинхронная транзакция продажи выполнена.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка EF-транзакции: " + ex.Message;
            }
        }

        private static Product ProductFromDataRow(DataRowView row)
        {
            if (row == null)
                return null;

            return new Product
            {
                ProductId = Convert.ToInt32(row["ProductId"]),
                Id = row.Row.Table.Columns.Contains("ProductUid") ? (Guid)row["ProductUid"] : Guid.Empty,
                ShortName = Convert.ToString(row["ShortName"]) ?? string.Empty,
                FullName = Convert.ToString(row["FullName"]) ?? string.Empty,
                Price = Convert.ToDecimal(row["Price"]),
                QuantityInStock = Convert.ToInt32(row["QuantityInStock"]),
                Category = Convert.ToString(row["CategoryName"]) ?? "Разное",
                Manufacturer = Convert.ToString(row["ManufacturerName"]) ?? "Новый поставщик",
                Rating = Convert.ToDouble(row["Rating"]),
                IsOnSale = Convert.ToBoolean(row["IsOnSale"]),
                PurchasedCount = Convert.ToInt32(row["PurchasedCount"]),
                Color = "Микс",
                Size = "Средний",
                DeliveryCountry = "Беларусь"
            };
        }

        private static int? GetProductId(DataRowView row)
        {
            if (row == null || !row.Row.Table.Columns.Contains("ProductId"))
                return null;

            return Convert.ToInt32(row["ProductId"]);
        }

        private void AddDatabaseRow()
        {
            if (CurrentDatabaseTable == null || SelectedDatabaseTable == "ProductAudit")
                return;

            var row = CurrentDatabaseTable.NewRow();
            FillDatabaseDefaults(row);
            CurrentDatabaseTable.Rows.Add(row);
            _ = SaveDatabaseTableAsync();
        }

        private void DeleteDatabaseRow(DataRowView row)
        {
            if (row == null || SelectedDatabaseTable == "ProductAudit")
                return;

            row.Row.Delete();
            _ = SaveDatabaseTableAsync();
        }

        private async Task SaveDatabaseTableAsync()
        {
            if (CurrentDatabaseTable == null || SelectedDatabaseTable == "ProductAudit")
                return;

            try
            {
                await _database.SaveTableAsync(CurrentDatabaseTable, SelectedDatabaseTable);
                await LoadDatabaseTableAsync();
                await LoadProductsFromDatabaseAsync();
                DatabaseStatus = "Изменения сохранены в транзакции.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка сохранения: " + ex.Message;
            }
        }

        private void FillDatabaseDefaults(DataRow row)
        {
            if (SelectedDatabaseTable == "Categories")
            {
                row["Name"] = "Новая категория";
                row["Description"] = "Описание категории";
                return;
            }

            if (SelectedDatabaseTable == "Manufacturers")
            {
                row["Name"] = "Новый поставщик";
                row["Country"] = "Беларусь";
                row["Phone"] = "+375";
                return;
            }

            if (SelectedDatabaseTable == "Products")
            {
                row["ProductUid"] = Guid.NewGuid();
                row["ShortName"] = "Новый товар";
                row["FullName"] = "Новый цветочный товар";
                row["Description"] = "Описание товара";
                row["CategoryId"] = 1;
                row["ManufacturerId"] = 1;
                row["Rating"] = 0;
                row["Price"] = 0m;
                row["QuantityInStock"] = 0;
                row["Color"] = "Микс";
                row["Size"] = "Средний";
                row["DeliveryCountry"] = "Беларусь";
                row["IsOnSale"] = false;
                row["DiscountPercent"] = 0m;
                row["PurchasedCount"] = 0;
                row["ProductLogo"] = new byte[] { 0x89, 0x50, 0x4E, 0x47 };
            }
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
