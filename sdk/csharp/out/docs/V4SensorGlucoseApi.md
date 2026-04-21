# NightscoutFoundation.Nocturne.Api.V4SensorGlucoseApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**SensorGlucoseCreate**](V4SensorGlucoseApi.md#sensorglucosecreate) | **POST** /api/v4/glucose/sensor |  |
| [**SensorGlucoseDelete**](V4SensorGlucoseApi.md#sensorglucosedelete) | **DELETE** /api/v4/glucose/sensor/{id} |  |
| [**SensorGlucoseGetAll**](V4SensorGlucoseApi.md#sensorglucosegetall) | **GET** /api/v4/glucose/sensor |  |
| [**SensorGlucoseGetById**](V4SensorGlucoseApi.md#sensorglucosegetbyid) | **GET** /api/v4/glucose/sensor/{id} |  |
| [**SensorGlucoseUpdate**](V4SensorGlucoseApi.md#sensorglucoseupdate) | **PUT** /api/v4/glucose/sensor/{id} |  |

<a id="sensorglucosecreate"></a>
# **SensorGlucoseCreate**
> SensorGlucose SensorGlucoseCreate (UpsertSensorGlucoseRequest upsertSensorGlucoseRequest)



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
    public class SensorGlucoseCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4SensorGlucoseApi(httpClient, config, httpClientHandler);
            var upsertSensorGlucoseRequest = new UpsertSensorGlucoseRequest(); // UpsertSensorGlucoseRequest | 

            try
            {
                SensorGlucose result = apiInstance.SensorGlucoseCreate(upsertSensorGlucoseRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4SensorGlucoseApi.SensorGlucoseCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SensorGlucoseCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<SensorGlucose> response = apiInstance.SensorGlucoseCreateWithHttpInfo(upsertSensorGlucoseRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4SensorGlucoseApi.SensorGlucoseCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **upsertSensorGlucoseRequest** | [**UpsertSensorGlucoseRequest**](UpsertSensorGlucoseRequest.md) |  |  |

### Return type

[**SensorGlucose**](SensorGlucose.md)

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

<a id="sensorglucosedelete"></a>
# **SensorGlucoseDelete**
> void SensorGlucoseDelete (string id)



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
    public class SensorGlucoseDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4SensorGlucoseApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                apiInstance.SensorGlucoseDelete(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4SensorGlucoseApi.SensorGlucoseDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SensorGlucoseDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.SensorGlucoseDeleteWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4SensorGlucoseApi.SensorGlucoseDeleteWithHttpInfo: " + e.Message);
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

<a id="sensorglucosegetall"></a>
# **SensorGlucoseGetAll**
> PaginatedResponseOfSensorGlucose SensorGlucoseGetAll (DateTimeOffset? from = null, DateTimeOffset? to = null, int? limit = null, int? offset = null, string? sort = null, string? device = null, string? source = null)



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
    public class SensorGlucoseGetAllExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4SensorGlucoseApi(httpClient, config, httpClientHandler);
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var limit = 100;  // int? |  (optional)  (default to 100)
            var offset = 0;  // int? |  (optional)  (default to 0)
            var sort = "\"timestamp_desc\"";  // string? |  (optional)  (default to "timestamp_desc")
            var device = "device_example";  // string? |  (optional) 
            var source = "source_example";  // string? |  (optional) 

            try
            {
                PaginatedResponseOfSensorGlucose result = apiInstance.SensorGlucoseGetAll(from, to, limit, offset, sort, device, source);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4SensorGlucoseApi.SensorGlucoseGetAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SensorGlucoseGetAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<PaginatedResponseOfSensorGlucose> response = apiInstance.SensorGlucoseGetAllWithHttpInfo(from, to, limit, offset, sort, device, source);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4SensorGlucoseApi.SensorGlucoseGetAllWithHttpInfo: " + e.Message);
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

[**PaginatedResponseOfSensorGlucose**](PaginatedResponseOfSensorGlucose.md)

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

<a id="sensorglucosegetbyid"></a>
# **SensorGlucoseGetById**
> SensorGlucose SensorGlucoseGetById (string id)



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
    public class SensorGlucoseGetByIdExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4SensorGlucoseApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                SensorGlucose result = apiInstance.SensorGlucoseGetById(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4SensorGlucoseApi.SensorGlucoseGetById: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SensorGlucoseGetByIdWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<SensorGlucose> response = apiInstance.SensorGlucoseGetByIdWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4SensorGlucoseApi.SensorGlucoseGetByIdWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**SensorGlucose**](SensorGlucose.md)

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

<a id="sensorglucoseupdate"></a>
# **SensorGlucoseUpdate**
> SensorGlucose SensorGlucoseUpdate (string id, UpsertSensorGlucoseRequest upsertSensorGlucoseRequest)



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
    public class SensorGlucoseUpdateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4SensorGlucoseApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var upsertSensorGlucoseRequest = new UpsertSensorGlucoseRequest(); // UpsertSensorGlucoseRequest | 

            try
            {
                SensorGlucose result = apiInstance.SensorGlucoseUpdate(id, upsertSensorGlucoseRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4SensorGlucoseApi.SensorGlucoseUpdate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SensorGlucoseUpdateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<SensorGlucose> response = apiInstance.SensorGlucoseUpdateWithHttpInfo(id, upsertSensorGlucoseRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4SensorGlucoseApi.SensorGlucoseUpdateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **upsertSensorGlucoseRequest** | [**UpsertSensorGlucoseRequest**](UpsertSensorGlucoseRequest.md) |  |  |

### Return type

[**SensorGlucose**](SensorGlucose.md)

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

