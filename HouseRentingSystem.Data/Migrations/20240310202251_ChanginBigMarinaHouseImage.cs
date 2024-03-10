using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Data.Migrations
{
    public partial class ChanginBigMarinaHouseImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("294cf165-4e29-49b9-b8d1-f718bd43964c"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("3c73b4f6-572d-4444-9fc1-5e690af7bc51"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("4d415835-dcb9-4265-95c6-d4fa19b02268"));

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("62f2f316-0fad-4e7b-a114-d468aac44529"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("b2caf84b-c861-4971-b5a9-d88fb49c1d63"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 2, "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("df7b8b0d-c672-495f-8c97-ed60b9bba3cf"), "North London, UK (near the border)", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://images.rosewoodhotels.com/is/image/rwhg/rosewoodmiramarbeach-beachhousesuiteterrace-1:WIDE-LARGE-16-9", 2100.00m, new Guid("89acdcd8-815e-48a9-8fac-08dc3f924ad4"), "Big House Marina" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("62f2f316-0fad-4e7b-a114-d468aac44529"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("b2caf84b-c861-4971-b5a9-d88fb49c1d63"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("df7b8b0d-c672-495f-8c97-ed60b9bba3cf"));

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("294cf165-4e29-49b9-b8d1-f718bd43964c"), "North London, UK (near the border)", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2100.00m, new Guid("89acdcd8-815e-48a9-8fac-08dc3f924ad4"), "Big House Marina" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("3c73b4f6-572d-4444-9fc1-5e690af7bc51"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("4d415835-dcb9-4265-95c6-d4fa19b02268"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("2c11e7b0-06b1-44d3-b924-a7514ce7d1f8"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" });
        }
    }
}
