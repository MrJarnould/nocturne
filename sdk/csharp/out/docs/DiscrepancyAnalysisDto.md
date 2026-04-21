# NightscoutFoundation.Nocturne.Model.DiscrepancyAnalysisDto

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** |  | [optional] 
**CorrelationId** | **string** |  | [optional] 
**AnalysisTimestamp** | **DateTimeOffset** |  | [optional] 
**RequestMethod** | **string** |  | [optional] 
**RequestPath** | **string** |  | [optional] 
**OverallMatch** | **int** |  | [optional] 
**StatusCodeMatch** | **bool** |  | [optional] 
**BodyMatch** | **bool** |  | [optional] 
**NightscoutStatusCode** | **int?** |  | [optional] 
**NocturneStatusCode** | **int?** |  | [optional] 
**NightscoutResponseTimeMs** | **long?** |  | [optional] 
**NocturneResponseTimeMs** | **long?** |  | [optional] 
**TotalProcessingTimeMs** | **long** |  | [optional] 
**Summary** | **string** |  | [optional] 
**SelectedResponseTarget** | **string** |  | [optional] 
**SelectionReason** | **string** |  | [optional] 
**CriticalDiscrepancyCount** | **int** |  | [optional] 
**MajorDiscrepancyCount** | **int** |  | [optional] 
**MinorDiscrepancyCount** | **int** |  | [optional] 
**NightscoutMissing** | **bool** |  | [optional] 
**NocturneMissing** | **bool** |  | [optional] 
**ErrorMessage** | **string** |  | [optional] 
**Discrepancies** | [**List&lt;DiscrepancyDetailDto&gt;**](DiscrepancyDetailDto.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

