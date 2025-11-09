using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Payment_Expiration",
                table: "Orders",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Payment_CardName",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Payment_Expiration",
                table: "Orders",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Payment_CardName",
                table: "Orders",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
