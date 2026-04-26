using Microsoft.EntityFrameworkCore;
using TravelPlanner.Infrastructure.Data;
using TravelPlanner.Application.Interfaces;
using TravelPlanner.Application.Services;
using Microsoft.Extensions.FileProviders; // Yeni eklendi
using System.IO; // Yeni eklendi

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

// CORS - development convenience
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
// Register route service with typed HttpClient injection
builder.Services.AddHttpClient<IRouteService, RouteService>();

// Register HttpClient factory
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// CORS politikasını uygula
app.UseCors("AllowLocal");

// --- DIŞARIDAKİ UI KLASÖRÜNÜ TANITMA AYARLARI ---

// TravelPlanner.UI klasörünün tam yolunu belirliyoruz
var uiPath = Path.Combine(builder.Environment.ContentRootPath, "..", "TravelPlanner.UI");

// Varsayılan dosyaları (index.html gibi) dış klasörden okuması için
app.UseDefaultFiles(new DefaultFilesOptions
{
    FileProvider = new PhysicalFileProvider(uiPath),
    RequestPath = ""
});

// Statik dosyaları (CSS, JS, Resim) dış klasörden sunması için
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uiPath),
    RequestPath = ""
});

// ------------------------------------------------

app.UseHttpsRedirection();

// Controller rotalarını eşle
app.MapControllers();

// launchSettings.json'daki portları dinlemesi için parametresiz çalıştırıyoruz
app.Run();