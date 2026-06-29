using Microsoft.AspNetCore.HttpLogging; // Подключаем пространство имен для логирования HTTP-запросов
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args); // Создаем экземпляр WebApplicationBuilder с предустановленными настройками
        builder.Services.AddHttpLogging(options => {});// Добавляем сервис логирования HTTP-запросов

        var app = builder.Build(); // Создаем экземпляр приложения

        app.UseHttpLogging(); // Включаем логирование HTTP-запросов

        app.MapGet("/", () => "Моё первое ASPA"); // Создаем маршрут-обработчик по корневому адресу

        app.Run(); // Запускаем приложение
    }
}