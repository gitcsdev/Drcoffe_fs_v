using DrCoffee_BackEnd.DTOs;

namespace DrCoffee_BackEnd.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
    Task<CategoryResponseDto?> GetCategoryByIdAsync(int id);
    Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto createDto);
    Task<CategoryResponseDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateDto);
    Task<bool> DeleteCategoryAsync(int id);
    Task<bool> CategoryExistsAsync(int id);
    Task<bool> CategoryNameExistsAsync(string name, int? excludeCategoryId = null);
}



