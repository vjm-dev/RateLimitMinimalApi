namespace RateLimitMinimalApi.Core.App.DTOs;

public record ProductCreateRequest(string Name, decimal Price, string Category);
public record ProductResponse(int Id, string Name, decimal Price, string Category, DateTime CreatedAt);
public record ApiResponse<T>(string Message, T? Data, int Count = 0);
