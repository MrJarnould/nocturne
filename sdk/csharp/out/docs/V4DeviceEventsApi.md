# NightscoutFoundation.Nocturne.Api.V4DeviceEventsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DeviceEventCreate**](V4DeviceEventsApi.md#deviceeventcreate) | **POST** /api/v4/observations/device-events |  |
| [**DeviceEventDelete**](V4DeviceEventsApi.md#deviceeventdelete) | **DELETE** /api/v4/observations/device-events/{id} |  |
| [**DeviceEventGetAll**](V4DeviceEventsApi.md#deviceeventgetall) | **GET** /api/v4/observations/device-events |  |
| [**DeviceEventGetById**](V4DeviceEventsApi.md#deviceeventgetbyid) | **GET** /api/v4/observations/device-events/{id} |  |
| [**DeviceEventUpdate**](V4DeviceEventsApi.md#deviceeventupdate) | **PUT** /api/v4/observations/device-events/{id} |  |

<a id="deviceeventcreate"></a>
# **DeviceEventCreate**
> DeviceEvent DeviceEventCreate (UpsertDeviceEventRequest upsertDeviceEventRequest)



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
    public class DeviceEventCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DeviceEventsApi(httpClient, config, httpClientHandler);
            var upsertDeviceEventRequest = new UpsertDeviceEventRequest(); // UpsertDeviceEventRequest | 

            try
            {
                DeviceEvent result = apiInstance.DeviceEventCreate(upsertDeviceEventRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeviceEventsApi.DeviceEventCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceEventCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<DeviceEvent> response = apiInstance.DeviceEventCreateWithHttpInfo(upsertDeviceEventRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeviceEventsApi.DeviceEventCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **upsertDeviceEventRequest** | [**UpsertDeviceEventRequest**](UpsertDeviceEventRequest.md) |  |  |

### Return type

[**DeviceEvent**](DeviceEvent.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deviceeventdelete"></a>
# **DeviceEventDelete**
> void DeviceEventDelete (string id)



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
    public class DeviceEventDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DeviceEventsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                apiInstance.DeviceEventDelete(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeviceEventsApi.DeviceEventDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceEventDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.DeviceEventDeleteWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeviceEventsApi.DeviceEventDeleteWithHttpInfo: " + e.Message);
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
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deviceeventgetall"></a>
# **DeviceEventGetAll**
> PaginatedResponseOfDeviceEvent DeviceEventGetAll (DateTimeOffset? from = null, DateTimeOffset? to = null, int? limit = null, int? offset = null, string? sort = null, string? device = null, string? source = null)



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
    public class DeviceEventGetAllExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DeviceEventsApi(httpClient, config, httpClientHandler);
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var limit = 100;  // int? |  (optional)  (default to 100)
            var offset = 0;  // int? |  (optional)  (default to 0)
            var sort = "\"timestamp_desc\"";  // string? |  (optional)  (default to "timestamp_desc")
            var device = "device_example";  // string? |  (optional) 
            var source = "source_example";  // string? |  (optional) 

            try
            {
                PaginatedResponseOfDeviceEvent result = apiInstance.DeviceEventGetAll(from, to, limit, offset, sort, device, source);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeviceEventsApi.DeviceEventGetAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceEventGetAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<PaginatedResponseOfDeviceEvent> response = apiInstance.DeviceEventGetAllWithHttpInfo(from, to, limit, offset, sort, device, source);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeviceEventsApi.DeviceEventGetAllWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **from** | **DateTimeOffset?** |  | [optional]  |
| **to** | **DateTimeOffset?** |  | [optional]  |
| **limit** | **int?** |  | [optional] [default to 100] |
| **offset** | **int?** |  | [optional] [default to 0] |
| **sort** | **string?** |  | [optional] [default to &quot;timestamp_desc&quot;] |
| **device** | **string?** |  | [optional]  |
| **source** | **string?** |  | [optional]  |

### Return type

[**PaginatedResponseOfDeviceEvent**](PaginatedResponseOfDeviceEvent.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deviceeventgetbyid"></a>
# **DeviceEventGetById**
> DeviceEvent DeviceEventGetById (string id)



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
    public class DeviceEventGetByIdExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DeviceEventsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                DeviceEvent result = apiInstance.DeviceEventGetById(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeviceEventsApi.DeviceEventGetById: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceEventGetByIdWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<DeviceEvent> response = apiInstance.DeviceEventGetByIdWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeviceEventsApi.DeviceEventGetByIdWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**DeviceEvent**](DeviceEvent.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deviceeventupdate"></a>
# **DeviceEventUpdate**
> DeviceEvent DeviceEventUpdate (string id, UpsertDeviceEventRequest upsertDeviceEventRequest)



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
    public class DeviceEventUpdateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DeviceEventsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var upsertDeviceEventRequest = new UpsertDeviceEventRequest(); // UpsertDeviceEventRequest | 

            try
            {
                DeviceEvent result = apiInstance.DeviceEventUpdate(id, upsertDeviceEventRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeviceEventsApi.DeviceEventUpdate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceEventUpdateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<DeviceEvent> response = apiInstance.DeviceEventUpdateWithHttpInfo(id, upsertDeviceEventRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeviceEventsApi.DeviceEventUpdateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **upsertDeviceEventRequest** | [**UpsertDeviceEventRequest**](UpsertDeviceEventRequest.md) |  |  |

### Return type

[**DeviceEvent**](DeviceEvent.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

