using RateLimitMinimalApi.Core.Domain.Entities;
using RateLimitMinimalApi.Core.Domain.Interfaces.Repos;

namespace RateLimitMinimalApi.Infra.Repos;

public class InMemoryProductRepo : IProductRepo
{
    private readonly List<Product> _products;
    private int _nextId = 1;

    public InMemoryProductRepo()
    {
        _products = new List<Product>
        {
            new Product { Id = _nextId++, Name = "Laptop", Price = 999.99m, Category = "Electronics" },
            new Product { Id = _nextId++, Name = "Mouse", Price = 25.50m, Category = "Electronics" },
            new Product { Id = _nextId++, Name = "Keyboard", Price = 75.00m, Category = "Electronics" },
            new Product { Id = _nextId++, Name = "Monitor", Price = 299.99m, Category = "Electronics" }
        };
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_products.AsEnumerable());
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task<Product> CreateAsync(Product product)
    {
        product.Id = _nextId++;
        product.CreatedAt = DateTime.UtcNow;
        _products.Add(product);
        return Task.FromResult(product);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}
