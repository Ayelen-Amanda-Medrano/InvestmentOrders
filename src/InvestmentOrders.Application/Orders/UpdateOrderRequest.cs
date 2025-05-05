using InvestmentOrders.Application.Orders.Enums;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Representa la solicitud para actualizar el estado de una orden existente.
/// </summary>
public class UpdateOrderRequest
{
    /// <summary>
    /// Nuevo estado de la orden.
    /// </summary>
    [Required]
    public EstadoOrdenEnum Estado { get; set; }
}