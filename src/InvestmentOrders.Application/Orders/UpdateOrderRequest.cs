using InvestmentOrders.Domain.Enums;

namespace InvestmentOrders.Application.Orders;

public class UpdateOrderRequest
{
    public Estado Status { get; set; }
}
