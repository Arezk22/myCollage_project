using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp.netDay2.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Top_Id = table.Column<int>(type: "int", nullable: false),
                    Top_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Top_Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Crs_Id = table.Column<int>(type: "int", nullable: false),
                    Crs_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Crs_Duration = table.Column<int>(type: "int", nullable: true),
                    Top_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Crs_Id);
                    table.ForeignKey(
                        name: "FK_Course_Topic",
                        column: x => x.Top_Id,
                        principalTable: "Topic",
                        principalColumn: "Top_Id");
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Dept_Id = table.Column<int>(type: "int", nullable: false),
                    Dept_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Dept_Desc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Dept_Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Dept_Manager = table.Column<int>(type: "int", nullable: true),
                    Manager_hiredate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Dept_Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    Ins_Id = table.Column<int>(type: "int", nullable: false),
                    Ins_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ins_Degree = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Salary = table.Column<decimal>(type: "money", nullable: false),
                    Dept_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.Ins_Id);
                    table.ForeignKey(
                        name: "FK_Instructor_Department",
                        column: x => x.Dept_Id,
                        principalTable: "Department",
                        principalColumn: "Dept_Id");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    St_Id = table.Column<int>(type: "int", nullable: false),
                    St_Fname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    St_Lname = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    St_Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    St_Age = table.Column<int>(type: "int", nullable: true),
                    Dept_Id = table.Column<int>(type: "int", nullable: true),
                    St_super = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.St_Id);
                    table.ForeignKey(
                        name: "FK_Student_Department",
                        column: x => x.Dept_Id,
                        principalTable: "Department",
                        principalColumn: "Dept_Id");
                    table.ForeignKey(
                        name: "FK_Student_Student",
                        column: x => x.St_super,
                        principalTable: "Student",
                        principalColumn: "St_Id");
                });

            migrationBuilder.CreateTable(
                name: "Ins_Course",
                columns: table => new
                {
                    Ins_Id = table.Column<int>(type: "int", nullable: false),
                    Crs_Id = table.Column<int>(type: "int", nullable: false),
                    Evaluation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ins_Course", x => new { x.Ins_Id, x.Crs_Id });
                    table.ForeignKey(
                        name: "FK_Ins_Course_Course",
                        column: x => x.Crs_Id,
                        principalTable: "Course",
                        principalColumn: "Crs_Id");
                    table.ForeignKey(
                        name: "FK_Ins_Course_Instructor",
                        column: x => x.Ins_Id,
                        principalTable: "Instructor",
                        principalColumn: "Ins_Id");
                });

            migrationBuilder.CreateTable(
                name: "Stud_Course",
                columns: table => new
                {
                    Crs_Id = table.Column<int>(type: "int", nullable: false),
                    St_Id = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stud_Course", x => new { x.Crs_Id, x.St_Id });
                    table.ForeignKey(
                        name: "FK_Stud_Course_Course",
                        column: x => x.Crs_Id,
                        principalTable: "Course",
                        principalColumn: "Crs_Id");
                    table.ForeignKey(
                        name: "FK_Stud_Course_Student",
                        column: x => x.St_Id,
                        principalTable: "Student",
                        principalColumn: "St_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_Top_Id",
                table: "Course",
                column: "Top_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Dept_Manager",
                table: "Department",
                column: "Dept_Manager");

            migrationBuilder.CreateIndex(
                name: "IX_Ins_Course_Crs_Id",
                table: "Ins_Course",
                column: "Crs_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_Dept_Id",
                table: "Instructor",
                column: "Dept_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stud_Course_St_Id",
                table: "Stud_Course",
                column: "St_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Dept_Id",
                table: "Student",
                column: "Dept_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_St_super",
                table: "Student",
                column: "St_super");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Instructor",
                table: "Department",
                column: "Dept_Manager",
                principalTable: "Instructor",
                principalColumn: "Ins_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Instructor",
                table: "Department");

            migrationBuilder.DropTable(
                name: "Ins_Course");

            migrationBuilder.DropTable(
                name: "Stud_Course");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
