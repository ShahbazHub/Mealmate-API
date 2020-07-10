using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Mealmate");

            migrationBuilder.EnsureSchema(
                name: "Lookup");

            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "VARCHAR(25)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(350)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(25)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionItem",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId1",
                        column: x => x.RoleId1,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    RoleId1 = table.Column<int>(nullable: true),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId1",
                        column: x => x.RoleId1,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.LoginProvider, x.UserId, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                schema: "Mealmate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(1000)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurant_User",
                        column: x => x.OwnerId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "Mealmate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR(1000)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    RestaurantId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branch_Restaurant",
                        column: x => x.RestaurantId,
                        principalSchema: "Mealmate",
                        principalTable: "Restaurant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "Mealmate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    BranchId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Mealmate",
                        principalTable: "Branch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "Mealmate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    ServiceTime = table.Column<TimeSpan>(type: "TIME(7)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    BranchId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Mealmate",
                        principalTable: "Branch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Table",
                schema: "Mealmate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    LocationId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Location",
                        column: x => x.LocationId,
                        principalSchema: "Mealmate",
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                schema: "Mealmate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    MenuId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItem_Menu",
                        column: x => x.MenuId,
                        principalSchema: "Mealmate",
                        principalTable: "Menu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QRCode",
                schema: "Mealmate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    TableId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QRCode_Table",
                        column: x => x.TableId,
                        principalSchema: "Mealmate",
                        principalTable: "Table",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuItemOption",
                schema: "Mealmate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(10, 2)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    MenuItemId = table.Column<int>(type: "INT", nullable: false),
                    OptionItemId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemOption_MenuItem",
                        column: x => x.MenuItemId,
                        principalSchema: "Mealmate",
                        principalTable: "MenuItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MenuItemOption_OptionItem",
                        column: x => x.OptionItemId,
                        principalSchema: "Lookup",
                        principalTable: "OptionItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "Identity",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId1",
                schema: "Identity",
                table: "RoleClaim",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "Identity",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId1",
                schema: "Identity",
                table: "UserClaim",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "Identity",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId1",
                schema: "Identity",
                table: "UserLogin",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "Identity",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId1",
                schema: "Identity",
                table: "UserRole",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId1",
                schema: "Identity",
                table: "UserRole",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserId",
                schema: "Identity",
                table: "UserToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserId1",
                schema: "Identity",
                table: "UserToken",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_RestaurantId",
                schema: "Mealmate",
                table: "Branch",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_BranchId",
                schema: "Mealmate",
                table: "Location",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_BranchId",
                schema: "Mealmate",
                table: "Menu",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuId",
                schema: "Mealmate",
                table: "MenuItem",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOption_MenuItemId",
                schema: "Mealmate",
                table: "MenuItemOption",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOption_OptionItemId",
                schema: "Mealmate",
                table: "MenuItemOption",
                column: "OptionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_QRCode_TableId",
                schema: "Mealmate",
                table: "QRCode",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_OwnerId",
                schema: "Mealmate",
                table: "Restaurant",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_LocationId",
                schema: "Mealmate",
                table: "Table",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "MenuItemOption",
                schema: "Mealmate");

            migrationBuilder.DropTable(
                name: "QRCode",
                schema: "Mealmate");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "MenuItem",
                schema: "Mealmate");

            migrationBuilder.DropTable(
                name: "OptionItem",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "Table",
                schema: "Mealmate");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "Mealmate");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "Mealmate");

            migrationBuilder.DropTable(
                name: "Branch",
                schema: "Mealmate");

            migrationBuilder.DropTable(
                name: "Restaurant",
                schema: "Mealmate");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Identity");
        }
    }
}
