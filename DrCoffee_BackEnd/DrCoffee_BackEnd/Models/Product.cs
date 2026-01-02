using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrCoffee_BackEnd.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(50)]
    public string ProductCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string NameEn { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string NameAr { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public int CaffeineIndex { get; set; } = 0;
    public bool IsCustomizable { get; set; } = false;
    public bool IsActive { get; set; } = true; // This is IsAvailable in the requirements
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = null!;

    public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
    public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
    public ICollection<ProductFlavor> ProductFlavors { get; set; } = new List<ProductFlavor>();
}

// Supporting models for Product
public class ProductPrice
{
    [Key]
    public int ProductPriceId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Size { get; set; } = string.Empty; // "small", "medium", "large", etc.

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;
}

public class ProductTag
{
    [Key]
    public int ProductTagId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Tag { get; set; } = string.Empty; // "Hot", "Cold"

    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;
}

public class ProductFlavor
{
    [Key]
    public int ProductFlavorId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FlavorName { get; set; } = string.Empty;

    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;
}



