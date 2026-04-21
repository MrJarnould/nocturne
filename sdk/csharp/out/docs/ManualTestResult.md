# NightscoutFoundation.Nocturne.Model.ManualTestResult
Result of manual API comparison test

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**QueryPath** | **string** | The API path that was tested | [optional] 
**Method** | **string** | HTTP method used | [optional] 
**Timestamp** | **DateTimeOffset** | When the test was performed | [optional] 
**NightscoutResponse** | **string** | Raw JSON response from Nightscout | [optional] 
**NocturneResponse** | **string** | Raw JSON response from Nocturne | [optional] 
**NightscoutStatusCode** | **int?** | HTTP status code from Nightscout | [optional] 
**NocturneStatusCode** | **int?** | HTTP status code from Nocturne | [optional] 
**NightscoutResponseTimeMs** | **long** | Response time from Nightscout in milliseconds | [optional] 
**NocturneResponseTimeMs** | **long** | Response time from Nocturne in milliseconds | [optional] 
**NightscoutError** | **string** | Error message if Nightscout request failed | [optional] 
**NocturneError** | **string** | Error message if Nocturne request failed | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

