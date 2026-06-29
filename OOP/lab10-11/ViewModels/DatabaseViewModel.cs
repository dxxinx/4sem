using FlowerShop.Data;
using FlowerShop.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using Product = FlowerShop.Models.Product;

namespace FlowerShop.ViewModels
{
    public class DatabaseViewModel : BaseViewModel
    {
        private readonly DatabaseService _database = new DatabaseService();
        private readonly EntityProductService _efDatabase = new EntityProductService();

        public event Func<Task> ProductCatalogChanged;

        public ObservableCollection<string> DatabaseTables { get; } = new ObservableCollection<string>
        {
            "Products",
            "Categories",
            "Manufacturers",
            "ProductAudit"
        };

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

        public DatabaseViewModel()
        {
            LoadDatabaseTableCommand = new RelayCommand(async _ => await LoadDatabaseTableAsync());
            AddDatabaseRowCommand = new RelayCommand(_ => AddDatabaseRow(), _ => SelectedDatabaseTable != "ProductAudit");
            DeleteDatabaseRowCommand = new RelayCommand(row => DeleteDatabaseRow(row as DataRowView), _ => SelectedDatabaseTable != "ProductAudit");
            SaveDatabaseTableCommand = new RelayCommand(async _ => await SaveDatabaseTableAsync(), _ => SelectedDatabaseTable != "ProductAudit");
            SortDatabaseAscCommand = new RelayCommand(async _ => await SortDatabaseTableAsync(false));
            SortDatabaseDescCommand = new RelayCommand(async _ => await SortDatabaseTableAsync(true));
            SearchProductsAsyncCommand = new RelayCommand(async _ => await SearchProductsAsync());
            ExecuteProcedureAsyncCommand = new RelayCommand(async _ => await ExecuteProcedureAsync());
            LoadEfProductsCommand = new RelayCommand(async _ => await LoadEfProductsTableAsync(false));
            AddEfProductCommand = new RelayCommand(async _ => await AddEfProductAsync());
            UpdateEfProductCommand = new RelayCommand(async row => await UpdateEfProductAsync(row as DataRowView));
            DeleteEfProductCommand = new RelayCommand(async row => await DeleteEfProductAsync(row as DataRowView));
            SellEfProductCommand = new RelayCommand(async row => await SellEfProductAsync(row as DataRowView));

            _ = InitializeDatabaseAsync();
        }

        public async Task CommitDatabaseChangesAsync()
        {
            await SaveDatabaseTableAsync();
        }

        private async Task InitializeDatabaseAsync()
        {
            try
            {
                DatabaseStatus = "SQL Server: проверка базы данных...";
                await _database.EnsureDatabaseAsync();
                await _efDatabase.EnsureDatabaseAsync();
                await RunRequiredDatabaseQueriesAsync();
                await LoadDatabaseTableAsync();
                await NotifyProductCatalogChangedAsync();
                DatabaseStatus = "SQL Server + Entity Framework: база FlowerShopLab8 готова. Repository + Unit of Work подключены.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "SQL Server: " + ex.Message;
            }
        }

        private async Task RunRequiredDatabaseQueriesAsync()
        {
            await _database.SearchProductsAsync(string.Empty, string.Empty);
            await _database.ExecuteProductsByCategoryProcedureAsync(string.Empty);
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
                DatabaseStatus = "EF: товары загружены через Repository + Unit of Work.";
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
                    Description = "CRUD: добавление сущности через Repository + Unit of Work.",
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
                await NotifyProductCatalogChangedAsync();
                DatabaseStatus = "EF CRUD: товар добавлен через Repository и сохранён Unit of Work.";
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

                product.Description = "CRUD: запись отредактирована через Repository + Unit of Work.";
                product.Price += 1;
                await _efDatabase.UpdateProductAsync(product);
                await LoadEfProductsTableAsync(false);
                await NotifyProductCatalogChangedAsync();
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
                await NotifyProductCatalogChangedAsync();
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
                await NotifyProductCatalogChangedAsync();
                DatabaseStatus = "EF: асинхронная транзакция продажи выполнена.";
            }
            catch (Exception ex)
            {
                DatabaseStatus = "Ошибка EF-транзакции: " + ex.Message;
            }
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
                await NotifyProductCatalogChangedAsync();
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

        private async Task NotifyProductCatalogChangedAsync()
        {
            var handler = ProductCatalogChanged;
            if (handler != null)
                await handler.Invoke();
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
    }
}