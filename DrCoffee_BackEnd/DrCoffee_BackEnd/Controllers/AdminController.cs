using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DrCoffee_BackEnd.DTOs;
using DrCoffee_BackEnd.Repositories;

namespace DrCoffee_BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Manager")]
public class AdminController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOrderRepository _orderRepository;

    public AdminController(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _orderRepository = orderRepository;
    }

    #region Product Management

    /// <summary>
    /// Get all products (public endpoint for menu display)
    /// </summary>
    [HttpGet("products")]
    [AllowAnonymous] // Allow public access for menu display
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        // Only return active products for public menu
        var activeProducts = products.Where(p => p.IsActive).ToList();
        return Ok(activeProducts);
    }

    /// <summary>
    /// Get product by ID
    /// </summary>
    [HttpGet("products/{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });

        return Ok(product);
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    [HttpPost("products")]
    public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] CreateProductDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if product code already exists
        if (await _productRepository.ProductCodeExistsAsync(createDto.ProductCode))
            return Conflict(new { message = "Product code already exists" });

        // Verify category exists
        var categoryExists = await _categoryRepository.CategoryExistsAsync(createDto.CategoryId);
        if (!categoryExists)
            return BadRequest(new { message = "Category not found" });

        var product = await _productRepository.CreateProductAsync(createDto);
        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
    }

    /// <summary>
    /// Update a product (including prices and availability)
    /// </summary>
    [HttpPut("products/{id}")]
    public async Task<ActionResult<ProductResponseDto>> UpdateProduct(int id, [FromBody] UpdateProductDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var productExists = await _productRepository.ProductExistsAsync(id);
        if (!productExists)
            return NotFound(new { message = "Product not found" });

        // If category is being updated, verify it exists
        if (updateDto.CategoryId.HasValue)
        {
            var categoryExists = await _categoryRepository.CategoryExistsAsync(updateDto.CategoryId.Value);
            if (!categoryExists)
                return BadRequest(new { message = "Category not found" });
        }

        var product = await _productRepository.UpdateProductAsync(id, updateDto);
        if (product == null)
            return NotFound(new { message = "Product not found" });

        return Ok(product);
    }

    /// <summary>
    /// Delete a product (soft delete)
    /// </summary>
    [HttpDelete("products/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productRepository.DeleteProductAsync(id);
        if (!result)
            return NotFound(new { message = "Product not found" });

        return NoContent();
    }

    #endregion

    #region Category Management

    /// <summary>
    /// Get all categories
    /// </summary>
    [HttpGet("categories")]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Get category by ID
    /// </summary>
    [HttpGet("categories/{id}")]
    public async Task<ActionResult<CategoryResponseDto>> GetCategory(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category == null)
            return NotFound(new { message = "Category not found" });

        return Ok(category);
    }

    /// <summary>
    /// Create a new category
    /// </summary>
    [HttpPost("categories")]
    public async Task<ActionResult<CategoryResponseDto>> CreateCategory([FromBody] CreateCategoryDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if category name already exists
        if (await _categoryRepository.CategoryNameExistsAsync(createDto.Name))
            return Conflict(new { message = "Category name already exists" });

        var category = await _categoryRepository.CreateCategoryAsync(createDto);
        return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
    }

    /// <summary>
    /// Update a category
    /// </summary>
    [HttpPut("categories/{id}")]
    public async Task<ActionResult<CategoryResponseDto>> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // If name is being updated, check if it already exists
        if (!string.IsNullOrEmpty(updateDto.Name))
        {
            if (await _categoryRepository.CategoryNameExistsAsync(updateDto.Name, id))
                return Conflict(new { message = "Category name already exists" });
        }

        var category = await _categoryRepository.UpdateCategoryAsync(id, updateDto);
        if (category == null)
            return NotFound(new { message = "Category not found" });

        return Ok(category);
    }

    /// <summary>
    /// Delete a category (soft delete)
    /// </summary>
    [HttpDelete("categories/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryRepository.DeleteCategoryAsync(id);
        if (!result)
            return NotFound(new { message = "Category not found" });

        return NoContent();
    }

    #endregion

    #region Order Management

    /// <summary>
    /// Get all orders
    /// </summary>
    [HttpGet("orders")]
    public async Task<ActionResult<IEnumerable<OrderSummaryDto>>> GetOrders()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();
        return Ok(orders);
    }

    /// <summary>
    /// Get order by ID
    /// </summary>
    [HttpGet("orders/{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrder(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        if (order == null)
            return NotFound(new { message = "Order not found" });

        return Ok(order);
    }

    /// <summary>
    /// Update order status (from Pending to Done, etc.)
    /// </summary>
    [HttpPut("orders/{id}/status")]
    public async Task<ActionResult<OrderResponseDto>> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var order = await _orderRepository.UpdateOrderStatusAsync(id, updateDto);
        if (order == null)
            return NotFound(new { message = "Order not found or invalid status" });

        return Ok(order);
    }

    /// <summary>
    /// Get orders by status
    /// </summary>
    [HttpGet("orders/status/{status}")]
    public async Task<ActionResult<IEnumerable<OrderSummaryDto>>> GetOrdersByStatus(string status)
    {
        var orders = await _orderRepository.GetOrdersByStatusAsync(status);
        return Ok(orders);
    }

    #endregion
}



