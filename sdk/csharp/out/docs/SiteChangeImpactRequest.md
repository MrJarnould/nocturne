# NightscoutFoundation.Nocturne.Model.SiteChangeImpactRequest
Request model for site change impact analysis

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Entries** | [**List&lt;SensorGlucose&gt;**](SensorGlucose.md) | Collection of sensor glucose readings | [optional] 
**DeviceEvents** | [**List&lt;DeviceEvent&gt;**](DeviceEvent.md) | Collection of device events (must include site changes) | [optional] 
**HoursBeforeChange** | **int** | Hours before site change to analyze (default: 12) | [optional] 
**HoursAfterChange** | **int** | Hours after site change to analyze (default: 24) | [optional] 
**BucketSizeMinutes** | **int** | Time bucket size for averaging in minutes (default: 30) | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

