# NightscoutFoundation.Nocturne.Api.ChatIdentityDirectoryApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ChatIdentityDirectoryCreatePending**](ChatIdentityDirectoryApi.md#chatidentitydirectorycreatepending) | **POST** /api/v4/chat-identity/directory/pending-links |  |
| [**ChatIdentityDirectoryResolve**](ChatIdentityDirectoryApi.md#chatidentitydirectoryresolve) | **GET** /api/v4/chat-identity/directory/resolve | Returns ALL directory candidates for a (platform, platformUserId). Caller is responsible for label disambiguation. Each candidate includes the tenantSlug (joined from tenants table). |
| [**ChatIdentityDirectoryRevokeByPlatformUser**](ChatIdentityDirectoryApi.md#chatidentitydirectoryrevokebyplatformuser) | **DELETE** /api/v4/chat-identity/directory/links/{id} | Revoke a link by id, verifying the (platform, platformUserId) on the row matches the body. Used by /disconnect from the bot. |

<a id="chatidentitydirectorycreatepending"></a>
# **ChatIdentityDirectoryCreatePending**
> PendingLinkResponse ChatIdentityDirectoryCreatePending (CreatePendingLinkRequest createPendingLinkRequest)



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
    public class ChatIdentityDirectoryCreatePendingExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ChatIdentityDirectoryApi(httpClient, config, httpClientHandler);
            var createPendingLinkRequest = new CreatePendingLinkRequest(); // CreatePendingLinkRequest | 

            try
            {
                PendingLinkResponse result = apiInstance.ChatIdentityDirectoryCreatePending(createPendingLinkRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChatIdentityDirectoryApi.ChatIdentityDirectoryCreatePending: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityDirectoryCreatePendingWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<PendingLinkResponse> response = apiInstance.ChatIdentityDirectoryCreatePendingWithHttpInfo(createPendingLinkRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ChatIdentityDirectoryApi.ChatIdentityDirectoryCreatePendingWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createPendingLinkRequest** | [**CreatePendingLinkRequest**](CreatePendingLinkRequest.md) |  |  |

### Return type

[**PendingLinkResponse**](PendingLinkResponse.md)

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

<a id="chatidentitydirectoryresolve"></a>
# **ChatIdentityDirectoryResolve**
> DirectoryCandidatesResponse ChatIdentityDirectoryResolve (string? platform = null, string? platformUserId = null)

Returns ALL directory candidates for a (platform, platformUserId). Caller is responsible for label disambiguation. Each candidate includes the tenantSlug (joined from tenants table).

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
    public class ChatIdentityDirectoryResolveExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ChatIdentityDirectoryApi(httpClient, config, httpClientHandler);
            var platform = "platform_example";  // string? | Chat platform identifier (e.g., \"discord\", \"telegram\"). (optional) 
            var platformUserId = "platformUserId_example";  // string? | Unique user identifier on the specified platform. (optional) 

            try
            {
                // Returns ALL directory candidates for a (platform, platformUserId). Caller is responsible for label disambiguation. Each candidate includes the tenantSlug (joined from tenants table).
                DirectoryCandidatesResponse result = apiInstance.ChatIdentityDirectoryResolve(platform, platformUserId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChatIdentityDirectoryApi.ChatIdentityDirectoryResolve: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityDirectoryResolveWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Returns ALL directory candidates for a (platform, platformUserId). Caller is responsible for label disambiguation. Each candidate includes the tenantSlug (joined from tenants table).
    ApiResponse<DirectoryCandidatesResponse> response = apiInstance.ChatIdentityDirectoryResolveWithHttpInfo(platform, platformUserId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ChatIdentityDirectoryApi.ChatIdentityDirectoryResolveWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **platform** | **string?** | Chat platform identifier (e.g., \&quot;discord\&quot;, \&quot;telegram\&quot;). | [optional]  |
| **platformUserId** | **string?** | Unique user identifier on the specified platform. | [optional]  |

### Return type

[**DirectoryCandidatesResponse**](DirectoryCandidatesResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | DirectoryCandidatesResponse with all matching tenant candidates, or 404 if no candidates are found. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="chatidentitydirectoryrevokebyplatformuser"></a>
# **ChatIdentityDirectoryRevokeByPlatformUser**
> FileParameter ChatIdentityDirectoryRevokeByPlatformUser (string id, RevokeByPlatformUserRequest revokeByPlatformUserRequest)

Revoke a link by id, verifying the (platform, platformUserId) on the row matches the body. Used by /disconnect from the bot.

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
    public class ChatIdentityDirectoryRevokeByPlatformUserExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ChatIdentityDirectoryApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var revokeByPlatformUserRequest = new RevokeByPlatformUserRequest(); // RevokeByPlatformUserRequest | 

            try
            {
                // Revoke a link by id, verifying the (platform, platformUserId) on the row matches the body. Used by /disconnect from the bot.
                FileParameter result = apiInstance.ChatIdentityDirectoryRevokeByPlatformUser(id, revokeByPlatformUserRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChatIdentityDirectoryApi.ChatIdentityDirectoryRevokeByPlatformUser: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityDirectoryRevokeByPlatformUserWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Revoke a link by id, verifying the (platform, platformUserId) on the row matches the body. Used by /disconnect from the bot.
    ApiResponse<FileParameter> response = apiInstance.ChatIdentityDirectoryRevokeByPlatformUserWithHttpInfo(id, revokeByPlatformUserRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ChatIdentityDirectoryApi.ChatIdentityDirectoryRevokeByPlatformUserWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **revokeByPlatformUserRequest** | [**RevokeByPlatformUserRequest**](RevokeByPlatformUserRequest.md) |  |  |

### Return type

[**FileParameter**](FileParameter.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

