using Microsoft.EntityFrameworkCore;
using TravelPlanner.Infrastructure.Data;
using TravelPlanner.Application.Interfaces;
using TravelPlanner.Application.Services;
using TravelPlanner.Application.Interfaces;
using TravelPlanner.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Add MVC controllers
builder.Services.AddControllers();

// CORS - development convenience (allow browser requests from local UI)
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Enable CORS (apply before mapping controllers)
app.UseCors("AllowLocal");

// Map controller routes
app.MapControllers();

app.UseHttpsRedirection();


app.Run();


