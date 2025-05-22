using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noo.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    path = table.Column<string>(type: "VARCHAR(255)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    actual_name = table.Column<string>(type: "VARCHAR(255)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extension = table.Column<string>(type: "VARCHAR(127)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    size = table.Column<int>(type: "INT(11)", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "subject",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(63)", maxLength: 63, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    color = table.Column<string>(type: "VARCHAR(63)", maxLength: 63, nullable: false, collation: "utf8mb4_general_ci")
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
                    name = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "VARCHAR(63)", maxLength: 63, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "VARCHAR(255)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telegram_id = table.Column<string>(type: "VARCHAR(63)", maxLength: 63, nullable: true, collation: "utf8mb4_general_ci")
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
                name: "course",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    thumbnail_id = table.Column<byte[]>(type: "BINARY(16)", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_media_thumbnail_id",
                        column: x => x.thumbnail_id,
                        principalTable: "media",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "work",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    title = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "ENUM('Test','MiniTest','Phrase','TrialWork','SecondPart')", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subject_id = table.Column<byte[]>(type: "BINARY(16)", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "course_chapter",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    title = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    color = table.Column<string>(type: "VARCHAR(63)", maxLength: 63, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "TINYINT(1)", nullable: false),
                    course_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    parent_chapter_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true),
                    order = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_chapter", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_chapter_course_chapter_parent_chapter_id",
                        column: x => x.parent_chapter_id,
                        principalTable: "course_chapter",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_chapter_course_course_id",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "course_membership",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    is_active = table.Column<bool>(type: "TINYINT(1)", nullable: false),
                    is_archived = table.Column<bool>(type: "TINYINT(1)", nullable: false),
                    course_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    student_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    assigner_id = table.Column<byte[]>(type: "BINARY(16)", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_membership", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_membership_course_course_id",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_membership_user_assigner_id",
                        column: x => x.assigner_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_course_membership_user_student_id",
                        column: x => x.student_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "course_mm_CoursesAsAuthor_user",
                columns: table => new
                {
                    user_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    course_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_mm_CoursesAsAuthor_user", x => new { x.user_id, x.course_id });
                    table.ForeignKey(
                        name: "FK_course_mm_CoursesAsAuthor_user_course_course_id",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_mm_CoursesAsAuthor_user_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "course_mm_CoursesAsEditor_user",
                columns: table => new
                {
                    course_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    user_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_mm_CoursesAsEditor_user", x => new { x.course_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_course_mm_CoursesAsEditor_user_course_course_id",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_mm_CoursesAsEditor_user_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "course_material_content",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    content = table.Column<string>(type: "JSON", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    work_id = table.Column<byte[]>(type: "BINARY(16)", nullable: true),
                    is_work_available = table.Column<bool>(type: "TINYINT(1)", nullable: false),
                    work_solve_deadline_at = table.Column<DateTime>(type: "DATETIME(0)", nullable: true),
                    work_check_deadline_at = table.Column<DateTime>(type: "DATETIME(0)", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_material_content", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_material_content_work_work_id",
                        column: x => x.work_id,
                        principalTable: "work",
                        principalColumn: "id");
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
                    right_answers = table.Column<string>(type: "VARCHAR(512)", maxLength: 16, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "ENUM('Word','Text','Essay','FinalEssay')", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    check_strategy = table.Column<string>(type: "ENUM('Manual','Auto')", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    max_score = table.Column<byte>(type: "TINYINT UNSIGNED", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "course_material",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    title = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title_color = table.Column<string>(type: "VARCHAR(63)", maxLength: 63, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "TINYINT(1)", nullable: false),
                    publish_at = table.Column<DateTime>(type: "DATETIME(0)", nullable: true),
                    chapter_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    content_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true),
                    order = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_material", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_material_course_chapter_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "course_chapter",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_material_course_material_content_content_id",
                        column: x => x.content_id,
                        principalTable: "course_material_content",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "course_reaction",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    material_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    user_id = table.Column<byte[]>(type: "BINARY(16)", nullable: false),
                    reaction = table.Column<string>(type: "ENUM('check', 'thinking')", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_reaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_reaction_course_material_material_id",
                        column: x => x.material_id,
                        principalTable: "course_material",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_reaction_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_course_name",
                table: "course",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_course_thumbnail_id",
                table: "course",
                column: "thumbnail_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_chapter_course_id",
                table: "course_chapter",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_chapter_parent_chapter_id",
                table: "course_chapter",
                column: "parent_chapter_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_material_chapter_id",
                table: "course_material",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_material_content_id",
                table: "course_material",
                column: "content_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_course_material_content_work_id",
                table: "course_material_content",
                column: "work_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_membership_assigner_id",
                table: "course_membership",
                column: "assigner_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_membership_course_id",
                table: "course_membership",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_membership_student_id",
                table: "course_membership",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_mm_CoursesAsAuthor_user_course_id",
                table: "course_mm_CoursesAsAuthor_user",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_mm_CoursesAsEditor_user_user_id",
                table: "course_mm_CoursesAsEditor_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_reaction_material_id",
                table: "course_reaction",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_reaction_user_id",
                table: "course_reaction",
                column: "user_id");

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
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_name",
                table: "user",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_user_telegram_id",
                table: "user",
                column: "telegram_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_telegram_username",
                table: "user",
                column: "telegram_username");

            migrationBuilder.CreateIndex(
                name: "IX_user_username",
                table: "user",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_work_subject_id",
                table: "work",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_title",
                table: "work",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "IX_work_type",
                table: "work",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_work_task_work_id",
                table: "work_task",
                column: "work_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_membership");

            migrationBuilder.DropTable(
                name: "course_mm_CoursesAsAuthor_user");

            migrationBuilder.DropTable(
                name: "course_mm_CoursesAsEditor_user");

            migrationBuilder.DropTable(
                name: "course_reaction");

            migrationBuilder.DropTable(
                name: "mentor_assignment");

            migrationBuilder.DropTable(
                name: "work_task");

            migrationBuilder.DropTable(
                name: "course_material");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "course_chapter");

            migrationBuilder.DropTable(
                name: "course_material_content");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "work");

            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropTable(
                name: "subject");
        }
    }
}
