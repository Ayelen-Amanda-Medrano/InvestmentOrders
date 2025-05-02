namespace InvestmentOrders.Domain.Common;

public abstract class Enumeration : IComparable
{
    public int Id { get; }
    public string Descripcion { get; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Descripcion = name;
    }

    public override string ToString() => Descripcion;

    public int CompareTo(object? other) => Id.CompareTo(((Enumeration)other!).Id);
    
}

