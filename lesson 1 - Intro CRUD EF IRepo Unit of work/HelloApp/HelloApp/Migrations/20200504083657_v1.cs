using Microsoft.EntityFrameworkCore.Migrations;

namespace HelloApp.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>("Height", "Users", nullable: false, defaultValue: 100);
                
                //AlterTable(
                //name: "Users",
                //schema: table => new
                //{
                //    Id = table.Column<int>(nullable: false)
                //        .Annotation("SqlServer:Identity", "1, 1"),
                //    Name = table.Column<string>(nullable: true),
                //    Age = table.Column<int>(nullable: false),
                //    Height = table.Column<int>(nullable: false)
                //}
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
