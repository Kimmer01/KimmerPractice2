using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePractice.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "T_Books",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "T_Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Books_CategoryId",
                table: "T_Books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Books_T_Category_CategoryId",
                table: "T_Books",
                column: "CategoryId",
                principalTable: "T_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Books_T_Category_CategoryId",
                table: "T_Books");

            migrationBuilder.DropTable(
                name: "T_Category");

            migrationBuilder.DropIndex(
                name: "IX_T_Books_CategoryId",
                table: "T_Books");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "T_Books");
        }
    }
}
