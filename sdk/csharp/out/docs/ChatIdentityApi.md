# NightscoutFoundation.Nocturne.Api.ChatIdentityApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ChatIdentityClaimLink**](ChatIdentityApi.md#chatidentityclaimlink) | **POST** /api/v4/chat-identity/links/claim | Claim a pending link token after /connect slash command auth. |
| [**ChatIdentityCreateDirectLink**](ChatIdentityApi.md#chatidentitycreatedirectlink) | **POST** /api/v4/chat-identity/links/direct | Directly create a link for the current tenant (used by OAuth2 finalize hop). |
| [**ChatIdentityGetLinks**](ChatIdentityApi.md#chatidentitygetlinks) | **GET** /api/v4/chat-identity | List active chat identity links for the current tenant. |
| [**ChatIdentityGetPending**](ChatIdentityApi.md#chatidentitygetpending) | **GET** /api/v4/chat-identity/links/pending/{token} | Read-only lookup of a pending link token, used by the authorize page to validate and render the confirmation step. |
| [**ChatIdentityRevokeLink**](ChatIdentityApi.md#chatidentityrevokelink) | **DELETE** /api/v4/chat-identity/links/{id} |  |
| [**ChatIdentitySetDefault**](ChatIdentityApi.md#chatidentitysetdefault) | **POST** /api/v4/chat-identity/links/{id}/set-default |  |
| [**ChatIdentityUpdateLink**](ChatIdentityApi.md#chatidentityupdatelink) | **PATCH** /api/v4/chat-identity/links/{id} |  |

<a id="chatidentityclaimlink"></a>
# **ChatIdentityClaimLink**
> ChatIdentityLinkResponse ChatIdentityClaimLink (ClaimChatIdentityLinkRequest claimChatIdentityLinkRequest)

Claim a pending link token after /connect slash command auth.

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
    public class ChatIdentityClaimLinkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ChatIdentityApi(httpClient, config, httpClientHandler);
            var claimChatIdentityLinkRequest = new ClaimChatIdentityLinkRequest(); // ClaimChatIdentityLinkRequest | 

            try
            {
                // Claim a pending link token after /connect slash command auth.
                ChatIdentityLinkResponse result = apiInstance.ChatIdentityClaimLink(claimChatIdentityLinkRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityClaimLink: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityClaimLinkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Claim a pending link token after /connect slash command auth.
    ApiResponse<ChatIdentityLinkResponse> response = apiInstance.ChatIdentityClaimLinkWithHttpInfo(claimChatIdentityLinkRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityClaimLinkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **claimChatIdentityLinkRequest** | [**ClaimChatIdentityLinkRequest**](ClaimChatIdentityLinkRequest.md) |  |  |

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
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="chatidentitycreatedirectlink"></a>
# **ChatIdentityCreateDirectLink**
> ChatIdentityLinkResponse ChatIdentityCreateDirectLink (CreateDirectLinkRequest createDirectLinkRequest)

Directly create a link for the current tenant (used by OAuth2 finalize hop).

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
    public class ChatIdentityCreateDirectLinkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ChatIdentityApi(httpClient, config, httpClientHandler);
            var createDirectLinkRequest = new CreateDirectLinkRequest(); // CreateDirectLinkRequest | 

            try
            {
                // Directly create a link for the current tenant (used by OAuth2 finalize hop).
                ChatIdentityLinkResponse result = apiInstance.ChatIdentityCreateDirectLink(createDirectLinkRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityCreateDirectLink: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityCreateDirectLinkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Directly create a link for the current tenant (used by OAuth2 finalize hop).
    ApiResponse<ChatIdentityLinkResponse> response = apiInstance.ChatIdentityCreateDirectLinkWithHttpInfo(createDirectLinkRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityCreateDirectLinkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createDirectLinkRequest** | [**CreateDirectLinkRequest**](CreateDirectLinkRequest.md) |  |  |

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
| **200** |  |  -  |

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
            var apiInstance = new ChatIdentityApi(httpClient, config, httpClientHandler);

            try
            {
                // List active chat identity links for the current tenant.
                List<ChatIdentityLinkResponse> result = apiInstance.ChatIdentityGetLinks();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityGetLinks: " + e.Message);
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
    Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityGetLinksWithHttpInfo: " + e.Message);
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

<a id="chatidentitygetpending"></a>
# **ChatIdentityGetPending**
> PendingLinkViewResponse ChatIdentityGetPending (string token)

Read-only lookup of a pending link token, used by the authorize page to validate and render the confirmation step.

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
    public class ChatIdentityGetPendingExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ChatIdentityApi(httpClient, config, httpClientHandler);
            var token = "token_example";  // string | 

            try
            {
                // Read-only lookup of a pending link token, used by the authorize page to validate and render the confirmation step.
                PendingLinkViewResponse result = apiInstance.ChatIdentityGetPending(token);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityGetPending: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityGetPendingWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Read-only lookup of a pending link token, used by the authorize page to validate and render the confirmation step.
    ApiResponse<PendingLinkViewResponse> response = apiInstance.ChatIdentityGetPendingWithHttpInfo(token);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityGetPendingWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **token** | **string** |  |  |

### Return type

[**PendingLinkViewResponse**](PendingLinkViewResponse.md)

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
            var apiInstance = new ChatIdentityApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                apiInstance.ChatIdentityRevokeLink(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityRevokeLink: " + e.Message);
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
    apiInstance.ChatIdentityRevokeLinkWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityRevokeLinkWithHttpInfo: " + e.Message);
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

<a id="chatidentitysetdefault"></a>
# **ChatIdentitySetDefault**
> void ChatIdentitySetDefault (string id)



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
    public class ChatIdentitySetDefaultExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ChatIdentityApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                apiInstance.ChatIdentitySetDefault(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChatIdentityApi.ChatIdentitySetDefault: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentitySetDefaultWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.ChatIdentitySetDefaultWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ChatIdentityApi.ChatIdentitySetDefaultWithHttpInfo: " + e.Message);
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

<a id="chatidentityupdatelink"></a>
# **ChatIdentityUpdateLink**
> void ChatIdentityUpdateLink (string id, UpdateChatIdentityLinkRequest updateChatIdentityLinkRequest)



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
    public class ChatIdentityUpdateLinkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ChatIdentityApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var updateChatIdentityLinkRequest = new UpdateChatIdentityLinkRequest(); // UpdateChatIdentityLinkRequest | 

            try
            {
                apiInstance.ChatIdentityUpdateLink(id, updateChatIdentityLinkRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityUpdateLink: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ChatIdentityUpdateLinkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.ChatIdentityUpdateLinkWithHttpInfo(id, updateChatIdentityLinkRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ChatIdentityApi.ChatIdentityUpdateLinkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **updateChatIdentityLinkRequest** | [**UpdateChatIdentityLinkRequest**](UpdateChatIdentityLinkRequest.md) |  |  |

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

