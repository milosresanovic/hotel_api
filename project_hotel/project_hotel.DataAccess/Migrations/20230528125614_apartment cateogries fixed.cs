using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_hotel.DataAccess.Migrations
{
    public partial class apartmentcateogriesfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApartmentCategoryId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Apartments");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Apartments",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AverageRating",
                table: "Apartments",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(700)",
                oldMaxLength: 700);

            migrationBuilder.AlterColumn<float>(
                name: "AverageRating",
                table: "Apartments",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentCategoryId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
