namespace RateLimitMinimalApi.Core.App.Interfaces;

public interface IRateLimitService
{
    Task<bool> IsAllowedAsync(string endpoint, string identifier);
    Task<RateLimitInfo> GetRateLimitInfoAsync(string identifier);
}

public record RateLimitInfo(
    string Endpoint,
    int CurrentCount,
    int Limit,
    TimeSpan Window,
    DateTime ResetTime
);