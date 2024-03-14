using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTech.CreditCard.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class StatusFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "CreditCards",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CreditCards");
        }
    }
}
