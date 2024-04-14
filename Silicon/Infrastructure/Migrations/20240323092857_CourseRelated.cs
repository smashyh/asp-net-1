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
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

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
                name: "CourseCreators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YouTubeSubscriberCount = table.Column<int>(type: "int", nullable: false),
                    FacebookFollowerCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCreators", x => x.Id);
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
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_CourseEntities_CourseCreators_CourseCreatorId",
                        column: x => x.CourseCreatorId,
                        principalTable: "CourseCreators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseBadges",
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
                    table.PrimaryKey("PK_CourseBadges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseBadges_CourseEntities_CourseEntityId",
                        column: x => x.CourseEntityId,
                        principalTable: "CourseEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseBadges_CourseEntityId",
                table: "CourseBadges",
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
                name: "CourseBadges");

            migrationBuilder.DropTable(
                name: "CourseEntities");

            migrationBuilder.DropTable(
                name: "CourseCategories");

            migrationBuilder.DropTable(
                name: "CourseCreators");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
