using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrCoffee_BackEnd.Models;

public class CustomizationOption
{
    [Key]
    public int CustomizationOptionId { get; set; }

    [Required]
    [MaxLength(100)]
    public string OptionCode { get; set; } = string.Empty; // e.g., "extra_syrup"

    [Required]
    [MaxLength(200)]
    public string NameEn { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string NameAr { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; } = 0; // For ordering in UI
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

