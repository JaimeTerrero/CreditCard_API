using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTech.CreditCard.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CashAdvancedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CashAdvance",
                table: "CreditCards",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashAdvance",
                table: "CreditCards");
        }
    }
}
