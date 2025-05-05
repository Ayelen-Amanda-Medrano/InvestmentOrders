using FluentValidation.TestHelper;
using InvestmentOrders.Application.Orders;
using InvestmentOrders.Application.Orders.Enums;
using Xunit;

namespace InvestmentOrders.UnitTests.Orders;

public class UpdateOrderRequestValidatorTests
{
    private readonly UpdateOrderRequestValidator _validator;

    public UpdateOrderRequestValidatorTests()
    {
        _validator = new UpdateOrderRequestValidator();
    }

    [Fact]
    public void ShouldHaveValidationError_WhenEstadoIsInvalid()
    {
        // Arrange
        var request = new UpdateOrderRequest { Estado = (EstadoOrdenEnum)10 };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Estado)
            .WithErrorMessage("El estado proporcionado no es válido.");
    }

    [Fact]
    public void ShouldNotHaveValidationError_WhenEstadoIsValid()
    {
        // Arrange
        var request = new UpdateOrderRequest { Estado = EstadoOrdenEnum.Ejecutada };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Estado);
    }
}