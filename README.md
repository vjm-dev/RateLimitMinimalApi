# RateLimitMinimalApi

.NET Web API (.NET Core 8) project test to experiment and study how rate limiting middleware works.<br/>
Reference: https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-9.0

**Rate Limiting** is a fundamental security measure to protect web apps from excessive and malicious use.

#### Types of Rate Limiting Policies in .NET
The framework offers 4 main control algorithms:
Type | Description
--- | ---
**Fixed Window Limiter** | Allows a fixed number of requests within a time window. Once the time window expires, the counters are reset.
**Sliding Window Limiter** | Similar to the previous one, but the time window is divided into segments. As time passes, the oldest segments are discarded and new ones are opened, providing more granular and smooth traffic control.
**Token Bucket Limiter** | Based on a "token bucket." The bucket fills at a constant rate, and each request consumes one token. If the bucket is empty, the request is rejected. This allows for controlled traffic bursts.
**Concurrency Limiter** | Limits the number of simultaneous requests the server can handle. Additional requests are either queued or rejected. This method is not based on a time period, but on the instantaneous capacity of the system.

## API Endpoints
Endpoint | 	Method | 	Rate Limit Policy | 	Description
--- | --- | --- | ---
`/api/admin/stats` | 	GET | 	IP-based (8/30s) | 	Get admin statistics
`/api/products` | 	GET | 	Fixed (5/10s) | 	Get all products
`/api/products` | 	POST | 	Fixed (5/10s) | 	Create new product
`/api/products/{id}` | 	GET | 	Sliding (10/min) | 	Get product by ID
`/health` | 	GET | 	None | 	Health check endpoint
`/api/rate-limit-info` | 	GET | 	None | 	Get rate limit policies info
`/api/users` | 	GET | 	Token Bucket (20 tokens) | 	Get all users
`/api/users` | 	POST | 	Token Bucket (20 tokens) | 	Create new user account
`/api/users/{id}` | 	GET | 	Sliding (10/min) | 	Get user details by ID


## Project architecture overview
Commonly a **Hexagonal Architecture** (ports and adapters):

- **Core**: Domain entities, business rules and interfaces
- **Core/App**: Use cases, services, and DTOs  
- **Infra**: External concerns (repositories, external services, ...)
- **Api**: Presentation layer (endpoints, configs, ...)