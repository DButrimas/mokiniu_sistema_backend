using Microsoft.EntityFrameworkCore.Migrations;

namespace mokiniu_sistema.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rajonai",
                columns: table => new
                {
                    RajonasId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pavadinimas = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rajonai", x => x.RajonasId);
                });

            migrationBuilder.CreateTable(
                name: "Tevai",
                columns: table => new
                {
                    TevasId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vardas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tevai", x => x.TevasId);
                });

            migrationBuilder.CreateTable(
                name: "Mokyklos",
                columns: table => new
                {
                    MokyklaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pavadinimas = table.Column<string>(nullable: false),
                    RajonasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mokyklos", x => x.MokyklaId);
                    table.ForeignKey(
                        name: "FK_Mokyklos_Rajonai_RajonasId",
                        column: x => x.RajonasId,
                        principalTable: "Rajonai",
                        principalColumn: "RajonasId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vaikai",
                columns: table => new
                {
                    VaikasId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vardas = table.Column<string>(nullable: false),
                    PridejimoKodas = table.Column<string>(nullable: true),
                    TevasId = table.Column<int>(nullable: true),
                    MokyklaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaikai", x => x.VaikasId);
                    table.ForeignKey(
                        name: "FK_Vaikai_Mokyklos_MokyklaId",
                        column: x => x.MokyklaId,
                        principalTable: "Mokyklos",
                        principalColumn: "MokyklaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vaikai_Tevai_TevasId",
                        column: x => x.TevasId,
                        principalTable: "Tevai",
                        principalColumn: "TevasId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mokyklos_RajonasId",
                table: "Mokyklos",
                column: "RajonasId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaikai_MokyklaId",
                table: "Vaikai",
                column: "MokyklaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaikai_TevasId",
                table: "Vaikai",
                column: "TevasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vaikai");

            migrationBuilder.DropTable(
                name: "Mokyklos");

            migrationBuilder.DropTable(
                name: "Tevai");

            migrationBuilder.DropTable(
                name: "Rajonai");
        }
    }
}
