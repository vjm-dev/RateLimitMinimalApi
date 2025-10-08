using RateLimitMinimalApi.Api.Configs;

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
                    Name = Constants.FIXED_POLICY,
                    Type = Constants.POLICY_TYPE_FIXED_WINDOW,
                    Description = Constants.FIXED_DESCRIPTION,
                    Window = Constants.FIXED_WINDOW_DISPLAY,
                    Limit = Constants.FIXED_PERMIT_LIMIT,
                    QueueLimit = Constants.FIXED_QUEUE_LIMIT
                },
                new {
                    Name = Constants.SLIDING_POLICY,
                    Type = Constants.POLICY_TYPE_SLIDING_WINDOW,
                    Description = Constants.SLIDING_DESCRIPTION,
                    Window = Constants.SLIDING_WINDOW_DISPLAY,
                    Limit = Constants.SLIDING_PERMIT_LIMIT,
                    QueueLimit = Constants.SLIDING_QUEUE_LIMIT
                },
                new {
                    Name = Constants.TOKEN_POLICY,
                    Type = Constants.POLICY_TYPE_TOKEN_BUCKET,
                    Description = Constants.TOKEN_DESCRIPTION,
                    Window = Constants.TOKEN_WINDOW_DISPLAY,
                    Limit = Constants.TOKEN_LIMIT,
                    QueueLimit = Constants.TOKEN_QUEUE_LIMIT
                },
                new {
                    Name = Constants.IP_BASED_POLICY,
                    Type = Constants.POLICY_TYPE_IP_BASED,
                    Description = Constants.IP_DESCRIPTION,
                    Window = Constants.IP_WINDOW_DISPLAY,
                    Limit = Constants.IP_PERMIT_LIMIT,
                    QueueLimit = Constants.IP_QUEUE_LIMIT
                },
                new {
                    Name = Constants.CONCURRENCY_POLICY,
                    Type = Constants.POLICY_TYPE_CONCURRENCY,
                    Description = Constants.CONCURRENCY_DESCRIPTION,
                    Window = Constants.CONCURRENCY_WINDOW_DISPLAY,
                    Limit = Constants.CONCURRENCY_PERMIT_LIMIT,
                    QueueLimit = Constants.CONCURRENCY_QUEUE_LIMIT
                }
            };

            return Results.Ok(new { 
                rateLimitPolicies = policies,
                globalSettings = new {
                    retryAfterSeconds = Constants.GLOBAL_RETRY_AFTER_SECONDS,
                    contentType = Constants.GLOBAL_RESPONSE_CONTENT_TYPE,
                    errorType = Constants.GLOBAL_ERROR_TYPE
                }
            });
        })
        .WithName("GetRateLimitInfo")
        .WithOpenApi();
    }
}
