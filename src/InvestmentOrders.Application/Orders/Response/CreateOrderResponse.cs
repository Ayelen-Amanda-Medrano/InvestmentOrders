namespace InvestmentOrders.Application.Orders.Response;

/// <summary>
/// Representa la respuesta al crear una nueva orden de inversión.
/// </summary>
public class CreateOrderResponse
{
    /// <summary>
    /// Identificador único de la orden creada.
    /// </summary>
    public int OrderId { get; set; }
}
