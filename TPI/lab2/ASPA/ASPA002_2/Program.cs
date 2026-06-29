using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();

app.MapGet("/aspnetcore", () => "Привет, ASP.NET Core!");

app.MapGet("/", async context =>
{
    context.Response.Redirect("/Neumann.html");
});

app.MapGet("/ASPA002", async context =>
{
    context.Response.ContentType = "image/jpg";
    await context.Response.SendFileAsync("Picture/Neumann.jpg");
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Picture")),
    RequestPath = "/static"
});

app.Run();