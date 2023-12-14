using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeeMgmBackend.Migrations
{
    /// <inheritdoc />
    public partial class Fineentityupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                table: "Fines",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                table: "Fines");
        }
    }
}
