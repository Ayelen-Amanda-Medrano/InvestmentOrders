using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvestmentOrders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadoOrdenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoOrdenes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposActivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposActivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ticker = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    TipoActivoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activos", x => x.Id);
                    table.UniqueConstraint("AK_Activos_Nombre", x => x.Nombre);
                    table.ForeignKey(
                        name: "FK_Activos_TiposActivo_TipoActivoId",
                        column: x => x.TipoActivoId,
                        principalTable: "TiposActivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CuentaId = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreActivo = table.Column<string>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    Operacion = table.Column<char>(type: "TEXT", maxLength: 1, nullable: false),
                    EstadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenes_Activos_NombreActivo",
                        column: x => x.NombreActivo,
                        principalTable: "Activos",
                        principalColumn: "Nombre",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordenes_EstadoOrdenes_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "EstadoOrdenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EstadoOrdenes",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 0, "En Proceso" },
                    { 1, "Ejecutada" },
                    { 3, "Cancelada" }
                });

            migrationBuilder.InsertData(
                table: "TiposActivo",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Acción" },
                    { 2, "Bono" },
                    { 3, "FCI" }
                });

            migrationBuilder.InsertData(
                table: "Activos",
                columns: new[] { "Id", "Nombre", "PrecioUnitario", "Ticker", "TipoActivoId" },
                values: new object[,]
                {
                    { 1, "Apple", 177.97m, "AAPL", 1 },
                    { 2, "Alphabet Inc", 138.21m, "GOOGL", 1 },
                    { 3, "Microsoft", 329.04m, "MSFT", 1 },
                    { 4, "Coca Cola", 58.3m, "KO", 1 },
                    { 5, "Walmart", 163.42m, "WMT", 1 },
                    { 6, "BONOS ARGENTINA USD 2030 L.A", 307.4m, "AL30", 2 },
                    { 7, "Bonos Globales Argentina USD Step Up 2030", 336.1m, "GD30", 2 },
                    { 8, "Delta Pesos Clase A", 0.0181m, "Delta.Pesos", 3 },
                    { 9, "Fima Premium Clase A", 0.0317m, "Fima.Premium", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activos_TipoActivoId",
                table: "Activos",
                column: "TipoActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_EstadoId",
                table: "Ordenes",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_NombreActivo",
                table: "Ordenes",
                column: "NombreActivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Activos");

            migrationBuilder.DropTable(
                name: "EstadoOrdenes");

            migrationBuilder.DropTable(
                name: "TiposActivo");
        }
    }
}
