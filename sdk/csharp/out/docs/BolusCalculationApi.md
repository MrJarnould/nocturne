# NightscoutFoundation.Nocturne.Api.BolusCalculationApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**BolusCalculationCreate**](BolusCalculationApi.md#boluscalculationcreate) | **POST** /api/v4/insulin/calculations | Creates a new record and returns it with a &#x60;Location&#x60; header pointing to the created resource. |
| [**BolusCalculationDelete**](BolusCalculationApi.md#boluscalculationdelete) | **DELETE** /api/v4/insulin/calculations/{id} | Deletes a record by ID. |
| [**BolusCalculationGetAll**](BolusCalculationApi.md#boluscalculationgetall) | **GET** /api/v4/insulin/calculations | Lists records with pagination, optional date range, device, and source filtering. |
| [**BolusCalculationGetById**](BolusCalculationApi.md#boluscalculationgetbyid) | **GET** /api/v4/insulin/calculations/{id} | Retrieves a single record by its unique identifier. |
| [**BolusCalculationUpdate**](BolusCalculationApi.md#boluscalculationupdate) | **PUT** /api/v4/insulin/calculations/{id} | Updates an existing record by ID and returns the updated record. |

<a id="boluscalculationcreate"></a>
# **BolusCalculationCreate**
> BolusCalculation BolusCalculationCreate (UpsertBolusCalculationRequest upsertBolusCalculationRequest)

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
    public class BolusCalculationCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BolusCalculationApi(httpClient, config, httpClientHandler);
            var upsertBolusCalculationRequest = new UpsertBolusCalculationRequest(); // UpsertBolusCalculationRequest | The data used to create the record.

            try
            {
                // Creates a new record and returns it with a `Location` header pointing to the created resource.
                BolusCalculation result = apiInstance.BolusCalculationCreate(upsertBolusCalculationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BolusCalculationApi.BolusCalculationCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BolusCalculationCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Creates a new record and returns it with a `Location` header pointing to the created resource.
    ApiResponse<BolusCalculation> response = apiInstance.BolusCalculationCreateWithHttpInfo(upsertBolusCalculationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BolusCalculationApi.BolusCalculationCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **upsertBolusCalculationRequest** | [**UpsertBolusCalculationRequest**](UpsertBolusCalculationRequest.md) | The data used to create the record. |  |

### Return type

[**BolusCalculation**](BolusCalculation.md)

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

<a id="boluscalculationdelete"></a>
# **BolusCalculationDelete**
> void BolusCalculationDelete (string id)

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
    public class BolusCalculationDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BolusCalculationApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The unique identifier of the record to delete.

            try
            {
                // Deletes a record by ID.
                apiInstance.BolusCalculationDelete(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BolusCalculationApi.BolusCalculationDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BolusCalculationDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Deletes a record by ID.
    apiInstance.BolusCalculationDeleteWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BolusCalculationApi.BolusCalculationDeleteWithHttpInfo: " + e.Message);
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

<a id="boluscalculationgetall"></a>
# **BolusCalculationGetAll**
> PaginatedResponseOfBolusCalculation BolusCalculationGetAll (DateTimeOffset? from = null, DateTimeOffset? to = null, int? limit = null, int? offset = null, string? sort = null, string? device = null, string? source = null)

Lists records with pagination, optional date range, device, and source filtering.

The `sort` parameter accepts exactly two values: - `timestamp_asc` — oldest records first - `timestamp_desc` — newest records first (default)              Use `limit` and `offset` together for paginated access to large result sets.

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
    public class BolusCalculationGetAllExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BolusCalculationApi(httpClient, config, httpClientHandler);
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | Inclusive start of the date range filter. (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | Inclusive end of the date range filter. (optional) 
            var limit = 100;  // int? | Maximum number of records to return. Defaults to `100`. (optional)  (default to 100)
            var offset = 0;  // int? | Number of records to skip for pagination. Defaults to `0`. (optional)  (default to 0)
            var sort = "\"timestamp_desc\"";  // string? | Sort order for results by timestamp. Defaults to `timestamp_desc`. (optional)  (default to "timestamp_desc")
            var device = "device_example";  // string? | Optional filter to restrict results to a specific device. (optional) 
            var source = "source_example";  // string? | Optional filter to restrict results to a specific data source. (optional) 

            try
            {
                // Lists records with pagination, optional date range, device, and source filtering.
                PaginatedResponseOfBolusCalculation result = apiInstance.BolusCalculationGetAll(from, to, limit, offset, sort, device, source);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BolusCalculationApi.BolusCalculationGetAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BolusCalculationGetAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Lists records with pagination, optional date range, device, and source filtering.
    ApiResponse<PaginatedResponseOfBolusCalculation> response = apiInstance.BolusCalculationGetAllWithHttpInfo(from, to, limit, offset, sort, device, source);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BolusCalculationApi.BolusCalculationGetAllWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **from** | **DateTimeOffset?** | Inclusive start of the date range filter. | [optional]  |
| **to** | **DateTimeOffset?** | Inclusive end of the date range filter. | [optional]  |
| **limit** | **int?** | Maximum number of records to return. Defaults to &#x60;100&#x60;. | [optional] [default to 100] |
| **offset** | **int?** | Number of records to skip for pagination. Defaults to &#x60;0&#x60;. | [optional] [default to 0] |
| **sort** | **string?** | Sort order for results by timestamp. Defaults to &#x60;timestamp_desc&#x60;. | [optional] [default to &quot;timestamp_desc&quot;] |
| **device** | **string?** | Optional filter to restrict results to a specific device. | [optional]  |
| **source** | **string?** | Optional filter to restrict results to a specific data source. | [optional]  |

### Return type

[**PaginatedResponseOfBolusCalculation**](PaginatedResponseOfBolusCalculation.md)

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

<a id="boluscalculationgetbyid"></a>
# **BolusCalculationGetById**
> BolusCalculation BolusCalculationGetById (string id)

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
    public class BolusCalculationGetByIdExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BolusCalculationApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The unique identifier of the record.

            try
            {
                // Retrieves a single record by its unique identifier.
                BolusCalculation result = apiInstance.BolusCalculationGetById(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BolusCalculationApi.BolusCalculationGetById: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BolusCalculationGetByIdWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Retrieves a single record by its unique identifier.
    ApiResponse<BolusCalculation> response = apiInstance.BolusCalculationGetByIdWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BolusCalculationApi.BolusCalculationGetByIdWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The unique identifier of the record. |  |

### Return type

[**BolusCalculation**](BolusCalculation.md)

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

<a id="boluscalculationupdate"></a>
# **BolusCalculationUpdate**
> BolusCalculation BolusCalculationUpdate (string id, UpsertBolusCalculationRequest upsertBolusCalculationRequest)

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
    public class BolusCalculationUpdateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BolusCalculationApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The unique identifier of the record to update.
            var upsertBolusCalculationRequest = new UpsertBolusCalculationRequest(); // UpsertBolusCalculationRequest | The data to apply to the existing record.

            try
            {
                // Updates an existing record by ID and returns the updated record.
                BolusCalculation result = apiInstance.BolusCalculationUpdate(id, upsertBolusCalculationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BolusCalculationApi.BolusCalculationUpdate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BolusCalculationUpdateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Updates an existing record by ID and returns the updated record.
    ApiResponse<BolusCalculation> response = apiInstance.BolusCalculationUpdateWithHttpInfo(id, upsertBolusCalculationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BolusCalculationApi.BolusCalculationUpdateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The unique identifier of the record to update. |  |
| **upsertBolusCalculationRequest** | [**UpsertBolusCalculationRequest**](UpsertBolusCalculationRequest.md) | The data to apply to the existing record. |  |

### Return type

[**BolusCalculation**](BolusCalculation.md)

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

