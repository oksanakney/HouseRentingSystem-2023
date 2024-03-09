﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Data.Migrations
{
    public partial class AddCreatedOnColumnToHouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("21000cee-e1d4-48d8-8a19-6eb0d1e1497f"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("480e362e-1249-4e05-bf98-271f7de68b76"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("ecc3c3ea-420b-4368-a375-96d6f4f6f0d4"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Houses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 8, 20, 17, 0, 291, DateTimeKind.Utc).AddTicks(9506));

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("19dc21d5-2fcf-4e27-a98a-756c2e326656"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("563f6abf-eb1a-4189-93c9-b0b9294d4870"), "North London, UK (near the border)", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg", 2100.00m, new Guid("89acdcd8-815e-48a9-8fac-08dc3f924ad4"), "Big House Marina" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("99f50d15-6485-4923-829b-6525f43698f8"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 2, "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("19dc21d5-2fcf-4e27-a98a-756c2e326656"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("563f6abf-eb1a-4189-93c9-b0b9294d4870"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("99f50d15-6485-4923-829b-6525f43698f8"));

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Houses");

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("21000cee-e1d4-48d8-8a19-6eb0d1e1497f"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("480e362e-1249-4e05-bf98-271f7de68b76"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 2, "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("ecc3c3ea-420b-4368-a375-96d6f4f6f0d4"), "North London, UK (near the border)", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg", 2100.00m, new Guid("89acdcd8-815e-48a9-8fac-08dc3f924ad4"), "Big House Marina" });
        }
    }
}
