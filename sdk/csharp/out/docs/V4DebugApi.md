# NightscoutFoundation.Nocturne.Api.V4DebugApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DebugCreateSimpleTestNotification**](V4DebugApi.md#debugcreatesimpletestnotification) | **GET** /api/v4/debug/test/notification | Simple test endpoint for creating in-app notifications without authentication Creates a test notification to verify the real-time notification system is working |
| [**DebugCreateTestNotification**](V4DebugApi.md#debugcreatetestnotification) | **POST** /api/v4/debug/test/inappnotification | Test endpoint for creating in-app notifications (development only) Creates a test notification for the current user to verify the notification system |
| [**DebugEchoQuery**](V4DebugApi.md#debugechoquery) | **GET** /api/v4/debug/echo/{echo} | Echo endpoint for debugging MongoDB queries Returns information about how REST API parameters translate into MongoDB queries |
| [**DebugEchoQueryWithModel**](V4DebugApi.md#debugechoquerywithmodel) | **GET** /api/v4/debug/echo/{echo}/{model} | Echo endpoint for debugging MongoDB queries with model Returns information about how REST API parameters translate into MongoDB queries |
| [**DebugEchoQueryWithModelAndSpec**](V4DebugApi.md#debugechoquerywithmodelandspec) | **GET** /api/v4/debug/echo/{echo}/{model}/{spec} | Echo endpoint for debugging MongoDB queries with model and spec Returns information about how REST API parameters translate into MongoDB queries |
| [**DebugPreviewEntries**](V4DebugApi.md#debugpreviewentries) | **POST** /api/v4/debug/entries/preview | Preview endpoint for entry creation without persistence Allows previewing entry data without actually storing it in the database |
| [**DebugTestSignalRBroadcast**](V4DebugApi.md#debugtestsignalrbroadcast) | **GET** /api/v4/debug/test/signalr-broadcast | Test endpoint to broadcast a raw SignalR notification event This bypasses the database and directly tests the SignalR broadcast |

<a id="debugcreatesimpletestnotification"></a>
# **DebugCreateSimpleTestNotification**
> InAppNotificationDto DebugCreateSimpleTestNotification (string? type = null, string? title = null)

Simple test endpoint for creating in-app notifications without authentication Creates a test notification to verify the real-time notification system is working

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
    public class DebugCreateSimpleTestNotificationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DebugApi(httpClient, config, httpClientHandler);
            var type = "\"info\"";  // string? | Notification type (info, warn, hazard, urgent) (optional)  (default to "info")
            var title = "title_example";  // string? | Optional notification title (optional) 

            try
            {
                // Simple test endpoint for creating in-app notifications without authentication Creates a test notification to verify the real-time notification system is working
                InAppNotificationDto result = apiInstance.DebugCreateSimpleTestNotification(type, title);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DebugApi.DebugCreateSimpleTestNotification: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DebugCreateSimpleTestNotificationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Simple test endpoint for creating in-app notifications without authentication Creates a test notification to verify the real-time notification system is working
    ApiResponse<InAppNotificationDto> response = apiInstance.DebugCreateSimpleTestNotificationWithHttpInfo(type, title);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DebugApi.DebugCreateSimpleTestNotificationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **type** | **string?** | Notification type (info, warn, hazard, urgent) | [optional] [default to &quot;info&quot;] |
| **title** | **string?** | Optional notification title | [optional]  |

### Return type

[**InAppNotificationDto**](InAppNotificationDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Notification created and broadcast successfully |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="debugcreatetestnotification"></a>
# **DebugCreateTestNotification**
> InAppNotificationDto DebugCreateTestNotification (TestNotificationRequest testNotificationRequest)

Test endpoint for creating in-app notifications (development only) Creates a test notification for the current user to verify the notification system

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
    public class DebugCreateTestNotificationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DebugApi(httpClient, config, httpClientHandler);
            var testNotificationRequest = new TestNotificationRequest(); // TestNotificationRequest | The test notification parameters

            try
            {
                // Test endpoint for creating in-app notifications (development only) Creates a test notification for the current user to verify the notification system
                InAppNotificationDto result = apiInstance.DebugCreateTestNotification(testNotificationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DebugApi.DebugCreateTestNotification: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DebugCreateTestNotificationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Test endpoint for creating in-app notifications (development only) Creates a test notification for the current user to verify the notification system
    ApiResponse<InAppNotificationDto> response = apiInstance.DebugCreateTestNotificationWithHttpInfo(testNotificationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DebugApi.DebugCreateTestNotificationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **testNotificationRequest** | [**TestNotificationRequest**](TestNotificationRequest.md) | The test notification parameters |  |

### Return type

[**InAppNotificationDto**](InAppNotificationDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Notification created successfully |  -  |
| **400** | Invalid request parameters |  -  |
| **401** | User not authenticated |  -  |
| **403** | Endpoint only available in development |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="debugechoquery"></a>
# **DebugEchoQuery**
> Object DebugEchoQuery (string echo)

Echo endpoint for debugging MongoDB queries Returns information about how REST API parameters translate into MongoDB queries

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
    public class DebugEchoQueryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DebugApi(httpClient, config, httpClientHandler);
            var echo = "echo_example";  // string | Storage type to query (entries, treatments, devicestatus, activity)

            try
            {
                // Echo endpoint for debugging MongoDB queries Returns information about how REST API parameters translate into MongoDB queries
                Object result = apiInstance.DebugEchoQuery(echo);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DebugApi.DebugEchoQuery: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DebugEchoQueryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Echo endpoint for debugging MongoDB queries Returns information about how REST API parameters translate into MongoDB queries
    ApiResponse<Object> response = apiInstance.DebugEchoQueryWithHttpInfo(echo);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DebugApi.DebugEchoQueryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **echo** | **string** | Storage type to query (entries, treatments, devicestatus, activity) |  |

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Query information returned successfully |  -  |
| **400** | Invalid parameters |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="debugechoquerywithmodel"></a>
# **DebugEchoQueryWithModel**
> Object DebugEchoQueryWithModel (string echo, string model)

Echo endpoint for debugging MongoDB queries with model Returns information about how REST API parameters translate into MongoDB queries

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
    public class DebugEchoQueryWithModelExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DebugApi(httpClient, config, httpClientHandler);
            var echo = "echo_example";  // string | Storage type to query (entries, treatments, devicestatus, activity)
            var model = "model_example";  // string | Model specification

            try
            {
                // Echo endpoint for debugging MongoDB queries with model Returns information about how REST API parameters translate into MongoDB queries
                Object result = apiInstance.DebugEchoQueryWithModel(echo, model);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DebugApi.DebugEchoQueryWithModel: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DebugEchoQueryWithModelWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Echo endpoint for debugging MongoDB queries with model Returns information about how REST API parameters translate into MongoDB queries
    ApiResponse<Object> response = apiInstance.DebugEchoQueryWithModelWithHttpInfo(echo, model);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DebugApi.DebugEchoQueryWithModelWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **echo** | **string** | Storage type to query (entries, treatments, devicestatus, activity) |  |
| **model** | **string** | Model specification |  |

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Query information returned successfully |  -  |
| **400** | Invalid parameters |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="debugechoquerywithmodelandspec"></a>
# **DebugEchoQueryWithModelAndSpec**
> Object DebugEchoQueryWithModelAndSpec (string echo, string model, string spec)

Echo endpoint for debugging MongoDB queries with model and spec Returns information about how REST API parameters translate into MongoDB queries

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
    public class DebugEchoQueryWithModelAndSpecExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DebugApi(httpClient, config, httpClientHandler);
            var echo = "echo_example";  // string | Storage type to query (entries, treatments, devicestatus, activity)
            var model = "model_example";  // string | Model specification
            var spec = "spec_example";  // string | Specification parameter

            try
            {
                // Echo endpoint for debugging MongoDB queries with model and spec Returns information about how REST API parameters translate into MongoDB queries
                Object result = apiInstance.DebugEchoQueryWithModelAndSpec(echo, model, spec);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DebugApi.DebugEchoQueryWithModelAndSpec: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DebugEchoQueryWithModelAndSpecWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Echo endpoint for debugging MongoDB queries with model and spec Returns information about how REST API parameters translate into MongoDB queries
    ApiResponse<Object> response = apiInstance.DebugEchoQueryWithModelAndSpecWithHttpInfo(echo, model, spec);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DebugApi.DebugEchoQueryWithModelAndSpecWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **echo** | **string** | Storage type to query (entries, treatments, devicestatus, activity) |  |
| **model** | **string** | Model specification |  |
| **spec** | **string** | Specification parameter |  |

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Query information returned successfully |  -  |
| **400** | Invalid parameters |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="debugpreviewentries"></a>
# **DebugPreviewEntries**
> Object DebugPreviewEntries (Object body)

Preview endpoint for entry creation without persistence Allows previewing entry data without actually storing it in the database

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
    public class DebugPreviewEntriesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DebugApi(httpClient, config, httpClientHandler);
            var body = null;  // Object | Entry data to preview (single object or array)

            try
            {
                // Preview endpoint for entry creation without persistence Allows previewing entry data without actually storing it in the database
                Object result = apiInstance.DebugPreviewEntries(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DebugApi.DebugPreviewEntries: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DebugPreviewEntriesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Preview endpoint for entry creation without persistence Allows previewing entry data without actually storing it in the database
    ApiResponse<Object> response = apiInstance.DebugPreviewEntriesWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DebugApi.DebugPreviewEntriesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | **Object** | Entry data to preview (single object or array) |  |

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Entry data previewed successfully |  -  |
| **400** | Invalid entry data |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="debugtestsignalrbroadcast"></a>
# **DebugTestSignalRBroadcast**
> Object DebugTestSignalRBroadcast ()

Test endpoint to broadcast a raw SignalR notification event This bypasses the database and directly tests the SignalR broadcast

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
    public class DebugTestSignalRBroadcastExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4DebugApi(httpClient, config, httpClientHandler);

            try
            {
                // Test endpoint to broadcast a raw SignalR notification event This bypasses the database and directly tests the SignalR broadcast
                Object result = apiInstance.DebugTestSignalRBroadcast();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DebugApi.DebugTestSignalRBroadcast: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DebugTestSignalRBroadcastWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Test endpoint to broadcast a raw SignalR notification event This bypasses the database and directly tests the SignalR broadcast
    ApiResponse<Object> response = apiInstance.DebugTestSignalRBroadcastWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DebugApi.DebugTestSignalRBroadcastWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Confirmation of broadcast |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

