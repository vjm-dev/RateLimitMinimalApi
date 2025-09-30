using RateLimitMinimalApi.Core.Domain.Interfaces.Repos;

namespace RateLimitMinimalApi.Api.Endpoints;

public static class AdminEndpoints
{
    public static void MapAdminEndpoints(this WebApplication app)
    {
        var adminGroup = app.MapGroup("/api/admin");

        adminGroup.MapGet("/stats", GetAdminStats)
            .RequireRateLimiting("IPBasedPolicy")
            .WithName("GetAdminStats")
            .WithOpenApi();
    }

    private static async Task<IResult> GetAdminStats(
        IProductRepo productRepo,
        IUserRepo userRepo)
    {
        var products = await productRepo.GetAllAsync();
        var users = await userRepo.GetAllAsync();

        var stats = new
        {
            totalProducts = products.Count(),
            totalUsers = users.Count(),
            serverTime = DateTime.UtcNow,
            memoryUsage = GC.GetTotalMemory(false) / 1024 / 1024
        };

        return Results.Ok(new { message = "Admin stats", data = stats });
    }
}
