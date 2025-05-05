using FluentValidation;

namespace InvestmentOrders.Application.Orders;

/// <summary>
/// Validator for the request to update an investment order.
/// </summary>
public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateOrderRequestValidator"/> class.
    /// </summary>
    public UpdateOrderRequestValidator()
    {
        RuleFor(x => x.Estado)
            .IsInEnum().WithMessage("El estado proporcionado no es válido.");
    }
}