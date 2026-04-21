# NightscoutFoundation.Nocturne.Model.ConnectorConfigSnapshotDto
Serialized representation of a connector configuration with decrypted secrets for snapshot export/import.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** |  | [optional] 
**TenantId** | **string** |  | [optional] 
**ConnectorName** | **string** |  | [optional] 
**ConfigurationJson** | **string** |  | [optional] 
**SecretsPlaintext** | **Dictionary&lt;string, string&gt;** |  | [optional] 
**SchemaVersion** | **int** |  | [optional] 
**LastModified** | **DateTimeOffset** |  | [optional] 
**ModifiedBy** | **string** |  | [optional] 
**SysCreatedAt** | **DateTimeOffset** |  | [optional] 
**SysUpdatedAt** | **DateTimeOffset** |  | [optional] 
**LastSyncAttempt** | **DateTimeOffset?** |  | [optional] 
**LastSuccessfulSync** | **DateTimeOffset?** |  | [optional] 
**LastErrorMessage** | **string** |  | [optional] 
**LastErrorAt** | **DateTimeOffset?** |  | [optional] 
**IsHealthy** | **bool** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

