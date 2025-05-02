using InvestmentOrders.Domain.Common;

namespace InvestmentOrders.Domain.Entities;

public class EstadoOrden : Enumeration
{
    public static readonly EstadoOrden InProcess = new(0, "En proceso");
    public static readonly EstadoOrden Executed = new(1, "Ejecutada");
    public static readonly EstadoOrden Cancelled = new(3, "Cancelada");

    public EstadoOrden(int id, string description) : base(id, description)
    {
    }
}
