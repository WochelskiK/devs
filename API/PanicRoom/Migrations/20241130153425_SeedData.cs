using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PanicRoom.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Electrical" },
                    { 2, "Plumbing" },
                    { 3, "Carpentry" }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "AdditionalInformations", "CategoryID", "Created", "Description", "Email", "IssuePriorityEnum", "IssueStatusEnum", "Location", "Name", "ReportedById", "ResolvedById", "Title", "Updated", "UserAssignedID" },
                values: new object[,]
                {
                    { 1, "", 1, new DateTime(2024, 11, 30, 15, 34, 25, 74, DateTimeKind.Utc).AddTicks(575), "Light in hallway is not working.", "", 1, 0, "", "", null, null, "Broken Light", new DateTime(2024, 11, 30, 15, 34, 25, 74, DateTimeKind.Utc).AddTicks(576), null },
                    { 2, "", 2, new DateTime(2024, 11, 30, 15, 34, 25, 74, DateTimeKind.Utc).AddTicks(577), "Pipe under kitchen sink is leaking.", "", 2, 0, "", "", null, null, "Leaking Pipe", new DateTime(2024, 11, 30, 15, 34, 25, 74, DateTimeKind.Utc).AddTicks(577), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
