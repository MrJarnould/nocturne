# NightscoutFoundation.Nocturne.Model.PasskeyCredentialEntityDto
Serialized representation of a WebAuthn passkey credential. Binary fields (CredentialId, PublicKey) are base64-encoded strings for JSON portability.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** |  | [optional] 
**SubjectId** | **string** |  | [optional] 
**CredentialId** | **string** |  | [optional] 
**PublicKey** | **string** |  | [optional] 
**SignCount** | **int** |  | [optional] 
**Transports** | **List&lt;string&gt;** |  | [optional] 
**Label** | **string** |  | [optional] 
**CreatedAt** | **DateTimeOffset** |  | [optional] 
**LastUsedAt** | **DateTimeOffset?** |  | [optional] 
**AaGuid** | **string** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

