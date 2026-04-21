# NightscoutFoundation.Nocturne.Api.V4DataOverviewApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DataOverviewGetAvailableYears**](V4DataOverviewApi.md#dataoverviewgetavailableyears) | **GET** /api/v4/year-overview/years | Get the list of years that contain data and available data sources |
| [**DataOverviewGetDailySummary**](V4DataOverviewApi.md#dataoverviewgetdailysummary) | **GET** /api/v4/year-overview/daily-summary | Get day-level aggregated counts and average glucose for a given year |
| [**DataOverviewGetGriTimeline**](V4DataOverviewApi.md#dataoverviewgetgritimeline) | **GET** /api/v4/year-overview/gri-timeline | Get monthly GRI (Glycemic Risk Index) scores for a given year |

<a id="dataoverviewgetavailableyears"></a>
# **DataOverviewGetAvailableYears**
> DataOverviewYearsResponse DataOverviewGetAvailableYears ()

Get the list of years that contain data and available data sources

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
    public class DataOverviewGetAvailableYearsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DataOverviewApi(httpClient, config, httpClientHandler);

            try
            {
                // Get the list of years that contain data and available data sources
                DataOverviewYearsResponse result = apiInstance.DataOverviewGetAvailableYears();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DataOverviewApi.DataOverviewGetAvailableYears: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DataOverviewGetAvailableYearsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get the list of years that contain data and available data sources
    ApiResponse<DataOverviewYearsResponse> response = apiInstance.DataOverviewGetAvailableYearsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DataOverviewApi.DataOverviewGetAvailableYearsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**DataOverviewYearsResponse**](DataOverviewYearsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dataoverviewgetdailysummary"></a>
# **DataOverviewGetDailySummary**
> DailySummaryResponse DataOverviewGetDailySummary (int? year = null, List<string>? dataSources = null)

Get day-level aggregated counts and average glucose for a given year

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
    public class DataOverviewGetDailySummaryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DataOverviewApi(httpClient, config, httpClientHandler);
            var year = 56;  // int? | The year to aggregate (optional) 
            var dataSources = new List<string>?(); // List<string>? | Optional data source filters (multiple allowed) (optional) 

            try
            {
                // Get day-level aggregated counts and average glucose for a given year
                DailySummaryResponse result = apiInstance.DataOverviewGetDailySummary(year, dataSources);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DataOverviewApi.DataOverviewGetDailySummary: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DataOverviewGetDailySummaryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get day-level aggregated counts and average glucose for a given year
    ApiResponse<DailySummaryResponse> response = apiInstance.DataOverviewGetDailySummaryWithHttpInfo(year, dataSources);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DataOverviewApi.DataOverviewGetDailySummaryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **year** | **int?** | The year to aggregate | [optional]  |
| **dataSources** | [**List&lt;string&gt;?**](string.md) | Optional data source filters (multiple allowed) | [optional]  |

### Return type

[**DailySummaryResponse**](DailySummaryResponse.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dataoverviewgetgritimeline"></a>
# **DataOverviewGetGriTimeline**
> GriTimelineResponse DataOverviewGetGriTimeline (int? year = null, List<string>? dataSources = null)

Get monthly GRI (Glycemic Risk Index) scores for a given year

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
    public class DataOverviewGetGriTimelineExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DataOverviewApi(httpClient, config, httpClientHandler);
            var year = 56;  // int? | The year to compute GRI timeline for (optional) 
            var dataSources = new List<string>?(); // List<string>? | Optional data source filters (multiple allowed) (optional) 

            try
            {
                // Get monthly GRI (Glycemic Risk Index) scores for a given year
                GriTimelineResponse result = apiInstance.DataOverviewGetGriTimeline(year, dataSources);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DataOverviewApi.DataOverviewGetGriTimeline: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DataOverviewGetGriTimelineWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get monthly GRI (Glycemic Risk Index) scores for a given year
    ApiResponse<GriTimelineResponse> response = apiInstance.DataOverviewGetGriTimelineWithHttpInfo(year, dataSources);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DataOverviewApi.DataOverviewGetGriTimelineWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **year** | **int?** | The year to compute GRI timeline for | [optional]  |
| **dataSources** | [**List&lt;string&gt;?**](string.md) | Optional data source filters (multiple allowed) | [optional]  |

### Return type

[**GriTimelineResponse**](GriTimelineResponse.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

