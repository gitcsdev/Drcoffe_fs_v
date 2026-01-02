using Microsoft.EntityFrameworkCore;
using DrCoffee_BackEnd.Data;
using DrCoffee_BackEnd.DTOs;
using DrCoffee_BackEnd.Models;

namespace DrCoffee_BackEnd.Repositories;

public class CustomizationOptionRepository : ICustomizationOptionRepository
{
    private readonly ApplicationDbContext _context;

    public CustomizationOptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CustomizationOptionResponseDto>> GetAllCustomizationOptionsAsync()
    {
        return await _context.CustomizationOptions
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.NameEn)
            .Select(c => new CustomizationOptionResponseDto
            {
                CustomizationOptionId = c.CustomizationOptionId,
                OptionCode = c.OptionCode,
                NameEn = c.NameEn,
                NameAr = c.NameAr,
                Price = c.Price,
                IsActive = c.IsActive,
                DisplayOrder = c.DisplayOrder,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<CustomizationOptionResponseDto?> GetCustomizationOptionByIdAsync(int id)
    {
        var option = await _context.CustomizationOptions.FindAsync(id);
        if (option == null) return null;

        return new CustomizationOptionResponseDto
        {
            CustomizationOptionId = option.CustomizationOptionId,
            OptionCode = option.OptionCode,
            NameEn = option.NameEn,
            NameAr = option.NameAr,
            Price = option.Price,
            IsActive = option.IsActive,
            DisplayOrder = option.DisplayOrder,
            CreatedAt = option.CreatedAt,
            UpdatedAt = option.UpdatedAt
        };
    }

    public async Task<CustomizationOptionResponseDto?> GetCustomizationOptionByCodeAsync(string optionCode)
    {
        var option = await _context.CustomizationOptions
            .FirstOrDefaultAsync(c => c.OptionCode == optionCode);

        if (option == null) return null;

        return new CustomizationOptionResponseDto
        {
            CustomizationOptionId = option.CustomizationOptionId,
            OptionCode = option.OptionCode,
            NameEn = option.NameEn,
            NameAr = option.NameAr,
            Price = option.Price,
            IsActive = option.IsActive,
            DisplayOrder = option.DisplayOrder,
            CreatedAt = option.CreatedAt,
            UpdatedAt = option.UpdatedAt
        };
    }

    public async Task<bool> OptionCodeExistsAsync(string optionCode, int? excludeId = null)
    {
        var query = _context.CustomizationOptions.Where(c => c.OptionCode == optionCode);
        if (excludeId.HasValue)
        {
            query = query.Where(c => c.CustomizationOptionId != excludeId.Value);
        }
        return await query.AnyAsync();
    }

    public async Task<CustomizationOptionResponseDto> CreateCustomizationOptionAsync(CreateCustomizationOptionDto createDto)
    {
        var option = new CustomizationOption
        {
            OptionCode = createDto.OptionCode,
            NameEn = createDto.NameEn,
            NameAr = createDto.NameAr,
            Price = createDto.Price,
            IsActive = createDto.IsActive,
            DisplayOrder = createDto.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.CustomizationOptions.Add(option);
        await _context.SaveChangesAsync();

        return new CustomizationOptionResponseDto
        {
            CustomizationOptionId = option.CustomizationOptionId,
            OptionCode = option.OptionCode,
            NameEn = option.NameEn,
            NameAr = option.NameAr,
            Price = option.Price,
            IsActive = option.IsActive,
            DisplayOrder = option.DisplayOrder,
            CreatedAt = option.CreatedAt,
            UpdatedAt = option.UpdatedAt
        };
    }

    public async Task<CustomizationOptionResponseDto?> UpdateCustomizationOptionAsync(int id, UpdateCustomizationOptionDto updateDto)
    {
        var option = await _context.CustomizationOptions.FindAsync(id);
        if (option == null) return null;

        if (!string.IsNullOrEmpty(updateDto.OptionCode))
            option.OptionCode = updateDto.OptionCode;
        if (!string.IsNullOrEmpty(updateDto.NameEn))
            option.NameEn = updateDto.NameEn;
        if (!string.IsNullOrEmpty(updateDto.NameAr))
            option.NameAr = updateDto.NameAr;
        if (updateDto.Price.HasValue)
            option.Price = updateDto.Price.Value;
        if (updateDto.IsActive.HasValue)
            option.IsActive = updateDto.IsActive.Value;
        if (updateDto.DisplayOrder.HasValue)
            option.DisplayOrder = updateDto.DisplayOrder.Value;

        option.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new CustomizationOptionResponseDto
        {
            CustomizationOptionId = option.CustomizationOptionId,
            OptionCode = option.OptionCode,
            NameEn = option.NameEn,
            NameAr = option.NameAr,
            Price = option.Price,
            IsActive = option.IsActive,
            DisplayOrder = option.DisplayOrder,
            CreatedAt = option.CreatedAt,
            UpdatedAt = option.UpdatedAt
        };
    }

    public async Task<bool> DeleteCustomizationOptionAsync(int id)
    {
        var option = await _context.CustomizationOptions.FindAsync(id);
        if (option == null) return false;

        _context.CustomizationOptions.Remove(option);
        await _context.SaveChangesAsync();
        return true;
    }
}

