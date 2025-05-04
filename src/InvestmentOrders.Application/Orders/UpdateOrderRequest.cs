using InvestmentOrders.Application.Orders.Enums;

namespace InvestmentOrders.Application.Orders;

public class UpdateOrderRequest
{
    public EstadoOrdenEnum Estado { get; set; }
}
