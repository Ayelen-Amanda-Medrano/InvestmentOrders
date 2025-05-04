using System.ComponentModel.DataAnnotations;

namespace InvestmentOrders.Application.Orders;

public class CreteOrderRequest
{
    [Required]
    public int CuentaId { get; set; }

    [Required]
    public string NombreActivo { get; set; }

    [Required]
    public int Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    [Required]
    public char Operacion { get; set; }
}
