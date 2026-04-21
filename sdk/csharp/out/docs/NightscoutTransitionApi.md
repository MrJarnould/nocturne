# NightscoutFoundation.Nocturne.Api.NightscoutTransitionApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**NightscoutTransitionGetTransitionStatus**](NightscoutTransitionApi.md#nightscouttransitiongettransitionstatus) | **GET** /api/v4/nightscout-transition/status | Get the current Nightscout transition status including migration progress, write-back health, and disconnect readiness recommendation. |

<a id="nightscouttransitiongettransitionstatus"></a>
# **NightscoutTransitionGetTransitionStatus**
> NightscoutTransitionStatus NightscoutTransitionGetTransitionStatus ()

Get the current Nightscout transition status including migration progress, write-back health, and disconnect readiness recommendation.

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
    public class NightscoutTransitionGetTransitionStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new NightscoutTransitionApi(httpClient, config, httpClientHandler);

            try
            {
                // Get the current Nightscout transition status including migration progress, write-back health, and disconnect readiness recommendation.
                NightscoutTransitionStatus result = apiInstance.NightscoutTransitionGetTransitionStatus();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling NightscoutTransitionApi.NightscoutTransitionGetTransitionStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NightscoutTransitionGetTransitionStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get the current Nightscout transition status including migration progress, write-back health, and disconnect readiness recommendation.
    ApiResponse<NightscoutTransitionStatus> response = apiInstance.NightscoutTransitionGetTransitionStatusWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling NightscoutTransitionApi.NightscoutTransitionGetTransitionStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**NightscoutTransitionStatus**](NightscoutTransitionStatus.md)

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

