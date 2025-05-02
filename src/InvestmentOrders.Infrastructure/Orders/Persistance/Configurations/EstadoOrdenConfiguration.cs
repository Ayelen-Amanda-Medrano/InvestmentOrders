using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Configurations;

public class EstadoOrdenConfiguration : IEntityTypeConfiguration<EstadoOrden>
{
    public void Configure(EntityTypeBuilder<EstadoOrden> builder)
    {
        builder.HasKey(provider => provider.Id);

        builder.Property(provider => provider.Descripcion).IsRequired();
    }
}
   