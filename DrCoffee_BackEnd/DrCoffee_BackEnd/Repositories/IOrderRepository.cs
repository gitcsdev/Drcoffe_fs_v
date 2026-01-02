using DrCoffee_BackEnd.DTOs;

namespace DrCoffee_BackEnd.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<OrderSummaryDto>> GetAllOrdersAsync();
    Task<OrderResponseDto?> GetOrderByIdAsync(int id);
    Task<OrderResponseDto?> GetOrderByNumberAsync(string orderNumber);
    Task<OrderResponseDto?> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto updateDto);
    Task<IEnumerable<OrderSummaryDto>> GetOrdersByStatusAsync(string status);
}



