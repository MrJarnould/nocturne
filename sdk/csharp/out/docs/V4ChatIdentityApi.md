# NightscoutFoundation.Nocturne.Api.V4ChatIdentityApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ChatIdentityCreateLink**](V4ChatIdentityApi.md#chatidentitycreatelink) | **POST** /api/v4/chat-identity | Create a new chat identity link. |
| [**ChatIdentityGetLinks**](V4ChatIdentityApi.md#chatidentitygetlinks) | **GET** /api/v4/chat-identity | List active chat identity links for the current tenant. |
| [**ChatIdentityResolve**](V4ChatIdentityApi.md#chatidentityresolve) | **GET** /api/v4/chat-identity/resolve | Resolve a platform identity to a Nocturne user. Used by the bot service to look up which tenant/user a chat message belongs to. |
| [**ChatIdentityRevokeLink**](V4ChatIdentityApi.md#chatidentityrevokelink) | **DELETE** /api/v4/chat-identity/{id} | Revoke (soft-delete) a chat identity link. |

<a id="chatidentitycreatelink"></a>
# **ChatIdentityCreateLink**
> ChatIdentityLinkResponse ChatIdentityCreateLink (CreateChatIdentityLinkRequest createChatIdentityLinkRequest)

Create a new chat identity link.

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
    public class ChatIdentityCreateLinkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ChatIdentityApi(httpClient, config, httpClientHandler);
            var createChatIdentityLinkRequest = new CreateChatIdentityLinkRequest(); // CreateChatIdentityLinkRequest | 

            try
            {
                // Create a new chat identity link.
                ChatIdentityLinkResponse result = apiInstance.ChatIdentityCreateLink(createChatIdentityLinkRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ChatIdentityApi.ChatIdentityCreateLink: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityCreateLinkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a new chat identity link.
    ApiResponse<ChatIdentityLinkResponse> response = apiInstance.ChatIdentityCreateLinkWithHttpInfo(createChatIdentityLinkRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ChatIdentityApi.ChatIdentityCreateLinkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createChatIdentityLinkRequest** | [**CreateChatIdentityLinkRequest**](CreateChatIdentityLinkRequest.md) |  |  |

### Return type

[**ChatIdentityLinkResponse**](ChatIdentityLinkResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="chatidentitygetlinks"></a>
# **ChatIdentityGetLinks**
> List&lt;ChatIdentityLinkResponse&gt; ChatIdentityGetLinks ()

List active chat identity links for the current tenant.

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
    public class ChatIdentityGetLinksExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ChatIdentityApi(httpClient, config, httpClientHandler);

            try
            {
                // List active chat identity links for the current tenant.
                List<ChatIdentityLinkResponse> result = apiInstance.ChatIdentityGetLinks();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ChatIdentityApi.ChatIdentityGetLinks: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityGetLinksWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List active chat identity links for the current tenant.
    ApiResponse<List<ChatIdentityLinkResponse>> response = apiInstance.ChatIdentityGetLinksWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ChatIdentityApi.ChatIdentityGetLinksWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;ChatIdentityLinkResponse&gt;**](ChatIdentityLinkResponse.md)

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

<a id="chatidentityresolve"></a>
# **ChatIdentityResolve**
> ChatIdentityLinkResponse ChatIdentityResolve (string? platform = null, string? platformUserId = null)

Resolve a platform identity to a Nocturne user. Used by the bot service to look up which tenant/user a chat message belongs to.

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
    public class ChatIdentityResolveExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ChatIdentityApi(httpClient, config, httpClientHandler);
            var platform = "platform_example";  // string? |  (optional) 
            var platformUserId = "platformUserId_example";  // string? |  (optional) 

            try
            {
                // Resolve a platform identity to a Nocturne user. Used by the bot service to look up which tenant/user a chat message belongs to.
                ChatIdentityLinkResponse result = apiInstance.ChatIdentityResolve(platform, platformUserId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ChatIdentityApi.ChatIdentityResolve: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityResolveWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Resolve a platform identity to a Nocturne user. Used by the bot service to look up which tenant/user a chat message belongs to.
    ApiResponse<ChatIdentityLinkResponse> response = apiInstance.ChatIdentityResolveWithHttpInfo(platform, platformUserId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ChatIdentityApi.ChatIdentityResolveWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **platform** | **string?** |  | [optional]  |
| **platformUserId** | **string?** |  | [optional]  |

### Return type

[**ChatIdentityLinkResponse**](ChatIdentityLinkResponse.md)

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

<a id="chatidentityrevokelink"></a>
# **ChatIdentityRevokeLink**
> void ChatIdentityRevokeLink (string id)

Revoke (soft-delete) a chat identity link.

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
    public class ChatIdentityRevokeLinkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ChatIdentityApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Revoke (soft-delete) a chat identity link.
                apiInstance.ChatIdentityRevokeLink(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ChatIdentityApi.ChatIdentityRevokeLink: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityRevokeLinkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Revoke (soft-delete) a chat identity link.
    apiInstance.ChatIdentityRevokeLinkWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ChatIdentityApi.ChatIdentityRevokeLinkWithHttpInfo: " + e.Message);
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
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

