# NightscoutFoundation.Nocturne.Api.MeterGlucoseApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**MeterGlucoseCreate**](MeterGlucoseApi.md#meterglucosecreate) | **POST** /api/v4/glucose/meter | Creates a new record and returns it with a &#x60;Location&#x60; header pointing to the created resource. |
| [**MeterGlucoseDelete**](MeterGlucoseApi.md#meterglucosedelete) | **DELETE** /api/v4/glucose/meter/{id} | Deletes a record by ID. |
| [**MeterGlucoseGetAll**](MeterGlucoseApi.md#meterglucosegetall) | **GET** /api/v4/glucose/meter |  |
| [**MeterGlucoseGetById**](MeterGlucoseApi.md#meterglucosegetbyid) | **GET** /api/v4/glucose/meter/{id} | Retrieves a single record by its unique identifier. |
| [**MeterGlucoseUpdate**](MeterGlucoseApi.md#meterglucoseupdate) | **PUT** /api/v4/glucose/meter/{id} | Updates an existing record by ID and returns the updated record. |

<a id="meterglucosecreate"></a>
# **MeterGlucoseCreate**
> MeterGlucose MeterGlucoseCreate (UpsertMeterGlucoseRequest upsertMeterGlucoseRequest)

Creates a new record and returns it with a `Location` header pointing to the created resource.

`Timestamp` must be set on the mapped model; requests that resolve to a default timestamp are rejected with `400 Bad Request`.              On success, responds with `201 Created` and a `Location` header containing the URL of the newly created record.

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
    public class MeterGlucoseCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MeterGlucoseApi(httpClient, config, httpClientHandler);
            var upsertMeterGlucoseRequest = new UpsertMeterGlucoseRequest(); // UpsertMeterGlucoseRequest | The data used to create the record.

            try
            {
                // Creates a new record and returns it with a `Location` header pointing to the created resource.
                MeterGlucose result = apiInstance.MeterGlucoseCreate(upsertMeterGlucoseRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MeterGlucoseApi.MeterGlucoseCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MeterGlucoseCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Creates a new record and returns it with a `Location` header pointing to the created resource.
    ApiResponse<MeterGlucose> response = apiInstance.MeterGlucoseCreateWithHttpInfo(upsertMeterGlucoseRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MeterGlucoseApi.MeterGlucoseCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **upsertMeterGlucoseRequest** | [**UpsertMeterGlucoseRequest**](UpsertMeterGlucoseRequest.md) | The data used to create the record. |  |

### Return type

[**MeterGlucose**](MeterGlucose.md)

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

<a id="meterglucosedelete"></a>
# **MeterGlucoseDelete**
> void MeterGlucoseDelete (string id)

Deletes a record by ID.

Returns `204 No Content` on success, or `404 Not Found` if no record with the given id exists.

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
    public class MeterGlucoseDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MeterGlucoseApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The unique identifier of the record to delete.

            try
            {
                // Deletes a record by ID.
                apiInstance.MeterGlucoseDelete(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MeterGlucoseApi.MeterGlucoseDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MeterGlucoseDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Deletes a record by ID.
    apiInstance.MeterGlucoseDeleteWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MeterGlucoseApi.MeterGlucoseDeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The unique identifier of the record to delete. |  |

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

<a id="meterglucosegetall"></a>
# **MeterGlucoseGetAll**
> PaginatedResponseOfMeterGlucose MeterGlucoseGetAll (DateTimeOffset? from = null, DateTimeOffset? to = null, int? limit = null, int? offset = null, string? sort = null, string? device = null, string? source = null)



Response is cached for 120 seconds, varied by all query parameters.

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
    public class MeterGlucoseGetAllExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MeterGlucoseApi(httpClient, config, httpClientHandler);
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var limit = 100;  // int? |  (optional)  (default to 100)
            var offset = 0;  // int? |  (optional)  (default to 0)
            var sort = "\"timestamp_desc\"";  // string? |  (optional)  (default to "timestamp_desc")
            var device = "device_example";  // string? |  (optional) 
            var source = "source_example";  // string? |  (optional) 

            try
            {
                PaginatedResponseOfMeterGlucose result = apiInstance.MeterGlucoseGetAll(from, to, limit, offset, sort, device, source);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MeterGlucoseApi.MeterGlucoseGetAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MeterGlucoseGetAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<PaginatedResponseOfMeterGlucose> response = apiInstance.MeterGlucoseGetAllWithHttpInfo(from, to, limit, offset, sort, device, source);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MeterGlucoseApi.MeterGlucoseGetAllWithHttpInfo: " + e.Message);
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

[**PaginatedResponseOfMeterGlucose**](PaginatedResponseOfMeterGlucose.md)

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

<a id="meterglucosegetbyid"></a>
# **MeterGlucoseGetById**
> MeterGlucose MeterGlucoseGetById (string id)

Retrieves a single record by its unique identifier.

Returns `404 Not Found` if no record with the given id exists.

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
    public class MeterGlucoseGetByIdExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MeterGlucoseApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The unique identifier of the record.

            try
            {
                // Retrieves a single record by its unique identifier.
                MeterGlucose result = apiInstance.MeterGlucoseGetById(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MeterGlucoseApi.MeterGlucoseGetById: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MeterGlucoseGetByIdWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Retrieves a single record by its unique identifier.
    ApiResponse<MeterGlucose> response = apiInstance.MeterGlucoseGetByIdWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MeterGlucoseApi.MeterGlucoseGetByIdWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The unique identifier of the record. |  |

### Return type

[**MeterGlucose**](MeterGlucose.md)

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

<a id="meterglucoseupdate"></a>
# **MeterGlucoseUpdate**
> MeterGlucose MeterGlucoseUpdate (string id, UpsertMeterGlucoseRequest upsertMeterGlucoseRequest)

Updates an existing record by ID and returns the updated record.

Returns `404 Not Found` if no record with the given id exists.              `Timestamp` must be set on the mapped model; requests that resolve to a default timestamp are rejected with `400 Bad Request`.

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
    public class MeterGlucoseUpdateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MeterGlucoseApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The unique identifier of the record to update.
            var upsertMeterGlucoseRequest = new UpsertMeterGlucoseRequest(); // UpsertMeterGlucoseRequest | The data to apply to the existing record.

            try
            {
                // Updates an existing record by ID and returns the updated record.
                MeterGlucose result = apiInstance.MeterGlucoseUpdate(id, upsertMeterGlucoseRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MeterGlucoseApi.MeterGlucoseUpdate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MeterGlucoseUpdateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Updates an existing record by ID and returns the updated record.
    ApiResponse<MeterGlucose> response = apiInstance.MeterGlucoseUpdateWithHttpInfo(id, upsertMeterGlucoseRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MeterGlucoseApi.MeterGlucoseUpdateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The unique identifier of the record to update. |  |
| **upsertMeterGlucoseRequest** | [**UpsertMeterGlucoseRequest**](UpsertMeterGlucoseRequest.md) | The data to apply to the existing record. |  |

### Return type

[**MeterGlucose**](MeterGlucose.md)

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

