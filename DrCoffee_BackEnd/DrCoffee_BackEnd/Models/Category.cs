namespace DrCoffee_BackEnd.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public ICollection<Product> Products { get; set; } = new List<Product>();
}



