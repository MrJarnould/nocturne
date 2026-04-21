# NightscoutFoundation.Nocturne.Api.SystemEventsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**SystemEventsCreateSystemEvent**](SystemEventsApi.md#systemeventscreatesystemevent) | **POST** /api/v4/system-events | Create a new system event (manual entry or import). |
| [**SystemEventsDeleteSystemEvent**](SystemEventsApi.md#systemeventsdeletesystemevent) | **DELETE** /api/v4/system-events/{id} | Delete a system event. |
| [**SystemEventsGetSystemEvent**](SystemEventsApi.md#systemeventsgetsystemevent) | **GET** /api/v4/system-events/{id} | Get a specific system event by ID. |
| [**SystemEventsGetSystemEvents**](SystemEventsApi.md#systemeventsgetsystemevents) | **GET** /api/v4/system-events | Query system events with optional filtering. |

<a id="systemeventscreatesystemevent"></a>
# **SystemEventsCreateSystemEvent**
> SystemEvent SystemEventsCreateSystemEvent (CreateSystemEventRequest createSystemEventRequest)

Create a new system event (manual entry or import).

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
    public class SystemEventsCreateSystemEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new SystemEventsApi(httpClient, config, httpClientHandler);
            var createSystemEventRequest = new CreateSystemEventRequest(); // CreateSystemEventRequest | 

            try
            {
                // Create a new system event (manual entry or import).
                SystemEvent result = apiInstance.SystemEventsCreateSystemEvent(createSystemEventRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SystemEventsApi.SystemEventsCreateSystemEvent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SystemEventsCreateSystemEventWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a new system event (manual entry or import).
    ApiResponse<SystemEvent> response = apiInstance.SystemEventsCreateSystemEventWithHttpInfo(createSystemEventRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SystemEventsApi.SystemEventsCreateSystemEventWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createSystemEventRequest** | [**CreateSystemEventRequest**](CreateSystemEventRequest.md) |  |  |

### Return type

[**SystemEvent**](SystemEvent.md)

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

<a id="systemeventsdeletesystemevent"></a>
# **SystemEventsDeleteSystemEvent**
> FileParameter SystemEventsDeleteSystemEvent (string id)

Delete a system event.

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
    public class SystemEventsDeleteSystemEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new SystemEventsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a system event.
                FileParameter result = apiInstance.SystemEventsDeleteSystemEvent(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SystemEventsApi.SystemEventsDeleteSystemEvent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SystemEventsDeleteSystemEventWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a system event.
    ApiResponse<FileParameter> response = apiInstance.SystemEventsDeleteSystemEventWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SystemEventsApi.SystemEventsDeleteSystemEventWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**FileParameter**](FileParameter.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="systemeventsgetsystemevent"></a>
# **SystemEventsGetSystemEvent**
> SystemEvent SystemEventsGetSystemEvent (string id)

Get a specific system event by ID.

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
    public class SystemEventsGetSystemEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new SystemEventsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Get a specific system event by ID.
                SystemEvent result = apiInstance.SystemEventsGetSystemEvent(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SystemEventsApi.SystemEventsGetSystemEvent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SystemEventsGetSystemEventWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a specific system event by ID.
    ApiResponse<SystemEvent> response = apiInstance.SystemEventsGetSystemEventWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SystemEventsApi.SystemEventsGetSystemEventWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**SystemEvent**](SystemEvent.md)

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

<a id="systemeventsgetsystemevents"></a>
# **SystemEventsGetSystemEvents**
> List&lt;SystemEvent&gt; SystemEventsGetSystemEvents (SystemEventsGetSystemEventsTypeParameter? type = null, SystemEventsGetSystemEventsCategoryParameter? category = null, DateTimeOffset? from = null, DateTimeOffset? to = null, string? source = null, int? count = null, int? skip = null)

Query system events with optional filtering.

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
    public class SystemEventsGetSystemEventsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new SystemEventsApi(httpClient, config, httpClientHandler);
            var type = new SystemEventsGetSystemEventsTypeParameter?(); // SystemEventsGetSystemEventsTypeParameter? |  (optional) 
            var category = new SystemEventsGetSystemEventsCategoryParameter?(); // SystemEventsGetSystemEventsCategoryParameter? |  (optional) 
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var source = "source_example";  // string? |  (optional) 
            var count = 100;  // int? |  (optional)  (default to 100)
            var skip = 0;  // int? |  (optional)  (default to 0)

            try
            {
                // Query system events with optional filtering.
                List<SystemEvent> result = apiInstance.SystemEventsGetSystemEvents(type, category, from, to, source, count, skip);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SystemEventsApi.SystemEventsGetSystemEvents: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SystemEventsGetSystemEventsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Query system events with optional filtering.
    ApiResponse<List<SystemEvent>> response = apiInstance.SystemEventsGetSystemEventsWithHttpInfo(type, category, from, to, source, count, skip);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SystemEventsApi.SystemEventsGetSystemEventsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **type** | [**SystemEventsGetSystemEventsTypeParameter?**](SystemEventsGetSystemEventsTypeParameter?.md) |  | [optional]  |
| **category** | [**SystemEventsGetSystemEventsCategoryParameter?**](SystemEventsGetSystemEventsCategoryParameter?.md) |  | [optional]  |
| **from** | **DateTimeOffset?** |  | [optional]  |
| **to** | **DateTimeOffset?** |  | [optional]  |
| **source** | **string?** |  | [optional]  |
| **count** | **int?** |  | [optional] [default to 100] |
| **skip** | **int?** |  | [optional] [default to 0] |

### Return type

[**List&lt;SystemEvent&gt;**](SystemEvent.md)

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

