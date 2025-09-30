namespace RateLimitMinimalApi.Api.Endpoints;

public static class SystemEndpoints
{
    public static void MapSystemEndpoints(this WebApplication app)
    {
        app.MapGet("/health", () =>
        {
            return Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
        })
        .WithName("HealthCheck")
        .WithOpenApi();

        app.MapGet("/api/rate-limit-info", () =>
        {
            var policies = new[]
            {
                new {
                    Name = "FixedPolicy",
                    Description = "Fixed Window - 5 requests per 10 seconds",
                    Window = "10 seconds",
                    Limit = 5
                },
                new {
                    Name = "SlidingPolicy",
                    Description = "Sliding Window - 10 requests per minute",
                    Window = "1 minute",
                    Limit = 10
                },
                new {
                    Name = "TokenPolicy",
                    Description = "Token Bucket - 20 tokens, 5 tokens/15 seconds",
                    Window = "15 seconds",
                    Limit = 20
                },
                new {
                    Name = "IPBasedPolicy",
                    Description = "IP Based - 8 requests per 30 seconds per IP",
                    Window = "30 seconds",
                    Limit = 8
                }
            };

            return Results.Ok(new { rateLimitPolicies = policies });
        })
        .WithName("GetRateLimitInfo")
        .WithOpenApi();
    }
}
