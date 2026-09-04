using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(265)", maxLength: 265, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.CheckConstraint("CK_Activities_Category_MinLength", "LEN([Category]) >= 2");
                    table.CheckConstraint("CK_Activities_City_MinLength", "LEN([City]) >= 2");
                    table.CheckConstraint("CK_Activities_Description_MinLength", "LEN([Description]) >= 10");
                    table.CheckConstraint("CK_Activities_Title_MinLength", "LEN([Title]) >= 3");
                    table.CheckConstraint("CK_Activities_Venue_MinLength", "LEN([Venue]) >= 2");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Category_Date",
                table: "Activities",
                columns: new[] { "Category", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_IsCancelled_Date",
                table: "Activities",
                columns: new[] { "IsCancelled", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
