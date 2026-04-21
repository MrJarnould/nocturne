# NightscoutFoundation.Nocturne.Api.V4ServicesApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ServicesDeleteConnectorData**](V4ServicesApi.md#servicesdeleteconnectordata) | **DELETE** /api/v4/services/connectors/{id}/data | Delete all data from a specific connector. WARNING: This is a destructive operation that cannot be undone. |
| [**ServicesDeleteDataSourceData**](V4ServicesApi.md#servicesdeletedatasourcedata) | **DELETE** /api/v4/services/data-sources/{id} | Delete all data from a specific data source. WARNING: This is a destructive operation that cannot be undone. |
| [**ServicesDeleteDemoData**](V4ServicesApi.md#servicesdeletedemodata) | **DELETE** /api/v4/services/data-sources/demo | Delete all demo data. This operation is safe as demo data can be easily regenerated. |
| [**ServicesGetActiveDataSources**](V4ServicesApi.md#servicesgetactivedatasources) | **GET** /api/v4/services/data-sources | Get all active data sources that have been sending data to this Nocturne instance. This includes CGM apps, AID systems, and any other uploaders. |
| [**ServicesGetApiInfo**](V4ServicesApi.md#servicesgetapiinfo) | **GET** /api/v4/services/api-info | Get API endpoint information for configuring external apps. This provides all the information needed to configure xDrip+, Loop, AAPS, etc. |
| [**ServicesGetAvailableConnectors**](V4ServicesApi.md#servicesgetavailableconnectors) | **GET** /api/v4/services/connectors | Get available connectors that can be configured to pull data into Nocturne. |
| [**ServicesGetConnectorCapabilities**](V4ServicesApi.md#servicesgetconnectorcapabilities) | **GET** /api/v4/services/connectors/{id}/capabilities | Get capabilities for a specific connector. |
| [**ServicesGetConnectorDataSummary**](V4ServicesApi.md#servicesgetconnectordatasummary) | **GET** /api/v4/services/connectors/{id}/data-summary | Get a summary of data counts for a specific connector. Returns the number of entries, treatments, and device statuses synced by this connector. |
| [**ServicesGetConnectorSyncStatus**](V4ServicesApi.md#servicesgetconnectorsyncstatus) | **GET** /api/v4/services/connectors/{id}/sync-status | Get sync status for a specific connector, including latest timestamps and connector state. Used by connectors on startup to determine where to resume syncing from. |
| [**ServicesGetDataSource**](V4ServicesApi.md#servicesgetdatasource) | **GET** /api/v4/services/data-sources/{id} | Get detailed information about a specific data source. |
| [**ServicesGetServicesOverview**](V4ServicesApi.md#servicesgetservicesoverview) | **GET** /api/v4/services | Get a complete overview of services, data sources, and available integrations. |
| [**ServicesGetUploaderApps**](V4ServicesApi.md#servicesgetuploaderapps) | **GET** /api/v4/services/uploaders | Get uploader apps that can push data to Nocturne with setup instructions. |
| [**ServicesGetUploaderSetup**](V4ServicesApi.md#servicesgetuploadersetup) | **GET** /api/v4/services/uploaders/{appId}/setup | Get setup instructions for a specific uploader app. |
| [**ServicesTriggerConnectorSync**](V4ServicesApi.md#servicestriggerconnectorsync) | **POST** /api/v4/services/connectors/{id}/sync | Trigger a manual sync for a specific connector. |

<a id="servicesdeleteconnectordata"></a>
# **ServicesDeleteConnectorData**
> DataSourceDeleteResult ServicesDeleteConnectorData (string id)

Delete all data from a specific connector. WARNING: This is a destructive operation that cannot be undone.

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
    public class ServicesDeleteConnectorDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | Connector ID (e.g., \"dexcom\")

            try
            {
                // Delete all data from a specific connector. WARNING: This is a destructive operation that cannot be undone.
                DataSourceDeleteResult result = apiInstance.ServicesDeleteConnectorData(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesDeleteConnectorData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesDeleteConnectorDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete all data from a specific connector. WARNING: This is a destructive operation that cannot be undone.
    ApiResponse<DataSourceDeleteResult> response = apiInstance.ServicesDeleteConnectorDataWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesDeleteConnectorDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Connector ID (e.g., \&quot;dexcom\&quot;) |  |

### Return type

[**DataSourceDeleteResult**](DataSourceDeleteResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Result of the delete operation |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesdeletedatasourcedata"></a>
# **ServicesDeleteDataSourceData**
> DataSourceDeleteResult ServicesDeleteDataSourceData (string id)

Delete all data from a specific data source. WARNING: This is a destructive operation that cannot be undone.

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
    public class ServicesDeleteDataSourceDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | Data source ID or device ID

            try
            {
                // Delete all data from a specific data source. WARNING: This is a destructive operation that cannot be undone.
                DataSourceDeleteResult result = apiInstance.ServicesDeleteDataSourceData(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesDeleteDataSourceData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesDeleteDataSourceDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete all data from a specific data source. WARNING: This is a destructive operation that cannot be undone.
    ApiResponse<DataSourceDeleteResult> response = apiInstance.ServicesDeleteDataSourceDataWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesDeleteDataSourceDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Data source ID or device ID |  |

### Return type

[**DataSourceDeleteResult**](DataSourceDeleteResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Result of the delete operation |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesdeletedemodata"></a>
# **ServicesDeleteDemoData**
> DataSourceDeleteResult ServicesDeleteDemoData ()

Delete all demo data. This operation is safe as demo data can be easily regenerated.

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
    public class ServicesDeleteDemoDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);

            try
            {
                // Delete all demo data. This operation is safe as demo data can be easily regenerated.
                DataSourceDeleteResult result = apiInstance.ServicesDeleteDemoData();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesDeleteDemoData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesDeleteDemoDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete all demo data. This operation is safe as demo data can be easily regenerated.
    ApiResponse<DataSourceDeleteResult> response = apiInstance.ServicesDeleteDemoDataWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesDeleteDemoDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**DataSourceDeleteResult**](DataSourceDeleteResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Result of the delete operation |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesgetactivedatasources"></a>
# **ServicesGetActiveDataSources**
> List&lt;DataSourceInfo&gt; ServicesGetActiveDataSources ()

Get all active data sources that have been sending data to this Nocturne instance. This includes CGM apps, AID systems, and any other uploaders.

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
    public class ServicesGetActiveDataSourcesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);

            try
            {
                // Get all active data sources that have been sending data to this Nocturne instance. This includes CGM apps, AID systems, and any other uploaders.
                List<DataSourceInfo> result = apiInstance.ServicesGetActiveDataSources();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesGetActiveDataSources: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesGetActiveDataSourcesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all active data sources that have been sending data to this Nocturne instance. This includes CGM apps, AID systems, and any other uploaders.
    ApiResponse<List<DataSourceInfo>> response = apiInstance.ServicesGetActiveDataSourcesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesGetActiveDataSourcesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;DataSourceInfo&gt;**](DataSourceInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of active data sources with their status |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesgetapiinfo"></a>
# **ServicesGetApiInfo**
> ApiEndpointInfo ServicesGetApiInfo ()

Get API endpoint information for configuring external apps. This provides all the information needed to configure xDrip+, Loop, AAPS, etc.

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
    public class ServicesGetApiInfoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);

            try
            {
                // Get API endpoint information for configuring external apps. This provides all the information needed to configure xDrip+, Loop, AAPS, etc.
                ApiEndpointInfo result = apiInstance.ServicesGetApiInfo();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesGetApiInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesGetApiInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get API endpoint information for configuring external apps. This provides all the information needed to configure xDrip+, Loop, AAPS, etc.
    ApiResponse<ApiEndpointInfo> response = apiInstance.ServicesGetApiInfoWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesGetApiInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ApiEndpointInfo**](ApiEndpointInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API endpoint information |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesgetavailableconnectors"></a>
# **ServicesGetAvailableConnectors**
> List&lt;AvailableConnector&gt; ServicesGetAvailableConnectors ()

Get available connectors that can be configured to pull data into Nocturne.

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
    public class ServicesGetAvailableConnectorsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);

            try
            {
                // Get available connectors that can be configured to pull data into Nocturne.
                List<AvailableConnector> result = apiInstance.ServicesGetAvailableConnectors();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesGetAvailableConnectors: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesGetAvailableConnectorsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get available connectors that can be configured to pull data into Nocturne.
    ApiResponse<List<AvailableConnector>> response = apiInstance.ServicesGetAvailableConnectorsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesGetAvailableConnectorsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;AvailableConnector&gt;**](AvailableConnector.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of available connectors |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesgetconnectorcapabilities"></a>
# **ServicesGetConnectorCapabilities**
> ConnectorCapabilities ServicesGetConnectorCapabilities (string id)

Get capabilities for a specific connector.

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
    public class ServicesGetConnectorCapabilitiesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The connector ID (e.g., \"dexcom\", \"libre\")

            try
            {
                // Get capabilities for a specific connector.
                ConnectorCapabilities result = apiInstance.ServicesGetConnectorCapabilities(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesGetConnectorCapabilities: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesGetConnectorCapabilitiesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get capabilities for a specific connector.
    ApiResponse<ConnectorCapabilities> response = apiInstance.ServicesGetConnectorCapabilitiesWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesGetConnectorCapabilitiesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The connector ID (e.g., \&quot;dexcom\&quot;, \&quot;libre\&quot;) |  |

### Return type

[**ConnectorCapabilities**](ConnectorCapabilities.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Connector capabilities |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesgetconnectordatasummary"></a>
# **ServicesGetConnectorDataSummary**
> ConnectorDataSummary ServicesGetConnectorDataSummary (string id)

Get a summary of data counts for a specific connector. Returns the number of entries, treatments, and device statuses synced by this connector.

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
    public class ServicesGetConnectorDataSummaryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | Connector ID (e.g., \"dexcom\")

            try
            {
                // Get a summary of data counts for a specific connector. Returns the number of entries, treatments, and device statuses synced by this connector.
                ConnectorDataSummary result = apiInstance.ServicesGetConnectorDataSummary(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesGetConnectorDataSummary: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesGetConnectorDataSummaryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a summary of data counts for a specific connector. Returns the number of entries, treatments, and device statuses synced by this connector.
    ApiResponse<ConnectorDataSummary> response = apiInstance.ServicesGetConnectorDataSummaryWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesGetConnectorDataSummaryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Connector ID (e.g., \&quot;dexcom\&quot;) |  |

### Return type

[**ConnectorDataSummary**](ConnectorDataSummary.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Data summary with counts by type |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesgetconnectorsyncstatus"></a>
# **ServicesGetConnectorSyncStatus**
> ConnectorSyncStatus ServicesGetConnectorSyncStatus (string id)

Get sync status for a specific connector, including latest timestamps and connector state. Used by connectors on startup to determine where to resume syncing from.

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
    public class ServicesGetConnectorSyncStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The connector ID (e.g., \"dexcom\", \"libre\", \"glooko\")

            try
            {
                // Get sync status for a specific connector, including latest timestamps and connector state. Used by connectors on startup to determine where to resume syncing from.
                ConnectorSyncStatus result = apiInstance.ServicesGetConnectorSyncStatus(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesGetConnectorSyncStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesGetConnectorSyncStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get sync status for a specific connector, including latest timestamps and connector state. Used by connectors on startup to determine where to resume syncing from.
    ApiResponse<ConnectorSyncStatus> response = apiInstance.ServicesGetConnectorSyncStatusWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesGetConnectorSyncStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The connector ID (e.g., \&quot;dexcom\&quot;, \&quot;libre\&quot;, \&quot;glooko\&quot;) |  |

### Return type

[**ConnectorSyncStatus**](ConnectorSyncStatus.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Complete sync status including timestamps for entries, treatments, and connector state |  -  |
| **400** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesgetdatasource"></a>
# **ServicesGetDataSource**
> DataSourceInfo ServicesGetDataSource (string id)

Get detailed information about a specific data source.

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
    public class ServicesGetDataSourceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | Data source ID or device ID

            try
            {
                // Get detailed information about a specific data source.
                DataSourceInfo result = apiInstance.ServicesGetDataSource(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesGetDataSource: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesGetDataSourceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get detailed information about a specific data source.
    ApiResponse<DataSourceInfo> response = apiInstance.ServicesGetDataSourceWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesGetDataSourceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Data source ID or device ID |  |

### Return type

[**DataSourceInfo**](DataSourceInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Data source information if found |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesgetservicesoverview"></a>
# **ServicesGetServicesOverview**
> ServicesOverview ServicesGetServicesOverview ()

Get a complete overview of services, data sources, and available integrations.

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
    public class ServicesGetServicesOverviewExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);

            try
            {
                // Get a complete overview of services, data sources, and available integrations.
                ServicesOverview result = apiInstance.ServicesGetServicesOverview();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesGetServicesOverview: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesGetServicesOverviewWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a complete overview of services, data sources, and available integrations.
    ApiResponse<ServicesOverview> response = apiInstance.ServicesGetServicesOverviewWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesGetServicesOverviewWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ServicesOverview**](ServicesOverview.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Complete services overview including active data sources, connectors, and uploader apps |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesgetuploaderapps"></a>
# **ServicesGetUploaderApps**
> List&lt;UploaderApp&gt; ServicesGetUploaderApps ()

Get uploader apps that can push data to Nocturne with setup instructions.

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
    public class ServicesGetUploaderAppsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);

            try
            {
                // Get uploader apps that can push data to Nocturne with setup instructions.
                List<UploaderApp> result = apiInstance.ServicesGetUploaderApps();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesGetUploaderApps: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesGetUploaderAppsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get uploader apps that can push data to Nocturne with setup instructions.
    ApiResponse<List<UploaderApp>> response = apiInstance.ServicesGetUploaderAppsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesGetUploaderAppsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;UploaderApp&gt;**](UploaderApp.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of uploader apps with setup instructions |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicesgetuploadersetup"></a>
# **ServicesGetUploaderSetup**
> UploaderSetupResponse ServicesGetUploaderSetup (string appId)

Get setup instructions for a specific uploader app.

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
    public class ServicesGetUploaderSetupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);
            var appId = "appId_example";  // string | The uploader app ID (e.g., \"xdrip\", \"loop\", \"aaps\")

            try
            {
                // Get setup instructions for a specific uploader app.
                UploaderSetupResponse result = apiInstance.ServicesGetUploaderSetup(appId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesGetUploaderSetup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesGetUploaderSetupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get setup instructions for a specific uploader app.
    ApiResponse<UploaderSetupResponse> response = apiInstance.ServicesGetUploaderSetupWithHttpInfo(appId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesGetUploaderSetupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **appId** | **string** | The uploader app ID (e.g., \&quot;xdrip\&quot;, \&quot;loop\&quot;, \&quot;aaps\&quot;) |  |

### Return type

[**UploaderSetupResponse**](UploaderSetupResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Setup instructions for the specified app |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="servicestriggerconnectorsync"></a>
# **ServicesTriggerConnectorSync**
> SyncResult ServicesTriggerConnectorSync (string id, SyncRequest syncRequest)

Trigger a manual sync for a specific connector.

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
    public class ServicesTriggerConnectorSyncExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ServicesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | Connector ID (e.g., \"dexcom\", \"tidepool\")
            var syncRequest = new SyncRequest(); // SyncRequest | Sync request parameters (date range and data types)

            try
            {
                // Trigger a manual sync for a specific connector.
                SyncResult result = apiInstance.ServicesTriggerConnectorSync(id, syncRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ServicesApi.ServicesTriggerConnectorSync: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServicesTriggerConnectorSyncWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Trigger a manual sync for a specific connector.
    ApiResponse<SyncResult> response = apiInstance.ServicesTriggerConnectorSyncWithHttpInfo(id, syncRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ServicesApi.ServicesTriggerConnectorSyncWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Connector ID (e.g., \&quot;dexcom\&quot;, \&quot;tidepool\&quot;) |  |
| **syncRequest** | [**SyncRequest**](SyncRequest.md) | Sync request parameters (date range and data types) |  |

### Return type

[**SyncResult**](SyncResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Sync result with success status and details |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

