using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitectureExample.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "RequestShippings",
                columns: table => new
                {
                    RequestShippingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NgayChotDon = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserProfileUserId = table.Column<string>(type: "text", nullable: false),
                    RecipientId = table.Column<int>(type: "integer", nullable: false),
                    PageId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false),
                    ShippingInfoId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    NgayDoiSoat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestShippings", x => x.RequestShippingId);
                    table.ForeignKey(
                        name: "FK_RequestShippings_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestShippings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestShippings_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestShippings_ShippingInfos_ShippingInfoId",
                        column: x => x.ShippingInfoId,
                        principalTable: "ShippingInfos",
                        principalColumn: "ShippingInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestShippings_UserProfiles_UserProfileUserId",
                        column: x => x.UserProfileUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

           
            migrationBuilder.CreateIndex(
                name: "IX_RequestShippings_PageId",
                table: "RequestShippings",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestShippings_ProductId",
                table: "RequestShippings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestShippings_RecipientId",
                table: "RequestShippings",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestShippings_ShippingInfoId",
                table: "RequestShippings",
                column: "ShippingInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestShippings_UserProfileUserId",
                table: "RequestShippings",
                column: "UserProfileUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.DropTable(
                name: "RequestShippings");

        }
    }
}
