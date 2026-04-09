using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nocturne.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRlsToNewTenantTables : Migration
    {
        /// <summary>
        /// Tenant-scoped tables added after the original EnforceMultitenancy migration
        /// (2026-02-27) that have been running without Row Level Security. All implement
        /// ITenantScoped but were never enrolled in the RLS regime.
        /// </summary>
        private static readonly string[] NewTenantScopedTables =
        [
            // Added 2026-02-27 (same day as EnforceMultitenancy, missed the list)
            "system_events",
            // Added 2026-03-20 in AddPatientRecordTables
            "patient_records", "patient_devices", "patient_insulins", "devices",
            // Added 2026-03-22 in the new alert engine (replaced alert_rules/alert_history)
            "alert_schedules", "alert_escalation_steps", "alert_step_channels",
            "alert_tracker_state", "alert_excursions", "alert_instances",
            "alert_deliveries", "alert_invites", "alert_custom_sounds",
            // Added 2026-04-06
            "body_weights",
        ];

        /// <summary>
        /// All tenant-scoped tables that should end up with grants on the nocturne_app
        /// role — the union of the original EnforceMultitenancy list minus tables that
        /// were subsequently dropped, plus the new tables added above. Kept in sync with
        /// the startup RlsSelfCheck so any drift fails loud.
        /// </summary>
        private static readonly string[] AllTenantScopedTables =
        [
            // Original EnforceMultitenancy list
            "entries", "treatments", "devicestatus", "foods",
            "connector_food_entries", "treatment_foods", "user_food_favorites",
            "settings", "profiles", "activities", "step_counts", "heart_rates",
            "discrepancy_analyses", "discrepancy_details",
            // NOTE: alert_rules and alert_history from the old list were dropped in
            // 20260322031413_DropOldAlertTables and intentionally excluded here.
            "notification_preferences", "emergency_contacts", "device_health",
            "data_source_metadata",
            "tracker_definitions", "tracker_instances", "tracker_presets",
            "tracker_notification_thresholds",
            "state_spans", "linked_records", "connector_configurations",
            "in_app_notifications", "clock_faces", "compression_low_suggestions",
            "sensor_glucose", "meter_glucose", "calibrations",
            "boluses", "carb_intakes", "bg_checks", "notes", "device_events",
            "bolus_calculations", "aps_snapshots", "pump_snapshots",
            "uploader_snapshots", "pump_devices", "temp_basals",
            "therapy_settings", "basal_schedules", "carb_ratio_schedules",
            "sensitivity_schedules", "target_range_schedules",
            // New tables
            "system_events",
            "patient_records", "patient_devices", "patient_insulins", "devices",
            "alert_schedules", "alert_escalation_steps", "alert_step_channels",
            "alert_tracker_state", "alert_excursions", "alert_instances",
            "alert_deliveries", "alert_invites", "alert_custom_sounds",
            "body_weights",
        ];

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Enable + force RLS on the tables that were missed, and add
            // tenant_isolation policies with USING and WITH CHECK so both reads and
            // writes are enforced. Mirrors the shape used by
            // 20260228071347_AddRlsWithCheckAndCompositeIndexes for the original set.
            foreach (var table in NewTenantScopedTables)
            {
                migrationBuilder.Sql($"ALTER TABLE {table} ENABLE ROW LEVEL SECURITY;");
                migrationBuilder.Sql($"ALTER TABLE {table} FORCE ROW LEVEL SECURITY;");
                migrationBuilder.Sql(
                    $"""
                    DROP POLICY IF EXISTS tenant_isolation ON {table};
                    CREATE POLICY tenant_isolation ON {table}
                        USING (tenant_id = current_setting('app.current_tenant_id')::uuid)
                        WITH CHECK (tenant_id = current_setting('app.current_tenant_id')::uuid);
                    """);
            }

            // Step 2: Ensure nocturne_app exists and is a non-privileged login role.
            // The original EnforceMultitenancy migration created this role as LOGIN
            // only; we explicitly strip SUPERUSER and BYPASSRLS so that even if a
            // future operator accidentally grants the role superuser powers, this
            // migration reverses it. RLS policies are ignored by superusers and by
            // roles with BYPASSRLS regardless of FORCE ROW LEVEL SECURITY.
            migrationBuilder.Sql(
                """
                DO $$
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'nocturne_app') THEN
                        CREATE ROLE nocturne_app LOGIN NOSUPERUSER NOBYPASSRLS NOCREATEDB NOCREATEROLE;
                    ELSE
                        ALTER ROLE nocturne_app NOSUPERUSER NOBYPASSRLS NOCREATEDB NOCREATEROLE;
                    END IF;
                END $$;
                """);

            // Step 3: Grant nocturne_app the privileges it needs to act as the
            // runtime application user on tenant-scoped tables plus the supporting
            // lookup tables it must read (tenants, subjects, etc.).
            //
            // We grant on ALL tables/sequences in the public schema rather than
            // enumerating, because the non-tenant-scoped tables the app touches are
            // numerous and drift over time. RLS is the isolation boundary — grants
            // are just "can this role see the table at all".
            migrationBuilder.Sql("GRANT USAGE ON SCHEMA public TO nocturne_app;");
            migrationBuilder.Sql("GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO nocturne_app;");
            migrationBuilder.Sql("GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO nocturne_app;");

            // Step 4: ALTER DEFAULT PRIVILEGES so tables created by future migrations
            // automatically grant nocturne_app the same privileges without needing a
            // follow-up GRANT statement. This is scoped to objects created by the
            // role that currently runs migrations (CURRENT_USER at migration time).
            migrationBuilder.Sql(
                """
                ALTER DEFAULT PRIVILEGES IN SCHEMA public
                    GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO nocturne_app;
                """);
            migrationBuilder.Sql(
                """
                ALTER DEFAULT PRIVILEGES IN SCHEMA public
                    GRANT USAGE, SELECT ON SEQUENCES TO nocturne_app;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse default privileges and grants. We do NOT drop the nocturne_app
            // role because it predates this migration (created by EnforceMultitenancy)
            // and may own session state on active connections.
            migrationBuilder.Sql(
                """
                ALTER DEFAULT PRIVILEGES IN SCHEMA public
                    REVOKE SELECT, INSERT, UPDATE, DELETE ON TABLES FROM nocturne_app;
                """);
            migrationBuilder.Sql(
                """
                ALTER DEFAULT PRIVILEGES IN SCHEMA public
                    REVOKE USAGE, SELECT ON SEQUENCES FROM nocturne_app;
                """);
            migrationBuilder.Sql("REVOKE USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public FROM nocturne_app;");
            migrationBuilder.Sql("REVOKE SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public FROM nocturne_app;");
            migrationBuilder.Sql("REVOKE USAGE ON SCHEMA public FROM nocturne_app;");

            // Drop the tenant_isolation policies and disable RLS on the new tables.
            foreach (var table in NewTenantScopedTables)
            {
                migrationBuilder.Sql($"DROP POLICY IF EXISTS tenant_isolation ON {table};");
                migrationBuilder.Sql($"ALTER TABLE {table} NO FORCE ROW LEVEL SECURITY;");
                migrationBuilder.Sql($"ALTER TABLE {table} DISABLE ROW LEVEL SECURITY;");
            }
        }
    }
}
