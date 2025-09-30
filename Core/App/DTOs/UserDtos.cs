namespace RateLimitMinimalApi.Core.App.DTOs;

public record UserCreateRequest(string Username, string Email);
public record UserResponse(int Id, string Username, string Email, DateTime CreatedAt);
