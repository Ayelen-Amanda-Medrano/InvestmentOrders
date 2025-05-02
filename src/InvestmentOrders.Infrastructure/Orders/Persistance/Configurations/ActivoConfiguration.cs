using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Configurations;

public class ActivoConfiguration : IEntityTypeConfiguration<Activo>
{
    public void Configure(EntityTypeBuilder<Activo> builder)
    {
        builder.ToTable("Activos");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Ticker)
            .IsRequired();

        builder.Property(a => a.Nombre)
            .IsRequired();

        builder.Property(a => a.PrecioUnitario)
            .IsRequired();

        builder.HasOne(a => a.TipoActivo)
            .WithMany()
            .HasForeignKey(a => a.TipoActivoId)
            .IsRequired();
    }
}