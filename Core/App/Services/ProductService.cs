using RateLimitMinimalApi.Core.App.DTOs;
using RateLimitMinimalApi.Core.Domain.Entities;
using RateLimitMinimalApi.Core.Domain.Interfaces.Repos;

namespace RateLimitMinimalApi.Core.App.Services;

public interface IProductService
{
    Task<ApiResponse<IEnumerable<ProductResponse>>> GetAllProductsAsync();
    Task<ApiResponse<ProductResponse?>> GetProductByIdAsync(int id);
    Task<ApiResponse<ProductResponse>> CreateProductAsync(ProductCreateRequest request);
}

public class ProductService : IProductService
{
    private readonly IProductRepo _productRepository;

    public ProductService(IProductRepo productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ApiResponse<IEnumerable<ProductResponse>>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var productResponses = products.Select(p => new ProductResponse(
            p.Id, p.Name, p.Price, p.Category, p.CreatedAt
        ));

        return new ApiResponse<IEnumerable<ProductResponse>>(
            "Products retrieved successfully",
            productResponses,
            productResponses.Count()
        );
    }

    public async Task<ApiResponse<ProductResponse?>> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return new ApiResponse<ProductResponse?>("Product not found", null);
        }

        var productResponse = new ProductResponse(
            product.Id, product.Name, product.Price, product.Category, product.CreatedAt
        );

        return new ApiResponse<ProductResponse?>("Product found", productResponse);
    }

    public async Task<ApiResponse<ProductResponse>> CreateProductAsync(ProductCreateRequest request)
    {
        var product = new Product(request.Name, request.Price, request.Category);
        var createdProduct = await _productRepository.CreateAsync(product);

        var productResponse = new ProductResponse(
            createdProduct.Id, createdProduct.Name, createdProduct.Price,
            createdProduct.Category, createdProduct.CreatedAt
        );

        return new ApiResponse<ProductResponse>("Product created successfully", productResponse);
    }
}
