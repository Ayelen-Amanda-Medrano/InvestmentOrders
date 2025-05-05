using AutoMapper;
using FluentAssertions;
using InvestmentOrders.Application.Orders.Dtos;
using InvestmentOrders.Application.Orders.Enums;
using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using InvestmentOrders.Application.Orders.Services;
using InvestmentOrders.Domain.Entities;
using NSubstitute;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentOrders.UnitTests.Orders;

public class OrderServiceTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IMapper _mapper;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _assetRepository = Substitute.For<IAssetRepository>();
        _mapper = Substitute.For<IMapper>();

        _orderService = new OrderService(
            _orderRepository,
            _assetRepository,
            _mapper
        );
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldReturnFail_WhenAssetDoesNotExist()
    {
        // Arrange
        var request = new CreateOrderRequest
        {
            Ticker = "INVALID",
            CuentaId = 1,
            Cantidad = 10,
            Operacion = 'C'
        };

        _assetRepository.GetAssetByTickerAsync(request.Ticker).Returns((Activo?)null);

        // Act
        var result = await _orderService.CreateOrderAsync(request);

        // Assert
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Message.Should().Be($"El identificador '{request.Ticker}' del activo financiero no existe.");
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldReturnFail_WhenUnitPriceIsInvalid()
    {
        // Arrange
        var request = new CreateOrderRequest
        {
            Ticker = "VALID",
            CuentaId = 1,
            Cantidad = 10,
            Operacion = 'C',
            PrecioUnitario = -1
        };

        var asset = new Activo
        {
            Ticker = "VALID",
            Nombre = "Activo Válido",
            TipoActivo = TipoActivo.FCI,
            TipoActivoId = TipoActivo.FCI.Id
        };

        _assetRepository.GetAssetByTickerAsync(request.Ticker).Returns(asset);

        // Act
        var result = await _orderService.CreateOrderAsync(request);

        // Assert
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Message.Should().Be($"El precio unitario es obligatorio y debe ser mayor a 0 para el activo de tipo '{asset.TipoActivo.Descripcion}'.");
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldReturnSuccess_WhenOrderIsValid()
    {
        // Arrange
        var request = new CreateOrderRequest
        {
            Ticker = "VALID",
            CuentaId = 1,
            Cantidad = 10,
            Operacion = 'C',
            PrecioUnitario = 100
        };

        var asset = new Activo
        {
            Ticker = "VALID",
            Nombre = "Activo Válido",
            TipoActivo = TipoActivo.FCI,
            TipoActivoId = TipoActivo.FCI.Id
        };

        var order = Orden.CreateOrder(request.CuentaId, asset.Nombre, request.Cantidad, request.Operacion);
        order.SetAsset(asset);
        order.SetPrice(request.PrecioUnitario);
        order.CalculateTotalAmount();

        _assetRepository.GetAssetByTickerAsync(request.Ticker).Returns(asset);
        _orderRepository.AddAsync(Arg.Any<Orden>()).Returns(Task.CompletedTask);
        _orderRepository.SaveChangesAsync().Returns(Task.CompletedTask);

        // Act
        var result = await _orderService.CreateOrderAsync(request);

        // Assert
        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Data.Should().NotBeNull();
        result.Data!.OrderId.Should().Be(order.Id);
    }

    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnFail_WhenOrderDoesNotExist()
    {
        // Arrange
        int orderId = 1;
        _orderRepository.GetOrderByIdAsync(orderId).Returns((Orden?)null);

        // Act
        var result = await _orderService.GetOrderByIdAsync(orderId);

        // Assert
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().Be($"La orden con ID '{orderId}' no fue encontrada.");
    }

    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnSuccess_WhenOrderExists()
    {
        // Arrange
        int orderId = 1;
        var order = Orden.CreateOrder(123, "Activo 1", 10, 'C');

        var orderDto = new OrderDto
        {
            Id = orderId,
            CuentaId = 123,
            NombreActivo = "Activo 1",
            Cantidad = 10,
            Precio = 100,
            Operacion = 'C',
            Estado = "En proceso",
            MontoTotal = 1000
        };

        _orderRepository.GetOrderByIdAsync(orderId).Returns(order);
        _mapper.Map<OrderDto>(order).Returns(orderDto);

        // Act
        var result = await _orderService.GetOrderByIdAsync(orderId);

        // Assert
        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Data.Should().BeEquivalentTo(orderDto);
    }

    [Fact]
    public async Task UpdateOrderAsync_ShouldReturnFail_WhenOrderDoesNotExist()
    {
        // Arrange
        int orderId = 1;
        var request = new UpdateOrderRequest { Estado = EstadoOrdenEnum.Ejecutada };

        _orderRepository.GetOrderByIdAsync(orderId).Returns((Orden?)null);

        // Act
        var result = await _orderService.UpdateOrderAsync(orderId, request);

        // Assert
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().Be($"La orden con ID '{orderId}' no fue encontrada.");
    }

    [Fact]
    public async Task UpdateOrderAsync_ShouldReturnSuccess_WhenOrderIsUpdated()
    {
        // Arrange
        int orderId = 1;
        var request = new UpdateOrderRequest { Estado = EstadoOrdenEnum.Ejecutada };

        var order = new Orden
        {
            Id = orderId,
            Estado = new EstadoOrden(0, "En proceso")
        };

        var updatedOrderDto = new OrderDto
        {
            Id = orderId,
            Estado = "Ejecutada"
        };

        _orderRepository.GetOrderByIdAsync(orderId).Returns(order);
        _mapper.Map<OrderDto>(order).Returns(updatedOrderDto);

        // Act
        var result = await _orderService.UpdateOrderAsync(orderId, request);

        // Assert
        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Data.Should().BeEquivalentTo(updatedOrderDto);
        order.EstadoId.Should().Be((int)EstadoOrdenEnum.Ejecutada);
        await _orderRepository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task DeleteOrderAsync_ShouldReturnFail_WhenOrderDoesNotExist()
    {
        // Arrange
        int orderId = 1;
        _orderRepository.GetOrderByIdAsync(orderId).Returns((Orden?)null);

        // Act
        var result = await _orderService.DeleteOrderAsync(orderId);

        // Assert
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().Be($"La orden con ID '{orderId}' no fue encontrada.");
    }

    [Fact]
    public async Task DeleteOrderAsync_ShouldReturnSuccess_WhenOrderIsDeleted()
    {
        // Arrange
        int orderId = 1;
        var order = new Orden { Id = orderId };

        _orderRepository.GetOrderByIdAsync(orderId).Returns(order);

        // Act
        var result = await _orderService.DeleteOrderAsync(orderId);

        // Assert
        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        result.Data.Should().BeNull();
        await _orderRepository.Received(1).DeleteAsync(order);
    }
}