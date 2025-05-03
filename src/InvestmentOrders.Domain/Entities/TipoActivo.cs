namespace InvestmentOrders.Domain.Entities;

public class TipoActivo
{
    public int Id { get; set; }
    public string Descripcion { get; set; }

    public TipoActivo() { }

    public TipoActivo(int id, string descripcion)
    {
        Id = id;
        Descripcion = descripcion;
    }

    public static readonly TipoActivo Accion = new(1, "Acción");
    public static readonly TipoActivo Bono = new(2, "Bono");
    public static readonly TipoActivo FCI = new(3, "FCI");
}
