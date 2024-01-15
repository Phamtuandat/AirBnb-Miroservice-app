using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyApi.Migrations
{
    /// <inheritdoc />
    public partial class ResetupThePropertyAndMeidaFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Properties_PropertyId",
                table: "Medias");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Medias",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsThumb",
                table: "Medias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Properties_PropertyId",
                table: "Medias",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Properties_PropertyId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "IsThumb",
                table: "Medias");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Medias",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Properties_PropertyId",
                table: "Medias",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
