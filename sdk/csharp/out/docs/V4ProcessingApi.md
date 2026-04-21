# NightscoutFoundation.Nocturne.Api.V4ProcessingApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ProcessingGetProcessingStatus**](V4ProcessingApi.md#processinggetprocessingstatus) | **GET** /api/v4/processing/status/{correlationId} | Get the processing status for a correlation ID |
| [**ProcessingWaitForCompletion**](V4ProcessingApi.md#processingwaitforcompletion) | **GET** /api/v4/processing/status/{correlationId}/wait | Wait for processing to complete with long polling |

<a id="processinggetprocessingstatus"></a>
# **ProcessingGetProcessingStatus**
> ProcessingStatusResponse ProcessingGetProcessingStatus (string correlationId)

Get the processing status for a correlation ID

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
    public class ProcessingGetProcessingStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ProcessingApi(httpClient, config, httpClientHandler);
            var correlationId = "correlationId_example";  // string | The correlation ID to check

            try
            {
                // Get the processing status for a correlation ID
                ProcessingStatusResponse result = apiInstance.ProcessingGetProcessingStatus(correlationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ProcessingApi.ProcessingGetProcessingStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ProcessingGetProcessingStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get the processing status for a correlation ID
    ApiResponse<ProcessingStatusResponse> response = apiInstance.ProcessingGetProcessingStatusWithHttpInfo(correlationId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ProcessingApi.ProcessingGetProcessingStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **correlationId** | **string** | The correlation ID to check |  |

### Return type

[**ProcessingStatusResponse**](ProcessingStatusResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Processing status or 404 if not found |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="processingwaitforcompletion"></a>
# **ProcessingWaitForCompletion**
> ProcessingStatusResponse ProcessingWaitForCompletion (string correlationId, int? timeoutSeconds = null)

Wait for processing to complete with long polling

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
    public class ProcessingWaitForCompletionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ProcessingApi(httpClient, config, httpClientHandler);
            var correlationId = "correlationId_example";  // string | The correlation ID to wait for
            var timeoutSeconds = 30;  // int? | Maximum time to wait in seconds (default: 30) (optional)  (default to 30)

            try
            {
                // Wait for processing to complete with long polling
                ProcessingStatusResponse result = apiInstance.ProcessingWaitForCompletion(correlationId, timeoutSeconds);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ProcessingApi.ProcessingWaitForCompletion: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ProcessingWaitForCompletionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Wait for processing to complete with long polling
    ApiResponse<ProcessingStatusResponse> response = apiInstance.ProcessingWaitForCompletionWithHttpInfo(correlationId, timeoutSeconds);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ProcessingApi.ProcessingWaitForCompletionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **correlationId** | **string** | The correlation ID to wait for |  |
| **timeoutSeconds** | **int?** | Maximum time to wait in seconds (default: 30) | [optional] [default to 30] |

### Return type

[**ProcessingStatusResponse**](ProcessingStatusResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Processing status when completed or timeout response |  -  |
| **404** |  |  -  |
| **408** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

