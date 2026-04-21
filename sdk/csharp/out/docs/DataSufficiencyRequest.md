# NightscoutFoundation.Nocturne.Model.DataSufficiencyRequest
Request model for data sufficiency assessment

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Entries** | [**List&lt;SensorGlucose&gt;**](SensorGlucose.md) | Collection of sensor glucose readings | [optional] 
**Days** | **int** | Number of days to assess (default: 14) | [optional] 
**ExpectedReadingsPerDay** | **int** | Expected readings per day based on sensor type (default: 288 for 5-minute intervals) | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

