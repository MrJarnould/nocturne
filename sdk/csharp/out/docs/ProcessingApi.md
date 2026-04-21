# NightscoutFoundation.Nocturne.Api.ProcessingApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ProcessingGetProcessingStatus**](ProcessingApi.md#processinggetprocessingstatus) | **GET** /api/v4/processing/status/{correlationId} |  |
| [**ProcessingWaitForCompletion**](ProcessingApi.md#processingwaitforcompletion) | **GET** /api/v4/processing/status/{correlationId}/wait |  |

<a id="processinggetprocessingstatus"></a>
# **ProcessingGetProcessingStatus**
> ProcessingStatusResponse ProcessingGetProcessingStatus (string correlationId)



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
            var apiInstance = new ProcessingApi(httpClient, config, httpClientHandler);
            var correlationId = "correlationId_example";  // string | 

            try
            {
                ProcessingStatusResponse result = apiInstance.ProcessingGetProcessingStatus(correlationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ProcessingApi.ProcessingGetProcessingStatus: " + e.Message);
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
    ApiResponse<ProcessingStatusResponse> response = apiInstance.ProcessingGetProcessingStatusWithHttpInfo(correlationId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ProcessingApi.ProcessingGetProcessingStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **correlationId** | **string** |  |  |

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
| **200** |  |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="processingwaitforcompletion"></a>
# **ProcessingWaitForCompletion**
> ProcessingStatusResponse ProcessingWaitForCompletion (string correlationId, int? timeoutSeconds = null)



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
            var apiInstance = new ProcessingApi(httpClient, config, httpClientHandler);
            var correlationId = "correlationId_example";  // string | 
            var timeoutSeconds = 30;  // int? |  (optional)  (default to 30)

            try
            {
                ProcessingStatusResponse result = apiInstance.ProcessingWaitForCompletion(correlationId, timeoutSeconds);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ProcessingApi.ProcessingWaitForCompletion: " + e.Message);
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
    ApiResponse<ProcessingStatusResponse> response = apiInstance.ProcessingWaitForCompletionWithHttpInfo(correlationId, timeoutSeconds);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ProcessingApi.ProcessingWaitForCompletionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **correlationId** | **string** |  |  |
| **timeoutSeconds** | **int?** |  | [optional] [default to 30] |

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
| **200** |  |  -  |
| **404** |  |  -  |
| **408** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

