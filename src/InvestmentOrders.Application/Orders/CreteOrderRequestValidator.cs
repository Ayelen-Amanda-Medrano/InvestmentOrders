using FluentValidation;
using InvestmentOrders.Application.Orders.Enums;

namespace InvestmentOrders.Application.Orders;

/// <summary>
/// Validator for the request to create a new investment order.
/// </summary>
public class CreteOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreteOrderRequestValidator"/> class.
    /// </summary>
    public CreteOrderRequestValidator()
    {
        RuleFor(x => x.NombreActivo)
            .NotEmpty().WithMessage("El nombre del activo es obligatorio.")
            .MaximumLength(32).WithMessage("El nombre del activo no puede exceder los 32 caracteres.");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

        RuleFor(x => x.Operacion)
            .Must(op => op == (char)OperacionEnum.Compra || op == (char)OperacionEnum.Venta)
            .WithMessage("La operación debe ser 'C' (compra) o 'V' (venta).");
    }
}
