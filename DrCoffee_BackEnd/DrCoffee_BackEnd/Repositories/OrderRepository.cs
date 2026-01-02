using Microsoft.EntityFrameworkCore;
using DrCoffee_BackEnd.Data;
using DrCoffee_BackEnd.Models;
using DrCoffee_BackEnd.DTOs;

namespace DrCoffee_BackEnd.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderSummaryDto>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .OrderByDescending(o => o.OrderDate)
            .Select(o => new OrderSummaryDto
            {
                OrderId = o.OrderId,
                OrderNumber = o.OrderNumber,
                CustomerName = o.CustomerName,
                CustomerPhone = o.CustomerPhone,
                OrderStatus = o.OrderStatus,
                TotalAmount = o.TotalAmount,
                ItemCount = o.OrderItems.Count,
                OrderDate = o.OrderDate
            })
            .ToListAsync();
    }

    public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null) return null;

        return new OrderResponseDto
        {
            OrderId = order.OrderId,
            OrderNumber = order.OrderNumber,
            CustomerName = order.CustomerName,
            CustomerPhone = order.CustomerPhone,
            CustomerWhatsApp = order.CustomerWhatsApp,
            CustomerAddress = order.CustomerAddress,
            OrderStatus = order.OrderStatus,
            PaymentStatus = order.PaymentStatus,
            PaymentMethod = order.PaymentMethod,
            SubTotal = order.SubTotal,
            TaxAmount = order.TaxAmount,
            TotalAmount = order.TotalAmount,
            Notes = order.Notes,
            OrderDate = order.OrderDate,
            OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto
            {
                OrderItemId = oi.OrderItemId,
                ProductCode = oi.ProductCode,
                ProductNameEn = oi.ProductNameEn,
                ProductNameAr = oi.ProductNameAr,
                Size = oi.Size,
                UnitPrice = oi.UnitPrice,
                Quantity = oi.Quantity,
                CustomizationTotal = oi.CustomizationTotal,
                ItemTotal = oi.ItemTotal,
                Flavor = oi.Flavor
            }).ToList(),
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }

    public async Task<OrderResponseDto?> GetOrderByNumberAsync(string orderNumber)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

        if (order == null) return null;

        return new OrderResponseDto
        {
            OrderId = order.OrderId,
            OrderNumber = order.OrderNumber,
            CustomerName = order.CustomerName,
            CustomerPhone = order.CustomerPhone,
            CustomerWhatsApp = order.CustomerWhatsApp,
            CustomerAddress = order.CustomerAddress,
            OrderStatus = order.OrderStatus,
            PaymentStatus = order.PaymentStatus,
            PaymentMethod = order.PaymentMethod,
            SubTotal = order.SubTotal,
            TaxAmount = order.TaxAmount,
            TotalAmount = order.TotalAmount,
            Notes = order.Notes,
            OrderDate = order.OrderDate,
            OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto
            {
                OrderItemId = oi.OrderItemId,
                ProductCode = oi.ProductCode,
                ProductNameEn = oi.ProductNameEn,
                ProductNameAr = oi.ProductNameAr,
                Size = oi.Size,
                UnitPrice = oi.UnitPrice,
                Quantity = oi.Quantity,
                CustomizationTotal = oi.CustomizationTotal,
                ItemTotal = oi.ItemTotal,
                Flavor = oi.Flavor
            }).ToList(),
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }

    public async Task<OrderResponseDto?> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto updateDto)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return null;

        // Validate status
        var validStatuses = new[] { "Pending", "Confirmed", "Preparing", "Ready", "Completed", "Cancelled" };
        if (!validStatuses.Contains(updateDto.OrderStatus))
            return null;

        order.OrderStatus = updateDto.OrderStatus;
        order.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return await GetOrderByIdAsync(id);
    }

    public async Task<IEnumerable<OrderSummaryDto>> GetOrdersByStatusAsync(string status)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.OrderStatus == status)
            .OrderByDescending(o => o.OrderDate)
            .Select(o => new OrderSummaryDto
            {
                OrderId = o.OrderId,
                OrderNumber = o.OrderNumber,
                CustomerName = o.CustomerName,
                CustomerPhone = o.CustomerPhone,
                OrderStatus = o.OrderStatus,
                TotalAmount = o.TotalAmount,
                ItemCount = o.OrderItems.Count,
                OrderDate = o.OrderDate
            })
            .ToListAsync();
    }
}



