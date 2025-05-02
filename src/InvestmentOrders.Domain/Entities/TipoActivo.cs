using InvestmentOrders.Domain.Common;

namespace InvestmentOrders.Domain.Entities;

public class TipoActivo : Enumeration
{
    public static readonly TipoActivo Accion = new(1, "Acción");
    public static readonly TipoActivo Bono = new(2, "Bono");
    public static readonly TipoActivo FCI = new(3, "FCI");

    public TipoActivo(int id, string description) : base(id, description)
    {
    }
}
