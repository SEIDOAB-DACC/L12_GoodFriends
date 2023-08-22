using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    /// <inheritdoc />
    public partial class mi_adrfk1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Addresses_AddressId",
                schema: "supusr",
                table: "Friends");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Addresses_AddressId",
                schema: "supusr",
                table: "Friends",
                column: "AddressId",
                principalSchema: "supusr",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Addresses_AddressId",
                schema: "supusr",
                table: "Friends");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Addresses_AddressId",
                schema: "supusr",
                table: "Friends",
                column: "AddressId",
                principalSchema: "supusr",
                principalTable: "Addresses",
                principalColumn: "AddressId");
        }
    }
}
