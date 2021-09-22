using Microsoft.EntityFrameworkCore.Migrations;

namespace ZapWeb.Migrations
{
    public partial class AddAlteracaMensagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Mensagens",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Mensagens");
        }
    }
}
