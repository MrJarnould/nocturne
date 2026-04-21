# NightscoutFoundation.Nocturne.Api.CompatibilityApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CompatibilityGetAnalyses**](CompatibilityApi.md#compatibilitygetanalyses) | **GET** /api/v4/compatibility/analyses | Get list of analyses with filtering and pagination |
| [**CompatibilityGetAnalysisDetail**](CompatibilityApi.md#compatibilitygetanalysisdetail) | **GET** /api/v4/compatibility/analyses/{id} | Get detailed analysis by ID |
| [**CompatibilityGetConfiguration**](CompatibilityApi.md#compatibilitygetconfiguration) | **GET** /api/v4/compatibility/config | Get current proxy configuration |
| [**CompatibilityGetEndpointMetrics**](CompatibilityApi.md#compatibilitygetendpointmetrics) | **GET** /api/v4/compatibility/endpoints | Get per-endpoint compatibility metrics |
| [**CompatibilityGetMetrics**](CompatibilityApi.md#compatibilitygetmetrics) | **GET** /api/v4/compatibility/metrics | Get overall compatibility metrics |
| [**CompatibilityTestApiComparison**](CompatibilityApi.md#compatibilitytestapicomparison) | **POST** /api/v4/compatibility/test | Test API compatibility by comparing responses from Nightscout and Nocturne |

<a id="compatibilitygetanalyses"></a>
# **CompatibilityGetAnalyses**
> AnalysesListResponse CompatibilityGetAnalyses (string? requestPath = null, CompatibilityGetAnalysesOverallMatchParameter? overallMatch = null, string? requestMethod = null, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null, int? count = null, int? skip = null)

Get list of analyses with filtering and pagination

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
    public class CompatibilityGetAnalysesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompatibilityApi(httpClient, config, httpClientHandler);
            var requestPath = "requestPath_example";  // string? |  (optional) 
            var overallMatch = new CompatibilityGetAnalysesOverallMatchParameter?(); // CompatibilityGetAnalysesOverallMatchParameter? |  (optional) 
            var requestMethod = "requestMethod_example";  // string? |  (optional) 
            var fromDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var toDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var count = 100;  // int? |  (optional)  (default to 100)
            var skip = 0;  // int? |  (optional)  (default to 0)

            try
            {
                // Get list of analyses with filtering and pagination
                AnalysesListResponse result = apiInstance.CompatibilityGetAnalyses(requestPath, overallMatch, requestMethod, fromDate, toDate, count, skip);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompatibilityApi.CompatibilityGetAnalyses: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompatibilityGetAnalysesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get list of analyses with filtering and pagination
    ApiResponse<AnalysesListResponse> response = apiInstance.CompatibilityGetAnalysesWithHttpInfo(requestPath, overallMatch, requestMethod, fromDate, toDate, count, skip);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompatibilityApi.CompatibilityGetAnalysesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **requestPath** | **string?** |  | [optional]  |
| **overallMatch** | [**CompatibilityGetAnalysesOverallMatchParameter?**](CompatibilityGetAnalysesOverallMatchParameter?.md) |  | [optional]  |
| **requestMethod** | **string?** |  | [optional]  |
| **fromDate** | **DateTimeOffset?** |  | [optional]  |
| **toDate** | **DateTimeOffset?** |  | [optional]  |
| **count** | **int?** |  | [optional] [default to 100] |
| **skip** | **int?** |  | [optional] [default to 0] |

### Return type

[**AnalysesListResponse**](AnalysesListResponse.md)

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

<a id="compatibilitygetanalysisdetail"></a>
# **CompatibilityGetAnalysisDetail**
> AnalysisDetailDto CompatibilityGetAnalysisDetail (string id)

Get detailed analysis by ID

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
    public class CompatibilityGetAnalysisDetailExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompatibilityApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Get detailed analysis by ID
                AnalysisDetailDto result = apiInstance.CompatibilityGetAnalysisDetail(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompatibilityApi.CompatibilityGetAnalysisDetail: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompatibilityGetAnalysisDetailWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get detailed analysis by ID
    ApiResponse<AnalysisDetailDto> response = apiInstance.CompatibilityGetAnalysisDetailWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompatibilityApi.CompatibilityGetAnalysisDetailWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**AnalysisDetailDto**](AnalysisDetailDto.md)

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

<a id="compatibilitygetconfiguration"></a>
# **CompatibilityGetConfiguration**
> ProxyConfigurationDto CompatibilityGetConfiguration ()

Get current proxy configuration

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
    public class CompatibilityGetConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompatibilityApi(httpClient, config, httpClientHandler);

            try
            {
                // Get current proxy configuration
                ProxyConfigurationDto result = apiInstance.CompatibilityGetConfiguration();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompatibilityApi.CompatibilityGetConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompatibilityGetConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get current proxy configuration
    ApiResponse<ProxyConfigurationDto> response = apiInstance.CompatibilityGetConfigurationWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompatibilityApi.CompatibilityGetConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ProxyConfigurationDto**](ProxyConfigurationDto.md)

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

<a id="compatibilitygetendpointmetrics"></a>
# **CompatibilityGetEndpointMetrics**
> List&lt;EndpointMetrics&gt; CompatibilityGetEndpointMetrics (DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null)

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
    public class CompatibilityGetEndpointMetricsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompatibilityApi(httpClient, config, httpClientHandler);
            var fromDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var toDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 

            try
            {
                // Get per-endpoint compatibility metrics
                List<EndpointMetrics> result = apiInstance.CompatibilityGetEndpointMetrics(fromDate, toDate);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompatibilityApi.CompatibilityGetEndpointMetrics: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompatibilityGetEndpointMetricsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get per-endpoint compatibility metrics
    ApiResponse<List<EndpointMetrics>> response = apiInstance.CompatibilityGetEndpointMetricsWithHttpInfo(fromDate, toDate);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompatibilityApi.CompatibilityGetEndpointMetricsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **fromDate** | **DateTimeOffset?** |  | [optional]  |
| **toDate** | **DateTimeOffset?** |  | [optional]  |

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
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="compatibilitygetmetrics"></a>
# **CompatibilityGetMetrics**
> CompatibilityMetrics CompatibilityGetMetrics (DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null)

Get overall compatibility metrics

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
    public class CompatibilityGetMetricsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompatibilityApi(httpClient, config, httpClientHandler);
            var fromDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var toDate = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 

            try
            {
                // Get overall compatibility metrics
                CompatibilityMetrics result = apiInstance.CompatibilityGetMetrics(fromDate, toDate);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompatibilityApi.CompatibilityGetMetrics: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompatibilityGetMetricsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get overall compatibility metrics
    ApiResponse<CompatibilityMetrics> response = apiInstance.CompatibilityGetMetricsWithHttpInfo(fromDate, toDate);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompatibilityApi.CompatibilityGetMetricsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **fromDate** | **DateTimeOffset?** |  | [optional]  |
| **toDate** | **DateTimeOffset?** |  | [optional]  |

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
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="compatibilitytestapicomparison"></a>
# **CompatibilityTestApiComparison**
> ManualTestResult CompatibilityTestApiComparison (ManualTestRequest manualTestRequest)

Test API compatibility by comparing responses from Nightscout and Nocturne

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
    public class CompatibilityTestApiComparisonExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompatibilityApi(httpClient, config, httpClientHandler);
            var manualTestRequest = new ManualTestRequest(); // ManualTestRequest | 

            try
            {
                // Test API compatibility by comparing responses from Nightscout and Nocturne
                ManualTestResult result = apiInstance.CompatibilityTestApiComparison(manualTestRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompatibilityApi.CompatibilityTestApiComparison: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompatibilityTestApiComparisonWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Test API compatibility by comparing responses from Nightscout and Nocturne
    ApiResponse<ManualTestResult> response = apiInstance.CompatibilityTestApiComparisonWithHttpInfo(manualTestRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompatibilityApi.CompatibilityTestApiComparisonWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **manualTestRequest** | [**ManualTestRequest**](ManualTestRequest.md) |  |  |

### Return type

[**ManualTestResult**](ManualTestResult.md)

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

