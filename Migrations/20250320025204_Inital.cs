using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricalProspectingProfiling.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Имя = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Контакты = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Geodesist",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Имя = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Контакты = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geodesist", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Squares",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    КоординатыID = table.Column<int>(type: "int", nullable: false),
                    Название = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Высота = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squares", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CoordinatsProfile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XКоордината = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YКоордината = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SquareID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoordinatsProfile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CoordinatsProfile_Squares_SquareID",
                        column: x => x.SquareID,
                        principalTable: "Squares",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoordinatsSquare",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XКоордината = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YКоордината = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SquareID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoordinatsSquare", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CoordinatsSquare_Squares_SquareID",
                        column: x => x.SquareID,
                        principalTable: "Squares",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ПлощадьID = table.Column<int>(type: "int", nullable: false),
                    КоординатыID = table.Column<int>(type: "int", nullable: false),
                    МетодПрофилирования = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Profile_CoordinatsProfile_КоординатыID",
                        column: x => x.КоординатыID,
                        principalTable: "CoordinatsProfile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Profile_Squares_ПлощадьID",
                        column: x => x.ПлощадьID,
                        principalTable: "Squares",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Picket",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ПрофильID = table.Column<int>(type: "int", nullable: false),
                    Координаты = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Номер = table.Column<int>(type: "int", nullable: false),
                    Дистанция = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picket", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Picket_Profile_ПрофильID",
                        column: x => x.ПрофильID,
                        principalTable: "Profile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measurement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ПикетID = table.Column<int>(type: "int", nullable: false),
                    Дата = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ТипПрофилирования = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ДистанцияМеждуЭлектродами = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ток = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Вольтаж = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Сопротивление = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Температура = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Measurement_Picket_ПикетID",
                        column: x => x.ПикетID,
                        principalTable: "Picket",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeologicalData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ИзмеренияID = table.Column<int>(type: "int", nullable: false),
                    ГеодезистID = table.Column<int>(type: "int", nullable: false),
                    ТипПороды = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ОписаниеСтруктуры = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Загрязнение = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeologicalData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GeologicalData_Geodesist_ГеодезистID",
                        column: x => x.ГеодезистID,
                        principalTable: "Geodesist",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeologicalData_Measurement_ИзмеренияID",
                        column: x => x.ИзмеренияID,
                        principalTable: "Measurement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    КлиентID = table.Column<int>(type: "int", nullable: false),
                    ГеологическиеДанныеID = table.Column<int>(type: "int", nullable: false),
                    ПлощадьID = table.Column<int>(type: "int", nullable: false),
                    Контакты = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    НачалоДата = table.Column<DateTime>(type: "datetime2", nullable: false),
                    КонецДата = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contracts_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_GeologicalData_ГеологическиеДанныеID",
                        column: x => x.ГеологическиеДанныеID,
                        principalTable: "GeologicalData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Squares_ПлощадьID",
                        column: x => x.ПлощадьID,
                        principalTable: "Squares",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ГеологическиеДанныеID",
                table: "Contracts",
                column: "ГеологическиеДанныеID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ПлощадьID",
                table: "Contracts",
                column: "ПлощадьID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerID",
                table: "Contracts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CoordinatsProfile_SquareID",
                table: "CoordinatsProfile",
                column: "SquareID");

            migrationBuilder.CreateIndex(
                name: "IX_CoordinatsSquare_SquareID",
                table: "CoordinatsSquare",
                column: "SquareID");

            migrationBuilder.CreateIndex(
                name: "IX_GeologicalData_ГеодезистID",
                table: "GeologicalData",
                column: "ГеодезистID");

            migrationBuilder.CreateIndex(
                name: "IX_GeologicalData_ИзмеренияID",
                table: "GeologicalData",
                column: "ИзмеренияID");

            migrationBuilder.CreateIndex(
                name: "IX_Measurement_ПикетID",
                table: "Measurement",
                column: "ПикетID");

            migrationBuilder.CreateIndex(
                name: "IX_Picket_ПрофильID",
                table: "Picket",
                column: "ПрофильID");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_КоординатыID",
                table: "Profile",
                column: "КоординатыID");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_ПлощадьID",
                table: "Profile",
                column: "ПлощадьID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "CoordinatsSquare");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "GeologicalData");

            migrationBuilder.DropTable(
                name: "Geodesist");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropTable(
                name: "Picket");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "CoordinatsProfile");

            migrationBuilder.DropTable(
                name: "Squares");
        }
    }
}
