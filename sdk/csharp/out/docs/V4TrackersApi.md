# NightscoutFoundation.Nocturne.Api.V4TrackersApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**TrackersAckInstance**](V4TrackersApi.md#trackersackinstance) | **POST** /api/v4/trackers/instances/{id}/ack | Acknowledge/snooze a tracker notification |
| [**TrackersApplyPreset**](V4TrackersApi.md#trackersapplypreset) | **POST** /api/v4/trackers/presets/{id}/apply | Apply a preset (starts a new instance) |
| [**TrackersCompleteInstance**](V4TrackersApi.md#trackerscompleteinstance) | **PUT** /api/v4/trackers/instances/{id}/complete | Complete a tracker instance |
| [**TrackersCreateDefinition**](V4TrackersApi.md#trackerscreatedefinition) | **POST** /api/v4/trackers/definitions | Create a new tracker definition |
| [**TrackersCreatePreset**](V4TrackersApi.md#trackerscreatepreset) | **POST** /api/v4/trackers/presets | Create a new preset |
| [**TrackersDeleteDefinition**](V4TrackersApi.md#trackersdeletedefinition) | **DELETE** /api/v4/trackers/definitions/{id} | Delete a tracker definition |
| [**TrackersDeleteInstance**](V4TrackersApi.md#trackersdeleteinstance) | **DELETE** /api/v4/trackers/instances/{id} | Delete a tracker instance |
| [**TrackersDeletePreset**](V4TrackersApi.md#trackersdeletepreset) | **DELETE** /api/v4/trackers/presets/{id} | Delete a preset |
| [**TrackersGetActiveInstances**](V4TrackersApi.md#trackersgetactiveinstances) | **GET** /api/v4/trackers/instances | Get active tracker instances |
| [**TrackersGetDefinition**](V4TrackersApi.md#trackersgetdefinition) | **GET** /api/v4/trackers/definitions/{id} | Get a specific tracker definition |
| [**TrackersGetDefinitions**](V4TrackersApi.md#trackersgetdefinitions) | **GET** /api/v4/trackers/definitions | Get all tracker definitions. Returns public trackers for unauthenticated users, or all visible trackers for authenticated users. |
| [**TrackersGetInstanceHistory**](V4TrackersApi.md#trackersgetinstancehistory) | **GET** /api/v4/trackers/instances/history | Get completed tracker instances (history) |
| [**TrackersGetPresets**](V4TrackersApi.md#trackersgetpresets) | **GET** /api/v4/trackers/presets | Get all presets for the current user |
| [**TrackersGetUpcomingInstances**](V4TrackersApi.md#trackersgetupcominginstances) | **GET** /api/v4/trackers/instances/upcoming | Get upcoming tracker expirations for calendar |
| [**TrackersStartInstance**](V4TrackersApi.md#trackersstartinstance) | **POST** /api/v4/trackers/instances | Start a new tracker instance |
| [**TrackersUpdateDefinition**](V4TrackersApi.md#trackersupdatedefinition) | **PUT** /api/v4/trackers/definitions/{id} | Update a tracker definition |

<a id="trackersackinstance"></a>
# **TrackersAckInstance**
> void TrackersAckInstance (string id, AckTrackerRequest ackTrackerRequest)

Acknowledge/snooze a tracker notification

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
    public class TrackersAckInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var ackTrackerRequest = new AckTrackerRequest(); // AckTrackerRequest | 

            try
            {
                // Acknowledge/snooze a tracker notification
                apiInstance.TrackersAckInstance(id, ackTrackerRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersAckInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersAckInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Acknowledge/snooze a tracker notification
    apiInstance.TrackersAckInstanceWithHttpInfo(id, ackTrackerRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersAckInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **ackTrackerRequest** | [**AckTrackerRequest**](AckTrackerRequest.md) |  |  |

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

<a id="trackersapplypreset"></a>
# **TrackersApplyPreset**
> TrackerInstanceDto TrackersApplyPreset (string id, ApplyPresetRequest? applyPresetRequest = null)

Apply a preset (starts a new instance)

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
    public class TrackersApplyPresetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var applyPresetRequest = new ApplyPresetRequest?(); // ApplyPresetRequest? |  (optional) 

            try
            {
                // Apply a preset (starts a new instance)
                TrackerInstanceDto result = apiInstance.TrackersApplyPreset(id, applyPresetRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersApplyPreset: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersApplyPresetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Apply a preset (starts a new instance)
    ApiResponse<TrackerInstanceDto> response = apiInstance.TrackersApplyPresetWithHttpInfo(id, applyPresetRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersApplyPresetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **applyPresetRequest** | [**ApplyPresetRequest?**](ApplyPresetRequest?.md) |  | [optional]  |

### Return type

[**TrackerInstanceDto**](TrackerInstanceDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackerscompleteinstance"></a>
# **TrackersCompleteInstance**
> TrackerInstanceDto TrackersCompleteInstance (string id, CompleteTrackerInstanceRequest completeTrackerInstanceRequest)

Complete a tracker instance

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
    public class TrackersCompleteInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var completeTrackerInstanceRequest = new CompleteTrackerInstanceRequest(); // CompleteTrackerInstanceRequest | 

            try
            {
                // Complete a tracker instance
                TrackerInstanceDto result = apiInstance.TrackersCompleteInstance(id, completeTrackerInstanceRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersCompleteInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersCompleteInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Complete a tracker instance
    ApiResponse<TrackerInstanceDto> response = apiInstance.TrackersCompleteInstanceWithHttpInfo(id, completeTrackerInstanceRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersCompleteInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **completeTrackerInstanceRequest** | [**CompleteTrackerInstanceRequest**](CompleteTrackerInstanceRequest.md) |  |  |

### Return type

[**TrackerInstanceDto**](TrackerInstanceDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackerscreatedefinition"></a>
# **TrackersCreateDefinition**
> TrackerDefinitionDto TrackersCreateDefinition (CreateTrackerDefinitionRequest createTrackerDefinitionRequest)

Create a new tracker definition

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
    public class TrackersCreateDefinitionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var createTrackerDefinitionRequest = new CreateTrackerDefinitionRequest(); // CreateTrackerDefinitionRequest | 

            try
            {
                // Create a new tracker definition
                TrackerDefinitionDto result = apiInstance.TrackersCreateDefinition(createTrackerDefinitionRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersCreateDefinition: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersCreateDefinitionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a new tracker definition
    ApiResponse<TrackerDefinitionDto> response = apiInstance.TrackersCreateDefinitionWithHttpInfo(createTrackerDefinitionRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersCreateDefinitionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createTrackerDefinitionRequest** | [**CreateTrackerDefinitionRequest**](CreateTrackerDefinitionRequest.md) |  |  |

### Return type

[**TrackerDefinitionDto**](TrackerDefinitionDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackerscreatepreset"></a>
# **TrackersCreatePreset**
> TrackerPresetDto TrackersCreatePreset (CreateTrackerPresetRequest createTrackerPresetRequest)

Create a new preset

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
    public class TrackersCreatePresetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var createTrackerPresetRequest = new CreateTrackerPresetRequest(); // CreateTrackerPresetRequest | 

            try
            {
                // Create a new preset
                TrackerPresetDto result = apiInstance.TrackersCreatePreset(createTrackerPresetRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersCreatePreset: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersCreatePresetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a new preset
    ApiResponse<TrackerPresetDto> response = apiInstance.TrackersCreatePresetWithHttpInfo(createTrackerPresetRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersCreatePresetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createTrackerPresetRequest** | [**CreateTrackerPresetRequest**](CreateTrackerPresetRequest.md) |  |  |

### Return type

[**TrackerPresetDto**](TrackerPresetDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersdeletedefinition"></a>
# **TrackersDeleteDefinition**
> void TrackersDeleteDefinition (string id)

Delete a tracker definition

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
    public class TrackersDeleteDefinitionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a tracker definition
                apiInstance.TrackersDeleteDefinition(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersDeleteDefinition: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersDeleteDefinitionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a tracker definition
    apiInstance.TrackersDeleteDefinitionWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersDeleteDefinitionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersdeleteinstance"></a>
# **TrackersDeleteInstance**
> void TrackersDeleteInstance (string id)

Delete a tracker instance

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
    public class TrackersDeleteInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a tracker instance
                apiInstance.TrackersDeleteInstance(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersDeleteInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersDeleteInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a tracker instance
    apiInstance.TrackersDeleteInstanceWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersDeleteInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersdeletepreset"></a>
# **TrackersDeletePreset**
> void TrackersDeletePreset (string id)

Delete a preset

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
    public class TrackersDeletePresetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a preset
                apiInstance.TrackersDeletePreset(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersDeletePreset: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersDeletePresetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a preset
    apiInstance.TrackersDeletePresetWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersDeletePresetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersgetactiveinstances"></a>
# **TrackersGetActiveInstances**
> List&lt;TrackerInstanceDto&gt; TrackersGetActiveInstances ()

Get active tracker instances

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
    public class TrackersGetActiveInstancesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);

            try
            {
                // Get active tracker instances
                List<TrackerInstanceDto> result = apiInstance.TrackersGetActiveInstances();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersGetActiveInstances: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersGetActiveInstancesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get active tracker instances
    ApiResponse<List<TrackerInstanceDto>> response = apiInstance.TrackersGetActiveInstancesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersGetActiveInstancesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;TrackerInstanceDto&gt;**](TrackerInstanceDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersgetdefinition"></a>
# **TrackersGetDefinition**
> TrackerDefinitionDto TrackersGetDefinition (string id)

Get a specific tracker definition

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
    public class TrackersGetDefinitionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Get a specific tracker definition
                TrackerDefinitionDto result = apiInstance.TrackersGetDefinition(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersGetDefinition: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersGetDefinitionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a specific tracker definition
    ApiResponse<TrackerDefinitionDto> response = apiInstance.TrackersGetDefinitionWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersGetDefinitionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**TrackerDefinitionDto**](TrackerDefinitionDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersgetdefinitions"></a>
# **TrackersGetDefinitions**
> List&lt;TrackerDefinitionDto&gt; TrackersGetDefinitions (TrackersGetDefinitionsCategoryParameter? category = null)

Get all tracker definitions. Returns public trackers for unauthenticated users, or all visible trackers for authenticated users.

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
    public class TrackersGetDefinitionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var category = new TrackersGetDefinitionsCategoryParameter?(); // TrackersGetDefinitionsCategoryParameter? |  (optional) 

            try
            {
                // Get all tracker definitions. Returns public trackers for unauthenticated users, or all visible trackers for authenticated users.
                List<TrackerDefinitionDto> result = apiInstance.TrackersGetDefinitions(category);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersGetDefinitions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersGetDefinitionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all tracker definitions. Returns public trackers for unauthenticated users, or all visible trackers for authenticated users.
    ApiResponse<List<TrackerDefinitionDto>> response = apiInstance.TrackersGetDefinitionsWithHttpInfo(category);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersGetDefinitionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **category** | [**TrackersGetDefinitionsCategoryParameter?**](TrackersGetDefinitionsCategoryParameter?.md) |  | [optional]  |

### Return type

[**List&lt;TrackerDefinitionDto&gt;**](TrackerDefinitionDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersgetinstancehistory"></a>
# **TrackersGetInstanceHistory**
> List&lt;TrackerInstanceDto&gt; TrackersGetInstanceHistory (int? limit = null)

Get completed tracker instances (history)

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
    public class TrackersGetInstanceHistoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var limit = 100;  // int? |  (optional)  (default to 100)

            try
            {
                // Get completed tracker instances (history)
                List<TrackerInstanceDto> result = apiInstance.TrackersGetInstanceHistory(limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersGetInstanceHistory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersGetInstanceHistoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get completed tracker instances (history)
    ApiResponse<List<TrackerInstanceDto>> response = apiInstance.TrackersGetInstanceHistoryWithHttpInfo(limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersGetInstanceHistoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **limit** | **int?** |  | [optional] [default to 100] |

### Return type

[**List&lt;TrackerInstanceDto&gt;**](TrackerInstanceDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersgetpresets"></a>
# **TrackersGetPresets**
> List&lt;TrackerPresetDto&gt; TrackersGetPresets ()

Get all presets for the current user

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
    public class TrackersGetPresetsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);

            try
            {
                // Get all presets for the current user
                List<TrackerPresetDto> result = apiInstance.TrackersGetPresets();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersGetPresets: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersGetPresetsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all presets for the current user
    ApiResponse<List<TrackerPresetDto>> response = apiInstance.TrackersGetPresetsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersGetPresetsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;TrackerPresetDto&gt;**](TrackerPresetDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersgetupcominginstances"></a>
# **TrackersGetUpcomingInstances**
> List&lt;TrackerInstanceDto&gt; TrackersGetUpcomingInstances (DateTimeOffset? from = null, DateTimeOffset? to = null)

Get upcoming tracker expirations for calendar

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
    public class TrackersGetUpcomingInstancesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 

            try
            {
                // Get upcoming tracker expirations for calendar
                List<TrackerInstanceDto> result = apiInstance.TrackersGetUpcomingInstances(from, to);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersGetUpcomingInstances: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersGetUpcomingInstancesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get upcoming tracker expirations for calendar
    ApiResponse<List<TrackerInstanceDto>> response = apiInstance.TrackersGetUpcomingInstancesWithHttpInfo(from, to);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersGetUpcomingInstancesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **from** | **DateTimeOffset?** |  | [optional]  |
| **to** | **DateTimeOffset?** |  | [optional]  |

### Return type

[**List&lt;TrackerInstanceDto&gt;**](TrackerInstanceDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersstartinstance"></a>
# **TrackersStartInstance**
> TrackerInstanceDto TrackersStartInstance (StartTrackerInstanceRequest startTrackerInstanceRequest)

Start a new tracker instance

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
    public class TrackersStartInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var startTrackerInstanceRequest = new StartTrackerInstanceRequest(); // StartTrackerInstanceRequest | 

            try
            {
                // Start a new tracker instance
                TrackerInstanceDto result = apiInstance.TrackersStartInstance(startTrackerInstanceRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersStartInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersStartInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start a new tracker instance
    ApiResponse<TrackerInstanceDto> response = apiInstance.TrackersStartInstanceWithHttpInfo(startTrackerInstanceRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersStartInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startTrackerInstanceRequest** | [**StartTrackerInstanceRequest**](StartTrackerInstanceRequest.md) |  |  |

### Return type

[**TrackerInstanceDto**](TrackerInstanceDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackersupdatedefinition"></a>
# **TrackersUpdateDefinition**
> TrackerDefinitionDto TrackersUpdateDefinition (string id, UpdateTrackerDefinitionRequest updateTrackerDefinitionRequest)

Update a tracker definition

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
    public class TrackersUpdateDefinitionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TrackersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var updateTrackerDefinitionRequest = new UpdateTrackerDefinitionRequest(); // UpdateTrackerDefinitionRequest | 

            try
            {
                // Update a tracker definition
                TrackerDefinitionDto result = apiInstance.TrackersUpdateDefinition(id, updateTrackerDefinitionRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TrackersApi.TrackersUpdateDefinition: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackersUpdateDefinitionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update a tracker definition
    ApiResponse<TrackerDefinitionDto> response = apiInstance.TrackersUpdateDefinitionWithHttpInfo(id, updateTrackerDefinitionRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TrackersApi.TrackersUpdateDefinitionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **updateTrackerDefinitionRequest** | [**UpdateTrackerDefinitionRequest**](UpdateTrackerDefinitionRequest.md) |  |  |

### Return type

[**TrackerDefinitionDto**](TrackerDefinitionDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

