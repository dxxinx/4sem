using DAL_Celebrity_MSSQL;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ANC25_WEBAPI_DLL.Services
{
    public static class CelebritiesServices
    {
        public static WebApplicationBuilder AddCelebritiesServices(this WebApplicationBuilder builder)
        {
            var connectionString = AppConfig.ConnectionString;

            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IRepository>(provider =>
            {
                return new Repository(connectionString);
            });

            builder.Services.AddSingleton<CountryCodes>(provider =>
            {
                var configuredPath = builder.Configuration[$"{CelebritiesConfig.SectionName}:ISO3166alpha2Path"]
                    ?? Path.Combine("CountryCodes", "iso3166-1-alpha2-country-codes.json");
                var countriesPath = Path.IsPathRooted(configuredPath)
                    ? configuredPath
                    : Path.Combine(builder.Environment.ContentRootPath, configuredPath);

                return CountryCodes.LoadFromFile(countriesPath);
            });

            builder.Services.AddSingleton<CelebrityTitles>(new CelebrityTitles
            {
                Title = "Celebrities Management System",
                Head = "Famous People Database",
                Copyright = $"© {DateTime.Now.Year} Celebrities App"
            });

            builder.Services.Configure<CelebritiesConfig>(builder.Configuration.GetSection(CelebritiesConfig.SectionName));

            return builder;
        }

        public static WebApplicationBuilder AddCelebritiesDatabase(this WebApplicationBuilder builder)
        {
            var connectionString = AppConfig.ConnectionString;

            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IRepository>(provider =>
            {
                return new Repository(connectionString);
            });

            return builder;
        }
    }

    public class CountryCodes : IEnumerable<Country>
    {
        private readonly List<Country> _countries;

        private CountryCodes(List<Country> countries)
        {
            _countries = countries ?? throw new ArgumentNullException(nameof(countries));
        }

        public static CountryCodes LoadFromFile(string filePath)
        {
            try
            {
                var json = File.ReadAllText(filePath);
                var countries = JsonSerializer.Deserialize<List<Country>>(json);
                return new CountryCodes(countries);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load country codes: {ex.Message}", ex);
            }
        }

        public IEnumerator<Country> GetEnumerator() => _countries.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
