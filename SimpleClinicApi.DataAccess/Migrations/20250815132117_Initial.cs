using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleClinicApi.DataAccess.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                Id = table.Column<string>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            }
        );

        migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
            {
                Id = table.Column<string>(type: "TEXT", nullable: false),
                UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(
                    type: "TEXT",
                    maxLength: 256,
                    nullable: true
                ),
                Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(
                    type: "TEXT",
                    maxLength: 256,
                    nullable: true
                ),
                EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            }
        );

        migrationBuilder.CreateTable(
            name: "Doctors",
            columns: table => new
            {
                Id = table
                    .Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Specialty = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Doctors", x => x.Id);
            }
        );

        migrationBuilder.CreateTable(
            name: "Medications",
            columns: table => new
            {
                Id = table
                    .Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                Description = table.Column<string>(type: "TEXT", nullable: true),
                Cost = table.Column<decimal>(type: "TEXT", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Medications", x => x.Id);
            }
        );

        migrationBuilder.CreateTable(
            name: "Patients",
            columns: table => new
            {
                Id = table
                    .Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                Address = table.Column<string>(type: "TEXT", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Patients", x => x.Id);
            }
        );

        migrationBuilder.CreateTable(
            name: "Procedures",
            columns: table => new
            {
                Id = table
                    .Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                Description = table.Column<string>(type: "TEXT", nullable: false),
                Cost = table.Column<decimal>(type: "TEXT", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Procedures", x => x.Id);
            }
        );

        migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
            {
                Id = table
                    .Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                RoleId = table.Column<string>(type: "TEXT", nullable: false),
                ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                ClaimValue = table.Column<string>(type: "TEXT", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            columns: table => new
            {
                Id = table
                    .Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<string>(type: "TEXT", nullable: false),
                ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                ClaimValue = table.Column<string>(type: "TEXT", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                UserId = table.Column<string>(type: "TEXT", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey(
                    "PK_AspNetUserLogins",
                    x => new { x.LoginProvider, x.ProviderKey }
                );
                table.ForeignKey(
                    name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            columns: table => new
            {
                UserId = table.Column<string>(type: "TEXT", nullable: false),
                RoleId = table.Column<string>(type: "TEXT", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            columns: table => new
            {
                UserId = table.Column<string>(type: "TEXT", nullable: false),
                LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Value = table.Column<string>(type: "TEXT", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey(
                    "PK_AspNetUserTokens",
                    x => new
                    {
                        x.UserId,
                        x.LoginProvider,
                        x.Name,
                    }
                );
                table.ForeignKey(
                    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "Visits",
            columns: table => new
            {
                Id = table
                    .Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                VisitDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                DoctorId = table.Column<int>(type: "INTEGER", nullable: false),
                IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Visits", x => x.Id);
                table.ForeignKey(
                    name: "FK_Visits_Doctors_DoctorId",
                    column: x => x.DoctorId,
                    principalTable: "Doctors",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                    name: "FK_Visits_Patients_PatientId",
                    column: x => x.PatientId,
                    principalTable: "Patients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "VisitMedications",
            columns: table => new
            {
                Id = table
                    .Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                MedicationId = table.Column<int>(type: "INTEGER", nullable: false),
                Dosage = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_VisitMedications", x => x.Id);
                table.ForeignKey(
                    name: "FK_VisitMedications_Medications_MedicationId",
                    column: x => x.MedicationId,
                    principalTable: "Medications",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_VisitMedications_Visits_VisitId",
                    column: x => x.VisitId,
                    principalTable: "Visits",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "VisitProcedures",
            columns: table => new
            {
                Id = table
                    .Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_VisitProcedures", x => x.Id);
                table.ForeignKey(
                    name: "FK_VisitProcedures_Procedures_ProcedureId",
                    column: x => x.ProcedureId,
                    principalTable: "Procedures",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_VisitProcedures_Visits_VisitId",
                    column: x => x.VisitId,
                    principalTable: "Visits",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateIndex(
            name: "IX_AspNetRoleClaims_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId"
        );

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles",
            column: "NormalizedName",
            unique: true
        );

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserClaims_UserId",
            table: "AspNetUserClaims",
            column: "UserId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserLogins_UserId",
            table: "AspNetUserLogins",
            column: "UserId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId"
        );

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "AspNetUsers",
            column: "NormalizedEmail"
        );

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "AspNetUsers",
            column: "NormalizedUserName",
            unique: true
        );

        migrationBuilder.CreateIndex(
            name: "IX_Doctor_FullName",
            table: "Doctors",
            column: "FullName"
        );

        migrationBuilder.CreateIndex(
            name: "IX_Doctor_Specialty",
            table: "Doctors",
            column: "Specialty"
        );

        migrationBuilder.CreateIndex(
            name: "IX_Medication_Name",
            table: "Medications",
            column: "Name"
        );

        migrationBuilder.CreateIndex(
            name: "IX_Patient_FullName",
            table: "Patients",
            column: "FullName"
        );

        migrationBuilder.CreateIndex(
            name: "IX_Patient_PhoneNumber",
            table: "Patients",
            column: "PhoneNumber"
        );

        migrationBuilder.CreateIndex(
            name: "IX_Procedure_Name",
            table: "Procedures",
            column: "Name"
        );

        migrationBuilder.CreateIndex(
            name: "IX_VisitMedication_Visit_Medication",
            table: "VisitMedications",
            columns: new[] { "VisitId", "MedicationId" }
        );

        migrationBuilder.CreateIndex(
            name: "IX_VisitMedications_MedicationId",
            table: "VisitMedications",
            column: "MedicationId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_VisitProcedure_Visit_Procedure",
            table: "VisitProcedures",
            columns: new[] { "VisitId", "ProcedureId" }
        );

        migrationBuilder.CreateIndex(
            name: "IX_VisitProcedures_ProcedureId",
            table: "VisitProcedures",
            column: "ProcedureId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_Visit_DoctorId",
            table: "Visits",
            column: "DoctorId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_Visit_Patient_VisitDate",
            table: "Visits",
            columns: new[] { "PatientId", "VisitDate" }
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "AspNetRoleClaims");

        migrationBuilder.DropTable(name: "AspNetUserClaims");

        migrationBuilder.DropTable(name: "AspNetUserLogins");

        migrationBuilder.DropTable(name: "AspNetUserRoles");

        migrationBuilder.DropTable(name: "AspNetUserTokens");

        migrationBuilder.DropTable(name: "VisitMedications");

        migrationBuilder.DropTable(name: "VisitProcedures");

        migrationBuilder.DropTable(name: "AspNetRoles");

        migrationBuilder.DropTable(name: "AspNetUsers");

        migrationBuilder.DropTable(name: "Medications");

        migrationBuilder.DropTable(name: "Procedures");

        migrationBuilder.DropTable(name: "Visits");

        migrationBuilder.DropTable(name: "Doctors");

        migrationBuilder.DropTable(name: "Patients");
    }
}
