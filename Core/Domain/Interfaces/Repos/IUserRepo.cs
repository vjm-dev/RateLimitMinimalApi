using RateLimitMinimalApi.Core.Domain.Entities;

namespace RateLimitMinimalApi.Core.Domain.Interfaces.Repos;

public interface IUserRepo
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User user);
}
