namespace DrCoffee_BackEnd.DTOs;

// Request DTOs
public class UpdateOrderStatusDto
{
    public string OrderStatus { get; set; } = string.Empty; // Pending, Confirmed, Preparing, Ready, Completed, Cancelled
}

// Response DTOs
public class OrderResponseDto
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string? CustomerName { get; set; }
    public string? CustomerPhone { get; set; }
    public string? CustomerWhatsApp { get; set; }
    public string? CustomerAddress { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string? PaymentMethod { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemResponseDto> OrderItems { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class OrderItemResponseDto
{
    public int OrderItemId { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string ProductNameEn { get; set; } = string.Empty;
    public string ProductNameAr { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal CustomizationTotal { get; set; }
    public decimal ItemTotal { get; set; }
    public string? Flavor { get; set; }
}

public class OrderSummaryDto
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string? CustomerName { get; set; }
    public string? CustomerPhone { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public int ItemCount { get; set; }
    public DateTime OrderDate { get; set; }
}



