using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DrCoffee_BackEnd.DTOs;
using DrCoffee_BackEnd.Repositories;

namespace DrCoffee_BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous] // Public controller for menu display
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Get all active products (public endpoint for menu display)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        // Only return active products for public menu
        var activeProducts = products.Where(p => p.IsActive).ToList();
        return Ok(activeProducts);
    }

    /// <summary>
    /// Get product by code (public endpoint)
    /// </summary>
    [HttpGet("by-code/{productCode}")]
    public async Task<ActionResult<ProductResponseDto>> GetProductByCode(string productCode)
    {
        var products = await _productRepository.GetAllProductsAsync();
        var product = products.FirstOrDefault(p => p.ProductCode == productCode && p.IsActive);
        
        if (product == null)
            return NotFound(new { message = "Product not found" });

        return Ok(product);
    }
}

