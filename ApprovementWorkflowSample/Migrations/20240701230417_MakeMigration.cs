using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApprovementWorkflowSample.Migrations
{
    public partial class MakeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    organization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_update_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ApproverRoles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverRoles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Workflows",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    workflow_type_id = table.Column<int>(type: "int", nullable: false),
                    approved_date = table.Column<DateTime>(type: "date", nullable: true),
                    last_update_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.id);
                    table.ForeignKey(
                        name: "FK_Workflows_WorkflowType_workflow_type_id",
                        column: x => x.workflow_type_id,
                        principalTable: "WorkflowType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApproverGroups",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    workflow_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverGroups", x => x.id);
                    table.ForeignKey(
                        name: "FK_ApproverGroups_Workflows_workflow_id",
                        column: x => x.workflow_id,
                        principalTable: "Workflows",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approvers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    approver_group_id = table.Column<int>(type: "int", nullable: false),
                    approver_role_id = table.Column<int>(type: "int", nullable: false),
                    approved_date = table.Column<DateTime>(type: "date", nullable: true),
                    last_update_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Approvers_ApproverGroups_approver_group_id",
                        column: x => x.approver_group_id,
                        principalTable: "ApproverGroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvers_ApproverRoles_approver_role_id",
                        column: x => x.approver_role_id,
                        principalTable: "ApproverRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApproverRoles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Author" },
                    { 2, "Checker" },
                    { 3, "Approver" }
                });

            migrationBuilder.InsertData(
                table: "WorkflowType",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { 1, "WorkflowTypeA" },
                    { 2, "WorkflowTypeA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_mail",
                table: "ApplicationUsers",
                column: "mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApproverGroups_workflow_id",
                table: "ApproverGroups",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "IX_Approvers_approver_group_id",
                table: "Approvers",
                column: "approver_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Approvers_approver_role_id",
                table: "Approvers",
                column: "approver_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_workflow_type_id",
                table: "Workflows",
                column: "workflow_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Approvers");

            migrationBuilder.DropTable(
                name: "ApproverGroups");

            migrationBuilder.DropTable(
                name: "ApproverRoles");

            migrationBuilder.DropTable(
                name: "Workflows");

            migrationBuilder.DropTable(
                name: "WorkflowType");
        }
    }
}
