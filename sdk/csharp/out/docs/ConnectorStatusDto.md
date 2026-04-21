# NightscoutFoundation.Nocturne.Model.ConnectorStatusDto
DTO representing the current operational status of a data source connector (e.g. Dexcom, Glooko, Libre). Returned by the admin connector status endpoints.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | Unique identifier of the connector configuration. | [optional] 
**Name** | **string** | Human-readable connector name (e.g. \&quot;Dexcom Share\&quot;, \&quot;LibreLink\&quot;). | [optional] 
**Status** | **string** | High-level status label (e.g. \&quot;Active\&quot;, \&quot;Error\&quot;, \&quot;Disabled\&quot;). | [optional] 
**TotalEntries** | **long** | Total number of entries imported by this connector over its lifetime. | [optional] 
**LastEntryTime** | **DateTimeOffset?** | Timestamp of the most recent entry imported by this connector. | [optional] 
**EntriesLast24Hours** | **int** | Number of entries imported in the last 24 hours. | [optional] 
**State** | **string** | Current operational state of the connector. | [optional] 
**StateMessage** | **string** | Optional message providing detail about the current state (e.g. error description). | [optional] 
**IsHealthy** | **bool** | Whether the connector is considered healthy based on recent sync activity. | [optional] 
**LastSyncAttempt** | **DateTimeOffset?** | When the connector last attempted to sync | [optional] 
**LastSuccessfulSync** | **DateTimeOffset?** | When the connector last successfully completed a sync | [optional] 
**LastErrorAt** | **DateTimeOffset?** | When the last error occurred | [optional] 
**TotalItemsBreakdown** | **Dictionary&lt;string, long&gt;** | Breakdown of total items processed by data type Keys are data type names (e.g., \&quot;Glucose\&quot;, \&quot;Treatments\&quot;, \&quot;Food\&quot;) | [optional] 
**ItemsLast24HoursBreakdown** | **Dictionary&lt;string, int&gt;** | Breakdown of items processed in the last 24 hours by data type Keys are data type names (e.g., \&quot;Glucose\&quot;, \&quot;Treatments\&quot;, \&quot;Food\&quot;) | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

