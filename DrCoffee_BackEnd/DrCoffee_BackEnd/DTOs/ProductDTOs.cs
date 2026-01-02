namespace DrCoffee_BackEnd.DTOs;

// Request DTOs
public class CreateProductDto
{
    public string ProductCode { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public int CaffeineIndex { get; set; } = 0;
    public bool IsCustomizable { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public List<ProductPriceDto> Prices { get; set; } = new();
    public List<string> Tags { get; set; } = new();
    public List<string> Flavors { get; set; } = new();
    public List<int> CustomizationOptionIds { get; set; } = new(); // IDs of available customization options
}

public class UpdateProductDto
{
    public string? NameEn { get; set; }
    public string? NameAr { get; set; }
    public string? ImageUrl { get; set; }
    public int? CategoryId { get; set; }
    public int? CaffeineIndex { get; set; }
    public bool? IsCustomizable { get; set; }
    public bool? IsActive { get; set; }
    public List<ProductPriceDto>? Prices { get; set; }
    public List<string>? Tags { get; set; }
    public List<string>? Flavors { get; set; }
    public List<int>? CustomizationOptionIds { get; set; } // IDs of available customization options
}

public class ProductPriceDto
{
    public string Size { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

// Response DTOs
public class ProductResponseDto
{
    public int ProductId { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int CaffeineIndex { get; set; }
    public bool IsCustomizable { get; set; }
    public bool IsActive { get; set; }
    public List<ProductPriceResponseDto> Prices { get; set; } = new();
    public List<string> Tags { get; set; } = new();
    public List<string> Flavors { get; set; } = new();
    public List<int> CustomizationOptionIds { get; set; } = new(); // IDs of available customization options
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class ProductPriceResponseDto
{
    public int ProductPriceId { get; set; }
    public string Size { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}



