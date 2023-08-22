using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    /// <inheritdoc />
    public partial class mi_enum1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_csFriendDbMcsQuoteDbM_Friends_FriendDbMFriendId",
                schema: "supusr",
                table: "csFriendDbMcsQuoteDbM");

            migrationBuilder.RenameColumn(
                name: "FriendDbMFriendId",
                schema: "supusr",
                table: "csFriendDbMcsQuoteDbM",
                newName: "FriendsDbMFriendId");

            migrationBuilder.AddColumn<string>(
                name: "strKind",
                schema: "supusr",
                table: "Pets",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "strMood",
                schema: "supusr",
                table: "Pets",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_csFriendDbMcsQuoteDbM_Friends_FriendsDbMFriendId",
                schema: "supusr",
                table: "csFriendDbMcsQuoteDbM",
                column: "FriendsDbMFriendId",
                principalSchema: "supusr",
                principalTable: "Friends",
                principalColumn: "FriendId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_csFriendDbMcsQuoteDbM_Friends_FriendsDbMFriendId",
                schema: "supusr",
                table: "csFriendDbMcsQuoteDbM");

            migrationBuilder.DropColumn(
                name: "strKind",
                schema: "supusr",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "strMood",
                schema: "supusr",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "FriendsDbMFriendId",
                schema: "supusr",
                table: "csFriendDbMcsQuoteDbM",
                newName: "FriendDbMFriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_csFriendDbMcsQuoteDbM_Friends_FriendDbMFriendId",
                schema: "supusr",
                table: "csFriendDbMcsQuoteDbM",
                column: "FriendDbMFriendId",
                principalSchema: "supusr",
                principalTable: "Friends",
                principalColumn: "FriendId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
