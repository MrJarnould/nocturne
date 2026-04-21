# NightscoutFoundation.Nocturne.Api.V4NotificationsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**NotificationsDismissNotification**](V4NotificationsApi.md#notificationsdismissnotification) | **DELETE** /api/v4/notifications/{id} | Dismiss a notification (archive with dismissed reason) |
| [**NotificationsExecuteAction**](V4NotificationsApi.md#notificationsexecuteaction) | **POST** /api/v4/notifications/{id}/actions/{actionId} | Execute an action on a notification |
| [**NotificationsGetNotifications**](V4NotificationsApi.md#notificationsgetnotifications) | **GET** /api/v4/notifications | Get all active notifications for the current user |

<a id="notificationsdismissnotification"></a>
# **NotificationsDismissNotification**
> void NotificationsDismissNotification (string id)

Dismiss a notification (archive with dismissed reason)

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
    public class NotificationsDismissNotificationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NotificationsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The notification ID to dismiss

            try
            {
                // Dismiss a notification (archive with dismissed reason)
                apiInstance.NotificationsDismissNotification(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NotificationsApi.NotificationsDismissNotification: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NotificationsDismissNotificationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Dismiss a notification (archive with dismissed reason)
    apiInstance.NotificationsDismissNotificationWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NotificationsApi.NotificationsDismissNotificationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The notification ID to dismiss |  |

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
| **204** | No content if successful |  -  |
| **401** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="notificationsexecuteaction"></a>
# **NotificationsExecuteAction**
> void NotificationsExecuteAction (string id, string actionId)

Execute an action on a notification

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
    public class NotificationsExecuteActionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NotificationsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The notification ID
            var actionId = "actionId_example";  // string | The action ID to execute

            try
            {
                // Execute an action on a notification
                apiInstance.NotificationsExecuteAction(id, actionId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NotificationsApi.NotificationsExecuteAction: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NotificationsExecuteActionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Execute an action on a notification
    apiInstance.NotificationsExecuteActionWithHttpInfo(id, actionId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NotificationsApi.NotificationsExecuteActionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The notification ID |  |
| **actionId** | **string** | The action ID to execute |  |

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
| **204** | No content if successful |  -  |
| **401** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="notificationsgetnotifications"></a>
# **NotificationsGetNotifications**
> List&lt;InAppNotificationDto&gt; NotificationsGetNotifications ()

Get all active notifications for the current user

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
    public class NotificationsGetNotificationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NotificationsApi(httpClient, config, httpClientHandler);

            try
            {
                // Get all active notifications for the current user
                List<InAppNotificationDto> result = apiInstance.NotificationsGetNotifications();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NotificationsApi.NotificationsGetNotifications: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NotificationsGetNotificationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all active notifications for the current user
    ApiResponse<List<InAppNotificationDto>> response = apiInstance.NotificationsGetNotificationsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NotificationsApi.NotificationsGetNotificationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;InAppNotificationDto&gt;**](InAppNotificationDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of active notifications |  -  |
| **401** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

