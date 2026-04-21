# NightscoutFoundation.Nocturne.Model.UpsertHeartRateRequest
Request body for upserting a heart rate measurement via the V4 API.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Timestamp** | **DateTimeOffset** | When the heart rate measurement was taken. | [optional] 
**UtcOffset** | **int?** | UTC offset in minutes at the time of the event, for local-time display. | [optional] 
**Bpm** | **int** | Heart rate in beats per minute. | [optional] 
**Accuracy** | **int** | Sensor accuracy indicator (device-specific scale). | [optional] 
**Device** | **string** | Identifier of the wearable or sensor device. | [optional] 
**App** | **string** | Name of the application that submitted this record. | [optional] 
**DataSource** | **string** | Upstream data source identifier. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

