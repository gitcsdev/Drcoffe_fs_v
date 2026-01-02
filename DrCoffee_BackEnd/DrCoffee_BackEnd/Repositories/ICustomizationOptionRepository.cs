using DrCoffee_BackEnd.DTOs;
using DrCoffee_BackEnd.Models;

namespace DrCoffee_BackEnd.Repositories;

public interface ICustomizationOptionRepository
{
    Task<IEnumerable<CustomizationOptionResponseDto>> GetAllCustomizationOptionsAsync();
    Task<CustomizationOptionResponseDto?> GetCustomizationOptionByIdAsync(int id);
    Task<CustomizationOptionResponseDto?> GetCustomizationOptionByCodeAsync(string optionCode);
    Task<bool> OptionCodeExistsAsync(string optionCode, int? excludeId = null);
    Task<CustomizationOptionResponseDto> CreateCustomizationOptionAsync(CreateCustomizationOptionDto createDto);
    Task<CustomizationOptionResponseDto?> UpdateCustomizationOptionAsync(int id, UpdateCustomizationOptionDto updateDto);
    Task<bool> DeleteCustomizationOptionAsync(int id);
}

