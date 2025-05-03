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

    [Required]
    public decimal Precio { get; set; }

    [Required]
    public char Operacion { get; set; }
}
