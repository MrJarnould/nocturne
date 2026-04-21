# NightscoutFoundation.Nocturne.Model.AnalysisListItemDto
Analysis list item DTO

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** |  | [optional] 
**CorrelationId** | **string** |  | [optional] 
**AnalysisTimestamp** | **DateTimeOffset** |  | [optional] 
**RequestMethod** | **string** |  | [optional] 
**RequestPath** | **string** |  | [optional] 
**OverallMatch** | **ResponseMatchType** |  | [optional] 
**StatusCodeMatch** | **bool** |  | [optional] 
**BodyMatch** | **bool** |  | [optional] 
**NightscoutStatusCode** | **int?** |  | [optional] 
**NocturneStatusCode** | **int?** |  | [optional] 
**NightscoutResponseTimeMs** | **long?** |  | [optional] 
**NocturneResponseTimeMs** | **long?** |  | [optional] 
**TotalProcessingTimeMs** | **long** |  | [optional] 
**Summary** | **string** |  | [optional] 
**CriticalDiscrepancyCount** | **int** |  | [optional] 
**MajorDiscrepancyCount** | **int** |  | [optional] 
**MinorDiscrepancyCount** | **int** |  | [optional] 
**NightscoutMissing** | **bool** |  | [optional] 
**NocturneMissing** | **bool** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

