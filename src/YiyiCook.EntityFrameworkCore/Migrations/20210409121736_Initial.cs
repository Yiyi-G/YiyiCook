using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiyiCook.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fcid = table.Column<long>(nullable: true),
                    name = table.Column<string>(maxLength: 20, nullable: false),
                    description = table.Column<string>(maxLength: 200, nullable: false),
                    headImgUrl = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    videoUrl = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    produceVideoUrl = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    isEnabled = table.Column<bool>(nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoodClassfy",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 20, nullable: false),
                    isEnabled = table.Column<bool>(nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodClassfy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoodImg",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fid = table.Column<long>(nullable: false),
                    url = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    isEnabled = table.Column<bool>(nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodImg", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoodIngredient",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fiid = table.Column<long>(nullable: false),
                    fid = table.Column<long>(nullable: false),
                    num = table.Column<int>(nullable: false),
                    description = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoodIngredientSource",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 20, nullable: false),
                    unitName = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredientSource", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoodOrder",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    type = table.Column<byte>(nullable: false),
                    state = table.Column<byte>(nullable: false),
                    description = table.Column<string>(maxLength: 200, nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOrder", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoodOrderItem",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    foid = table.Column<long>(nullable: false),
                    fid = table.Column<long>(nullable: false),
                    num = table.Column<int>(nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: true),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOrderItem", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoodProduceProcess",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fid = table.Column<long>(nullable: false),
                    rankNum = table.Column<int>(nullable: false),
                    description = table.Column<string>(maxLength: 20, nullable: true),
                    isEnabled = table.Column<bool>(nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodProduceProcess", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoodProduceProcessImg",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fppid = table.Column<long>(nullable: false),
                    url = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    isEnabled = table.Column<bool>(nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodProduceProcessImg", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "FoodClassfy");

            migrationBuilder.DropTable(
                name: "FoodImg");

            migrationBuilder.DropTable(
                name: "FoodIngredient");

            migrationBuilder.DropTable(
                name: "FoodIngredientSource");

            migrationBuilder.DropTable(
                name: "FoodOrder");

            migrationBuilder.DropTable(
                name: "FoodOrderItem");

            migrationBuilder.DropTable(
                name: "FoodProduceProcess");

            migrationBuilder.DropTable(
                name: "FoodProduceProcessImg");
        }
    }
}
