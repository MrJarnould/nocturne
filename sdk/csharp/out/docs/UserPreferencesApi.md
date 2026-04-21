# NightscoutFoundation.Nocturne.Api.UserPreferencesApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**UserPreferencesGetPreferences**](UserPreferencesApi.md#userpreferencesgetpreferences) | **GET** /api/v4/user/preferences | Get the current user&#39;s preferences |
| [**UserPreferencesUpdatePreferences**](UserPreferencesApi.md#userpreferencesupdatepreferences) | **PATCH** /api/v4/user/preferences | Update the current user&#39;s preferences |

<a id="userpreferencesgetpreferences"></a>
# **UserPreferencesGetPreferences**
> UserPreferencesResponse UserPreferencesGetPreferences ()

Get the current user's preferences

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
    public class UserPreferencesGetPreferencesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UserPreferencesApi(httpClient, config, httpClientHandler);

            try
            {
                // Get the current user's preferences
                UserPreferencesResponse result = apiInstance.UserPreferencesGetPreferences();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UserPreferencesApi.UserPreferencesGetPreferences: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UserPreferencesGetPreferencesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get the current user's preferences
    ApiResponse<UserPreferencesResponse> response = apiInstance.UserPreferencesGetPreferencesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UserPreferencesApi.UserPreferencesGetPreferencesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**UserPreferencesResponse**](UserPreferencesResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User preferences |  -  |
| **401** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="userpreferencesupdatepreferences"></a>
# **UserPreferencesUpdatePreferences**
> UserPreferencesResponse UserPreferencesUpdatePreferences (UpdateUserPreferencesRequest updateUserPreferencesRequest)

Update the current user's preferences

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
    public class UserPreferencesUpdatePreferencesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UserPreferencesApi(httpClient, config, httpClientHandler);
            var updateUserPreferencesRequest = new UpdateUserPreferencesRequest(); // UpdateUserPreferencesRequest | The preferences to update

            try
            {
                // Update the current user's preferences
                UserPreferencesResponse result = apiInstance.UserPreferencesUpdatePreferences(updateUserPreferencesRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UserPreferencesApi.UserPreferencesUpdatePreferences: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UserPreferencesUpdatePreferencesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update the current user's preferences
    ApiResponse<UserPreferencesResponse> response = apiInstance.UserPreferencesUpdatePreferencesWithHttpInfo(updateUserPreferencesRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UserPreferencesApi.UserPreferencesUpdatePreferencesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **updateUserPreferencesRequest** | [**UpdateUserPreferencesRequest**](UpdateUserPreferencesRequest.md) | The preferences to update |  |

### Return type

[**UserPreferencesResponse**](UserPreferencesResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Updated preferences |  -  |
| **400** |  |  -  |
| **401** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

