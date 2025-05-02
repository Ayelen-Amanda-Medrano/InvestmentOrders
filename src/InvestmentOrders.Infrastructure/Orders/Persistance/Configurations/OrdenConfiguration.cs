using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Configurations;

public class OrdenConfiguration : IEntityTypeConfiguration<Orden>
{
    public void Configure(EntityTypeBuilder<Orden> builder)
    {
        builder.ToTable("Ordenes");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CuentaId)
            .IsRequired();

        builder.Property(o => o.NombreActivo)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(o => o.Cantidad)
            .IsRequired();

        builder.Property(o => o.Precio)
            .IsRequired();

        builder.Property(o => o.Operacion)
            .IsRequired()
            .HasMaxLength(1);

        builder.Property(o => o.EstadoId)
            .IsRequired();

        builder.Property(o => o.MontoTotal);

        builder
            .HasOne(o => o.Estado)
            .WithMany()
            .HasForeignKey(o => o.EstadoId);

        builder
            .HasOne(o => o.Activo)
            .WithMany()
            .HasPrincipalKey(a => a.Nombre);
    }
}