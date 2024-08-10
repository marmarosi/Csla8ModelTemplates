using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Csla8ModelTemplates.Dal.Oracle.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    FolderKey = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ParentKey = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    RootKey = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    FolderOrder = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    FolderName = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.FolderKey);
                    table.ForeignKey(
                        name: "FK_Folders_Folders_ParentKey",
                        column: x => x.ParentKey,
                        principalTable: "Folders",
                        principalColumn: "FolderKey");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupKey = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    GroupCode = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true),
                    GroupName = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupKey);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonKey = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PersonCode = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true),
                    PersonName = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonKey);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamKey = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TeamGuid = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    TeamCode = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true),
                    TeamName = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamKey);
                });

            migrationBuilder.CreateTable(
                name: "GroupPersons",
                columns: table => new
                {
                    GroupKey = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    PersonKey = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPersons", x => new { x.GroupKey, x.PersonKey });
                    table.ForeignKey(
                        name: "FK_GroupPersons_Groups_GroupKey",
                        column: x => x.GroupKey,
                        principalTable: "Groups",
                        principalColumn: "GroupKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPersons_Persons_PersonKey",
                        column: x => x.PersonKey,
                        principalTable: "Persons",
                        principalColumn: "PersonKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerKey = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TeamKey = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    PlayerCode = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true),
                    PlayerName = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerKey);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamKey",
                        column: x => x.TeamKey,
                        principalTable: "Teams",
                        principalColumn: "TeamKey");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Folders_ParentKey_FolderOrder",
                table: "Folders",
                columns: new[] { "ParentKey", "FolderOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPersons_PersonKey",
                table: "GroupPersons",
                column: "PersonKey");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupCode",
                table: "Groups",
                column: "GroupCode",
                unique: true,
                filter: "\"GroupCode\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonCode",
                table: "Persons",
                column: "PersonCode",
                unique: true,
                filter: "\"PersonCode\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamKey_PlayerCode",
                table: "Players",
                columns: new[] { "TeamKey", "PlayerCode" },
                unique: true,
                filter: "\"TeamKey\" IS NOT NULL AND \"PlayerCode\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamCode",
                table: "Teams",
                column: "TeamCode",
                unique: true,
                filter: "\"TeamCode\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Folders");

            migrationBuilder.DropTable(
                name: "GroupPersons");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
