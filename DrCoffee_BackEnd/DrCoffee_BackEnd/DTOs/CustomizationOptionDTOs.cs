namespace DrCoffee_BackEnd.DTOs;

// Request DTOs
public class CreateCustomizationOptionDto
{
    public string OptionCode { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; } = 0;
}

public class UpdateCustomizationOptionDto
{
    public string? OptionCode { get; set; }
    public string? NameEn { get; set; }
    public string? NameAr { get; set; }
    public decimal? Price { get; set; }
    public bool? IsActive { get; set; }
    public int? DisplayOrder { get; set; }
}

// Response DTOs
public class CustomizationOptionResponseDto
{
    public int CustomizationOptionId { get; set; }
    public string OptionCode { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

