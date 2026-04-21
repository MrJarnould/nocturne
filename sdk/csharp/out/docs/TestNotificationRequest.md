# NightscoutFoundation.Nocturne.Model.TestNotificationRequest
Request model for creating test notifications

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Type** | **InAppNotificationType** |  | [optional] 
**Urgency** | **NotificationUrgency** |  | [optional] 
**Title** | **string** | Optional title (defaults to \&quot;Test {Type} Notification\&quot;) | [optional] 
**Subtitle** | **string** | Optional subtitle | [optional] 
**SourceId** | **string** | Optional source ID for grouping | [optional] 
**Actions** | [**List&lt;NotificationActionDto&gt;**](NotificationActionDto.md) | Optional actions for the notification | [optional] 
**ResolutionConditions** | [**ResolutionConditions**](ResolutionConditions.md) |  | [optional] 
**Metadata** | **Dictionary&lt;string, Object&gt;** | Optional metadata | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

