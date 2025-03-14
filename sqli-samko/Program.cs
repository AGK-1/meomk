using Microsoft.EntityFrameworkCore;
using sqli_samko;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Kestrel konfiqurasiyası (80 portu açıq)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // HTTP üçün
    //options.ListenAnyIP(443, listenOptions => listenOptions.UseHttps()); // HTTPS lazım olarsa
});

var app = builder.Build();

// Middleware-lər
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = ""; // Swagger kök URL-də işləsin ("/swagger" yazmadan açmaq üçün)
});

//app.UseHttpsRedirection(); // HTTPS lazım olsa aktiv edin

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
