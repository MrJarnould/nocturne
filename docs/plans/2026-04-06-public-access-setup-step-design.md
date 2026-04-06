# Public Access Setup Step

## Problem

The Public system subject exists but is never wired into the request pipeline. Unauthenticated requests get empty permissions, and `[AllowAnonymous]` attributes on data endpoints bypass the permission system entirely. This means every Nocturne instance is effectively publicly readable with no way to lock it down, and the RBAC system is circumvented for anonymous access.

## Approach

Use the existing RBAC system. The Public subject gets a `TenantMember` record per tenant with role assignments and `LimitTo24Hours` support. No new database tables or permission concepts. A new setup wizard step (step 2 of 8) lets the user choose between two presets, with an advanced escape hatch for granular control.

## Backend Changes

### 1. Public Subject Tenant Membership

- The Public subject's `TenantMember` is created during **tenant provisioning** (not startup seeding), guaranteeing it exists before the user reaches the setup wizard.
- Seeded with **no roles** (secure default, also serves as the "unconfigured" sentinel for onboarding completion).
- `IsSystemSubject` on the subject prevents deletion. The member removal endpoint rejects deletion of system subject memberships.

### 2. AuthenticationMiddleware — Public Subject Resolution

When no credentials are provided and a tenant is resolved:

1. Look up the Public subject's `TenantMember` for that tenant.
2. If it exists and has non-empty permissions, set `AuthContext` with the Public subject's ID, `AuthType.None`, `IsAuthenticated = false`, and the resolved scopes.
3. `MemberScopeMiddleware` resolves permissions normally via the membership's roles/direct permissions.
4. If no roles or denied, leave permissions empty (current behavior).

### 3. Performance: Permission Caching

Cache the Public subject's resolved permissions per tenant in `IMemoryCache`:

- Key: `public-permissions:{tenantId}` (tenant-scoped, no cross-contamination).
- TTL: 1-2 minutes.
- Explicit eviction when the Public subject's membership is modified (role change, permission change, LimitTo24Hours toggle) via the member management endpoints.

### 4. Custom Authorization Policy

Replace `[Authorize]` on data controllers with `[Authorize(Policy = "HasPermissions")]`:

- Passes if the permission trie is non-empty (regardless of `IsAuthenticated`).
- Unauthenticated requests with Public subject permissions pass through.
- Unauthenticated requests with empty permissions get **401**.
- Authenticated requests with insufficient permissions get **403**.
- Controllers that truly need authentication (write endpoints, settings) keep using the default `[Authorize]`.

### 5. `[AllowAnonymous]` Removal

Remove `[AllowAnonymous]` from all data endpoints across v1, v2, and v3 controllers. These endpoints now rely on the Public subject's resolved permissions.

**Endpoints that keep `[AllowAnonymous]` (infrastructure, not data):**

- `/api/v1/status.json` (instance discovery)
- `/api/v1/verifyauth` (credential testing)
- All OIDC endpoints (providers, login, callback, refresh, session)
- All OAuth endpoints (authorize, token, device, revoke, client-info, introspect)
- All Passkey endpoints (register, login, setup, recovery, invite, access-request)
- TOTP login
- OIDC discovery (`.well-known`)
- Alert invite token lookup
- Clock face public embed
- Internal connector config endpoints

**Data endpoints that become RBAC-gated:**

- V1: Entries (current, spec, list), DeviceStatus (list, .json), Activity (list, by-id), Food (list, .json)
- V2: DData (get, at, raw), Loop status, Summary, Properties, Notifications status
- V3: Entries (list, by-id, history), Treatments (list, by-id, history), DeviceStatus (list, by-id, history), Profiles (list, by-id), Settings (list, by-id), Food (list, by-id)

### 6. Development Mode

Dev mode auto-auth only activates when an existing session cookie is present. Bare requests (e.g. curl) resolve the Public subject, matching production behavior so developers can test public access locally.

### 7. Member Management Gaps

- Add `LimitTo24Hours` update to the member management API.
- Reject deletion of system subject memberships in the remove member endpoint.

### 8. Data Migration for Existing Tenants

Create a Public subject `TenantMember` for every existing tenant:

- Role: `readable` (preserving current behavior).
- `LimitTo24Hours`: `false` (preserving current behavior).
- Existing instances continue working without disruption. Users can tighten permissions later in Settings.

## Frontend Changes

### Setup Wizard Step

**Position:** Step 2 of 8 (after Passkey, before Patient Record). Icon: Shield.

**Required:** Yes. Onboarding completion check: "does the Public subject's membership have at least one role?"

**Two presets (default: Invite only):**

- **Invite only** — Public subject gets `denied` role. Summary: "Only people you invite can access this website."
- **Publicly readable** — Public subject gets `readable` role + `LimitTo24Hours = true`. Summary: "Anyone can access this website and view the last 24 hours of: [resource list]."

**24-hour toggle:** Only visible when "Publicly readable" is selected. Checked by default.

**Advanced disclosure:** Only visible when "Publicly readable" is selected. Per-resource read toggles (Glucose entries, Treatments, Device status, Profiles, Food, Activity) — all checked by default. Unchecking all resources shows a hint and is equivalent to "Invite only."

**Reactive summary at the bottom:**

- Publicly readable: "**Anyone** can access this website and view **the last 24 hours of** / **all**: Glucose, Treatments, ..."
- Invite only: "**Only people you invite** can access this website."

**Patient name:** Uses "your" as placeholder until patient record is created. Post-setup, uses the actual patient name.

**Footer note:** "You can change these settings at any time in Settings > Members."

**On save:** Uses existing member management APIs (setMemberRoles, setMemberPermissions) + new LimitTo24Hours update. No bespoke endpoint.

### Onboarding Integration

- Total steps increases from 7 to 8.
- All subsequent steps renumbered.
- `onboarding-check.ts` adds a 5th check: Public subject's membership has at least one role.
- The same permission editing component is reusable in Settings > Members for the Public subject.

## Scope Exclusions

- **Public SvelteKit dashboard:** The SvelteKit app continues to require authentication. Public access controls the API only. A public dashboard is future work.
- **Retroactive LimitTo24Hours on invited members:** The 24-hour toggle only applies to the Public subject. Per-member time limits are managed individually in Settings > Members.

## Test Impact

Removing `[AllowAnonymous]` will break existing controller tests that make unauthenticated requests expecting 200 responses. This is expected. Tests need to be updated to set up a Public subject membership with appropriate permissions in test fixtures.

New test cases needed:

- Both presets (readable vs denied) produce correct API behavior.
- Per-resource granular toggles.
- 24-hour filtering on Public subject requests.
- Cache invalidation on permission change.
- `HasPermissions` policy: 401 for empty permissions, 403 for authenticated-but-insufficient.
- System subject membership cannot be deleted.
- Tenant-scoped cache keys (no cross-tenant leakage).
