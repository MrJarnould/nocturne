# NightscoutFoundation.Nocturne.Api.RetrospectiveApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**RetrospectiveGetBasalTimeline**](RetrospectiveApi.md#retrospectivegetbasaltimeline) | **GET** /api/v4/Retrospective/basal-timeline | Get basal rate timeline for a day Returns basal rate data points showing scheduled and temp basal changes |
| [**RetrospectiveGetRetrospectiveData**](RetrospectiveApi.md#retrospectivegetretrospectivedata) | **GET** /api/v4/Retrospective/at | Get retrospective data at a specific point in time Returns IOB, COB, glucose, basal rate, and recent treatments |
| [**RetrospectiveGetRetrospectiveTimeline**](RetrospectiveApi.md#retrospectivegetretrospectivetimeline) | **GET** /api/v4/Retrospective/timeline | Get retrospective data for an entire day at specified interval Returns IOB, COB, glucose, and basal data for every interval throughout the day |

<a id="retrospectivegetbasaltimeline"></a>
# **RetrospectiveGetBasalTimeline**
> BasalTimelineResponse RetrospectiveGetBasalTimeline (string? date = null, int? intervalMinutes = null)

Get basal rate timeline for a day Returns basal rate data points showing scheduled and temp basal changes

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
    public class RetrospectiveGetBasalTimelineExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new RetrospectiveApi(httpClient, config, httpClientHandler);
            var date = "date_example";  // string? | Date in YYYY-MM-DD format (optional) 
            var intervalMinutes = 5;  // int? | Interval in minutes between data points (default: 5) (optional)  (default to 5)

            try
            {
                // Get basal rate timeline for a day Returns basal rate data points showing scheduled and temp basal changes
                BasalTimelineResponse result = apiInstance.RetrospectiveGetBasalTimeline(date, intervalMinutes);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RetrospectiveApi.RetrospectiveGetBasalTimeline: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RetrospectiveGetBasalTimelineWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get basal rate timeline for a day Returns basal rate data points showing scheduled and temp basal changes
    ApiResponse<BasalTimelineResponse> response = apiInstance.RetrospectiveGetBasalTimelineWithHttpInfo(date, intervalMinutes);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling RetrospectiveApi.RetrospectiveGetBasalTimelineWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **date** | **string?** | Date in YYYY-MM-DD format | [optional]  |
| **intervalMinutes** | **int?** | Interval in minutes between data points (default: 5) | [optional] [default to 5] |

### Return type

[**BasalTimelineResponse**](BasalTimelineResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Basal rate timeline for the day |  -  |
| **400** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="retrospectivegetretrospectivedata"></a>
# **RetrospectiveGetRetrospectiveData**
> RetrospectiveDataResponse RetrospectiveGetRetrospectiveData (long? time = null)

Get retrospective data at a specific point in time Returns IOB, COB, glucose, basal rate, and recent treatments

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
    public class RetrospectiveGetRetrospectiveDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new RetrospectiveApi(httpClient, config, httpClientHandler);
            var time = 789L;  // long? | Unix timestamp in milliseconds for the retrospective point (optional) 

            try
            {
                // Get retrospective data at a specific point in time Returns IOB, COB, glucose, basal rate, and recent treatments
                RetrospectiveDataResponse result = apiInstance.RetrospectiveGetRetrospectiveData(time);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RetrospectiveApi.RetrospectiveGetRetrospectiveData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RetrospectiveGetRetrospectiveDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get retrospective data at a specific point in time Returns IOB, COB, glucose, basal rate, and recent treatments
    ApiResponse<RetrospectiveDataResponse> response = apiInstance.RetrospectiveGetRetrospectiveDataWithHttpInfo(time);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling RetrospectiveApi.RetrospectiveGetRetrospectiveDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **time** | **long?** | Unix timestamp in milliseconds for the retrospective point | [optional]  |

### Return type

[**RetrospectiveDataResponse**](RetrospectiveDataResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the retrospective data |  -  |
| **400** | If the time parameter is invalid |  -  |
| **500** | If there was an internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="retrospectivegetretrospectivetimeline"></a>
# **RetrospectiveGetRetrospectiveTimeline**
> RetrospectiveTimelineResponse RetrospectiveGetRetrospectiveTimeline (string? date = null, int? intervalMinutes = null)

Get retrospective data for an entire day at specified interval Returns IOB, COB, glucose, and basal data for every interval throughout the day

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
    public class RetrospectiveGetRetrospectiveTimelineExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new RetrospectiveApi(httpClient, config, httpClientHandler);
            var date = "date_example";  // string? | Date in YYYY-MM-DD format (optional) 
            var intervalMinutes = 5;  // int? | Interval in minutes between data points (default: 5) (optional)  (default to 5)

            try
            {
                // Get retrospective data for an entire day at specified interval Returns IOB, COB, glucose, and basal data for every interval throughout the day
                RetrospectiveTimelineResponse result = apiInstance.RetrospectiveGetRetrospectiveTimeline(date, intervalMinutes);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RetrospectiveApi.RetrospectiveGetRetrospectiveTimeline: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RetrospectiveGetRetrospectiveTimelineWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get retrospective data for an entire day at specified interval Returns IOB, COB, glucose, and basal data for every interval throughout the day
    ApiResponse<RetrospectiveTimelineResponse> response = apiInstance.RetrospectiveGetRetrospectiveTimelineWithHttpInfo(date, intervalMinutes);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling RetrospectiveApi.RetrospectiveGetRetrospectiveTimelineWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **date** | **string?** | Date in YYYY-MM-DD format | [optional]  |
| **intervalMinutes** | **int?** | Interval in minutes between data points (default: 5) | [optional] [default to 5] |

### Return type

[**RetrospectiveTimelineResponse**](RetrospectiveTimelineResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the retrospective timeline data |  -  |
| **400** | If parameters are invalid |  -  |
| **500** | If there was an internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

