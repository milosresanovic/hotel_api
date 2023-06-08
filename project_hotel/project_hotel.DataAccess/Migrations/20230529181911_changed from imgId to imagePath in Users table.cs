using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_hotel.DataAccess.Migrations
{
    public partial class changedfromimgIdtoimagePathinUserstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AverageRating",
                table: "Apartments",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Users",
                type: "int",
                nullable: true);

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
    }
}
