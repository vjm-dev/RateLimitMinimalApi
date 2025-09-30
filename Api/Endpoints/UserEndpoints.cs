using RateLimitMinimalApi.Core.App.DTOs;
using RateLimitMinimalApi.Core.App.Services;

namespace RateLimitMinimalApi.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/users");

        userGroup.MapGet("/", GetAllUsers)
            .RequireRateLimiting("TokenPolicy")
            .WithName("GetUsers")
            .WithOpenApi();

        userGroup.MapGet("/{id:int}", GetUserById)
            .RequireRateLimiting("TokenPolicy")
            .WithName("GetUserById")
            .WithOpenApi();

        userGroup.MapPost("/", CreateUser)
            .RequireRateLimiting("TokenPolicy")
            .WithName("CreateUser")
            .WithOpenApi();
    }

    private static async Task<IResult> GetAllUsers(IUserService userService)
    {
        var result = await userService.GetAllUsersAsync();
        return Results.Ok(result);
    }

    private static async Task<IResult> GetUserById(int id, IUserService userService)
    {
        var result = await userService.GetUserByIdAsync(id);

        if (result.Data == null)
            return Results.NotFound(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> CreateUser(
        UserCreateRequest request,
        IUserService userService)
    {
        var result = await userService.CreateUserAsync(request);
        return Results.Created($"/api/users/{result.Data?.Id}", result);
    }
}