# NightscoutFoundation.Nocturne.Model.PredictionCurves
Different prediction curves for visualization.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Default** | **List&lt;double&gt;** | Main prediction curve (mg/dL values at 5-min intervals) | [optional] 
**IobOnly** | **List&lt;double&gt;** | IOB-only prediction (ignoring COB) | [optional] 
**Uam** | **List&lt;double&gt;** | UAM (Unannounced Meal) prediction | [optional] 
**Cob** | **List&lt;double&gt;** | COB-based prediction | [optional] 
**ZeroTemp** | **List&lt;double&gt;** | Zero-temp prediction (what happens if basal stops) | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

