using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FlowerShop.Models;

namespace FlowerShop.Data
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FlowerShopDb"].ConnectionString;
        }

        public async Task EnsureDatabaseAsync()
        {
            var builder = new SqlConnectionStringBuilder(_connectionString);
            var databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "master";

            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
IF DB_ID(@DatabaseName) IS NULL
BEGIN
    DECLARE @Sql nvarchar(max) = N'CREATE DATABASE ' + QUOTENAME(@DatabaseName);
    EXEC(@Sql);
END";
                    command.Parameters.AddWithValue("@DatabaseName", databaseName);
                    await command.ExecuteNonQueryAsync();
                }
            }

            await ExecuteNonQueryAsync(SchemaScript);
            await ExecuteNonQueryAsync(ProgrammabilityScript);
            await SeedAsync();
        }
        public async Task<DataTable> LoadTableAsync(string tableName, string sortColumn = null, bool descending = false) //обычный запрос
        {
            ValidateTableName(tableName);
            return await QueryAsync("SELECT * FROM dbo." + tableName + GetOrderBy(tableName, sortColumn, descending));
        }

        public async Task<DataTable> SearchProductsAsync(string searchText, string categoryName) //запрос с параметрами
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandText = @"
SELECT p.ProductId, p.ShortName, p.FullName, p.Price, p.QuantityInStock, c.Name AS CategoryName, m.Name AS ManufacturerName
FROM dbo.Products p
JOIN dbo.Categories c ON c.CategoryId = p.CategoryId
JOIN dbo.Manufacturers m ON m.ManufacturerId = p.ManufacturerId
WHERE (@SearchText = N'' OR p.ShortName LIKE N'%' + @SearchText + N'%' OR p.FullName LIKE N'%' + @SearchText + N'%')
  AND (@CategoryName = N'' OR c.Name = @CategoryName)
ORDER BY p.ShortName";
                command.Parameters.AddWithValue("@SearchText", searchText ?? string.Empty);
                command.Parameters.AddWithValue("@CategoryName", categoryName ?? string.Empty);

                var table = new DataTable("ProductSearch");
                await connection.OpenAsync();
                adapter.Fill(table);
                return table;
            }
        }

        public async Task<DataTable> ExecuteProductsByCategoryProcedureAsync(string categoryName)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandText = "dbo.usp_GetProductsByCategory";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryName", categoryName ?? string.Empty);

                var table = new DataTable("ProductsByCategory");
                await connection.OpenAsync();
                adapter.Fill(table);
                return table;
            }
        }

        public async Task<List<Product>> LoadProductsForShopAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandText = @"
SELECT p.ProductUid, p.ShortName, p.FullName, p.Description, p.Rating, p.Price, p.QuantityInStock,
       p.Color, p.Size, p.DeliveryCountry, p.IsOnSale, p.DiscountPercent, p.PurchasedCount,
       c.Name AS CategoryName, m.Name AS ManufacturerName
FROM dbo.Products p
JOIN dbo.Categories c ON c.CategoryId = p.CategoryId
JOIN dbo.Manufacturers m ON m.ManufacturerId = p.ManufacturerId
ORDER BY p.ShortName";

                var table = new DataTable();
                await connection.OpenAsync();
                adapter.Fill(table);

                return table.AsEnumerable()
                    .Select(row => new Product
                    {
                        Id = row.Field<Guid>("ProductUid"),
                        ShortName = row.Field<string>("ShortName") ?? string.Empty,
                        FullName = row.Field<string>("FullName") ?? string.Empty,
                        Description = row.Field<string>("Description") ?? string.Empty,
                        Rating = Convert.ToDouble(row["Rating"]),
                        Price = row.Field<decimal>("Price"),
                        QuantityInStock = row.Field<int>("QuantityInStock"),
                        Color = row.Field<string>("Color") ?? string.Empty,
                        Size = row.Field<string>("Size") ?? string.Empty,
                        DeliveryCountry = row.Field<string>("DeliveryCountry") ?? string.Empty,
                        IsOnSale = row.Field<bool>("IsOnSale"),
                        DiscountPercent = row.Field<decimal>("DiscountPercent"),
                        PurchasedCount = row.Field<int>("PurchasedCount"),
                        Category = row.Field<string>("CategoryName") ?? string.Empty,
                        Manufacturer = row.Field<string>("ManufacturerName") ?? string.Empty,
                        ImagePaths = { GetDefaultImagePath(row.Field<string>("ShortName")) }
                    })
                    .ToList();
            }
        }

        public async Task SaveTableAsync(DataTable table, string tableName)
        {
            ValidateTableName(tableName);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                using (var adapter = new SqlDataAdapter("SELECT * FROM dbo." + tableName, connection))
                {
                    adapter.SelectCommand.Transaction = transaction;
                    using (var builder = new SqlCommandBuilder(adapter))
                    {
                        builder.ConflictOption = ConflictOption.OverwriteChanges;
                        adapter.InsertCommand = builder.GetInsertCommand();
                        adapter.UpdateCommand = builder.GetUpdateCommand();
                        adapter.DeleteCommand = builder.GetDeleteCommand();
                        adapter.InsertCommand.Transaction = transaction;
                        adapter.UpdateCommand.Transaction = transaction;
                        adapter.DeleteCommand.Transaction = transaction;

                        try
                        {
                            adapter.Update(table);
                            transaction.Commit();
                            table.AcceptChanges();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        private async Task<int> ExecuteNonQueryAsync(string sql)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }

        private async Task<DataTable> QueryAsync(string sql)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                var table = new DataTable();
                await connection.OpenAsync();
                adapter.Fill(table);
                return table;
            }
        }

        private async Task SeedAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
IF NOT EXISTS (SELECT 1 FROM dbo.Categories)
BEGIN
    INSERT INTO dbo.Categories(Name, Description) VALUES
    (N'Букеты', N'Готовые цветочные композиции'),
    (N'Комнатные растения', N'Растения в горшках'),
    (N'Аксессуары', N'Открытки, упаковка и декор');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Manufacturers)
BEGIN
    INSERT INTO dbo.Manufacturers(Name, Country, Phone) VALUES
    (N'FlowerLand', N'Беларусь', N'+375291112233'),
    (N'SpringFlowers', N'Нидерланды', N'+31555111222');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Products)
BEGIN
    DECLARE @Bouquets int = (SELECT CategoryId FROM dbo.Categories WHERE Name = N'Букеты');
    DECLARE @FlowerLand int = (SELECT ManufacturerId FROM dbo.Manufacturers WHERE Name = N'FlowerLand');
    DECLARE @SpringFlowers int = (SELECT ManufacturerId FROM dbo.Manufacturers WHERE Name = N'SpringFlowers');

    INSERT INTO dbo.Products(ProductUid, ShortName, FullName, Description, CategoryId, ManufacturerId, Rating, Price,
                             QuantityInStock, Color, Size, DeliveryCountry, IsOnSale, DiscountPercent, PurchasedCount, ProductLogo)
    VALUES
    (NEWID(), N'Роза красная', N'Букет красных роз (11 шт.)', N'Классический букет для особых случаев.', @Bouquets, @FlowerLand, 4.8, 45, 10, N'Красный', N'Средний', N'Беларусь', 1, 10, 120, 0x89504E47),
    (NEWID(), N'Тюльпаны микс', N'Весенний букет тюльпанов (15 шт.)', N'Яркий весенний букет.', @Bouquets, @SpringFlowers, 4.6, 35, 15, N'Микс', N'Средний', N'Беларусь', 0, 0, 80, 0x89504E47);
END";
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private static string GetOrderBy(string tableName, string sortColumn, bool descending)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                sortColumn = tableName == "Products" ? "ShortName" :
                             tableName == "ProductAudit" ? "ChangedAt" : "Name";
            }

            return " ORDER BY [" + sortColumn.Replace("]", "]]") + "]" + (descending ? " DESC" : " ASC");
        }

        private static void ValidateTableName(string tableName)
        {
            var allowed = new[] { "Products", "Categories", "Manufacturers", "ProductAudit" };
            if (!allowed.Contains(tableName))
                throw new InvalidOperationException("Unknown database table: " + tableName);
        }

        private static string GetDefaultImagePath(string shortName)
        {
            return (shortName ?? string.Empty).IndexOf("тюльпан", StringComparison.OrdinalIgnoreCase) >= 0
                ? "Assets/tulip1.jpg"
                : "Assets/rose2.jpg";
        }

        private const string SchemaScript = @"
IF OBJECT_ID(N'dbo.ProductAudit', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ProductAudit
    (
        AuditId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ProductAudit PRIMARY KEY,
        ProductId int NOT NULL,
        ActionName nvarchar(20) NOT NULL,
        ChangedAt datetime2 NOT NULL CONSTRAINT DF_ProductAudit_ChangedAt DEFAULT SYSDATETIME(),
        OldPrice decimal(10,2) NULL,
        NewPrice decimal(10,2) NULL
    );
END

IF OBJECT_ID(N'dbo.Categories', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Categories
    (
        CategoryId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Categories PRIMARY KEY,
        Name nvarchar(100) NOT NULL CONSTRAINT UQ_Categories_Name UNIQUE,
        Description nvarchar(500) NULL
    );
END

IF OBJECT_ID(N'dbo.Manufacturers', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Manufacturers
    (
        ManufacturerId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Manufacturers PRIMARY KEY,
        Name nvarchar(120) NOT NULL CONSTRAINT UQ_Manufacturers_Name UNIQUE,
        Country nvarchar(100) NOT NULL,
        Phone nvarchar(30) NULL
    );
END

IF OBJECT_ID(N'dbo.Products', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Products
    (
        ProductId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Products PRIMARY KEY,
        ProductUid uniqueidentifier NOT NULL CONSTRAINT DF_Products_ProductUid DEFAULT NEWID(),
        ShortName nvarchar(120) NOT NULL,
        FullName nvarchar(250) NOT NULL,
        Description nvarchar(1000) NULL,
        CategoryId int NOT NULL CONSTRAINT FK_Products_Categories REFERENCES dbo.Categories(CategoryId),
        ManufacturerId int NOT NULL CONSTRAINT FK_Products_Manufacturers REFERENCES dbo.Manufacturers(ManufacturerId),
        Rating float NOT NULL CONSTRAINT DF_Products_Rating DEFAULT 0,
        Price decimal(10,2) NOT NULL,
        QuantityInStock int NOT NULL CONSTRAINT DF_Products_Quantity DEFAULT 0,
        Color nvarchar(60) NULL,
        Size nvarchar(60) NULL,
        DeliveryCountry nvarchar(100) NULL,
        IsOnSale bit NOT NULL CONSTRAINT DF_Products_IsOnSale DEFAULT 0,
        DiscountPercent decimal(5,2) NOT NULL CONSTRAINT DF_Products_Discount DEFAULT 0,
        PurchasedCount int NOT NULL CONSTRAINT DF_Products_Purchased DEFAULT 0,
        ProductLogo varbinary(max) NULL,
        CreatedAt datetime2 NOT NULL CONSTRAINT DF_Products_CreatedAt DEFAULT SYSDATETIME(),
        CONSTRAINT CK_Products_Price CHECK (Price >= 0),
        CONSTRAINT CK_Products_Quantity CHECK (QuantityInStock >= 0)
    );
END";

        private const string ProgrammabilityScript = @"
IF OBJECT_ID(N'dbo.usp_GetProductsByCategory', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.usp_GetProductsByCategory AS BEGIN SET NOCOUNT ON; END');

EXEC(N'
ALTER PROCEDURE dbo.usp_GetProductsByCategory
    @CategoryName nvarchar(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT p.ProductId, p.ShortName, p.FullName, p.Price, p.QuantityInStock,
           c.Name AS CategoryName, m.Name AS ManufacturerName, p.ProductLogo
    FROM dbo.Products p
    JOIN dbo.Categories c ON c.CategoryId = p.CategoryId
    JOIN dbo.Manufacturers m ON m.ManufacturerId = p.ManufacturerId
    WHERE @CategoryName = N'''' OR c.Name = @CategoryName
    ORDER BY p.ShortName;
END');

IF OBJECT_ID(N'dbo.trg_Products_Audit', N'TR') IS NULL
    EXEC(N'CREATE TRIGGER dbo.trg_Products_Audit ON dbo.Products AFTER INSERT, UPDATE, DELETE AS BEGIN SET NOCOUNT ON; END');

EXEC(N'
ALTER TRIGGER dbo.trg_Products_Audit
ON dbo.Products
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.ProductAudit(ProductId, ActionName, OldPrice, NewPrice)
    SELECT COALESCE(i.ProductId, d.ProductId),
           CASE WHEN i.ProductId IS NOT NULL AND d.ProductId IS NULL THEN N''INSERT''
                WHEN i.ProductId IS NOT NULL AND d.ProductId IS NOT NULL THEN N''UPDATE''
                ELSE N''DELETE'' END,
           d.Price,
           i.Price
    FROM inserted i
    FULL JOIN deleted d ON i.ProductId = d.ProductId;
END')";
    }
}
