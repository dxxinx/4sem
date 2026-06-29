using ASPA008_1.API;
using Microsoft.Extensions.FileProviders;
using ANC25_WEBAPI_DLL;
using ANC25_WEBAPI_DLL.Services;
using static ASPA008_1.Controllers.CelebritiesController;
using System.Text.Json;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddCelebritiesConfiguration();
        builder.AddCelebritiesServices();
        builder.Services.AddControllersWithViews();
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);


        var app = builder.Build();

        app.UseHttpsRedirection();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        var photosFolder = app.Configuration["Celebrities:PhotosFolder"] ?? "Photos";
        var photosPath = Path.IsPathRooted(photosFolder)
            ? photosFolder
            : Path.Combine(builder.Environment.ContentRootPath, photosFolder);
        var photosRequestPath = app.Configuration["Celebrities:PhotosFolderRequestPath"] ?? "/Photos";

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(photosPath),
            RequestPath = photosRequestPath
        });

        app.UseRouting();
        app.UseMiddleware<CelebritiesError>();
        app.UseAuthorization();

        app.MapCelebrities();
        app.MapLifeevents();
        app.MapPhotoCelebrities();

        app.MapCelebritiesEndpoints();
        app.MapLifeEventsEndpoints();

        app.MapControllerRoute(
            name: "new_celebrity",
            pattern: "/0",
            defaults: new { Controller = "Celebrities", Action = "NewHumanForm" });

        app.MapControllerRoute(
            name: "celebrityDetails",
            pattern: "Celebrities/{id:int}",
            defaults: new { controller = "Celebrities", action = "Celebrities" });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Celebrities}/{action=Index}/{id?}");

        app.Run();
    }
}
