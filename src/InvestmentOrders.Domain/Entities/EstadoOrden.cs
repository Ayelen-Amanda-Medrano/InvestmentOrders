namespace InvestmentOrders.Domain.Entities;

public class EstadoOrden
{
    public int Id { get; private set; }
    public string Descripcion { get; private set; }

    private EstadoOrden() { }

    public EstadoOrden(int id, string descripcion)
    {
        Id = id;
        Descripcion = descripcion;
    }

    public static readonly EstadoOrden InProcess = new(0, "En proceso");
    public static readonly EstadoOrden Executed = new(1, "Ejecutada");
    public static readonly EstadoOrden Cancelled = new(3, "Cancelada");
}
