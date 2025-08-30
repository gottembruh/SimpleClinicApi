using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleClinicApi.DataAccess.Migrations;

/// <inheritdoc />
public partial class NullableTypeFixes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Notes",
            table: "VisitMedications",
            type: "TEXT",
            maxLength: 500,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 500);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Procedures",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Notes",
            table: "VisitMedications",
            type: "TEXT",
            maxLength: 500,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 500,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Procedures",
            type: "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);
    }
}
