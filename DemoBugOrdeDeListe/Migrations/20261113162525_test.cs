using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBugOrdeDeListe.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enfant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enfant_Parent_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EnfantAvecIndex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnfantAvecIndex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnfantAvecIndex_Parent_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enfant_ParentId",
                table: "Enfant",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnfantAvecIndex_ParentId",
                table: "EnfantAvecIndex",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enfant");

            migrationBuilder.DropTable(
                name: "EnfantAvecIndex");

            migrationBuilder.DropTable(
                name: "Parent");
        }
    }
}
