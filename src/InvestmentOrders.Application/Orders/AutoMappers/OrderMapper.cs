using AutoMapper;
using InvestmentOrders.Application.Orders.Dtos;
using InvestmentOrders.Domain.Common;
using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Application.Orders.AutoMappers;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<Orden, OrderDto>()
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => Enumeration.FromId<EstadoOrden>(src.EstadoId).Descripcion));

        CreateMap<CreteOrderRequest, Orden>();
    }
}
