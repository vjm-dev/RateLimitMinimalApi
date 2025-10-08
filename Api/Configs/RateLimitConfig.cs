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
            rateLimiterOptions.AddFixedWindowLimiter(Constants.FIXED_POLICY, options =>
            {
                options.Window = TimeSpan.FromSeconds(Constants.FIXED_WINDOW_SECONDS);
                options.PermitLimit = Constants.FIXED_PERMIT_LIMIT;
                options.QueueLimit = Constants.FIXED_QUEUE_LIMIT;
                options.QueueProcessingOrder = Constants.FIXED_QUEUE_ORDER;
            });

            // Sliding Window Policy
            rateLimiterOptions.AddSlidingWindowLimiter(Constants.SLIDING_POLICY, options =>
            {
                options.Window = TimeSpan.FromMinutes(Constants.SLIDING_WINDOW_SECONDS);
                options.SegmentsPerWindow = Constants.SLIDING_SEGMENTS_PER_WINDOW;
                options.PermitLimit = Constants.SLIDING_PERMIT_LIMIT;
                options.QueueLimit = Constants.SLIDING_QUEUE_LIMIT;
            });

            // Token Bucket Policy
            rateLimiterOptions.AddTokenBucketLimiter(Constants.TOKEN_POLICY, options =>
            {
                options.TokenLimit = Constants.TOKEN_LIMIT;
                options.TokensPerPeriod = Constants.TOKENS_PER_PERIOD;
                options.ReplenishmentPeriod = TimeSpan.FromSeconds(Constants.TOKEN_REPLENISHMENT_SECONDS);
                options.AutoReplenishment = Constants.TOKEN_AUTO_REPLENISHMENT;
                options.QueueLimit = Constants.TOKEN_QUEUE_LIMIT;
            });

            // IP-based Rate Limiting
            rateLimiterOptions.AddPolicy<string>(Constants.IP_BASED_POLICY, context =>
            {
                var remoteIp = context.Connection.RemoteIpAddress?.ToString();
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: remoteIp ?? "unknown",
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        Window = TimeSpan.FromSeconds(Constants.IP_WINDOW_SECONDS),
                        PermitLimit = Constants.IP_PERMIT_LIMIT,
                        QueueLimit = Constants.IP_QUEUE_LIMIT
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
