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

    public static T FromId<T>(int id) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(e => e.Id == id);

        return matchingItem!;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var fields = typeof(T).GetFields(System.Reflection.BindingFlags.Public |
                                         System.Reflection.BindingFlags.Static |
                                         System.Reflection.BindingFlags.DeclaredOnly);

        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

}

