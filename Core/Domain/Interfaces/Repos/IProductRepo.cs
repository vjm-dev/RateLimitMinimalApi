using RateLimitMinimalApi.Core.Domain.Entities;

namespace RateLimitMinimalApi.Core.Domain.Interfaces.Repos;

public interface IProductRepo
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}
