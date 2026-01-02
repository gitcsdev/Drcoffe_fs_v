namespace DrCoffee_BackEnd.DTOs;

// Request DTOs
public class CreateCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 0;
}

public class UpdateCategoryDto
{
    public string? Name { get; set; }
    public int? DisplayOrder { get; set; }
    public bool? IsActive { get; set; }
}

// Response DTOs
public class CategoryResponseDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public int ProductCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}



