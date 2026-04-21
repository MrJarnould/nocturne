# NightscoutFoundation.Nocturne.Api.CompressionLowApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CompressionLowAcceptSuggestion**](CompressionLowApi.md#compressionlowacceptsuggestion) | **POST** /api/v4/compression-lows/suggestions/{id}/accept | Accept a suggestion with adjusted bounds |
| [**CompressionLowDeleteSuggestion**](CompressionLowApi.md#compressionlowdeletesuggestion) | **DELETE** /api/v4/compression-lows/suggestions/{id} | Delete a suggestion and its associated state span |
| [**CompressionLowDismissSuggestion**](CompressionLowApi.md#compressionlowdismisssuggestion) | **POST** /api/v4/compression-lows/suggestions/{id}/dismiss | Dismiss a suggestion |
| [**CompressionLowGetSuggestion**](CompressionLowApi.md#compressionlowgetsuggestion) | **GET** /api/v4/compression-lows/suggestions/{id} | Get a single suggestion with glucose entries for charting |
| [**CompressionLowGetSuggestions**](CompressionLowApi.md#compressionlowgetsuggestions) | **GET** /api/v4/compression-lows/suggestions | Get compression low suggestions with optional filtering |
| [**CompressionLowTriggerDetection**](CompressionLowApi.md#compressionlowtriggerdetection) | **POST** /api/v4/compression-lows/detect | Manually trigger detection for a date range (for testing) |

<a id="compressionlowacceptsuggestion"></a>
# **CompressionLowAcceptSuggestion**
> StateSpan CompressionLowAcceptSuggestion (string id, AcceptSuggestionRequest acceptSuggestionRequest)

Accept a suggestion with adjusted bounds

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
    public class CompressionLowAcceptSuggestionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompressionLowApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var acceptSuggestionRequest = new AcceptSuggestionRequest(); // AcceptSuggestionRequest | 

            try
            {
                // Accept a suggestion with adjusted bounds
                StateSpan result = apiInstance.CompressionLowAcceptSuggestion(id, acceptSuggestionRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompressionLowApi.CompressionLowAcceptSuggestion: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompressionLowAcceptSuggestionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Accept a suggestion with adjusted bounds
    ApiResponse<StateSpan> response = apiInstance.CompressionLowAcceptSuggestionWithHttpInfo(id, acceptSuggestionRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompressionLowApi.CompressionLowAcceptSuggestionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **acceptSuggestionRequest** | [**AcceptSuggestionRequest**](AcceptSuggestionRequest.md) |  |  |

### Return type

[**StateSpan**](StateSpan.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="compressionlowdeletesuggestion"></a>
# **CompressionLowDeleteSuggestion**
> void CompressionLowDeleteSuggestion (string id)

Delete a suggestion and its associated state span

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
    public class CompressionLowDeleteSuggestionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompressionLowApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a suggestion and its associated state span
                apiInstance.CompressionLowDeleteSuggestion(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompressionLowApi.CompressionLowDeleteSuggestion: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompressionLowDeleteSuggestionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a suggestion and its associated state span
    apiInstance.CompressionLowDeleteSuggestionWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompressionLowApi.CompressionLowDeleteSuggestionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="compressionlowdismisssuggestion"></a>
# **CompressionLowDismissSuggestion**
> void CompressionLowDismissSuggestion (string id)

Dismiss a suggestion

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
    public class CompressionLowDismissSuggestionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompressionLowApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Dismiss a suggestion
                apiInstance.CompressionLowDismissSuggestion(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompressionLowApi.CompressionLowDismissSuggestion: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompressionLowDismissSuggestionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Dismiss a suggestion
    apiInstance.CompressionLowDismissSuggestionWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompressionLowApi.CompressionLowDismissSuggestionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |
| **400** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="compressionlowgetsuggestion"></a>
# **CompressionLowGetSuggestion**
> CompressionLowSuggestionWithEntries CompressionLowGetSuggestion (string id)

Get a single suggestion with glucose entries for charting

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
    public class CompressionLowGetSuggestionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompressionLowApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Get a single suggestion with glucose entries for charting
                CompressionLowSuggestionWithEntries result = apiInstance.CompressionLowGetSuggestion(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompressionLowApi.CompressionLowGetSuggestion: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompressionLowGetSuggestionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a single suggestion with glucose entries for charting
    ApiResponse<CompressionLowSuggestionWithEntries> response = apiInstance.CompressionLowGetSuggestionWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompressionLowApi.CompressionLowGetSuggestionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**CompressionLowSuggestionWithEntries**](CompressionLowSuggestionWithEntries.md)

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

<a id="compressionlowgetsuggestions"></a>
# **CompressionLowGetSuggestions**
> List&lt;CompressionLowSuggestion&gt; CompressionLowGetSuggestions (CompressionLowGetSuggestionsStatusParameter? status = null, string? nightOf = null)

Get compression low suggestions with optional filtering

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
    public class CompressionLowGetSuggestionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompressionLowApi(httpClient, config, httpClientHandler);
            var status = new CompressionLowGetSuggestionsStatusParameter?(); // CompressionLowGetSuggestionsStatusParameter? |  (optional) 
            var nightOf = "nightOf_example";  // string? |  (optional) 

            try
            {
                // Get compression low suggestions with optional filtering
                List<CompressionLowSuggestion> result = apiInstance.CompressionLowGetSuggestions(status, nightOf);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompressionLowApi.CompressionLowGetSuggestions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompressionLowGetSuggestionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get compression low suggestions with optional filtering
    ApiResponse<List<CompressionLowSuggestion>> response = apiInstance.CompressionLowGetSuggestionsWithHttpInfo(status, nightOf);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompressionLowApi.CompressionLowGetSuggestionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **status** | [**CompressionLowGetSuggestionsStatusParameter?**](CompressionLowGetSuggestionsStatusParameter?.md) |  | [optional]  |
| **nightOf** | **string?** |  | [optional]  |

### Return type

[**List&lt;CompressionLowSuggestion&gt;**](CompressionLowSuggestion.md)

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

<a id="compressionlowtriggerdetection"></a>
# **CompressionLowTriggerDetection**
> DetectionResult CompressionLowTriggerDetection (TriggerDetectionRequest triggerDetectionRequest)

Manually trigger detection for a date range (for testing)

Provide either a single `nightOf` date or a range with `startDate` and `endDate`. When using a range, detection runs for each night in the range (inclusive).

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
    public class CompressionLowTriggerDetectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new CompressionLowApi(httpClient, config, httpClientHandler);
            var triggerDetectionRequest = new TriggerDetectionRequest(); // TriggerDetectionRequest | 

            try
            {
                // Manually trigger detection for a date range (for testing)
                DetectionResult result = apiInstance.CompressionLowTriggerDetection(triggerDetectionRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CompressionLowApi.CompressionLowTriggerDetection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CompressionLowTriggerDetectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Manually trigger detection for a date range (for testing)
    ApiResponse<DetectionResult> response = apiInstance.CompressionLowTriggerDetectionWithHttpInfo(triggerDetectionRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CompressionLowApi.CompressionLowTriggerDetectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **triggerDetectionRequest** | [**TriggerDetectionRequest**](TriggerDetectionRequest.md) |  |  |

### Return type

[**DetectionResult**](DetectionResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

