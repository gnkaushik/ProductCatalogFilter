using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Xunit;

namespace ProductCatalog.Tests.Integration;

public class ProductApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ProductApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/api/products");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task SearchProducts_WithValidQuery_ReturnsMatchingProducts()
    {
        // Arrange
        await _client.PostAsJsonAsync("/api/products/generate", new { count = 10 });

        // Act
        var response = await _client.GetAsync("/api/products/search?query=Product");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}