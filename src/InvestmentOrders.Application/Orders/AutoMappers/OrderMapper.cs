using AutoMapper;
using InvestmentOrders.Application.Orders.Dtos;
using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Application.Orders.AutoMappers;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<Orden, OrderDto>()
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Descripcion));

        CreateMap<CreateOrderRequest, Orden>();
    }
}
