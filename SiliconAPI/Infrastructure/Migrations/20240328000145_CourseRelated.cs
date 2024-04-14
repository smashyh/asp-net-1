using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseRelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseCreatorEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YouTubeSubscriberCount = table.Column<int>(type: "int", nullable: true),
                    FacebookFollowerCount = table.Column<int>(type: "int", nullable: true),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCreatorEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    CourseLengthHours = table.Column<int>(type: "int", nullable: false),
                    CourseCreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatYoullLearn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnDemandVideoHourCount = table.Column<int>(type: "int", nullable: true),
                    ArticleCount = table.Column<int>(type: "int", nullable: true),
                    DownloadableResourceCount = table.Column<int>(type: "int", nullable: true),
                    HasFullLifetimeAccess = table.Column<bool>(type: "bit", nullable: true),
                    HasCertificateOfCompletion = table.Column<bool>(type: "bit", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "money", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseThumbnailImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseEntities_CourseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CourseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseEntities_CourseCreatorEntities_CourseCreatorId",
                        column: x => x.CourseCreatorId,
                        principalTable: "CourseCreatorEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseBadgeEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BadgeLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackgroundColorStyling = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorStyling = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Important = table.Column<bool>(type: "bit", nullable: false),
                    CourseEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseBadgeEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseBadgeEntities_CourseEntities_CourseEntityId",
                        column: x => x.CourseEntityId,
                        principalTable: "CourseEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseBadgeEntities_CourseEntityId",
                table: "CourseBadgeEntities",
                column: "CourseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEntities_CategoryId",
                table: "CourseEntities",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEntities_CourseCreatorId",
                table: "CourseEntities",
                column: "CourseCreatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseBadgeEntities");

            migrationBuilder.DropTable(
                name: "CourseEntities");

            migrationBuilder.DropTable(
                name: "CourseCategories");

            migrationBuilder.DropTable(
                name: "CourseCreatorEntities");
        }
    }
}
