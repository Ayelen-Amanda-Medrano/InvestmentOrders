namespace InvestmentOrders.Domain.Entities;

public class Activo
{
    public int Id { get; set; }
    public required string Ticker { get; set; }
    public required string Nombre { get; set; }
    public int TipoActivoId { get; set; }
    public decimal PrecioUnitario { get; set; }

    public TipoActivo TipoActivo { get; set; } = null!;

    public bool NeedsUnitPrice()
    {
        return TipoActivoId == TipoActivo.FCI.Id || TipoActivoId == TipoActivo.Bono.Id;
    }
}
