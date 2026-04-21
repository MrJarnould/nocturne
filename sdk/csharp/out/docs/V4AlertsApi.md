# NightscoutFoundation.Nocturne.Api.V4AlertsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AlertsAcknowledge**](V4AlertsApi.md#alertsacknowledge) | **POST** /api/v4/alerts/acknowledge | Acknowledge all active alerts for the current tenant. |
| [**AlertsGetActiveAlerts**](V4AlertsApi.md#alertsgetactivealerts) | **GET** /api/v4/alerts/active | List active (unresolved) excursions for the current tenant. |
| [**AlertsGetAlertHistory**](V4AlertsApi.md#alertsgetalerthistory) | **GET** /api/v4/alerts/history | Get paginated history of resolved excursions. |
| [**AlertsGetPendingDeliveries**](V4AlertsApi.md#alertsgetpendingdeliveries) | **GET** /api/v4/alerts/deliveries/pending | Get pending deliveries for the specified channel types. Used by bot/adapter services to poll for work. |
| [**AlertsGetQuietHours**](V4AlertsApi.md#alertsgetquiethours) | **GET** /api/v4/alerts/quiet-hours | Get the current quiet hours configuration for the tenant. |
| [**AlertsMarkDelivered**](V4AlertsApi.md#alertsmarkdelivered) | **POST** /api/v4/alerts/deliveries/{deliveryId}/delivered | Mark a delivery as successfully sent by the channel adapter. |
| [**AlertsMarkFailed**](V4AlertsApi.md#alertsmarkfailed) | **POST** /api/v4/alerts/deliveries/{deliveryId}/failed | Mark a delivery as failed by the channel adapter. |
| [**AlertsSnoozeInstance**](V4AlertsApi.md#alertssnoozeinstance) | **POST** /api/v4/alerts/instances/{instanceId}/snooze | Snooze an alert instance for the specified duration. |
| [**AlertsUpdateQuietHours**](V4AlertsApi.md#alertsupdatequiethours) | **PUT** /api/v4/alerts/quiet-hours | Update quiet hours configuration for the tenant. |

<a id="alertsacknowledge"></a>
# **AlertsAcknowledge**
> void AlertsAcknowledge (AcknowledgeRequest acknowledgeRequest)

Acknowledge all active alerts for the current tenant.

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
    public class AlertsAcknowledgeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertsApi(httpClient, config, httpClientHandler);
            var acknowledgeRequest = new AcknowledgeRequest(); // AcknowledgeRequest | 

            try
            {
                // Acknowledge all active alerts for the current tenant.
                apiInstance.AlertsAcknowledge(acknowledgeRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertsApi.AlertsAcknowledge: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertsAcknowledgeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Acknowledge all active alerts for the current tenant.
    apiInstance.AlertsAcknowledgeWithHttpInfo(acknowledgeRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertsApi.AlertsAcknowledgeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **acknowledgeRequest** | [**AcknowledgeRequest**](AcknowledgeRequest.md) |  |  |

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
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="alertsgetactivealerts"></a>
# **AlertsGetActiveAlerts**
> List&lt;ActiveExcursionResponse&gt; AlertsGetActiveAlerts ()

List active (unresolved) excursions for the current tenant.

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
    public class AlertsGetActiveAlertsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertsApi(httpClient, config, httpClientHandler);

            try
            {
                // List active (unresolved) excursions for the current tenant.
                List<ActiveExcursionResponse> result = apiInstance.AlertsGetActiveAlerts();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertsApi.AlertsGetActiveAlerts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertsGetActiveAlertsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List active (unresolved) excursions for the current tenant.
    ApiResponse<List<ActiveExcursionResponse>> response = apiInstance.AlertsGetActiveAlertsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertsApi.AlertsGetActiveAlertsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;ActiveExcursionResponse&gt;**](ActiveExcursionResponse.md)

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

<a id="alertsgetalerthistory"></a>
# **AlertsGetAlertHistory**
> AlertHistoryResponse AlertsGetAlertHistory (int? page = null, int? pageSize = null)

Get paginated history of resolved excursions.

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
    public class AlertsGetAlertHistoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertsApi(httpClient, config, httpClientHandler);
            var page = 1;  // int? |  (optional)  (default to 1)
            var pageSize = 20;  // int? |  (optional)  (default to 20)

            try
            {
                // Get paginated history of resolved excursions.
                AlertHistoryResponse result = apiInstance.AlertsGetAlertHistory(page, pageSize);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertsApi.AlertsGetAlertHistory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertsGetAlertHistoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get paginated history of resolved excursions.
    ApiResponse<AlertHistoryResponse> response = apiInstance.AlertsGetAlertHistoryWithHttpInfo(page, pageSize);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertsApi.AlertsGetAlertHistoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **page** | **int?** |  | [optional] [default to 1] |
| **pageSize** | **int?** |  | [optional] [default to 20] |

### Return type

[**AlertHistoryResponse**](AlertHistoryResponse.md)

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

<a id="alertsgetpendingdeliveries"></a>
# **AlertsGetPendingDeliveries**
> List&lt;PendingDeliveryResponse&gt; AlertsGetPendingDeliveries (List<string>? channelType = null)

Get pending deliveries for the specified channel types. Used by bot/adapter services to poll for work.

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
    public class AlertsGetPendingDeliveriesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertsApi(httpClient, config, httpClientHandler);
            var channelType = new List<string>?(); // List<string>? |  (optional) 

            try
            {
                // Get pending deliveries for the specified channel types. Used by bot/adapter services to poll for work.
                List<PendingDeliveryResponse> result = apiInstance.AlertsGetPendingDeliveries(channelType);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertsApi.AlertsGetPendingDeliveries: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertsGetPendingDeliveriesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get pending deliveries for the specified channel types. Used by bot/adapter services to poll for work.
    ApiResponse<List<PendingDeliveryResponse>> response = apiInstance.AlertsGetPendingDeliveriesWithHttpInfo(channelType);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertsApi.AlertsGetPendingDeliveriesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **channelType** | [**List&lt;string&gt;?**](string.md) |  | [optional]  |

### Return type

[**List&lt;PendingDeliveryResponse&gt;**](PendingDeliveryResponse.md)

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

<a id="alertsgetquiethours"></a>
# **AlertsGetQuietHours**
> QuietHoursResponse AlertsGetQuietHours ()

Get the current quiet hours configuration for the tenant.

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
    public class AlertsGetQuietHoursExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertsApi(httpClient, config, httpClientHandler);

            try
            {
                // Get the current quiet hours configuration for the tenant.
                QuietHoursResponse result = apiInstance.AlertsGetQuietHours();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertsApi.AlertsGetQuietHours: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertsGetQuietHoursWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get the current quiet hours configuration for the tenant.
    ApiResponse<QuietHoursResponse> response = apiInstance.AlertsGetQuietHoursWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertsApi.AlertsGetQuietHoursWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**QuietHoursResponse**](QuietHoursResponse.md)

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

<a id="alertsmarkdelivered"></a>
# **AlertsMarkDelivered**
> void AlertsMarkDelivered (string deliveryId, MarkDeliveredRequest markDeliveredRequest)

Mark a delivery as successfully sent by the channel adapter.

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
    public class AlertsMarkDeliveredExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertsApi(httpClient, config, httpClientHandler);
            var deliveryId = "deliveryId_example";  // string | 
            var markDeliveredRequest = new MarkDeliveredRequest(); // MarkDeliveredRequest | 

            try
            {
                // Mark a delivery as successfully sent by the channel adapter.
                apiInstance.AlertsMarkDelivered(deliveryId, markDeliveredRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertsApi.AlertsMarkDelivered: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertsMarkDeliveredWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Mark a delivery as successfully sent by the channel adapter.
    apiInstance.AlertsMarkDeliveredWithHttpInfo(deliveryId, markDeliveredRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertsApi.AlertsMarkDeliveredWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **deliveryId** | **string** |  |  |
| **markDeliveredRequest** | [**MarkDeliveredRequest**](MarkDeliveredRequest.md) |  |  |

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
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="alertsmarkfailed"></a>
# **AlertsMarkFailed**
> void AlertsMarkFailed (string deliveryId, MarkFailedRequest markFailedRequest)

Mark a delivery as failed by the channel adapter.

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
    public class AlertsMarkFailedExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertsApi(httpClient, config, httpClientHandler);
            var deliveryId = "deliveryId_example";  // string | 
            var markFailedRequest = new MarkFailedRequest(); // MarkFailedRequest | 

            try
            {
                // Mark a delivery as failed by the channel adapter.
                apiInstance.AlertsMarkFailed(deliveryId, markFailedRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertsApi.AlertsMarkFailed: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertsMarkFailedWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Mark a delivery as failed by the channel adapter.
    apiInstance.AlertsMarkFailedWithHttpInfo(deliveryId, markFailedRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertsApi.AlertsMarkFailedWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **deliveryId** | **string** |  |  |
| **markFailedRequest** | [**MarkFailedRequest**](MarkFailedRequest.md) |  |  |

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
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="alertssnoozeinstance"></a>
# **AlertsSnoozeInstance**
> void AlertsSnoozeInstance (string instanceId, SnoozeRequest snoozeRequest)

Snooze an alert instance for the specified duration.

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
    public class AlertsSnoozeInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertsApi(httpClient, config, httpClientHandler);
            var instanceId = "instanceId_example";  // string | 
            var snoozeRequest = new SnoozeRequest(); // SnoozeRequest | 

            try
            {
                // Snooze an alert instance for the specified duration.
                apiInstance.AlertsSnoozeInstance(instanceId, snoozeRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertsApi.AlertsSnoozeInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertsSnoozeInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Snooze an alert instance for the specified duration.
    apiInstance.AlertsSnoozeInstanceWithHttpInfo(instanceId, snoozeRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertsApi.AlertsSnoozeInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** |  |  |
| **snoozeRequest** | [**SnoozeRequest**](SnoozeRequest.md) |  |  |

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
| **204** |  |  -  |
| **400** |  |  -  |
| **404** |  |  -  |
| **409** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="alertsupdatequiethours"></a>
# **AlertsUpdateQuietHours**
> void AlertsUpdateQuietHours (UpdateQuietHoursRequest updateQuietHoursRequest)

Update quiet hours configuration for the tenant.

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
    public class AlertsUpdateQuietHoursExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertsApi(httpClient, config, httpClientHandler);
            var updateQuietHoursRequest = new UpdateQuietHoursRequest(); // UpdateQuietHoursRequest | 

            try
            {
                // Update quiet hours configuration for the tenant.
                apiInstance.AlertsUpdateQuietHours(updateQuietHoursRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertsApi.AlertsUpdateQuietHours: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertsUpdateQuietHoursWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update quiet hours configuration for the tenant.
    apiInstance.AlertsUpdateQuietHoursWithHttpInfo(updateQuietHoursRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertsApi.AlertsUpdateQuietHoursWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **updateQuietHoursRequest** | [**UpdateQuietHoursRequest**](UpdateQuietHoursRequest.md) |  |  |

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
| **204** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

