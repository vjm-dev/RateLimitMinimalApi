using RateLimitMinimalApi.Core.Domain.Entities;
using RateLimitMinimalApi.Core.Domain.Interfaces.Repos;

namespace RateLimitMinimalApi.Infra.Repos;

public class InMemoryUserRepo : IUserRepo
{
    private readonly List<User> _users;
    private int _nextId = 1;

    public InMemoryUserRepo()
    {
        _users = new List<User>
        {
            new User { Id = _nextId++, Username = "john_doe", Email = "john@example.com" },
            new User { Id = _nextId++, Username = "jane_smith", Email = "jane@example.com" }
        };
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult(_users.AsEnumerable());
    }

    public Task<User?> GetByIdAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

    public Task<User> CreateAsync(User user)
    {
        user.Id = _nextId++;
        user.CreatedAt = DateTime.UtcNow;
        _users.Add(user);
        return Task.FromResult(user);
    }
}
