# NightscoutFoundation.Nocturne.Model.MigrationJobStatus
Real-time progress snapshot for a running MigrationJobInfo. EstimatedTimeRemaining is null until enough records have been processed to project a completion time.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**JobId** | **string** |  | [optional] 
**State** | **MigrationJobState** |  | [optional] 
**ProgressPercentage** | **double** |  | [optional] 
**CurrentOperation** | **string** |  | [optional] 
**ErrorMessage** | **string** |  | [optional] 
**StartedAt** | **DateTimeOffset** |  | [optional] 
**CompletedAt** | **DateTimeOffset?** |  | [optional] 
**EstimatedTimeRemaining** | **string** |  | [optional] 
**CollectionProgress** | [**Dictionary&lt;string, CollectionProgress&gt;**](CollectionProgress.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

