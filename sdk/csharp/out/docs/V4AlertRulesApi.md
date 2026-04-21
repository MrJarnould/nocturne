# NightscoutFoundation.Nocturne.Api.V4AlertRulesApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AlertRulesCreateRule**](V4AlertRulesApi.md#alertrulescreaterule) | **POST** /api/v4/alert-rules | Create an alert rule with nested schedules, escalation steps, and channels. |
| [**AlertRulesDeleteRule**](V4AlertRulesApi.md#alertrulesdeleterule) | **DELETE** /api/v4/alert-rules/{id} | Delete an alert rule (cascades to schedules, steps, channels). |
| [**AlertRulesGetRule**](V4AlertRulesApi.md#alertrulesgetrule) | **GET** /api/v4/alert-rules/{id} | Get a single alert rule with full schedule/escalation tree. |
| [**AlertRulesGetRules**](V4AlertRulesApi.md#alertrulesgetrules) | **GET** /api/v4/alert-rules | List all alert rules for the current tenant with schedules and escalation steps. |
| [**AlertRulesToggleRule**](V4AlertRulesApi.md#alertrulestogglerule) | **PATCH** /api/v4/alert-rules/{id}/toggle | Toggle an alert rule enabled/disabled. |
| [**AlertRulesUpdateRule**](V4AlertRulesApi.md#alertrulesupdaterule) | **PUT** /api/v4/alert-rules/{id} | Update an alert rule. |

<a id="alertrulescreaterule"></a>
# **AlertRulesCreateRule**
> AlertRuleResponse AlertRulesCreateRule (CreateAlertRuleRequest createAlertRuleRequest)

Create an alert rule with nested schedules, escalation steps, and channels.

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
    public class AlertRulesCreateRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertRulesApi(httpClient, config, httpClientHandler);
            var createAlertRuleRequest = new CreateAlertRuleRequest(); // CreateAlertRuleRequest | 

            try
            {
                // Create an alert rule with nested schedules, escalation steps, and channels.
                AlertRuleResponse result = apiInstance.AlertRulesCreateRule(createAlertRuleRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesCreateRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertRulesCreateRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create an alert rule with nested schedules, escalation steps, and channels.
    ApiResponse<AlertRuleResponse> response = apiInstance.AlertRulesCreateRuleWithHttpInfo(createAlertRuleRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesCreateRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createAlertRuleRequest** | [**CreateAlertRuleRequest**](CreateAlertRuleRequest.md) |  |  |

### Return type

[**AlertRuleResponse**](AlertRuleResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="alertrulesdeleterule"></a>
# **AlertRulesDeleteRule**
> void AlertRulesDeleteRule (string id)

Delete an alert rule (cascades to schedules, steps, channels).

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
    public class AlertRulesDeleteRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertRulesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete an alert rule (cascades to schedules, steps, channels).
                apiInstance.AlertRulesDeleteRule(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesDeleteRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertRulesDeleteRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete an alert rule (cascades to schedules, steps, channels).
    apiInstance.AlertRulesDeleteRuleWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesDeleteRuleWithHttpInfo: " + e.Message);
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

<a id="alertrulesgetrule"></a>
# **AlertRulesGetRule**
> AlertRuleResponse AlertRulesGetRule (string id)

Get a single alert rule with full schedule/escalation tree.

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
    public class AlertRulesGetRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertRulesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Get a single alert rule with full schedule/escalation tree.
                AlertRuleResponse result = apiInstance.AlertRulesGetRule(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesGetRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertRulesGetRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a single alert rule with full schedule/escalation tree.
    ApiResponse<AlertRuleResponse> response = apiInstance.AlertRulesGetRuleWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesGetRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**AlertRuleResponse**](AlertRuleResponse.md)

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="alertrulesgetrules"></a>
# **AlertRulesGetRules**
> List&lt;AlertRuleResponse&gt; AlertRulesGetRules ()

List all alert rules for the current tenant with schedules and escalation steps.

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
    public class AlertRulesGetRulesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertRulesApi(httpClient, config, httpClientHandler);

            try
            {
                // List all alert rules for the current tenant with schedules and escalation steps.
                List<AlertRuleResponse> result = apiInstance.AlertRulesGetRules();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesGetRules: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertRulesGetRulesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List all alert rules for the current tenant with schedules and escalation steps.
    ApiResponse<List<AlertRuleResponse>> response = apiInstance.AlertRulesGetRulesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesGetRulesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;AlertRuleResponse&gt;**](AlertRuleResponse.md)

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

<a id="alertrulestogglerule"></a>
# **AlertRulesToggleRule**
> AlertRuleResponse AlertRulesToggleRule (string id)

Toggle an alert rule enabled/disabled.

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
    public class AlertRulesToggleRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertRulesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Toggle an alert rule enabled/disabled.
                AlertRuleResponse result = apiInstance.AlertRulesToggleRule(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesToggleRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertRulesToggleRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Toggle an alert rule enabled/disabled.
    ApiResponse<AlertRuleResponse> response = apiInstance.AlertRulesToggleRuleWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesToggleRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**AlertRuleResponse**](AlertRuleResponse.md)

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="alertrulesupdaterule"></a>
# **AlertRulesUpdateRule**
> AlertRuleResponse AlertRulesUpdateRule (string id, UpdateAlertRuleRequest updateAlertRuleRequest)

Update an alert rule.

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
    public class AlertRulesUpdateRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertRulesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var updateAlertRuleRequest = new UpdateAlertRuleRequest(); // UpdateAlertRuleRequest | 

            try
            {
                // Update an alert rule.
                AlertRuleResponse result = apiInstance.AlertRulesUpdateRule(id, updateAlertRuleRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesUpdateRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertRulesUpdateRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update an alert rule.
    ApiResponse<AlertRuleResponse> response = apiInstance.AlertRulesUpdateRuleWithHttpInfo(id, updateAlertRuleRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertRulesApi.AlertRulesUpdateRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **updateAlertRuleRequest** | [**UpdateAlertRuleRequest**](UpdateAlertRuleRequest.md) |  |  |

### Return type

[**AlertRuleResponse**](AlertRuleResponse.md)

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

