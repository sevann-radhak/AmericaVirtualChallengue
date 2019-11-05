using Microsoft.EntityFrameworkCore.Migrations;

namespace AmericaVirtualChallengue.Web.Migrations
{
    public partial class ChangeSaleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OptionId",
                table: "Sales",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_OptionId",
                table: "Sales",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Options_OptionId",
                table: "Sales",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Options_OptionId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_OptionId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "Sales");
        }
    }
}
