using InvestmentOrders.Application.Orders.Enums;

/// <summary>
/// Representa la solicitud para actualizar el estado de una orden existente.
/// </summary>
public class UpdateOrderRequest
{
    /// <summary>
    /// Nuevo estado de la orden.
    /// </summary>
    public EstadoOrdenEnum Estado { get; set; }
}