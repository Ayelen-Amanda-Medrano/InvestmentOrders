using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Configurations;

public class TipoActivoConfiguration : IEntityTypeConfiguration<TipoActivo>
{
    public void Configure(EntityTypeBuilder<TipoActivo> builder)
    {
        builder.HasKey(provider => provider.Id);

        builder.Property(provider => provider.Descripcion).IsRequired();
    }
}
