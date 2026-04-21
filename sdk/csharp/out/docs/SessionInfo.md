# NightscoutFoundation.Nocturne.Model.SessionInfo
Current session information

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**IsAuthenticated** | **bool** | Whether the user is authenticated | [optional] 
**SubjectId** | **string** | Subject ID | [optional] 
**Name** | **string** | User name | [optional] 
**Email** | **string** | Email address | [optional] 
**Roles** | **List&lt;string&gt;** | Assigned roles | [optional] 
**Permissions** | **List&lt;string&gt;** | Resolved permissions | [optional] 
**ExpiresAt** | **DateTimeOffset?** | Session expiration time | [optional] 
**PreferredLanguage** | **string** | User&#39;s preferred language code (e.g., \&quot;en\&quot;, \&quot;fr\&quot;, \&quot;de\&quot;) | [optional] 
**IsPlatformAdmin** | **bool** | Whether this subject has platform-level admin access | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

