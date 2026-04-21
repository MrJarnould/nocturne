# NightscoutFoundation.Nocturne.Model.UpsertNoteRequest
Request body for upserting a user note or announcement via the V4 API.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Timestamp** | **DateTimeOffset** | When the note was created. | [optional] 
**UtcOffset** | **int?** | UTC offset in minutes at the time of the event, for local-time display. | [optional] 
**Device** | **string** | Identifier of the device that created the note. | [optional] 
**App** | **string** | Name of the application that submitted this record. | [optional] 
**DataSource** | **string** | Upstream data source identifier. | [optional] 
**Text** | **string** | Note body text (capped at 10,000 characters). | [optional] 
**EventType** | **string** | Nightscout-compatible event type string (capped at 200 characters). | [optional] 
**IsAnnouncement** | **bool** | When true, the note is displayed as a prominent announcement. | [optional] 
**SyncIdentifier** | **string** | Upstream sync identifier for deduplication. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

