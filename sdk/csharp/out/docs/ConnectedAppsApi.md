# NightscoutFoundation.Nocturne.Api.ConnectedAppsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ConnectedAppsList**](ConnectedAppsApi.md#connectedappslist) | **GET** /api/v4/account/connected-apps | List all connected apps (OAuth app grants) for the authenticated user on the current tenant. |
| [**ConnectedAppsRevoke**](ConnectedAppsApi.md#connectedappsrevoke) | **DELETE** /api/v4/account/connected-apps/{grantId} | Revoke a connected app. Soft-deletes the grant and invalidates all associated refresh tokens; previously-issued access tokens become unusable on next request via the revocation cache. |

<a id="connectedappslist"></a>
# **ConnectedAppsList**
> List&lt;ConnectedAppDto&gt; ConnectedAppsList ()

List all connected apps (OAuth app grants) for the authenticated user on the current tenant.

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
    public class ConnectedAppsListExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConnectedAppsApi(httpClient, config, httpClientHandler);

            try
            {
                // List all connected apps (OAuth app grants) for the authenticated user on the current tenant.
                List<ConnectedAppDto> result = apiInstance.ConnectedAppsList();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConnectedAppsApi.ConnectedAppsList: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConnectedAppsListWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List all connected apps (OAuth app grants) for the authenticated user on the current tenant.
    ApiResponse<List<ConnectedAppDto>> response = apiInstance.ConnectedAppsListWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConnectedAppsApi.ConnectedAppsListWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;ConnectedAppDto&gt;**](ConnectedAppDto.md)

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

<a id="connectedappsrevoke"></a>
# **ConnectedAppsRevoke**
> void ConnectedAppsRevoke (string grantId)

Revoke a connected app. Soft-deletes the grant and invalidates all associated refresh tokens; previously-issued access tokens become unusable on next request via the revocation cache.

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
    public class ConnectedAppsRevokeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConnectedAppsApi(httpClient, config, httpClientHandler);
            var grantId = "grantId_example";  // string | 

            try
            {
                // Revoke a connected app. Soft-deletes the grant and invalidates all associated refresh tokens; previously-issued access tokens become unusable on next request via the revocation cache.
                apiInstance.ConnectedAppsRevoke(grantId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConnectedAppsApi.ConnectedAppsRevoke: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConnectedAppsRevokeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Revoke a connected app. Soft-deletes the grant and invalidates all associated refresh tokens; previously-issued access tokens become unusable on next request via the revocation cache.
    apiInstance.ConnectedAppsRevokeWithHttpInfo(grantId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConnectedAppsApi.ConnectedAppsRevokeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **grantId** | **string** |  |  |

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

