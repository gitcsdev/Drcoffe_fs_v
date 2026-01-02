using DrCoffee_BackEnd.Models;
using DrCoffee_BackEnd.DTOs;

namespace DrCoffee_BackEnd.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
    Task<ProductResponseDto?> GetProductByIdAsync(int id);
    Task<ProductResponseDto?> GetProductByCodeAsync(string productCode);
    Task<IEnumerable<ProductResponseDto>> GetProductsByCategoryAsync(int categoryId);
    Task<ProductResponseDto> CreateProductAsync(CreateProductDto createDto);
    Task<ProductResponseDto?> UpdateProductAsync(int id, UpdateProductDto updateDto);
    Task<bool> DeleteProductAsync(int id);
    Task<bool> ProductExistsAsync(int id);
    Task<bool> ProductCodeExistsAsync(string productCode, int? excludeProductId = null);
}



