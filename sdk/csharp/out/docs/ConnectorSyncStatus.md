# NightscoutFoundation.Nocturne.Model.ConnectorSyncStatus
Response model for connector sync status

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ConnectorId** | **string** | The connector ID (e.g., \&quot;dexcom\&quot;, \&quot;libre\&quot;) | [optional] 
**DataSource** | **string** | The data source name used in the database (e.g., \&quot;dexcom-connector\&quot;) | [optional] 
**LatestEntryTimestamp** | **DateTimeOffset?** | The timestamp of the latest entry, or null if no entries exist | [optional] 
**OldestEntryTimestamp** | **DateTimeOffset?** | The timestamp of the oldest entry, or null if no entries exist | [optional] 
**LatestTreatmentTimestamp** | **DateTimeOffset?** | The timestamp of the latest treatment, or null if no treatments exist | [optional] 
**OldestTreatmentTimestamp** | **DateTimeOffset?** | The timestamp of the oldest treatment, or null if no treatments exist | [optional] 
**HasEntries** | **bool** | Whether any entries exist for this connector | [optional] 
**HasTreatments** | **bool** | Whether any treatments exist for this connector | [optional] 
**State** | **string** | Current connector state (Idle, Syncing, BackingOff, Error) | [optional] 
**StateMessage** | **string** | Optional message describing the current state | [optional] 
**IsHealthy** | **bool** | Whether the connector is healthy | [optional] 
**QueriedAt** | **DateTimeOffset** | When this status was queried | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

