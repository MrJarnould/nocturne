# NightscoutFoundation.Nocturne.Api.V4SystemApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**SystemHeartbeat**](V4SystemApi.md#systemheartbeat) | **POST** /api/v4/system/heartbeat | Accept a heartbeat from an external service (e.g. bot adapter). Returns 200 OK. Actual health tracking will be added later. |

<a id="systemheartbeat"></a>
# **SystemHeartbeat**
> void SystemHeartbeat (HeartbeatRequest heartbeatRequest)

Accept a heartbeat from an external service (e.g. bot adapter). Returns 200 OK. Actual health tracking will be added later.

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
    public class SystemHeartbeatExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4SystemApi(httpClient, config, httpClientHandler);
            var heartbeatRequest = new HeartbeatRequest(); // HeartbeatRequest | 

            try
            {
                // Accept a heartbeat from an external service (e.g. bot adapter). Returns 200 OK. Actual health tracking will be added later.
                apiInstance.SystemHeartbeat(heartbeatRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4SystemApi.SystemHeartbeat: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SystemHeartbeatWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Accept a heartbeat from an external service (e.g. bot adapter). Returns 200 OK. Actual health tracking will be added later.
    apiInstance.SystemHeartbeatWithHttpInfo(heartbeatRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4SystemApi.SystemHeartbeatWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **heartbeatRequest** | [**HeartbeatRequest**](HeartbeatRequest.md) |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

