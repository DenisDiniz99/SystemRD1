using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemRD1.Data.Migrations
{
    public partial class Field_FirstName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FisrtName",
                table: "Customers",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customers",
                newName: "FisrtName");
        }
    }
}
