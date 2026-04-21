# NightscoutFoundation.Nocturne.Api.ConfigurationApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ConfigurationDeleteConfiguration**](ConfigurationApi.md#configurationdeleteconfiguration) | **DELETE** /api/v4/connectors/config/{connectorName} | Deletes all configuration and secrets for a connector. |
| [**ConfigurationGetAllConnectorStatus**](ConfigurationApi.md#configurationgetallconnectorstatus) | **GET** /api/v4/connectors/config | Gets status information for all registered connectors. |
| [**ConfigurationGetConfiguration**](ConfigurationApi.md#configurationgetconfiguration) | **GET** /api/v4/connectors/config/{connectorName} | Gets the configuration for a specific connector. Returns runtime configuration only (secrets are not included). |
| [**ConfigurationGetEffectiveConfiguration**](ConfigurationApi.md#configurationgeteffectiveconfiguration) | **GET** /api/v4/connectors/config/{connectorName}/effective | Gets the effective configuration from a running connector. This returns the actual runtime values including those resolved from environment variables. This endpoint is public since it only exposes non-secret configuration values. |
| [**ConfigurationGetSchema**](ConfigurationApi.md#configurationgetschema) | **GET** /api/v4/connectors/config/{connectorName}/schema | Gets the JSON Schema for a connector&#39;s configuration. Schema is generated from the connector&#39;s configuration class attributes. This endpoint is public since schema is just metadata, not sensitive data. |
| [**ConfigurationSaveConfiguration**](ConfigurationApi.md#configurationsaveconfiguration) | **PUT** /api/v4/connectors/config/{connectorName} | Saves or updates runtime configuration for a connector. Only properties marked with [RuntimeConfigurable] are accepted. Validates the configuration against the connector&#39;s schema before saving. |
| [**ConfigurationSaveSecrets**](ConfigurationApi.md#configurationsavesecrets) | **PUT** /api/v4/connectors/config/{connectorName}/secrets | Saves encrypted secrets for a connector. Secrets are encrypted using AES-256-GCM before storage. |
| [**ConfigurationSetActive**](ConfigurationApi.md#configurationsetactive) | **PATCH** /api/v4/connectors/config/{connectorName}/active | Enables or disables a connector. |

<a id="configurationdeleteconfiguration"></a>
# **ConfigurationDeleteConfiguration**
> void ConfigurationDeleteConfiguration (string connectorName)

Deletes all configuration and secrets for a connector.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using NightscoutFoundation.Nocturne.Api;
using NightscoutFoundation.Nocturne.Client;
using NightscoutFoundation.Nocturne.Model;

namespace Example
{
    public class ConfigurationDeleteConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConfigurationApi(httpClient, config, httpClientHandler);
            var connectorName = "connectorName_example";  // string | The connector name

            try
            {
                // Deletes all configuration and secrets for a connector.
                apiInstance.ConfigurationDeleteConfiguration(connectorName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConfigurationApi.ConfigurationDeleteConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConfigurationDeleteConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Deletes all configuration and secrets for a connector.
    apiInstance.ConfigurationDeleteConfigurationWithHttpInfo(connectorName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConfigurationApi.ConfigurationDeleteConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectorName** | **string** | The connector name |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="configurationgetallconnectorstatus"></a>
# **ConfigurationGetAllConnectorStatus**
> List&lt;ConnectorStatusInfo&gt; ConfigurationGetAllConnectorStatus ()

Gets status information for all registered connectors.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using NightscoutFoundation.Nocturne.Api;
using NightscoutFoundation.Nocturne.Client;
using NightscoutFoundation.Nocturne.Model;

namespace Example
{
    public class ConfigurationGetAllConnectorStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConfigurationApi(httpClient, config, httpClientHandler);

            try
            {
                // Gets status information for all registered connectors.
                List<ConnectorStatusInfo> result = apiInstance.ConfigurationGetAllConnectorStatus();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConfigurationApi.ConfigurationGetAllConnectorStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConfigurationGetAllConnectorStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets status information for all registered connectors.
    ApiResponse<List<ConnectorStatusInfo>> response = apiInstance.ConfigurationGetAllConnectorStatusWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConfigurationApi.ConfigurationGetAllConnectorStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;ConnectorStatusInfo&gt;**](ConnectorStatusInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of connector status information |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="configurationgetconfiguration"></a>
# **ConfigurationGetConfiguration**
> ConnectorConfigurationResponse ConfigurationGetConfiguration (string connectorName)

Gets the configuration for a specific connector. Returns runtime configuration only (secrets are not included).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using NightscoutFoundation.Nocturne.Api;
using NightscoutFoundation.Nocturne.Client;
using NightscoutFoundation.Nocturne.Model;

namespace Example
{
    public class ConfigurationGetConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConfigurationApi(httpClient, config, httpClientHandler);
            var connectorName = "connectorName_example";  // string | The connector name (e.g., \"Dexcom\", \"Glooko\")

            try
            {
                // Gets the configuration for a specific connector. Returns runtime configuration only (secrets are not included).
                ConnectorConfigurationResponse result = apiInstance.ConfigurationGetConfiguration(connectorName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConfigurationApi.ConfigurationGetConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConfigurationGetConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets the configuration for a specific connector. Returns runtime configuration only (secrets are not included).
    ApiResponse<ConnectorConfigurationResponse> response = apiInstance.ConfigurationGetConfigurationWithHttpInfo(connectorName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConfigurationApi.ConfigurationGetConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectorName** | **string** | The connector name (e.g., \&quot;Dexcom\&quot;, \&quot;Glooko\&quot;) |  |

### Return type

[**ConnectorConfigurationResponse**](ConnectorConfigurationResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Configuration response or 404 if not found |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="configurationgeteffectiveconfiguration"></a>
# **ConfigurationGetEffectiveConfiguration**
> Dictionary&lt;string, Object&gt; ConfigurationGetEffectiveConfiguration (string connectorName)

Gets the effective configuration from a running connector. This returns the actual runtime values including those resolved from environment variables. This endpoint is public since it only exposes non-secret configuration values.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using NightscoutFoundation.Nocturne.Api;
using NightscoutFoundation.Nocturne.Client;
using NightscoutFoundation.Nocturne.Model;

namespace Example
{
    public class ConfigurationGetEffectiveConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConfigurationApi(httpClient, config, httpClientHandler);
            var connectorName = "connectorName_example";  // string | The connector name

            try
            {
                // Gets the effective configuration from a running connector. This returns the actual runtime values including those resolved from environment variables. This endpoint is public since it only exposes non-secret configuration values.
                Dictionary<string, Object> result = apiInstance.ConfigurationGetEffectiveConfiguration(connectorName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConfigurationApi.ConfigurationGetEffectiveConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConfigurationGetEffectiveConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets the effective configuration from a running connector. This returns the actual runtime values including those resolved from environment variables. This endpoint is public since it only exposes non-secret configuration values.
    ApiResponse<Dictionary<string, Object>> response = apiInstance.ConfigurationGetEffectiveConfigurationWithHttpInfo(connectorName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConfigurationApi.ConfigurationGetEffectiveConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectorName** | **string** | The connector name |  |

### Return type

**Dictionary<string, Object>**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Dictionary of property names to effective values |  -  |
| **503** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="configurationgetschema"></a>
# **ConfigurationGetSchema**
> JsonDocument ConfigurationGetSchema (string connectorName)

Gets the JSON Schema for a connector's configuration. Schema is generated from the connector's configuration class attributes. This endpoint is public since schema is just metadata, not sensitive data.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using NightscoutFoundation.Nocturne.Api;
using NightscoutFoundation.Nocturne.Client;
using NightscoutFoundation.Nocturne.Model;

namespace Example
{
    public class ConfigurationGetSchemaExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConfigurationApi(httpClient, config, httpClientHandler);
            var connectorName = "connectorName_example";  // string | The connector name

            try
            {
                // Gets the JSON Schema for a connector's configuration. Schema is generated from the connector's configuration class attributes. This endpoint is public since schema is just metadata, not sensitive data.
                JsonDocument result = apiInstance.ConfigurationGetSchema(connectorName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConfigurationApi.ConfigurationGetSchema: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConfigurationGetSchemaWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets the JSON Schema for a connector's configuration. Schema is generated from the connector's configuration class attributes. This endpoint is public since schema is just metadata, not sensitive data.
    ApiResponse<JsonDocument> response = apiInstance.ConfigurationGetSchemaWithHttpInfo(connectorName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConfigurationApi.ConfigurationGetSchemaWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectorName** | **string** | The connector name |  |

### Return type

[**JsonDocument**](JsonDocument.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | JSON Schema document |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="configurationsaveconfiguration"></a>
# **ConfigurationSaveConfiguration**
> ConnectorConfigurationResponse ConfigurationSaveConfiguration (string connectorName, JsonDocument jsonDocument)

Saves or updates runtime configuration for a connector. Only properties marked with [RuntimeConfigurable] are accepted. Validates the configuration against the connector's schema before saving.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using NightscoutFoundation.Nocturne.Api;
using NightscoutFoundation.Nocturne.Client;
using NightscoutFoundation.Nocturne.Model;

namespace Example
{
    public class ConfigurationSaveConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConfigurationApi(httpClient, config, httpClientHandler);
            var connectorName = "connectorName_example";  // string | The connector name
            var jsonDocument = new JsonDocument(); // JsonDocument | Configuration values as JSON

            try
            {
                // Saves or updates runtime configuration for a connector. Only properties marked with [RuntimeConfigurable] are accepted. Validates the configuration against the connector's schema before saving.
                ConnectorConfigurationResponse result = apiInstance.ConfigurationSaveConfiguration(connectorName, jsonDocument);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConfigurationApi.ConfigurationSaveConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConfigurationSaveConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Saves or updates runtime configuration for a connector. Only properties marked with [RuntimeConfigurable] are accepted. Validates the configuration against the connector's schema before saving.
    ApiResponse<ConnectorConfigurationResponse> response = apiInstance.ConfigurationSaveConfigurationWithHttpInfo(connectorName, jsonDocument);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConfigurationApi.ConfigurationSaveConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectorName** | **string** | The connector name |  |
| **jsonDocument** | [**JsonDocument**](JsonDocument.md) | Configuration values as JSON |  |

### Return type

[**ConnectorConfigurationResponse**](ConnectorConfigurationResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The saved configuration |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="configurationsavesecrets"></a>
# **ConfigurationSaveSecrets**
> void ConfigurationSaveSecrets (string connectorName, Dictionary<string, string> requestBody)

Saves encrypted secrets for a connector. Secrets are encrypted using AES-256-GCM before storage.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using NightscoutFoundation.Nocturne.Api;
using NightscoutFoundation.Nocturne.Client;
using NightscoutFoundation.Nocturne.Model;

namespace Example
{
    public class ConfigurationSaveSecretsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConfigurationApi(httpClient, config, httpClientHandler);
            var connectorName = "connectorName_example";  // string | The connector name
            var requestBody = new Dictionary<string, string>(); // Dictionary<string, string> | Dictionary of secret property names to plaintext values

            try
            {
                // Saves encrypted secrets for a connector. Secrets are encrypted using AES-256-GCM before storage.
                apiInstance.ConfigurationSaveSecrets(connectorName, requestBody);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConfigurationApi.ConfigurationSaveSecrets: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConfigurationSaveSecretsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Saves encrypted secrets for a connector. Secrets are encrypted using AES-256-GCM before storage.
    apiInstance.ConfigurationSaveSecretsWithHttpInfo(connectorName, requestBody);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConfigurationApi.ConfigurationSaveSecretsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectorName** | **string** | The connector name |  |
| **requestBody** | [**Dictionary&lt;string, string&gt;**](string.md) | Dictionary of secret property names to plaintext values |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="configurationsetactive"></a>
# **ConfigurationSetActive**
> void ConfigurationSetActive (string connectorName, SetActiveRequest setActiveRequest)

Enables or disables a connector.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using NightscoutFoundation.Nocturne.Api;
using NightscoutFoundation.Nocturne.Client;
using NightscoutFoundation.Nocturne.Model;

namespace Example
{
    public class ConfigurationSetActiveExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConfigurationApi(httpClient, config, httpClientHandler);
            var connectorName = "connectorName_example";  // string | The connector name
            var setActiveRequest = new SetActiveRequest(); // SetActiveRequest | Request containing the active state

            try
            {
                // Enables or disables a connector.
                apiInstance.ConfigurationSetActive(connectorName, setActiveRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConfigurationApi.ConfigurationSetActive: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConfigurationSetActiveWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Enables or disables a connector.
    apiInstance.ConfigurationSetActiveWithHttpInfo(connectorName, setActiveRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConfigurationApi.ConfigurationSetActiveWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectorName** | **string** | The connector name |  |
| **setActiveRequest** | [**SetActiveRequest**](SetActiveRequest.md) | Request containing the active state |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

