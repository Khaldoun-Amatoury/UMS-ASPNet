using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace Project.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    MaxStudentsNumber = table.Column<int>(type: "integer", nullable: true),
                    EnrolmentDateRange = table.Column<NpgsqlRange<DateOnly>>(type: "daterange", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("courses_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionTime",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sessiontime_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"Users_id_seq\"'::regclass)"),
                    Name = table.Column<string>(type: "character varying", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    FirebaseId = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pk", x => x.Id);
                    table.ForeignKey(
                        name: "users_role_id_fk",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherPerCourse",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"Class_Id_seq\"'::regclass)"),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"Class_TeacherId_seq\"'::regclass)"),
                    CourseId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"Class_CourseId_seq\"'::regclass)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("class_pk", x => x.Id);
                    table.ForeignKey(
                        name: "class_course_id_fk",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "class_teacher_id_fk",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassEnrollment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassId = table.Column<long>(type: "bigint", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("classenrollment_pk", x => x.Id);
                    table.ForeignKey(
                        name: "classenrollment_class_id_fk",
                        column: x => x.ClassId,
                        principalTable: "TeacherPerCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "classenrollment_users_id_fk",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeacherPerCoursePerSessionTime",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"ClassSessions_Id_seq\"'::regclass)"),
                    TeacherPerCourseId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"ClassSessions_ClassId_seq\"'::regclass)"),
                    SessionTimeId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"ClassSessions_SessionTimeId_seq\"'::regclass)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("classsessions_pk", x => x.Id);
                    table.ForeignKey(
                        name: "classsessions_class_id_fk",
                        column: x => x.TeacherPerCourseId,
                        principalTable: "TeacherPerCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "classsessions_sessiontime_id_fk",
                        column: x => x.SessionTimeId,
                        principalTable: "SessionTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "classenrollment_id_uindex",
                table: "ClassEnrollment",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassEnrollment_ClassId",
                table: "ClassEnrollment",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassEnrollment_StudentId",
                table: "ClassEnrollment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "courses_\"name\"_uindex",
                table: "Courses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "courses_id_uindex",
                table: "Courses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "roles_\"id\"_uindex",
                table: "Roles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "roles_\"name\"_uindex",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "sessiontime_id_uindex",
                table: "SessionTime",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "class_id_uindex",
                table: "TeacherPerCourse",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPerCourse_CourseId",
                table: "TeacherPerCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPerCourse_TeacherId",
                table: "TeacherPerCourse",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "classsessions_id_uindex",
                table: "TeacherPerCoursePerSessionTime",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPerCoursePerSessionTime_SessionTimeId",
                table: "TeacherPerCoursePerSessionTime",
                column: "SessionTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPerCoursePerSessionTime_TeacherPerCourseId",
                table: "TeacherPerCoursePerSessionTime",
                column: "TeacherPerCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "users_\"email\"_uindex",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_\"firebaseid\"_uindex",
                table: "Users",
                column: "FirebaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_\"id\"_uindex",
                table: "Users",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassEnrollment");

            migrationBuilder.DropTable(
                name: "TeacherPerCoursePerSessionTime");

            migrationBuilder.DropTable(
                name: "TeacherPerCourse");

            migrationBuilder.DropTable(
                name: "SessionTime");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
