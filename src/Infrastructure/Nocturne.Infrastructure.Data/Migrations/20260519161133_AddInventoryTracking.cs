using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nocturne.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventory_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    kind = table.Column<string>(type: "text", nullable: false),
                    unit_label = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    low_stock_threshold = table.Column<decimal>(type: "numeric", nullable: false),
                    target_stock = table.Column<decimal>(type: "numeric", nullable: true),
                    auto_consume_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    auto_consume_source = table.Column<string>(type: "text", nullable: false),
                    patient_insulin_id = table.Column<Guid>(type: "uuid", nullable: true),
                    device_event_types_json = table.Column<string>(type: "jsonb", nullable: false),
                    linked_insulin_item_id = table.Column<Guid>(type: "uuid", nullable: true),
                    linked_insulin_units_per_use = table.Column<decimal>(type: "numeric", nullable: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    is_archived = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_inventory_items_inventory_items_linked_insulin_item_id",
                        column: x => x.linked_insulin_item_id,
                        principalTable: "inventory_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_inventory_items_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventory_batches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    inventory_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    received_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    remaining_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    received_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    lot_number = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    storage_state = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_batches", x => x.id);
                    table.ForeignKey(
                        name: "FK_inventory_batches_inventory_items_inventory_item_id",
                        column: x => x.inventory_item_id,
                        principalTable: "inventory_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_batches_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventory_transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    inventory_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    inventory_batch_id = table.Column<Guid>(type: "uuid", nullable: true),
                    type = table.Column<string>(type: "text", nullable: false),
                    quantity_delta = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity_after = table.Column<decimal>(type: "numeric", nullable: false),
                    reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    source_type = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    source_id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    source_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_inventory_transactions_inventory_batches_inventory_batch_id",
                        column: x => x.inventory_batch_id,
                        principalTable: "inventory_batches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_inventory_transactions_inventory_items_inventory_item_id",
                        column: x => x.inventory_item_id,
                        principalTable: "inventory_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_transactions_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_inventory_batches_expires_at",
                table: "inventory_batches",
                column: "expires_at",
                filter: "expires_at IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_batches_item_expiry_received",
                table: "inventory_batches",
                columns: new[] { "inventory_item_id", "expires_at", "received_at" });

            migrationBuilder.CreateIndex(
                name: "ix_inventory_batches_item_id",
                table: "inventory_batches",
                column: "inventory_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_batches_tenant_id",
                table: "inventory_batches",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_linked_insulin_id",
                table: "inventory_items",
                column: "linked_insulin_item_id",
                filter: "linked_insulin_item_id IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_patient_insulin_id",
                table: "inventory_items",
                column: "patient_insulin_id",
                filter: "patient_insulin_id IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_tenant_archived",
                table: "inventory_items",
                columns: new[] { "tenant_id", "is_archived" });

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_tenant_category",
                table: "inventory_items",
                columns: new[] { "tenant_id", "category" });

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_tenant_kind_archived",
                table: "inventory_items",
                columns: new[] { "tenant_id", "kind", "is_archived" });

            migrationBuilder.CreateIndex(
                name: "ix_inventory_transactions_batch_id",
                table: "inventory_transactions",
                column: "inventory_batch_id",
                filter: "inventory_batch_id IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_transactions_item_id",
                table: "inventory_transactions",
                column: "inventory_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_transactions_source",
                table: "inventory_transactions",
                columns: new[] { "tenant_id", "inventory_item_id", "source_type", "source_id" },
                unique: true,
                filter: "source_type IS NOT NULL AND source_id IS NOT NULL");

            // Row Level Security: tenant isolation. FORCE applies the policy even
            // to the table owner (migrator role), matching the pattern from
            // EnforceMultitenancy. Runtime queries set
            // current_setting('app.current_tenant_id') via TenantConnectionInterceptor.
            foreach (var table in new[] { "inventory_items", "inventory_batches", "inventory_transactions" })
            {
                migrationBuilder.Sql($"ALTER TABLE {table} ENABLE ROW LEVEL SECURITY;");
                migrationBuilder.Sql($"ALTER TABLE {table} FORCE ROW LEVEL SECURITY;");
                migrationBuilder.Sql($"""
                    CREATE POLICY tenant_isolation_{table}
                    ON {table}
                    FOR ALL
                    USING (tenant_id = NULLIF(current_setting('app.current_tenant_id', true), '')::uuid)
                    WITH CHECK (tenant_id = NULLIF(current_setting('app.current_tenant_id', true), '')::uuid);
                    """);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var table in new[] { "inventory_transactions", "inventory_batches", "inventory_items" })
            {
                migrationBuilder.Sql($"DROP POLICY IF EXISTS tenant_isolation_{table} ON {table};");
                migrationBuilder.Sql($"ALTER TABLE {table} NO FORCE ROW LEVEL SECURITY;");
                migrationBuilder.Sql($"ALTER TABLE {table} DISABLE ROW LEVEL SECURITY;");
            }

            migrationBuilder.DropTable(
                name: "inventory_transactions");

            migrationBuilder.DropTable(
                name: "inventory_batches");

            migrationBuilder.DropTable(
                name: "inventory_items");
        }
    }
}
