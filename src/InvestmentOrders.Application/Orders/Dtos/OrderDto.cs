namespace InvestmentOrders.Application.Orders.Dtos;

/// <summary>
/// Representa los datos de una orden de inversión.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Identificador único de la orden.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificador de la cuenta asociada a la orden.
    /// </summary>
    public int CuentaId { get; set; }

    /// <summary>
    /// Nombre del activo financiero asociado a la orden.
    /// </summary>
    public string NombreActivo { get; set; }

    /// <summary>
    /// Cantidad de unidades del activo financiero.
    /// </summary>
    public int Cantidad { get; set; }

    /// <summary>
    /// Precio unitario del activo financiero.
    /// </summary>
    public decimal Precio { get; set; }

    /// <summary>
    /// Tipo de operación realizada (por ejemplo, compra: 'C' o venta: 'V').
    /// </summary>
    public char Operacion { get; set; }

    /// <summary>
    /// Estado actual de la orden (por ejemplo, "En proceso", "Ejecutada", "Cancelada").
    /// </summary>
    public string Estado { get; set; }

    /// <summary>
    /// Monto total de la orden.
    /// </summary>
    public decimal MontoTotal { get; set; }
}
