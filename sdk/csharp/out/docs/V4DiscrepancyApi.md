# NightscoutFoundation.Nocturne.Api.V4DiscrepancyApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DiscrepancyGetCompatibilityMetrics**](V4DiscrepancyApi.md#discrepancygetcompatibilitymetrics) | **GET** /api/v4/Discrepancy/metrics | Get overall compatibility metrics for dashboard overview |
| [**DiscrepancyGetCompatibilityStatus**](V4DiscrepancyApi.md#discrepancygetcompatibilitystatus) | **GET** /api/v4/Discrepancy/status | Get real-time compatibility status summary |
| [**DiscrepancyGetDiscrepancyAnalyses**](V4DiscrepancyApi.md#discrepancygetdiscrepancyanalyses) | **GET** /api/v4/Discrepancy/analyses | Get detailed discrepancy analyses with filtering and pagination |
| [**DiscrepancyGetDiscrepancyAnalysis**](V4DiscrepancyApi.md#discrepancygetdiscrepancyanalysis) | **GET** /api/v4/Discrepancy/analyses/{id} | Get a specific discrepancy analysis by ID |
| [**DiscrepancyGetEndpointMetrics**](V4DiscrepancyApi.md#discrepancygetendpointmetrics) | **GET** /api/v4/Discrepancy/endpoints | Get per-endpoint compatibility metrics |
| [**DiscrepancyIngestDiscrepancy**](V4DiscrepancyApi.md#discrepancyingestdiscrepancy) | **POST** /api/v4/Discrepancy/ingest | Receive forwarded discrepancies from remote Nocturne instances |

<a id="discrepancygetcompatibilitymetrics"></a>
# **DiscrepancyGetCompatibilityMetrics**
> CompatibilityMetrics DiscrepancyGetCompatibilityMetrics (DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null)

Get overall compatibility metrics for dashboard overview

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
    public class DiscrepancyGetCompatibilityMetricsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DiscrepancyApi(httpClient, config, httpClientHandler);
            var fromDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | Start date for metrics (optional) (optional) 
            var toDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | End date for metrics (optional) (optional) 

            try
            {
                // Get overall compatibility metrics for dashboard overview
                CompatibilityMetrics result = apiInstance.DiscrepancyGetCompatibilityMetrics(fromDate, toDate);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyGetCompatibilityMetrics: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DiscrepancyGetCompatibilityMetricsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get overall compatibility metrics for dashboard overview
    ApiResponse<CompatibilityMetrics> response = apiInstance.DiscrepancyGetCompatibilityMetricsWithHttpInfo(fromDate, toDate);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyGetCompatibilityMetricsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **fromDate** | **DateTimeOffset?** | Start date for metrics (optional) | [optional]  |
| **toDate** | **DateTimeOffset?** | End date for metrics (optional) | [optional]  |

### Return type

[**CompatibilityMetrics**](CompatibilityMetrics.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Compatibility metrics including success rate and response times |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="discrepancygetcompatibilitystatus"></a>
# **DiscrepancyGetCompatibilityStatus**
> CompatibilityStatus DiscrepancyGetCompatibilityStatus ()

Get real-time compatibility status summary

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
    public class DiscrepancyGetCompatibilityStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DiscrepancyApi(httpClient, config, httpClientHandler);

            try
            {
                // Get real-time compatibility status summary
                CompatibilityStatus result = apiInstance.DiscrepancyGetCompatibilityStatus();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyGetCompatibilityStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DiscrepancyGetCompatibilityStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get real-time compatibility status summary
    ApiResponse<CompatibilityStatus> response = apiInstance.DiscrepancyGetCompatibilityStatusWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyGetCompatibilityStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**CompatibilityStatus**](CompatibilityStatus.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Current compatibility status |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="discrepancygetdiscrepancyanalyses"></a>
# **DiscrepancyGetDiscrepancyAnalyses**
> List&lt;DiscrepancyAnalysisDto&gt; DiscrepancyGetDiscrepancyAnalyses (string? requestPath = null, int? overallMatch = null, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null, int? count = null, int? skip = null)

Get detailed discrepancy analyses with filtering and pagination

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
    public class DiscrepancyGetDiscrepancyAnalysesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DiscrepancyApi(httpClient, config, httpClientHandler);
            var requestPath = "requestPath_example";  // string? | Filter by request path (optional) (optional) 
            var overallMatch = 56;  // int? | Filter by overall match type (optional) (optional) 
            var fromDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | Start date for filter (optional) (optional) 
            var toDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | End date for filter (optional) (optional) 
            var count = 100;  // int? | Number of results to return (default: 100, max: 1000) (optional)  (default to 100)
            var skip = 0;  // int? | Number of results to skip for pagination (default: 0) (optional)  (default to 0)

            try
            {
                // Get detailed discrepancy analyses with filtering and pagination
                List<DiscrepancyAnalysisDto> result = apiInstance.DiscrepancyGetDiscrepancyAnalyses(requestPath, overallMatch, fromDate, toDate, count, skip);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyGetDiscrepancyAnalyses: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DiscrepancyGetDiscrepancyAnalysesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get detailed discrepancy analyses with filtering and pagination
    ApiResponse<List<DiscrepancyAnalysisDto>> response = apiInstance.DiscrepancyGetDiscrepancyAnalysesWithHttpInfo(requestPath, overallMatch, fromDate, toDate, count, skip);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyGetDiscrepancyAnalysesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **requestPath** | **string?** | Filter by request path (optional) | [optional]  |
| **overallMatch** | **int?** | Filter by overall match type (optional) | [optional]  |
| **fromDate** | **DateTimeOffset?** | Start date for filter (optional) | [optional]  |
| **toDate** | **DateTimeOffset?** | End date for filter (optional) | [optional]  |
| **count** | **int?** | Number of results to return (default: 100, max: 1000) | [optional] [default to 100] |
| **skip** | **int?** | Number of results to skip for pagination (default: 0) | [optional] [default to 0] |

### Return type

[**List&lt;DiscrepancyAnalysisDto&gt;**](DiscrepancyAnalysisDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of detailed discrepancy analyses |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="discrepancygetdiscrepancyanalysis"></a>
# **DiscrepancyGetDiscrepancyAnalysis**
> DiscrepancyAnalysisDto DiscrepancyGetDiscrepancyAnalysis (string id)

Get a specific discrepancy analysis by ID

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
    public class DiscrepancyGetDiscrepancyAnalysisExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DiscrepancyApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | Analysis ID

            try
            {
                // Get a specific discrepancy analysis by ID
                DiscrepancyAnalysisDto result = apiInstance.DiscrepancyGetDiscrepancyAnalysis(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyGetDiscrepancyAnalysis: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DiscrepancyGetDiscrepancyAnalysisWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a specific discrepancy analysis by ID
    ApiResponse<DiscrepancyAnalysisDto> response = apiInstance.DiscrepancyGetDiscrepancyAnalysisWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyGetDiscrepancyAnalysisWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Analysis ID |  |

### Return type

[**DiscrepancyAnalysisDto**](DiscrepancyAnalysisDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Detailed discrepancy analysis |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="discrepancygetendpointmetrics"></a>
# **DiscrepancyGetEndpointMetrics**
> List&lt;EndpointMetrics&gt; DiscrepancyGetEndpointMetrics (DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null)

Get per-endpoint compatibility metrics

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
    public class DiscrepancyGetEndpointMetricsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DiscrepancyApi(httpClient, config, httpClientHandler);
            var fromDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | Start date for metrics (optional) (optional) 
            var toDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | End date for metrics (optional) (optional) 

            try
            {
                // Get per-endpoint compatibility metrics
                List<EndpointMetrics> result = apiInstance.DiscrepancyGetEndpointMetrics(fromDate, toDate);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyGetEndpointMetrics: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DiscrepancyGetEndpointMetricsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get per-endpoint compatibility metrics
    ApiResponse<List<EndpointMetrics>> response = apiInstance.DiscrepancyGetEndpointMetricsWithHttpInfo(fromDate, toDate);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyGetEndpointMetricsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **fromDate** | **DateTimeOffset?** | Start date for metrics (optional) | [optional]  |
| **toDate** | **DateTimeOffset?** | End date for metrics (optional) | [optional]  |

### Return type

[**List&lt;EndpointMetrics&gt;**](EndpointMetrics.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of endpoint-specific compatibility metrics |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="discrepancyingestdiscrepancy"></a>
# **DiscrepancyIngestDiscrepancy**
> Object DiscrepancyIngestDiscrepancy (ForwardedDiscrepancyDto forwardedDiscrepancyDto)

Receive forwarded discrepancies from remote Nocturne instances

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
    public class DiscrepancyIngestDiscrepancyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DiscrepancyApi(httpClient, config, httpClientHandler);
            var forwardedDiscrepancyDto = new ForwardedDiscrepancyDto(); // ForwardedDiscrepancyDto | The forwarded discrepancy data

            try
            {
                // Receive forwarded discrepancies from remote Nocturne instances
                Object result = apiInstance.DiscrepancyIngestDiscrepancy(forwardedDiscrepancyDto);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyIngestDiscrepancy: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DiscrepancyIngestDiscrepancyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Receive forwarded discrepancies from remote Nocturne instances
    ApiResponse<Object> response = apiInstance.DiscrepancyIngestDiscrepancyWithHttpInfo(forwardedDiscrepancyDto);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DiscrepancyApi.DiscrepancyIngestDiscrepancyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **forwardedDiscrepancyDto** | [**ForwardedDiscrepancyDto**](ForwardedDiscrepancyDto.md) | The forwarded discrepancy data |  |

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Acknowledgement of receipt |  -  |
| **400** |  |  -  |
| **401** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

