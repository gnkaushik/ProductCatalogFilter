using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Interfaces;
using ProductCatalog.Infrastructure.Data;
using ProductCatalog.Infrastructure.Repositories;
using ProductCatalog.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

// Add DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=products.db"));

// Add Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<DatabaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
}

app.UseHttpsRedirection();

// API Endpoints
app.MapGet("/api/products", async (
    IProductRepository repo,
    int page = 1,
    int pageSize = 50,
    string? sortBy = null,
    string? sortDirection = null) =>
{
    var products = await repo.GetProductsAsync(page, pageSize, sortBy, sortDirection);
    var total = await repo.GetTotalCountAsync();
    
    return Results.Ok(new { 
        data = products, 
        total,
        page,
        pageSize
    });
})
.WithName("GetProducts")
.WithOpenApi();

app.MapGet("/api/products/search", async (
    IProductRepository repo,
    string query,
    int page = 1,
    int pageSize = 50,
    string? sortBy = null,
    string? sortDirection = null) =>
{
    var products = await repo.SearchProductsAsync(query, page, pageSize, sortBy, sortDirection);
    return Results.Ok(products);
})
.WithName("SearchProducts")
.WithOpenApi();

app.MapGet("/api/products/{id}", async (IProductRepository repo, int id) =>
{
    var product = await repo.GetByIdAsync(id);
    return product is null ? Results.NotFound() : Results.Ok(product);
})
.WithName("GetProductById")
.WithOpenApi();

app.MapPost("/api/products/generate", async (IProductRepository repo, int count = 1000) =>
{
    var generatedCount = await repo.GenerateProductsAsync(count);
    return Results.Ok(new { count = generatedCount });
})
.WithName("GenerateProducts")
.WithOpenApi();

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var dbService = scope.ServiceProvider.GetRequiredService<DatabaseService>();
    await dbService.EnsureDatabaseCreatedAsync();
}

app.Run();
public partial class Program { }