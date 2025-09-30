using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace RateLimitMinimalApi.Api.Configs;

public static class RateLimitConfig
{
    public static IServiceCollection ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimiterOptions =>
        {
            // Fixed Window Policy
            rateLimiterOptions.AddFixedWindowLimiter("FixedPolicy", options =>
            {
                options.Window = TimeSpan.FromSeconds(10);
                options.PermitLimit = 5;
                options.QueueLimit = 2;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });

            // Sliding Window Policy
            rateLimiterOptions.AddSlidingWindowLimiter("SlidingPolicy", options =>
            {
                options.Window = TimeSpan.FromMinutes(1);
                options.SegmentsPerWindow = 2;
                options.PermitLimit = 10;
                options.QueueLimit = 3;
            });

            // Token Bucket Policy
            rateLimiterOptions.AddTokenBucketLimiter("TokenPolicy", options =>
            {
                options.TokenLimit = 20;
                options.TokensPerPeriod = 5;
                options.ReplenishmentPeriod = TimeSpan.FromSeconds(15);
                options.AutoReplenishment = true;
                options.QueueLimit = 5;
            });

            // IP-based Rate Limiting
            rateLimiterOptions.AddPolicy<string>("IPBasedPolicy", context =>
            {
                var remoteIp = context.Connection.RemoteIpAddress?.ToString();
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: remoteIp ?? "unknown",
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        Window = TimeSpan.FromSeconds(30),
                        PermitLimit = 8,
                        QueueLimit = 2
                    });
            });

            // Custom response when rate limit is exceeded
            rateLimiterOptions.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.Headers.RetryAfter = "60";

                await context.HttpContext.Response.WriteAsync(
                    "Rate limit exceeded. Too many requests. Please try again later.",
                    cancellationToken: token);
            };
        });

        return services;
    }
}
