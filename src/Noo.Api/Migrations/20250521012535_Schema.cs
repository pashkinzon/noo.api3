using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noo.Api.Migrations
{
    /// <inheritdoc />
    public partial class Schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subject",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subject", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "VARCHAR(200)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telegram_id = table.Column<string>(type: "VARCHAR(200)", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telegram_username = table.Column<string>(type: "VARCHAR(255)", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "ENUM('student', 'mentor', 'assistant', 'teacher', 'admin')", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_blocked = table.Column<bool>(type: "TINYINT(1)", nullable: false),
                    is_verified = table.Column<bool>(type: "TINYINT(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "work",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    title = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "VARCHAR(50)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubjectModelId = table.Column<byte[]>(type: "BINARY(16)", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_subject_SubjectModelId",
                        column: x => x.SubjectModelId,
                        principalTable: "subject",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "mentor_assignment",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    mentor_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    student_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    subject_id = table.Column<byte[]>(type: "BINARY(16)", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mentor_assignment", x => x.id);
                    table.ForeignKey(
                        name: "FK_mentor_assignment_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_mentor_assignment_user_mentor_id",
                        column: x => x.mentor_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mentor_assignment_user_student_id",
                        column: x => x.student_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "work_task",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    content = table.Column<string>(type: "JSON", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    solve_hint = table.Column<string>(type: "JSON", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    explanation = table.Column<string>(type: "JSON", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    right_answer = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "VARCHAR(50)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    check_strategy = table.Column<string>(type: "VARCHAR(50)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    max_score = table.Column<int>(type: "INT", nullable: false),
                    show_answer_before_check = table.Column<bool>(type: "TINYINT(1)", nullable: false),
                    check_one_by_one = table.Column<bool>(type: "TINYINT(1)", nullable: false),
                    work_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true),
                    order = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_task", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_task_work_work_id",
                        column: x => x.work_id,
                        principalTable: "work",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_mentor_assignment_mentor_id",
                table: "mentor_assignment",
                column: "mentor_id");

            migrationBuilder.CreateIndex(
                name: "IX_mentor_assignment_student_id_mentor_id_subject_id",
                table: "mentor_assignment",
                columns: new[] { "student_id", "mentor_id", "subject_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mentor_assignment_subject_id",
                table: "mentor_assignment",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_SubjectModelId",
                table: "work",
                column: "SubjectModelId");

            migrationBuilder.CreateIndex(
                name: "IX_work_task_work_id",
                table: "work_task",
                column: "work_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mentor_assignment");

            migrationBuilder.DropTable(
                name: "work_task");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "work");

            migrationBuilder.DropTable(
                name: "subject");
        }
    }
}
