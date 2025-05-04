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

        builder.Property(a => a.TipoActivoId)
            .IsRequired();

        builder.HasAlternateKey(a => a.Nombre);

        builder.Property(a => a.PrecioUnitario)
            .IsRequired();

        builder.HasOne(o => o.TipoActivo)
            .WithMany()
            .HasForeignKey(o => o.TipoActivoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Activo { Id = 1, Ticker = "AAPL", Nombre = "Apple", TipoActivoId = 1, PrecioUnitario = 177.97m },
            new Activo { Id = 2, Ticker = "GOOGL", Nombre = "Alphabet Inc", TipoActivoId = 1, PrecioUnitario = 138.21m },
            new Activo { Id = 3, Ticker = "MSFT", Nombre = "Microsoft", TipoActivoId = 1, PrecioUnitario = 329.04m },
            new Activo { Id = 4, Ticker = "KO", Nombre = "Coca Cola", TipoActivoId = 1, PrecioUnitario = 58.3m },
            new Activo { Id = 5, Ticker = "WMT", Nombre = "Walmart", TipoActivoId = 1, PrecioUnitario = 163.42m },
            new Activo { Id = 6, Ticker = "AL30", Nombre = "BONOS ARGENTINA USD 2030 L.A", TipoActivoId = 2, PrecioUnitario = 307.4m },
            new Activo { Id = 7, Ticker = "GD30", Nombre = "Bonos Globales Argentina USD Step Up 2030", TipoActivoId = 2, PrecioUnitario = 336.1m },
            new Activo { Id = 8, Ticker = "Delta.Pesos", Nombre = "Delta Pesos Clase A", TipoActivoId = 3, PrecioUnitario = 0.0181m },
            new Activo { Id = 9, Ticker = "Fima.Premium", Nombre = "Fima Premium Clase A", TipoActivoId = 3, PrecioUnitario = 0.0317m }
        );
    }
}