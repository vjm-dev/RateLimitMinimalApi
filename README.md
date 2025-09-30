# RateLimitMinimalApi

.NET Web API (.NET Core 8) project test to experiment and study how rate limiting middleware works.<br/>
Reference: https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-9.0

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
`/api/users` | 	POST | 	TokenPolicy (20 tokens) | 	Create new user account
`/api/users/{id}` | 	GET | 	SlidingPolicy (10/min) | 	Get user details by ID


## Project architecture overview

Commonly a **Hexagonal Architecture** (ports and adapters):

- **Core**: Domain entities, business rules and interfaces
- **Core/App**: Use cases, services, and DTOs  
- **Infra**: External concerns (repositories, external services, ...)
- **Api**: Presentation layer (endpoints, configs, ...)