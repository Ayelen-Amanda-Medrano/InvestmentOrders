using FluentValidation.TestHelper;
using InvestmentOrders.Application.Orders;
using Xunit;

namespace InvestmentOrders.UnitTests.Orders;

public class CreteOrderRequestValidatorTests
{
    private readonly CreteOrderRequestValidator _validator;

    public CreteOrderRequestValidatorTests()
    {
        _validator = new CreteOrderRequestValidator();
    }

    [Fact]
    public void ShouldHaveValidationError_WhenTickerIsEmpty()
    {
        // Arrange
        var request = new CreateOrderRequest { Ticker = string.Empty };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Ticker)
            .WithErrorMessage("El identificador del activo financiero es obligatorio.");
    }

    [Fact]
    public void ShouldNotHaveValidationError_WhenTickerIsValid()
    {
        // Arrange
        var request = new CreateOrderRequest { Ticker = "VALID" };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Ticker);
    }

    [Fact]
    public void ShouldHaveValidationError_WhenCantidadIsZeroOrLess()
    {
        // Arrange
        var request = new CreateOrderRequest { Cantidad = 0 };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Cantidad)
            .WithErrorMessage("La cantidad debe ser mayor a 0.");
    }

    [Fact]
    public void ShouldNotHaveValidationError_WhenCantidadIsGreaterThanZero()
    {
        // Arrange
        var request = new CreateOrderRequest { Cantidad = 10 };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Cantidad);
    }

    [Fact]
    public void ShouldHaveValidationError_WhenOperacionIsInvalid()
    {
        // Arrange
        var request = new CreateOrderRequest { Operacion = 'X' };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Operacion)
            .WithErrorMessage("La operación debe ser 'C' (compra) o 'V' (venta).");
    }

    [Fact]
    public void ShouldNotHaveValidationError_WhenOperacionIsValid()
    {
        // Arrange
        var requestCompra = new CreateOrderRequest { Operacion = 'C' };
        var requestVenta = new CreateOrderRequest { Operacion = 'V' };

        // Act
        var resultCompra = _validator.TestValidate(requestCompra);
        var resultVenta = _validator.TestValidate(requestVenta);

        // Assert
        resultCompra.ShouldNotHaveValidationErrorFor(x => x.Operacion);
        resultVenta.ShouldNotHaveValidationErrorFor(x => x.Operacion);
    }
}