using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FC_MVVC.data.migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    MeasureType = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PublicInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    LogDate = table.Column<DateTime>(nullable: false),
                    WeightValue = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "FirstName", "LastName", "MeasureType", "PublicInfo" },
                values: new object[] { "2204dae4-6cb2-4dbd-aac8-972d486ed767", 0, "1a148bbb-a33f-4f3c-a38a-53b640e58c14", "ApplicationUser", "ana.anic@gmail.com", false, false, null, "ANA.ANIC@GMAIL.COM", "ANA.ANIC@GMAIL.COM", "AQAAAAEAACcQAAAAELDDBjldTca23egUkVYyY+T1RPphJETIEqcDq142PV6dR2hy4Zbu0d7VHlmzZDVrOg==", null, false, "d790a802-fd7c-418a-972e-e051f6d1601a", false, "ana.anic@gmail.com", "Ana", "Anic", 0, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "FirstName", "LastName", "MeasureType", "PublicInfo" },
                values: new object[] { "a5ee4b19-904d-4834-9faf-3074b29c6551", 0, "f6fbdcdc-42ce-49a6-8630-faf4ec7e9e77", "ApplicationUser", "pero.peric@gmail.com", false, false, null, "PERO.PERIC@GMAIL.COM", "PERO.PERIC@GMAIL.COM", "AQAAAAEAACcQAAAAELDDBjldTca23egUkVYyY+T1RPphJETIEqcDq142PV6dR2hy4Zbu0d7VHlmzZDVrOg==", null, false, "735d6606-924b-42d1-8e47-9993de0f5585", false, "pero.peric@gmail.com", "Pero", "Peric", 0, null });

            migrationBuilder.InsertData(
                table: "Logs",
                columns: new[] { "Id", "LogDate", "UserId", "WeightValue" },
                values: new object[,]
                {
                    { new Guid("b5b0244f-b0b1-4c1e-83f6-e6c533ef6ecc"), new DateTime(2018, 10, 2, 10, 47, 48, 543, DateTimeKind.Local), "2204dae4-6cb2-4dbd-aac8-972d486ed767", 80f },
                    { new Guid("70866753-a2f6-41aa-9fb9-fb1ae1da0b83"), new DateTime(2018, 10, 9, 10, 47, 48, 543, DateTimeKind.Local), "a5ee4b19-904d-4834-9faf-3074b29c6551", 87f },
                    { new Guid("3b1513dd-9884-4f28-8dd0-8e4ed4bb27f4"), new DateTime(2018, 10, 8, 10, 47, 48, 543, DateTimeKind.Local), "a5ee4b19-904d-4834-9faf-3074b29c6551", 86f },
                    { new Guid("d54d3156-dda1-48e3-a63d-b56d8cab1278"), new DateTime(2018, 10, 7, 10, 47, 48, 543, DateTimeKind.Local), "a5ee4b19-904d-4834-9faf-3074b29c6551", 85f },
                    { new Guid("98ca5f55-4c40-492f-b4ce-bff7acfcb994"), new DateTime(2018, 10, 6, 10, 47, 48, 543, DateTimeKind.Local), "a5ee4b19-904d-4834-9faf-3074b29c6551", 84f },
                    { new Guid("7992d114-4988-4493-ad8c-259d164b8ae0"), new DateTime(2018, 10, 5, 10, 47, 48, 543, DateTimeKind.Local), "a5ee4b19-904d-4834-9faf-3074b29c6551", 83f },
                    { new Guid("abe94bfc-c620-45cc-b35c-e00b217a3b45"), new DateTime(2018, 10, 4, 10, 47, 48, 543, DateTimeKind.Local), "a5ee4b19-904d-4834-9faf-3074b29c6551", 82f },
                    { new Guid("f1ad5f1d-cc03-4b20-aed8-d75919fe3e6e"), new DateTime(2018, 10, 3, 10, 47, 48, 543, DateTimeKind.Local), "a5ee4b19-904d-4834-9faf-3074b29c6551", 81f },
                    { new Guid("e8d197c7-9731-4520-9c62-2f5c9d90b466"), new DateTime(2018, 10, 2, 10, 47, 48, 541, DateTimeKind.Local), "a5ee4b19-904d-4834-9faf-3074b29c6551", 80f },
                    { new Guid("b11896fd-704a-48a5-98e7-dc39387ad9bc"), new DateTime(2018, 10, 11, 10, 47, 48, 543, DateTimeKind.Local), "2204dae4-6cb2-4dbd-aac8-972d486ed767", 89f },
                    { new Guid("89a04c45-73a5-4d4e-9a27-444b9a16ce25"), new DateTime(2018, 10, 10, 10, 47, 48, 543, DateTimeKind.Local), "2204dae4-6cb2-4dbd-aac8-972d486ed767", 88f },
                    { new Guid("0597837d-d31b-4216-960d-16c3faa12a3c"), new DateTime(2018, 10, 9, 10, 47, 48, 543, DateTimeKind.Local), "2204dae4-6cb2-4dbd-aac8-972d486ed767", 87f },
                    { new Guid("0fd39b10-434d-4dcf-a17c-f2cb1634497b"), new DateTime(2018, 10, 8, 10, 47, 48, 543, DateTimeKind.Local), "2204dae4-6cb2-4dbd-aac8-972d486ed767", 86f },
                    { new Guid("3871ff75-c3ce-446c-8bfa-264b9e8c6b8e"), new DateTime(2018, 10, 7, 10, 47, 48, 543, DateTimeKind.Local), "2204dae4-6cb2-4dbd-aac8-972d486ed767", 85f },
                    { new Guid("c8fd7f2e-b217-4edc-aebb-e0c4dd7ae17e"), new DateTime(2018, 10, 6, 10, 47, 48, 543, DateTimeKind.Local), "2204dae4-6cb2-4dbd-aac8-972d486ed767", 84f },
                    { new Guid("3628b1eb-bd1a-4e26-8dc1-cd9aebafc24e"), new DateTime(2018, 10, 5, 10, 47, 48, 543, DateTimeKind.Local), "2204dae4-6cb2-4dbd-aac8-972d486ed767", 83f },
                    { new Guid("44af1c40-d762-431f-b986-fcb1b76c0517"), new DateTime(2018, 10, 4, 10, 47, 48, 543, DateTimeKind.Local), "2204dae4-6cb2-4dbd-aac8-972d486ed767", 82f },
                    { new Guid("4946a265-2a23-4091-b857-ea039ddc1bb6"), new DateTime(2018, 10, 3, 10, 47, 48, 543, DateTimeKind.Local), "2204dae4-6cb2-4dbd-aac8-972d486ed767", 81f },
                    { new Guid("4ab38c8a-d986-4251-a0cd-19fc4aa5e624"), new DateTime(2018, 10, 10, 10, 47, 48, 543, DateTimeKind.Local), "a5ee4b19-904d-4834-9faf-3074b29c6551", 88f },
                    { new Guid("53dc148a-cdaf-4f90-93ab-4766ba859292"), new DateTime(2018, 10, 11, 10, 47, 48, 543, DateTimeKind.Local), "a5ee4b19-904d-4834-9faf-3074b29c6551", 89f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
