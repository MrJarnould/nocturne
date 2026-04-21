# NightscoutFoundation.Nocturne.Api.SummaryApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**SummaryGetSummary**](SummaryApi.md#summarygetsummary) | **GET** /api/v4/summary | Get widget-friendly summary data including current glucose, IOB, COB, trackers, and alarm state. |

<a id="summarygetsummary"></a>
# **SummaryGetSummary**
> V4SummaryResponse SummaryGetSummary (int? hours = null, bool? includePredictions = null)

Get widget-friendly summary data including current glucose, IOB, COB, trackers, and alarm state.

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
    public class SummaryGetSummaryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new SummaryApi(httpClient, config, httpClientHandler);
            var hours = 0;  // int? | Number of hours of glucose history to include (default 0 for current reading only) (optional)  (default to 0)
            var includePredictions = false;  // bool? | Whether to include predicted glucose values (default false) (optional)  (default to false)

            try
            {
                // Get widget-friendly summary data including current glucose, IOB, COB, trackers, and alarm state.
                V4SummaryResponse result = apiInstance.SummaryGetSummary(hours, includePredictions);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SummaryApi.SummaryGetSummary: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SummaryGetSummaryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get widget-friendly summary data including current glucose, IOB, COB, trackers, and alarm state.
    ApiResponse<V4SummaryResponse> response = apiInstance.SummaryGetSummaryWithHttpInfo(hours, includePredictions);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SummaryApi.SummaryGetSummaryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **hours** | **int?** | Number of hours of glucose history to include (default 0 for current reading only) | [optional] [default to 0] |
| **includePredictions** | **bool?** | Whether to include predicted glucose values (default false) | [optional] [default to false] |

### Return type

[**V4SummaryResponse**](V4SummaryResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Widget summary response with aggregated diabetes management data |  -  |
| **401** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

