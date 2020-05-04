using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatServer.Migrations
{
    public partial class AddTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    UserIdTo = table.Column<string>(nullable: false),
                    UserIdFrom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => new { x.UserIdTo, x.UserIdFrom });
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_UserIdFrom",
                        column: x => x.UserIdFrom,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_UserIdTo",
                        column: x => x.UserIdTo,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserIdFrom",
                table: "Requests",
                column: "UserIdFrom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
