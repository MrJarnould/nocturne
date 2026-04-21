# NightscoutFoundation.Nocturne.Model.ManualTestRequest
Request for manual API comparison test

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**NightscoutUrl** | **string** | Base URL of the Nightscout server to compare against | [optional] 
**ApiSecret** | **string** | API secret (SHA1 hash or plain text) | [optional] 
**QueryPath** | **string** | API path to test (e.g., /api/v1/entries?count&#x3D;10) | [optional] 
**Method** | **string** | HTTP method (GET, POST, etc.) - defaults to GET | [optional] 
**RequestBody** | **string** | Optional request body for POST/PUT requests | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

