# NightscoutFoundation.Nocturne.Model.TenantMemberEntityDto
Serialized representation of a tenant membership (subject-to-tenant link) for snapshot export/import.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** |  | [optional] 
**TenantId** | **string** |  | [optional] 
**SubjectId** | **string** |  | [optional] 
**SysCreatedAt** | **DateTimeOffset** |  | [optional] 
**SysUpdatedAt** | **DateTimeOffset** |  | [optional] 
**DirectPermissions** | **List&lt;string&gt;** |  | [optional] 
**Label** | **string** |  | [optional] 
**LimitTo24Hours** | **bool** |  | [optional] 
**CreatedFromInviteId** | **string** |  | [optional] 
**LastUsedAt** | **DateTimeOffset?** |  | [optional] 
**LastUsedIp** | **string** |  | [optional] 
**LastUsedUserAgent** | **string** |  | [optional] 
**RevokedAt** | **DateTimeOffset?** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

