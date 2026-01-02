using Microsoft.EntityFrameworkCore;
using DrCoffee_BackEnd.Data;
using DrCoffee_BackEnd.Models;
using DrCoffee_BackEnd.DTOs;

namespace DrCoffee_BackEnd.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .Select(c => new CategoryResponseDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                DisplayOrder = c.DisplayOrder,
                IsActive = c.IsActive,
                ProductCount = c.Products.Count(p => p.IsActive),
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            })
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.CategoryId == id);

        if (category == null) return null;

        return new CategoryResponseDto
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            DisplayOrder = category.DisplayOrder,
            IsActive = category.IsActive,
            ProductCount = category.Products.Count(p => p.IsActive),
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }

    public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto createDto)
    {
        var category = new Category
        {
            Name = createDto.Name,
            DisplayOrder = createDto.DisplayOrder,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return (await GetCategoryByIdAsync(category.CategoryId))!;
    }

    public async Task<CategoryResponseDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateDto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return null;

        if (!string.IsNullOrEmpty(updateDto.Name))
            category.Name = updateDto.Name;
        if (updateDto.DisplayOrder.HasValue)
            category.DisplayOrder = updateDto.DisplayOrder.Value;
        if (updateDto.IsActive.HasValue)
            category.IsActive = updateDto.IsActive.Value;

        category.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return await GetCategoryByIdAsync(id);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        // Soft delete
        category.IsActive = false;
        category.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CategoryExistsAsync(int id)
    {
        return await _context.Categories.AnyAsync(c => c.CategoryId == id);
    }

    public async Task<bool> CategoryNameExistsAsync(string name, int? excludeCategoryId = null)
    {
        if (excludeCategoryId.HasValue)
            return await _context.Categories.AnyAsync(c => c.Name == name && c.CategoryId != excludeCategoryId.Value);
        return await _context.Categories.AnyAsync(c => c.Name == name);
    }
}



