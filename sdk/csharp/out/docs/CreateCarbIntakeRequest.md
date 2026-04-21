# NightscoutFoundation.Nocturne.Model.CreateCarbIntakeRequest
Request body for creating a new carbohydrate intake record via the V4 API.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Timestamp** | **DateTimeOffset** | When the carbs were consumed. | [optional] 
**UtcOffset** | **int?** | UTC offset in minutes at the time of the event, for local-time display. | [optional] 
**Device** | **string** | Identifier of the device that recorded the intake. | [optional] 
**App** | **string** | Name of the application that submitted this record. | [optional] 
**DataSource** | **string** | Upstream data source identifier; required when SyncIdentifier is supplied. | [optional] 
**Carbs** | **double** | Amount of carbohydrates consumed in grams. | [optional] 
**SyncIdentifier** | **string** | Upstream sync identifier for deduplication, paired with DataSource. | [optional] 
**CarbTime** | **double?** | Minutes from bolus time to expected carb absorption start (pre-bolus offset). | [optional] 
**AbsorptionTime** | **int?** | Expected carb absorption duration in minutes. | [optional] 
**CorrelationId** | **string** | Correlation identifier for grouping related events (e.g. a meal bolus and carb intake). | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

