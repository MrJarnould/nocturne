# NightscoutFoundation.Nocturne.Api.ApsSnapshotApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ApsSnapshotGetAll**](ApsSnapshotApi.md#apssnapshotgetall) | **GET** /api/v4/device-status/aps | Lists records with pagination, optional date range, device, and source filtering. |
| [**ApsSnapshotGetById**](ApsSnapshotApi.md#apssnapshotgetbyid) | **GET** /api/v4/device-status/aps/{id} | Retrieves a single record by its unique identifier. |

<a id="apssnapshotgetall"></a>
# **ApsSnapshotGetAll**
> PaginatedResponseOfApsSnapshot ApsSnapshotGetAll (DateTimeOffset? from = null, DateTimeOffset? to = null, int? limit = null, int? offset = null, string? sort = null, string? device = null, string? source = null)

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
    public class ApsSnapshotGetAllExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ApsSnapshotApi(httpClient, config, httpClientHandler);
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
                PaginatedResponseOfApsSnapshot result = apiInstance.ApsSnapshotGetAll(from, to, limit, offset, sort, device, source);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApsSnapshotApi.ApsSnapshotGetAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApsSnapshotGetAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Lists records with pagination, optional date range, device, and source filtering.
    ApiResponse<PaginatedResponseOfApsSnapshot> response = apiInstance.ApsSnapshotGetAllWithHttpInfo(from, to, limit, offset, sort, device, source);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApsSnapshotApi.ApsSnapshotGetAllWithHttpInfo: " + e.Message);
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

[**PaginatedResponseOfApsSnapshot**](PaginatedResponseOfApsSnapshot.md)

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

<a id="apssnapshotgetbyid"></a>
# **ApsSnapshotGetById**
> ApsSnapshot ApsSnapshotGetById (string id)

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
    public class ApsSnapshotGetByIdExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ApsSnapshotApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The unique identifier of the record.

            try
            {
                // Retrieves a single record by its unique identifier.
                ApsSnapshot result = apiInstance.ApsSnapshotGetById(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApsSnapshotApi.ApsSnapshotGetById: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApsSnapshotGetByIdWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Retrieves a single record by its unique identifier.
    ApiResponse<ApsSnapshot> response = apiInstance.ApsSnapshotGetByIdWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApsSnapshotApi.ApsSnapshotGetByIdWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The unique identifier of the record. |  |

### Return type

[**ApsSnapshot**](ApsSnapshot.md)

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

