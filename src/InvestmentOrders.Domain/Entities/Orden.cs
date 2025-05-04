namespace InvestmentOrders.Domain.Entities;

public class Orden
{
    public int Id { get; set; }
    public int CuentaId { get; private set; }
    public string NombreActivo { get; private set; } = string.Empty;
    public int Cantidad { get; private set; }
    public decimal Precio { get; private set; }
    public char Operacion { get; private set; }
    public int EstadoId { get; private set; }
    public decimal? MontoTotal { get; private set; }

    public Activo Activo { get; set; }
    public EstadoOrden Estado { get; set; }

    public static Orden CreateOrder(int cuentaId, string nombreActivo, int cantidad, char operacion)
    {
        var order = new Orden
        {
            CuentaId = cuentaId,
            NombreActivo = nombreActivo,
            Cantidad = cantidad,
            Operacion = operacion,
            EstadoId = EstadoOrden.InProcess.Id,
        };

        return order;
    }

    public void SetAsset(Activo activo)
    {
        Activo = activo;
    }

    public void SetPrice(decimal? precioUnitario)
    {
        if (Activo.TipoActivoId == TipoActivo.Accion.Id)
        {
            Precio = Activo.PrecioUnitario;
        }
        else
        {
            Precio = precioUnitario!.Value;
        }
    }

    public void CalculateTotalAmount()
    {
        var tipoActivo = Activo.TipoActivoId;

        if (tipoActivo == TipoActivo.FCI.Id)
        {
            MontoTotal = Cantidad * Precio;
        }

        if (tipoActivo == TipoActivo.Accion.Id)
        {
            decimal precioAccion = Precio;
            decimal totalAccion = Cantidad * precioAccion;
            decimal comisionAccion = totalAccion * 0.006m;
            decimal impuestosAccion = comisionAccion * 0.21m;
            MontoTotal = totalAccion + comisionAccion + impuestosAccion;
        }

        if (tipoActivo == TipoActivo.Bono.Id)
        {
            decimal totalBono = Cantidad * Precio;
            decimal comisionBono = totalBono * 0.002m;
            decimal impuestosBono = comisionBono * 0.21m;
            MontoTotal = totalBono + comisionBono + impuestosBono;
        }
    }

    public void UpdateStatus(int estadoId)
    {
        EstadoId = estadoId;
    }
}
