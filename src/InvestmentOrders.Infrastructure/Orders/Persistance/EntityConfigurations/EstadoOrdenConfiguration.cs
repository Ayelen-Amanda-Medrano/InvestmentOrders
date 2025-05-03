using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Configurations;

public class EstadoOrdenConfiguration : IEntityTypeConfiguration<EstadoOrden>
{
    public void Configure(EntityTypeBuilder<EstadoOrden> builder)
    {
        builder.ToTable("EstadoOrdenes");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .ValueGeneratedNever();

        builder.Property(provider => provider.Descripcion).IsRequired();

        builder.HasData(
            new EstadoOrden(0, "En Proceso"),
            new EstadoOrden(1, "Ejecutada"),
            new EstadoOrden(3, "Cancelada")
        );
    }
}
   