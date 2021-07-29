using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    MemberStatus = table.Column<int>(nullable: false),
                    IsOwner = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    RelationShip = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberDetails",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    NickName = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberDetails_Members_Id",
                        column: x => x.Id,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberHistories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MemberId = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Detail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberHistories_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberPayments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MemberId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsScheduled = table.Column<bool>(nullable: false),
                    PaymentType = table.Column<int>(nullable: false),
                    PaymentAmount = table.Column<double>(nullable: false),
                    PaymentTotal = table.Column<double>(nullable: false),
                    TaxPercentage = table.Column<double>(nullable: false),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberPayments_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberVisitors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MemberId = table.Column<string>(nullable: false),
                    VisitorType = table.Column<int>(nullable: false),
                    AccessesDate = table.Column<DateTime>(nullable: true),
                    VisitorStatus = table.Column<int>(nullable: false),
                    Gate = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberVisitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberVisitors_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledPayments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MemberPaymentId = table.Column<string>(nullable: false),
                    PaymentAmount = table.Column<double>(nullable: false),
                    PaymentDueDate = table.Column<DateTime>(nullable: false),
                    FulfiledDate = table.Column<DateTime>(nullable: true),
                    Fulfiled = table.Column<bool>(nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledPayments_MemberPayments_MemberPaymentId",
                        column: x => x.MemberPaymentId,
                        principalTable: "MemberPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberHistories_MemberId",
                table: "MemberHistories",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberPayments_MemberId",
                table: "MemberPayments",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberVisitors_MemberId",
                table: "MemberVisitors",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPayments_MemberPaymentId",
                table: "ScheduledPayments",
                column: "MemberPaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberDetails");

            migrationBuilder.DropTable(
                name: "MemberHistories");

            migrationBuilder.DropTable(
                name: "MemberVisitors");

            migrationBuilder.DropTable(
                name: "ScheduledPayments");

            migrationBuilder.DropTable(
                name: "MemberPayments");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
