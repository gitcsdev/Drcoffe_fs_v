using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrCoffee_BackEnd.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    [MaxLength(50)]
    public string OrderNumber { get; set; } = string.Empty;

    public int? CustomerId { get; set; }

    [MaxLength(200)]
    public string? CustomerName { get; set; }

    [MaxLength(20)]
    public string? CustomerPhone { get; set; }

    [MaxLength(20)]
    public string? CustomerWhatsApp { get; set; }

    [MaxLength(500)]
    public string? CustomerAddress { get; set; }

    [Required]
    [MaxLength(50)]
    public string OrderStatus { get; set; } = "Pending"; // Pending, Confirmed, Preparing, Ready, Completed, Cancelled

    [Required]
    [MaxLength(50)]
    public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid, Refunded

    [MaxLength(50)]
    public string? PaymentMethod { get; set; } // Cash, Card, Online

    [Column(TypeName = "decimal(10,2)")]
    public decimal SubTotal { get; set; } = 0;

    [Column(TypeName = "decimal(10,2)")]
    public decimal TaxAmount { get; set; } = 0;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalAmount { get; set; } = 0;

    [MaxLength(1000)]
    public string? Notes { get; set; }

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(50)]
    public string ProductCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string ProductNameEn { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string ProductNameAr { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Size { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }

    [Required]
    public int Quantity { get; set; } = 1;

    [Column(TypeName = "decimal(10,2)")]
    public decimal CustomizationTotal { get; set; } = 0;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal ItemTotal { get; set; }

    [MaxLength(100)]
    public string? Flavor { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("OrderId")]
    public Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;
}

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [MaxLength(200)]
    public string? Name { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(200)]
    public string? Email { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(20)]
    public string? WhatsAppNumber { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}



