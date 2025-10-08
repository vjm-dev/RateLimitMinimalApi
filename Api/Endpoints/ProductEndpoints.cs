using RateLimitMinimalApi.Core.App.DTOs;
using RateLimitMinimalApi.Core.App.Services;
using RateLimitMinimalApi.Api.Configs;

namespace RateLimitMinimalApi.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var productGroup = app.MapGroup("/api/products");

        productGroup.MapGet("/", GetAllProducts)
            .RequireRateLimiting(Constants.FIXED_POLICY)
            .WithName("GetProducts")
            .WithOpenApi();

        productGroup.MapGet("/{id:int}", GetProductById)
            .RequireRateLimiting(Constants.SLIDING_POLICY)
            .WithName("GetProductById")
            .WithOpenApi();

        productGroup.MapPost("/", CreateProduct)
            .RequireRateLimiting(Constants.FIXED_POLICY)
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
