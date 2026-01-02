using Microsoft.EntityFrameworkCore;
using DrCoffee_BackEnd.Data;
using DrCoffee_BackEnd.Models;
using DrCoffee_BackEnd.DTOs;

namespace DrCoffee_BackEnd.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductPrices.Where(pp => pp.IsActive))
            .Include(p => p.ProductTags)
            .Include(p => p.ProductFlavors)
            .Where(p => p.IsActive)
            .ToListAsync();

        var productIds = products.Select(p => p.ProductId).ToList();
        var customizationOptions = await _context.ProductCustomizationOptions
            .Where(pco => productIds.Contains(pco.ProductId) && pco.IsActive)
            .ToListAsync();

        var customizationOptionsByProduct = customizationOptions
            .GroupBy(pco => pco.ProductId)
            .ToDictionary(g => g.Key, g => g.Select(pco => pco.CustomizationOptionId).ToList());

        return products.Select(p => new ProductResponseDto
        {
            ProductId = p.ProductId,
            ProductCode = p.ProductCode,
            NameEn = p.NameEn,
            NameAr = p.NameAr,
            ImageUrl = p.ImageUrl,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.Name,
            CaffeineIndex = p.CaffeineIndex,
            IsCustomizable = p.IsCustomizable,
            IsActive = p.IsActive,
            Prices = p.ProductPrices.Where(pp => pp.IsActive).Select(pp => new ProductPriceResponseDto
            {
                ProductPriceId = pp.ProductPriceId,
                Size = pp.Size,
                Price = pp.Price,
                IsActive = pp.IsActive
            }).ToList(),
            Tags = p.ProductTags.Select(pt => pt.Tag).ToList(),
            Flavors = p.ProductFlavors.Select(pf => pf.FlavorName).ToList(),
            CustomizationOptionIds = customizationOptionsByProduct.GetValueOrDefault(p.ProductId, new List<int>()),
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        }).ToList();
    }

    public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductPrices.Where(pp => pp.IsActive))
            .Include(p => p.ProductTags)
            .Include(p => p.ProductFlavors)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null) return null;

        return new ProductResponseDto
        {
            ProductId = product.ProductId,
            ProductCode = product.ProductCode,
            NameEn = product.NameEn,
            NameAr = product.NameAr,
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            CaffeineIndex = product.CaffeineIndex,
            IsCustomizable = product.IsCustomizable,
            IsActive = product.IsActive,
            Prices = product.ProductPrices.Where(pp => pp.IsActive).Select(pp => new ProductPriceResponseDto
            {
                ProductPriceId = pp.ProductPriceId,
                Size = pp.Size,
                Price = pp.Price,
                IsActive = pp.IsActive
            }).ToList(),
            Tags = product.ProductTags.Select(pt => pt.Tag).ToList(),
            Flavors = product.ProductFlavors.Select(pf => pf.FlavorName).ToList(),
            CustomizationOptionIds = await _context.ProductCustomizationOptions
                .Where(pco => pco.ProductId == product.ProductId && pco.IsActive)
                .Select(pco => pco.CustomizationOptionId)
                .ToListAsync(),
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public async Task<ProductResponseDto?> GetProductByCodeAsync(string productCode)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductPrices.Where(pp => pp.IsActive))
            .Include(p => p.ProductTags)
            .Include(p => p.ProductFlavors)
            .FirstOrDefaultAsync(p => p.ProductCode == productCode);

        if (product == null) return null;

        var customizationOptionIds = await _context.ProductCustomizationOptions
            .Where(pco => pco.ProductId == product.ProductId && pco.IsActive)
            .Select(pco => pco.CustomizationOptionId)
            .ToListAsync();

        return new ProductResponseDto
        {
            ProductId = product.ProductId,
            ProductCode = product.ProductCode,
            NameEn = product.NameEn,
            NameAr = product.NameAr,
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            CaffeineIndex = product.CaffeineIndex,
            IsCustomizable = product.IsCustomizable,
            IsActive = product.IsActive,
            Prices = product.ProductPrices.Where(pp => pp.IsActive).Select(pp => new ProductPriceResponseDto
            {
                ProductPriceId = pp.ProductPriceId,
                Size = pp.Size,
                Price = pp.Price,
                IsActive = pp.IsActive
            }).ToList(),
            Tags = product.ProductTags.Select(pt => pt.Tag).ToList(),
            Flavors = product.ProductFlavors.Select(pf => pf.FlavorName).ToList(),
            CustomizationOptionIds = customizationOptionIds,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public async Task<IEnumerable<ProductResponseDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductPrices.Where(pp => pp.IsActive))
            .Include(p => p.ProductTags)
            .Include(p => p.ProductFlavors)
            .Where(p => p.CategoryId == categoryId && p.IsActive)
            .ToListAsync();

        var productIds = products.Select(p => p.ProductId).ToList();
        var customizationOptions = await _context.ProductCustomizationOptions
            .Where(pco => productIds.Contains(pco.ProductId) && pco.IsActive)
            .ToListAsync();

        var customizationOptionsByProduct = customizationOptions
            .GroupBy(pco => pco.ProductId)
            .ToDictionary(g => g.Key, g => g.Select(pco => pco.CustomizationOptionId).ToList());

        return products.Select(p => new ProductResponseDto
        {
            ProductId = p.ProductId,
            ProductCode = p.ProductCode,
            NameEn = p.NameEn,
            NameAr = p.NameAr,
            ImageUrl = p.ImageUrl,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.Name,
            CaffeineIndex = p.CaffeineIndex,
            IsCustomizable = p.IsCustomizable,
            IsActive = p.IsActive,
            Prices = p.ProductPrices.Where(pp => pp.IsActive).Select(pp => new ProductPriceResponseDto
            {
                ProductPriceId = pp.ProductPriceId,
                Size = pp.Size,
                Price = pp.Price,
                IsActive = pp.IsActive
            }).ToList(),
            Tags = p.ProductTags.Select(pt => pt.Tag).ToList(),
            Flavors = p.ProductFlavors.Select(pf => pf.FlavorName).ToList(),
            CustomizationOptionIds = customizationOptionsByProduct.GetValueOrDefault(p.ProductId, new List<int>()),
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        }).ToList();
    }

    public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto createDto)
    {
        var product = new Product
        {
            ProductCode = createDto.ProductCode,
            NameEn = createDto.NameEn,
            NameAr = createDto.NameAr,
            ImageUrl = createDto.ImageUrl,
            CategoryId = createDto.CategoryId,
            CaffeineIndex = createDto.CaffeineIndex,
            IsCustomizable = createDto.IsCustomizable,
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Add prices
        foreach (var priceDto in createDto.Prices)
        {
            var price = new ProductPrice
            {
                ProductId = product.ProductId,
                Size = priceDto.Size,
                Price = priceDto.Price,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.ProductPrices.Add(price);
        }

        // Add tags
        foreach (var tag in createDto.Tags)
        {
            var productTag = new ProductTag
            {
                ProductId = product.ProductId,
                Tag = tag
            };
            _context.ProductTags.Add(productTag);
        }

        // Add flavors
        foreach (var flavor in createDto.Flavors)
        {
            var productFlavor = new ProductFlavor
            {
                ProductId = product.ProductId,
                FlavorName = flavor
            };
            _context.ProductFlavors.Add(productFlavor);
        }

        // Add customization options - ONLY the ones specified in the DTO
        // If isCustomizable is true but no options are selected, don't add any
        if (createDto.CustomizationOptionIds != null && createDto.CustomizationOptionIds.Any())
        {
            foreach (var optionId in createDto.CustomizationOptionIds)
            {
                // Verify the customization option exists
                var optionExists = await _context.CustomizationOptions
                    .AnyAsync(co => co.CustomizationOptionId == optionId && co.IsActive);
                
                if (optionExists)
                {
                    var productCustomizationOption = new ProductCustomizationOption
                    {
                        ProductId = product.ProductId,
                        CustomizationOptionId = optionId,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.ProductCustomizationOptions.Add(productCustomizationOption);
                }
            }
        }
        // If isCustomizable is true but CustomizationOptionIds is null or empty, 
        // we don't add any options - the product is customizable but has no specific options yet

        await _context.SaveChangesAsync();

        return (await GetProductByIdAsync(product.ProductId))!;
    }

    public async Task<ProductResponseDto?> UpdateProductAsync(int id, UpdateProductDto updateDto)
    {
        var product = await _context.Products
            .Include(p => p.ProductPrices)
            .Include(p => p.ProductTags)
            .Include(p => p.ProductFlavors)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null) return null;

        // Update basic properties
        if (!string.IsNullOrEmpty(updateDto.NameEn))
            product.NameEn = updateDto.NameEn;
        if (!string.IsNullOrEmpty(updateDto.NameAr))
            product.NameAr = updateDto.NameAr;
        if (updateDto.ImageUrl != null)
            product.ImageUrl = updateDto.ImageUrl;
        if (updateDto.CategoryId.HasValue)
            product.CategoryId = updateDto.CategoryId.Value;
        if (updateDto.CaffeineIndex.HasValue)
            product.CaffeineIndex = updateDto.CaffeineIndex.Value;
        if (updateDto.IsCustomizable.HasValue)
            product.IsCustomizable = updateDto.IsCustomizable.Value;
        if (updateDto.IsActive.HasValue)
            product.IsActive = updateDto.IsActive.Value;

        product.UpdatedAt = DateTime.UtcNow;

        // Update prices if provided
        if (updateDto.Prices != null)
        {
            // Remove existing prices
            _context.ProductPrices.RemoveRange(product.ProductPrices);

            // Add new prices
            foreach (var priceDto in updateDto.Prices)
            {
                var price = new ProductPrice
                {
                    ProductId = product.ProductId,
                    Size = priceDto.Size,
                    Price = priceDto.Price,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.ProductPrices.Add(price);
            }
        }

        // Update tags if provided
        if (updateDto.Tags != null)
        {
            _context.ProductTags.RemoveRange(product.ProductTags);
            foreach (var tag in updateDto.Tags)
            {
                var productTag = new ProductTag
                {
                    ProductId = product.ProductId,
                    Tag = tag
                };
                _context.ProductTags.Add(productTag);
            }
        }

        // Update flavors if provided
        if (updateDto.Flavors != null)
        {
            _context.ProductFlavors.RemoveRange(product.ProductFlavors);
            foreach (var flavor in updateDto.Flavors)
            {
                var productFlavor = new ProductFlavor
                {
                    ProductId = product.ProductId,
                    FlavorName = flavor
                };
                _context.ProductFlavors.Add(productFlavor);
            }
        }

        await _context.SaveChangesAsync();
        return await GetProductByIdAsync(id);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        // Soft delete
        product.IsActive = false;
        product.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await _context.Products.AnyAsync(p => p.ProductId == id);
    }

    public async Task<bool> ProductCodeExistsAsync(string productCode, int? excludeProductId = null)
    {
        if (excludeProductId.HasValue)
            return await _context.Products.AnyAsync(p => p.ProductCode == productCode && p.ProductId != excludeProductId.Value);
        return await _context.Products.AnyAsync(p => p.ProductCode == productCode);
    }
}



