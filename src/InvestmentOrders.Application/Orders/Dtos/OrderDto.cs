namespace InvestmentOrders.Application.Orders.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public int CuentaId { get; set; }
    public string NombreActivo { get; set; }
    public int Cantidad { get; set; }
    public decimal? Precio { get; set; }
    public char Operacion { get; set; }
    public int EstadoId { get; set; }
    public decimal MontoTotal { get; set; }
}
