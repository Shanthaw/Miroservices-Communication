using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GloboTicket.Services.EventCatalog.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationEventLogs",
                columns: table => new
                {
                    IntegrationEventLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntegrationEventType = table.Column<string>(nullable: true),
                    ServiceBusTopicName = table.Column<string>(nullable: true),
                    IntegrationEventBody = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLogs", x => x.IntegrationEventLogId);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Artist = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false),
                    VenueId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Concerts" },
                    { new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), "Musicals" },
                    { new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"), "Plays" },
                    { new Guid("fe98f549-e790-4e9f-aa16-18c2292a2ee9"), "Conferences" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueId", "City", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("bcd82a4c-881e-44fc-ba94-c857ab1ab071"), "Toronto", "Canada", "Massey Hall" },
                    { new Guid("45929e17-9f3c-4dd4-8043-126c34733dc5"), "Montreal", "Canada", "L'Olympia" },
                    { new Guid("2d8f80ca-616f-4928-bd09-a1849fec5a9a"), "Vancouver", "Canada", "Commodore Ballroom" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Artist", "CategoryId", "Date", "Description", "ImageUrl", "Name", "Price", "VenueId" },
                values: new object[,]
                {
                    { new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"), "John Elbert", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), new DateTime(2021, 5, 30, 19, 0, 0, 0, DateTimeKind.Unspecified), "Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/banjo.jpg", "John Egbert Live", 65, new Guid("bcd82a4c-881e-44fc-ba94-c857ab1ab071") },
                    { new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"), "Michael Johnson", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), new DateTime(2021, 8, 30, 19, 0, 0, 0, DateTimeKind.Unspecified), "Michael Johnson doesn't need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/michael.jpg", "The State of Affairs: Michael Live!", 85, new Guid("bcd82a4c-881e-44fc-ba94-c857ab1ab071") },
                    { new Guid("b419a7ca-3321-4f38-be8e-4d7b6a529319"), "DJ 'The Mike'", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), new DateTime(2021, 3, 30, 19, 0, 0, 0, DateTimeKind.Unspecified), "DJs from all over the world will compete in this epic battle for eternal fame.", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/dj.jpg", "Clash of the DJs", 85, new Guid("45929e17-9f3c-4dd4-8043-126c34733dc5") },
                    { new Guid("62787623-4c52-43fe-b0c9-b7044fb5929b"), "Manuel Santinonisi", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), new DateTime(2021, 3, 30, 19, 0, 0, 0, DateTimeKind.Unspecified), "Get on the hype of Spanish Guitar concerts with Manuel.", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/guitar.jpg", "Spanish guitar hits with Manuel", 25, new Guid("45929e17-9f3c-4dd4-8043-126c34733dc5") },
                    { new Guid("1babd057-e980-4cb3-9cd2-7fdd9e525668"), "Many", new Guid("fe98f549-e790-4e9f-aa16-18c2292a2ee9"), new DateTime(2021, 9, 30, 19, 0, 0, 0, DateTimeKind.Unspecified), "The best tech conference in the world", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/conf.jpg", "Techorama 2021", 400, new Guid("2d8f80ca-616f-4928-bd09-a1849fec5a9a") },
                    { new Guid("adc42c09-08c1-4d2c-9f96-2d15bb1af299"), "Nick Sailor", new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), new DateTime(2021, 7, 30, 19, 0, 0, 0, DateTimeKind.Unspecified), "The critics are over the moon and so will you after you've watched this sing and dance extravaganza written by Nick Sailor, the man from 'My dad and sister'.", "https://neilmorrisseypluralsight.blob.core.windows.net/imgs/musical.jpg", "To the Moon and Back", 135, new Guid("2d8f80ca-616f-4928-bd09-a1849fec5a9a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "IntegrationEventLogs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
