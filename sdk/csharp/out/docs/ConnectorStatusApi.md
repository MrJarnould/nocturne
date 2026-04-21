# NightscoutFoundation.Nocturne.Api.ConnectorStatusApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ConnectorStatusGetStatus**](ConnectorStatusApi.md#connectorstatusgetstatus) | **GET** /api/v4/connectors/status | Gets the current status and metrics for all registered connectors |

<a id="connectorstatusgetstatus"></a>
# **ConnectorStatusGetStatus**
> List&lt;ConnectorStatusDto&gt; ConnectorStatusGetStatus ()

Gets the current status and metrics for all registered connectors

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
    public class ConnectorStatusGetStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConnectorStatusApi(httpClient, config, httpClientHandler);

            try
            {
                // Gets the current status and metrics for all registered connectors
                List<ConnectorStatusDto> result = apiInstance.ConnectorStatusGetStatus();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConnectorStatusApi.ConnectorStatusGetStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConnectorStatusGetStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets the current status and metrics for all registered connectors
    ApiResponse<List<ConnectorStatusDto>> response = apiInstance.ConnectorStatusGetStatusWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConnectorStatusApi.ConnectorStatusGetStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;ConnectorStatusDto&gt;**](ConnectorStatusDto.md)

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

