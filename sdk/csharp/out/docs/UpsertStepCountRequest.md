# NightscoutFoundation.Nocturne.Model.UpsertStepCountRequest
Request body for upserting a step count measurement via the V4 API.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Timestamp** | **DateTimeOffset** | When the step count was recorded. | [optional] 
**UtcOffset** | **int?** | UTC offset in minutes at the time of the event, for local-time display. | [optional] 
**Metric** | **int** | Step count metric value for the measurement period. | [optional] 
**Source** | **int** | Source identifier for the step data (device-specific). | [optional] 
**Device** | **string** | Identifier of the wearable or sensor device. | [optional] 
**App** | **string** | Name of the application that submitted this record. | [optional] 
**DataSource** | **string** | Upstream data source identifier. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

