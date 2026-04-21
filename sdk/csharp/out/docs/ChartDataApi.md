# NightscoutFoundation.Nocturne.Api.ChartDataApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ChartDataGetDashboardChartData**](ChartDataApi.md#chartdatagetdashboardchartdata) | **GET** /api/v4/ChartData/dashboard | Gets complete dashboard chart data in a single call. Returns pre-calculated IOB, COB, basal series, categorized treatment markers, state spans, system events, tracker markers, and glucose readings. |

<a id="chartdatagetdashboardchartdata"></a>
# **ChartDataGetDashboardChartData**
> DashboardChartData ChartDataGetDashboardChartData (long? startTime = null, long? endTime = null, int? intervalMinutes = null)

Gets complete dashboard chart data in a single call. Returns pre-calculated IOB, COB, basal series, categorized treatment markers, state spans, system events, tracker markers, and glucose readings.

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
    public class ChartDataGetDashboardChartDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ChartDataApi(httpClient, config, httpClientHandler);
            var startTime = 789L;  // long? | Start of the requested window as a Unix timestamp in milliseconds. (optional) 
            var endTime = 789L;  // long? | End of the requested window as a Unix timestamp in milliseconds.             Must be greater than startTime. (optional) 
            var intervalMinutes = 5;  // int? | Granularity of the returned series in minutes (1–60, default 5). (optional)  (default to 5)

            try
            {
                // Gets complete dashboard chart data in a single call. Returns pre-calculated IOB, COB, basal series, categorized treatment markers, state spans, system events, tracker markers, and glucose readings.
                DashboardChartData result = apiInstance.ChartDataGetDashboardChartData(startTime, endTime, intervalMinutes);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChartDataApi.ChartDataGetDashboardChartData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChartDataGetDashboardChartDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets complete dashboard chart data in a single call. Returns pre-calculated IOB, COB, basal series, categorized treatment markers, state spans, system events, tracker markers, and glucose readings.
    ApiResponse<DashboardChartData> response = apiInstance.ChartDataGetDashboardChartDataWithHttpInfo(startTime, endTime, intervalMinutes);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ChartDataApi.ChartDataGetDashboardChartDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startTime** | **long?** | Start of the requested window as a Unix timestamp in milliseconds. | [optional]  |
| **endTime** | **long?** | End of the requested window as a Unix timestamp in milliseconds.             Must be greater than startTime. | [optional]  |
| **intervalMinutes** | **int?** | Granularity of the returned series in minutes (1–60, default 5). | [optional] [default to 5] |

### Return type

[**DashboardChartData**](DashboardChartData.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A fully populated DashboardChartData object. |  -  |
| **400** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

