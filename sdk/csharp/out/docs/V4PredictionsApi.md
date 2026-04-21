# NightscoutFoundation.Nocturne.Api.V4PredictionsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**PredictionGetPredictions**](V4PredictionsApi.md#predictiongetpredictions) | **GET** /api/v4/predictions | Get glucose predictions based on current data. Returns predicted glucose values for the next 4 hours in 5-minute intervals. |
| [**PredictionGetStatus**](V4PredictionsApi.md#predictiongetstatus) | **GET** /api/v4/predictions/status | Check the status of the prediction service. |

<a id="predictiongetpredictions"></a>
# **PredictionGetPredictions**
> GlucosePredictionResponse PredictionGetPredictions (string? profileId = null)

Get glucose predictions based on current data. Returns predicted glucose values for the next 4 hours in 5-minute intervals.

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
    public class PredictionGetPredictionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PredictionsApi(httpClient, config, httpClientHandler);
            var profileId = "profileId_example";  // string? | Optional profile ID to use for predictions (optional) 

            try
            {
                // Get glucose predictions based on current data. Returns predicted glucose values for the next 4 hours in 5-minute intervals.
                GlucosePredictionResponse result = apiInstance.PredictionGetPredictions(profileId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PredictionsApi.PredictionGetPredictions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PredictionGetPredictionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get glucose predictions based on current data. Returns predicted glucose values for the next 4 hours in 5-minute intervals.
    ApiResponse<GlucosePredictionResponse> response = apiInstance.PredictionGetPredictionsWithHttpInfo(profileId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PredictionsApi.PredictionGetPredictionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **profileId** | **string?** | Optional profile ID to use for predictions | [optional]  |

### Return type

[**GlucosePredictionResponse**](GlucosePredictionResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Glucose predictions including IOB, UAM, COB, and zero-temp curves |  -  |
| **400** |  |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="predictiongetstatus"></a>
# **PredictionGetStatus**
> PredictionStatusResponse PredictionGetStatus ()

Check the status of the prediction service.

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
    public class PredictionGetStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PredictionsApi(httpClient, config, httpClientHandler);

            try
            {
                // Check the status of the prediction service.
                PredictionStatusResponse result = apiInstance.PredictionGetStatus();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PredictionsApi.PredictionGetStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PredictionGetStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Check the status of the prediction service.
    ApiResponse<PredictionStatusResponse> response = apiInstance.PredictionGetStatusWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PredictionsApi.PredictionGetStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**PredictionStatusResponse**](PredictionStatusResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Status of the prediction service including configured source |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

