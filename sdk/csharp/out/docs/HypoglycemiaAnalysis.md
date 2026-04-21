# NightscoutFoundation.Nocturne.Model.HypoglycemiaAnalysis

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**TotalEpisodes** | **int** |  | [optional] 
**SevereEpisodes** | **int** |  | [optional] 
**EpisodesPerDay** | **double** |  | [optional] 
**AverageDurationMinutes** | **double** |  | [optional] 
**AverageNadir** | **double** |  | [optional] 
**LowestGlucose** | **double** |  | [optional] 
**AverageRecoveryTimeMinutes** | **double** |  | [optional] 
**HourlyDistribution** | **Dictionary&lt;string, int&gt;** |  | [optional] 
**DayOfWeekDistribution** | **Dictionary&lt;string, int&gt;** |  | [optional] 
**PeakHour** | **int?** |  | [optional] 
**PeakDay** | **DayOfWeek** |  | [optional] 
**HasRecurringPattern** | **bool** |  | [optional] 
**PatternDescription** | **string** |  | [optional] 
**Episodes** | [**List&lt;HypoglycemiaEpisode&gt;**](HypoglycemiaEpisode.md) |  | [optional] 
**NocturnalEpisodes** | **int** |  | [optional] 
**NocturnalPercentage** | **double** |  | [optional] 
**RiskAssessment** | **string** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

