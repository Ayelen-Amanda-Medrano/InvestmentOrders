namespace InvestmentOrders.Domain.Entities;

public class Activo
{
    public int Id { get; set; }
    public required string Ticker { get; set; }
    public required string Nombre { get; set; }
    public int TipoActivoId { get; set; }
    public decimal PrecioUnitario { get; set; }

    public required TipoActivo TipoActivo { get; set; }
}
