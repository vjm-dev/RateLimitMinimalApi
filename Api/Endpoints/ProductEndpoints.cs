using RateLimitMinimalApi.Core.App.DTOs;
using RateLimitMinimalApi.Core.App.Services;

namespace RateLimitMinimalApi.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var productGroup = app.MapGroup("/api/products");

        productGroup.MapGet("/", GetAllProducts)
            .RequireRateLimiting("FixedPolicy")
            .WithName("GetProducts")
            .WithOpenApi();

        productGroup.MapGet("/{id:int}", GetProductById)
            .RequireRateLimiting("SlidingPolicy")
            .WithName("GetProductById")
            .WithOpenApi();

        productGroup.MapPost("/", CreateProduct)
            .RequireRateLimiting("FixedPolicy")
            .WithName("CreateProduct")
            .WithOpenApi();
    }

    private static async Task<IResult> GetAllProducts(IProductService productService)
    {
        var result = await productService.GetAllProductsAsync();
        return Results.Ok(result);
    }

    private static async Task<IResult> GetProductById(int id, IProductService productService)
    {
        var result = await productService.GetProductByIdAsync(id);

        if (result.Data == null)
            return Results.NotFound(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> CreateProduct(
        ProductCreateRequest request,
        IProductService productService)
    {
        var result = await productService.CreateProductAsync(request);
        return Results.Created($"/api/products/{result.Data?.Id}", result);
    }
}
