using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrCoffee_BackEnd.Models;

public class ProductCustomizationOption
{
    [Key]
    public int ProductCustomizationOptionId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int CustomizationOptionId { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;

    [ForeignKey("CustomizationOptionId")]
    public CustomizationOption CustomizationOption { get; set; } = null!;
}

