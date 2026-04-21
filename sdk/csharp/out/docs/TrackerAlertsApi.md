# NightscoutFoundation.Nocturne.Api.TrackerAlertsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**TrackerAlertsGetAvailableSounds**](TrackerAlertsApi.md#trackeralertsgetavailablesounds) | **GET** /api/v4/trackers/alerts/sounds | Get available alert sounds |
| [**TrackerAlertsGetPendingAlerts**](TrackerAlertsApi.md#trackeralertsgetpendingalerts) | **GET** /api/v4/trackers/alerts/pending |  |

<a id="trackeralertsgetavailablesounds"></a>
# **TrackerAlertsGetAvailableSounds**
> List&lt;string&gt; TrackerAlertsGetAvailableSounds ()

Get available alert sounds

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
    public class TrackerAlertsGetAvailableSoundsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new TrackerAlertsApi(httpClient, config, httpClientHandler);

            try
            {
                // Get available alert sounds
                List<string> result = apiInstance.TrackerAlertsGetAvailableSounds();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling TrackerAlertsApi.TrackerAlertsGetAvailableSounds: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackerAlertsGetAvailableSoundsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get available alert sounds
    ApiResponse<List<string>> response = apiInstance.TrackerAlertsGetAvailableSoundsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling TrackerAlertsApi.TrackerAlertsGetAvailableSoundsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

**List<string>**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of available sound preset names |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="trackeralertsgetpendingalerts"></a>
# **TrackerAlertsGetPendingAlerts**
> List&lt;TrackerAlertDto&gt; TrackerAlertsGetPendingAlerts ()



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
    public class TrackerAlertsGetPendingAlertsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new TrackerAlertsApi(httpClient, config, httpClientHandler);

            try
            {
                List<TrackerAlertDto> result = apiInstance.TrackerAlertsGetPendingAlerts();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling TrackerAlertsApi.TrackerAlertsGetPendingAlerts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TrackerAlertsGetPendingAlertsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<List<TrackerAlertDto>> response = apiInstance.TrackerAlertsGetPendingAlertsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling TrackerAlertsApi.TrackerAlertsGetPendingAlertsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;TrackerAlertDto&gt;**](TrackerAlertDto.md)

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

