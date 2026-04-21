# NightscoutFoundation.Nocturne.Api.AnalyticsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AnalyticsClearAnalyticsData**](AnalyticsApi.md#analyticsclearanalyticsdata) | **DELETE** /api/v4/Analytics/data | Clears all locally stored analytics data. This action cannot be undone. |
| [**AnalyticsGetAnalyticsStatus**](AnalyticsApi.md#analyticsgetanalyticsstatus) | **GET** /api/v4/Analytics/status | Gets the current analytics configuration and collection status. |
| [**AnalyticsGetPendingAnalyticsData**](AnalyticsApi.md#analyticsgetpendinganalyticsdata) | **GET** /api/v4/Analytics/data/pending | Gets any pending analytics data queued for collection, returned for transparency. Since data is stored locally only, this is informational. |
| [**AnalyticsGetPerformanceMetrics**](AnalyticsApi.md#analyticsgetperformancemetrics) | **GET** /api/v4/Analytics/metrics/performance | Gets current system performance metrics such as request latencies and memory usage. |
| [**AnalyticsGetPrivacyInformation**](AnalyticsApi.md#analyticsgetprivacyinformation) | **GET** /api/v4/Analytics/privacy | Get information about what data is collected and privacy policy |
| [**AnalyticsGetUsageStatistics**](AnalyticsApi.md#analyticsgetusagestatistics) | **GET** /api/v4/Analytics/metrics/usage | Gets current usage statistics such as endpoint hit counts and feature usage. |
| [**AnalyticsTrackCustomEvent**](AnalyticsApi.md#analyticstrackcustomevent) | **POST** /api/v4/Analytics/events | Tracks a custom AnalyticsEvent. Useful for integration testing or manually recording discrete events. Returns 400 Bad Request if analytics collection is disabled. |
| [**AnalyticsUpdateAnalyticsConfig**](AnalyticsApi.md#analyticsupdateanalyticsconfig) | **PUT** /api/v4/Analytics/config | Updates analytics collection configuration. |

<a id="analyticsclearanalyticsdata"></a>
# **AnalyticsClearAnalyticsData**
> void AnalyticsClearAnalyticsData ()

Clears all locally stored analytics data. This action cannot be undone.

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
    public class AnalyticsClearAnalyticsDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AnalyticsApi(httpClient, config, httpClientHandler);

            try
            {
                // Clears all locally stored analytics data. This action cannot be undone.
                apiInstance.AnalyticsClearAnalyticsData();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsClearAnalyticsData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AnalyticsClearAnalyticsDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Clears all locally stored analytics data. This action cannot be undone.
    apiInstance.AnalyticsClearAnalyticsDataWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AnalyticsApi.AnalyticsClearAnalyticsDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
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
| **200** | A confirmation message on success. |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="analyticsgetanalyticsstatus"></a>
# **AnalyticsGetAnalyticsStatus**
> void AnalyticsGetAnalyticsStatus ()

Gets the current analytics configuration and collection status.

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
    public class AnalyticsGetAnalyticsStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AnalyticsApi(httpClient, config, httpClientHandler);

            try
            {
                // Gets the current analytics configuration and collection status.
                apiInstance.AnalyticsGetAnalyticsStatus();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsGetAnalyticsStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AnalyticsGetAnalyticsStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets the current analytics configuration and collection status.
    apiInstance.AnalyticsGetAnalyticsStatusWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AnalyticsApi.AnalyticsGetAnalyticsStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
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
| **200** | An object containing whether collection is enabled, the current AnalyticsCollectionConfig,             system info, and a human-readable privacy note. |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="analyticsgetpendinganalyticsdata"></a>
# **AnalyticsGetPendingAnalyticsData**
> AnalyticsBatch AnalyticsGetPendingAnalyticsData ()

Gets any pending analytics data queued for collection, returned for transparency. Since data is stored locally only, this is informational.

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
    public class AnalyticsGetPendingAnalyticsDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AnalyticsApi(httpClient, config, httpClientHandler);

            try
            {
                // Gets any pending analytics data queued for collection, returned for transparency. Since data is stored locally only, this is informational.
                AnalyticsBatch result = apiInstance.AnalyticsGetPendingAnalyticsData();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsGetPendingAnalyticsData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AnalyticsGetPendingAnalyticsDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets any pending analytics data queued for collection, returned for transparency. Since data is stored locally only, this is informational.
    ApiResponse<AnalyticsBatch> response = apiInstance.AnalyticsGetPendingAnalyticsDataWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AnalyticsApi.AnalyticsGetPendingAnalyticsDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**AnalyticsBatch**](AnalyticsBatch.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An AnalyticsBatch if there is pending data, or a no-data message if none. |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="analyticsgetperformancemetrics"></a>
# **AnalyticsGetPerformanceMetrics**
> PerformanceMetrics AnalyticsGetPerformanceMetrics ()

Gets current system performance metrics such as request latencies and memory usage.

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
    public class AnalyticsGetPerformanceMetricsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AnalyticsApi(httpClient, config, httpClientHandler);

            try
            {
                // Gets current system performance metrics such as request latencies and memory usage.
                PerformanceMetrics result = apiInstance.AnalyticsGetPerformanceMetrics();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsGetPerformanceMetrics: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AnalyticsGetPerformanceMetricsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets current system performance metrics such as request latencies and memory usage.
    ApiResponse<PerformanceMetrics> response = apiInstance.AnalyticsGetPerformanceMetricsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AnalyticsApi.AnalyticsGetPerformanceMetricsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**PerformanceMetrics**](PerformanceMetrics.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A PerformanceMetrics snapshot. |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="analyticsgetprivacyinformation"></a>
# **AnalyticsGetPrivacyInformation**
> FileParameter AnalyticsGetPrivacyInformation ()

Get information about what data is collected and privacy policy

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
    public class AnalyticsGetPrivacyInformationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AnalyticsApi(httpClient, config, httpClientHandler);

            try
            {
                // Get information about what data is collected and privacy policy
                FileParameter result = apiInstance.AnalyticsGetPrivacyInformation();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsGetPrivacyInformation: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AnalyticsGetPrivacyInformationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get information about what data is collected and privacy policy
    ApiResponse<FileParameter> response = apiInstance.AnalyticsGetPrivacyInformationWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AnalyticsApi.AnalyticsGetPrivacyInformationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**FileParameter**](FileParameter.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Privacy and data collection information |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="analyticsgetusagestatistics"></a>
# **AnalyticsGetUsageStatistics**
> UsageStatistics AnalyticsGetUsageStatistics ()

Gets current usage statistics such as endpoint hit counts and feature usage.

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
    public class AnalyticsGetUsageStatisticsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AnalyticsApi(httpClient, config, httpClientHandler);

            try
            {
                // Gets current usage statistics such as endpoint hit counts and feature usage.
                UsageStatistics result = apiInstance.AnalyticsGetUsageStatistics();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsGetUsageStatistics: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AnalyticsGetUsageStatisticsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets current usage statistics such as endpoint hit counts and feature usage.
    ApiResponse<UsageStatistics> response = apiInstance.AnalyticsGetUsageStatisticsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AnalyticsApi.AnalyticsGetUsageStatisticsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**UsageStatistics**](UsageStatistics.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A UsageStatistics snapshot. |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="analyticstrackcustomevent"></a>
# **AnalyticsTrackCustomEvent**
> void AnalyticsTrackCustomEvent (AnalyticsEvent analyticsEvent)

Tracks a custom AnalyticsEvent. Useful for integration testing or manually recording discrete events. Returns 400 Bad Request if analytics collection is disabled.

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
    public class AnalyticsTrackCustomEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AnalyticsApi(httpClient, config, httpClientHandler);
            var analyticsEvent = new AnalyticsEvent(); // AnalyticsEvent | The custom AnalyticsEvent to record.

            try
            {
                // Tracks a custom AnalyticsEvent. Useful for integration testing or manually recording discrete events. Returns 400 Bad Request if analytics collection is disabled.
                apiInstance.AnalyticsTrackCustomEvent(analyticsEvent);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsTrackCustomEvent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AnalyticsTrackCustomEventWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Tracks a custom AnalyticsEvent. Useful for integration testing or manually recording discrete events. Returns 400 Bad Request if analytics collection is disabled.
    apiInstance.AnalyticsTrackCustomEventWithHttpInfo(analyticsEvent);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AnalyticsApi.AnalyticsTrackCustomEventWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **analyticsEvent** | [**AnalyticsEvent**](AnalyticsEvent.md) | The custom AnalyticsEvent to record. |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A confirmation message on success. |  -  |
| **400** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="analyticsupdateanalyticsconfig"></a>
# **AnalyticsUpdateAnalyticsConfig**
> AnalyticsCollectionConfig AnalyticsUpdateAnalyticsConfig (AnalyticsCollectionConfig analyticsCollectionConfig)

Updates analytics collection configuration.

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
    public class AnalyticsUpdateAnalyticsConfigExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AnalyticsApi(httpClient, config, httpClientHandler);
            var analyticsCollectionConfig = new AnalyticsCollectionConfig(); // AnalyticsCollectionConfig | The new AnalyticsCollectionConfig to persist.

            try
            {
                // Updates analytics collection configuration.
                AnalyticsCollectionConfig result = apiInstance.AnalyticsUpdateAnalyticsConfig(analyticsCollectionConfig);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsUpdateAnalyticsConfig: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AnalyticsUpdateAnalyticsConfigWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Updates analytics collection configuration.
    ApiResponse<AnalyticsCollectionConfig> response = apiInstance.AnalyticsUpdateAnalyticsConfigWithHttpInfo(analyticsCollectionConfig);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AnalyticsApi.AnalyticsUpdateAnalyticsConfigWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **analyticsCollectionConfig** | [**AnalyticsCollectionConfig**](AnalyticsCollectionConfig.md) | The new AnalyticsCollectionConfig to persist. |  |

### Return type

[**AnalyticsCollectionConfig**](AnalyticsCollectionConfig.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The updated configuration and a confirmation message. |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

