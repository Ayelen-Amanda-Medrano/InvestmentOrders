using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Configurations;

public class TipoActivoConfiguration : IEntityTypeConfiguration<TipoActivo>
{
    public void Configure(EntityTypeBuilder<TipoActivo> builder)
    {
        builder.ToTable("TiposActivo");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .ValueGeneratedNever();

        builder.Property(provider => provider.Descripcion).IsRequired();

        builder.HasData(
            new TipoActivo(1, "Acción"),
            new TipoActivo(2, "Bono"),
            new TipoActivo(3, "FCI")
        );
    }
}
