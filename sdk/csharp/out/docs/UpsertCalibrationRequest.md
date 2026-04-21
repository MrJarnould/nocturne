# NightscoutFoundation.Nocturne.Model.UpsertCalibrationRequest
Request body for upserting a CGM sensor calibration record via the V4 API.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Timestamp** | **DateTimeOffset** | When the calibration was performed. | [optional] 
**UtcOffset** | **int?** | UTC offset in minutes at the time of the event, for local-time display. | [optional] 
**Device** | **string** | Identifier of the CGM device being calibrated. | [optional] 
**App** | **string** | Name of the application that submitted this record. | [optional] 
**DataSource** | **string** | Upstream data source identifier. | [optional] 
**Slope** | **double?** | Linear calibration slope coefficient. | [optional] 
**Intercept** | **double?** | Linear calibration intercept value. | [optional] 
**Scale** | **double?** | Calibration scale factor. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

