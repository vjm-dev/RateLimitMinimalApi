using System.Threading.RateLimiting;

namespace RateLimitMinimalApi.Api.Configs;

public static class Constants
{
    // Policy names
    public const string FIXED_POLICY = "FixedPolicy";
    public const string SLIDING_POLICY = "SlidingPolicy";
    public const string TOKEN_POLICY = "TokenPolicy";
    public const string IP_BASED_POLICY = "IPBasedPolicy";
    public const string CONCURRENCY_POLICY = "ConcurrencyPolicy";

    // Policy types
    public const string POLICY_TYPE_FIXED_WINDOW = "Fixed Window";
    public const string POLICY_TYPE_SLIDING_WINDOW = "Sliding Window";
    public const string POLICY_TYPE_TOKEN_BUCKET = "Token Bucket";
    public const string POLICY_TYPE_IP_BASED = "IP Based Fixed Window";
    public const string POLICY_TYPE_CONCURRENCY = "Concurrency Limiter";

    // Fixed Policy
    public const int FIXED_WINDOW_SECONDS = 10;
    public const int FIXED_PERMIT_LIMIT = 5;
    public const int FIXED_QUEUE_LIMIT = 2;
    public const QueueProcessingOrder FIXED_QUEUE_ORDER = QueueProcessingOrder.OldestFirst;
    public const string FIXED_DESCRIPTION = "5 requests per 10 seconds";
    public const string FIXED_WINDOW_DISPLAY = "10 seconds";

    // Sliding Policy
    public const int SLIDING_WINDOW_SECONDS = 60;
    public const int SLIDING_SEGMENTS_PER_WINDOW = 2;
    public const int SLIDING_PERMIT_LIMIT = 10;
    public const int SLIDING_QUEUE_LIMIT = 3;
    public const string SLIDING_DESCRIPTION = "10 requests per minute with 2 segments";
    public const string SLIDING_WINDOW_DISPLAY = "1 minute";

    // Token Policy
    public const int TOKEN_LIMIT = 20;
    public const int TOKENS_PER_PERIOD = 5;
    public const int TOKEN_REPLENISHMENT_SECONDS = 15;
    public const bool TOKEN_AUTO_REPLENISHMENT = true;
    public const int TOKEN_QUEUE_LIMIT = 5;
    public const string TOKEN_DESCRIPTION = "20 tokens, 5 tokens replenished every 15 seconds";
    public const string TOKEN_WINDOW_DISPLAY = "15 seconds";

    // IP Based Policy
    public const int IP_WINDOW_SECONDS = 30;
    public const int IP_PERMIT_LIMIT = 8;
    public const int IP_QUEUE_LIMIT = 2;
    public const string IP_DESCRIPTION = "8 requests per 30 seconds per IP address";
    public const string IP_WINDOW_DISPLAY = "30 seconds";

    // Concurrency Policy
    public const int CONCURRENCY_PERMIT_LIMIT = 10;
    public const int CONCURRENCY_QUEUE_LIMIT = 5;
    public const string CONCURRENCY_DESCRIPTION = "10 concurrent requests maximum";
    public const string CONCURRENCY_WINDOW_DISPLAY = "N/A";

    // Global settings
    public const int GLOBAL_RETRY_AFTER_SECONDS = 60;
    public const string GLOBAL_RESPONSE_CONTENT_TYPE = "application/json";
    public const string GLOBAL_ERROR_MESSAGE = "Too many requests. Please try again later.";
    public const string GLOBAL_ERROR_TYPE = "Rate limit exceeded";
}