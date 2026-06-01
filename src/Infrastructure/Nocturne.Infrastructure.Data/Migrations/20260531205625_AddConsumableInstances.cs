using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nocturne.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConsumableInstances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consumable_instances",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    source_device_event_id = table.Column<Guid>(type: "uuid", nullable: true),
                    consumable_catalog_id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    kind = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    patient_device_id = table.Column<Guid>(type: "uuid", nullable: true),
                    device_id = table.Column<Guid>(type: "uuid", nullable: true),
                    inventory_item_id = table.Column<Guid>(type: "uuid", nullable: true),
                    inventory_batch_id = table.Column<Guid>(type: "uuid", nullable: true),
                    serial_number = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    insertion_site = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ended_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_reason = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    notes = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    snapshot_wear_days = table.Column<int>(type: "integer", nullable: true),
                    snapshot_reservoir_capacity = table.Column<decimal>(type: "numeric", nullable: true),
                    filled_units = table.Column<decimal>(type: "numeric", nullable: true),
                    residual_units = table.Column<decimal>(type: "numeric", nullable: true),
                    sys_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sys_updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumable_instances", x => x.id);
                    table.ForeignKey(
                        name: "FK_consumable_instances_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_consumable_instances_tenant_id",
                table: "consumable_instances",
                column: "tenant_id");

            // Fast "find the open instance of kind X for this tenant" lookup.
            migrationBuilder.CreateIndex(
                name: "IX_consumable_instances_tenant_id_kind_ended_at",
                table: "consumable_instances",
                columns: ["tenant_id", "kind", "ended_at"]);

            // Idempotency guard for the DeviceEvent-driven open hook. Partial
            // (only rows with a non-null source) so it stays tight even as
            // manually-created instances accumulate.
            migrationBuilder.Sql(
                """
                CREATE UNIQUE INDEX IX_consumable_instances_source_device_event_id
                    ON consumable_instances (source_device_event_id)
                    WHERE source_device_event_id IS NOT NULL;
                """);

            // Tenant-isolation RLS. ENABLE makes the policy apply; FORCE makes
            // even the table owner (migrator role) obey it. NULLIF + missing_ok
            // keeps the policy safe to evaluate when the GUC is unset.
            migrationBuilder.Sql("ALTER TABLE consumable_instances ENABLE ROW LEVEL SECURITY;");
            migrationBuilder.Sql("ALTER TABLE consumable_instances FORCE ROW LEVEL SECURITY;");
            migrationBuilder.Sql(
                """
                DROP POLICY IF EXISTS tenant_isolation ON consumable_instances;
                CREATE POLICY tenant_isolation ON consumable_instances
                    USING (tenant_id = NULLIF(current_setting('app.current_tenant_id', true), '')::uuid)
                    WITH CHECK (tenant_id = NULLIF(current_setting('app.current_tenant_id', true), '')::uuid);
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP POLICY IF EXISTS tenant_isolation ON consumable_instances;");
            migrationBuilder.Sql("ALTER TABLE consumable_instances NO FORCE ROW LEVEL SECURITY;");
            migrationBuilder.Sql("ALTER TABLE consumable_instances DISABLE ROW LEVEL SECURITY;");

            migrationBuilder.DropTable(
                name: "consumable_instances");
        }
    }
}
