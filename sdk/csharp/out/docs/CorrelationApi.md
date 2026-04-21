# NightscoutFoundation.Nocturne.Api.CorrelationApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CorrelationGetCorrelated**](CorrelationApi.md#correlationgetcorrelated) | **GET** /api/v4/correlated/{correlationId} | Retrieves all records that share the given correlation ID across every V4 data type. |

<a id="correlationgetcorrelated"></a>
# **CorrelationGetCorrelated**
> void CorrelationGetCorrelated (string correlationId)

Retrieves all records that share the given correlation ID across every V4 data type.

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
    public class CorrelationGetCorrelatedExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CorrelationApi(httpClient, config, httpClientHandler);
            var correlationId = "correlationId_example";  // string | The shared correlation ID to look up.

            try
            {
                // Retrieves all records that share the given correlation ID across every V4 data type.
                apiInstance.CorrelationGetCorrelated(correlationId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CorrelationApi.CorrelationGetCorrelated: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CorrelationGetCorrelatedWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Retrieves all records that share the given correlation ID across every V4 data type.
    apiInstance.CorrelationGetCorrelatedWithHttpInfo(correlationId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CorrelationApi.CorrelationGetCorrelatedWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **correlationId** | **string** | The shared correlation ID to look up. |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An anonymous object with typed arrays for each data category (SensorGlucose, MeterGlucose, Calibrations, Boluses, CarbIntakes, BGChecks, Notes, BolusCalculations). Arrays are empty when no matching records exist in that category. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

