# NightscoutFoundation.Nocturne.Model.DevSnapshotDto
Top-level snapshot of all tenants and their associated identity/config data. Used for dev-only export/import of non-clinical setup state.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ExportedAt** | **DateTimeOffset** | Timestamp when the snapshot was exported. | [optional] 
**Tenants** | [**List&lt;TenantSnapshotDto&gt;**](TenantSnapshotDto.md) | All tenants and their associated identity/config data. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

