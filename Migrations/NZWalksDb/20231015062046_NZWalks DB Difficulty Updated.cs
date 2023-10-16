using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RESTAPI.Migrations.NZWalksDb
{
    /// <inheritdoc />
    public partial class NZWalksDBDifficultyUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("7d800031-c880-4afe-93f7-eeb5bc2000b6"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8e2c4e45-3398-4e88-9030-d39a3db83f4b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("aaa29505-6c64-4b05-8472-c2a10118ce1b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6891cfcc-69e8-4fa3-961e-fc026b31f0c9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6dddd6f7-8ab1-4315-9fad-fef65fe92553"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d0a71ca8-e216-443a-97ca-094d9ba04e84"));

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2126ddec-1c42-454a-ade8-6a96df39bbba"), "Easy" },
                    { new Guid("43fcfd58-1b65-4ef0-8379-05131d3f7225"), "Medium" },
                    { new Guid("4932477d-cacf-4023-8109-d55e4a2b55cd"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("0986b9f2-dbea-4a16-8419-476f2207c0f5"), "AKL", "IRLAND REGION", "Some_image.jpg" },
                    { new Guid("d8770156-0b55-4700-849a-b8713d9b2731"), "IRL", "ILAND STREET REGION", "Some_image.jpg" },
                    { new Guid("d8e9586b-ba4c-42f1-8bd4-9066bf70bae0"), "DBI", "DUBAI REGION", "Some_image.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("2126ddec-1c42-454a-ade8-6a96df39bbba"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("43fcfd58-1b65-4ef0-8379-05131d3f7225"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("4932477d-cacf-4023-8109-d55e4a2b55cd"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0986b9f2-dbea-4a16-8419-476f2207c0f5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d8770156-0b55-4700-849a-b8713d9b2731"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d8e9586b-ba4c-42f1-8bd4-9066bf70bae0"));

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7d800031-c880-4afe-93f7-eeb5bc2000b6"), "Medium" },
                    { new Guid("8e2c4e45-3398-4e88-9030-d39a3db83f4b"), "Hard" },
                    { new Guid("aaa29505-6c64-4b05-8472-c2a10118ce1b"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("6891cfcc-69e8-4fa3-961e-fc026b31f0c9"), "DBI", "DUBAI REGION", "Some_image.jpg" },
                    { new Guid("6dddd6f7-8ab1-4315-9fad-fef65fe92553"), "AKL", "IRLAND REGION", "Some_image.jpg" },
                    { new Guid("d0a71ca8-e216-443a-97ca-094d9ba04e84"), "IRL", "ILAND STREET REGION", "Some_image.jpg" }
                });
        }
    }
}
