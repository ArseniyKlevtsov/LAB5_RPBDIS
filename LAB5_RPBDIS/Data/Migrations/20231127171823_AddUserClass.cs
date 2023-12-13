using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayTrafficSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmployeeTrain",
                columns: table => new
                {
                    EmployeesEmployeeId = table.Column<int>(type: "int", nullable: false),
                    TrainsTrainId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTrain", x => new { x.EmployeesEmployeeId, x.TrainsTrainId });
                    table.ForeignKey(
                        name: "FK_EmployeeTrain_Employees_EmployeesEmployeeId",
                        column: x => x.EmployeesEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTrain_Trains_TrainsTrainId",
                        column: x => x.TrainsTrainId,
                        principalTable: "Trains",
                        principalColumn: "TrainId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StopTrain",
                columns: table => new
                {
                    StopsStopId = table.Column<int>(type: "int", nullable: false),
                    TrainsTrainId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopTrain", x => new { x.StopsStopId, x.TrainsTrainId });
                    table.ForeignKey(
                        name: "FK_StopTrain_Stops_StopsStopId",
                        column: x => x.StopsStopId,
                        principalTable: "Stops",
                        principalColumn: "StopId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StopTrain_Trains_TrainsTrainId",
                        column: x => x.TrainsTrainId,
                        principalTable: "Trains",
                        principalColumn: "TrainId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTrain_TrainsTrainId",
                table: "EmployeeTrain",
                column: "TrainsTrainId");

            migrationBuilder.CreateIndex(
                name: "IX_StopTrain_TrainsTrainId",
                table: "StopTrain",
                column: "TrainsTrainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTrain");

            migrationBuilder.DropTable(
                name: "StopTrain");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "AspNetUsers");
        }
    }
}
