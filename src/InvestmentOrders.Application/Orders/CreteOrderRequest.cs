using System.ComponentModel.DataAnnotations;

/// <summary>
/// Representa la solicitud para crear una nueva orden de inversión.
/// </summary>
public class CreteOrderRequest
{
    /// <summary>
    /// Identificador de la cuenta asociada a la orden.
    /// </summary>
    [Required]
    public int CuentaId { get; set; }

    /// <summary>
    /// Nombre del activo financiero asociado a la orden.
    /// </summary>
    [Required]
    public string NombreActivo { get; set; }

    /// <summary>
    /// Cantidad de unidades del activo financiero.
    /// </summary>
    [Required]
    public int Cantidad { get; set; }

    /// <summary>
    /// Precio unitario del activo financiero (opcional).
    /// Este campo es obligatorio para FCI y Bono.
    /// </summary>
    public decimal? PrecioUnitario { get; set; }

    /// <summary>
    /// Tipo de operación a realizar (por ejemplo, compra: 'C'  o venta: 'V').
    /// </summary>
    [Required]
    public char Operacion { get; set; }
}