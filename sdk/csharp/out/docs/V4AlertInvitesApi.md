# NightscoutFoundation.Nocturne.Api.V4AlertInvitesApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AlertInvitesCreateInvite**](V4AlertInvitesApi.md#alertinvitescreateinvite) | **POST** /api/v4/alert-invites | Generate an invite link for a follower to join an escalation step. |
| [**AlertInvitesRedeemInvite**](V4AlertInvitesApi.md#alertinvitesredeeminvite) | **POST** /api/v4/alert-invites/{token}/redeem | Redeem an invite token. |
| [**AlertInvitesRevokeInvite**](V4AlertInvitesApi.md#alertinvitesrevokeinvite) | **DELETE** /api/v4/alert-invites/{id} | Revoke an unredeemed invite. |
| [**AlertInvitesValidateInvite**](V4AlertInvitesApi.md#alertinvitesvalidateinvite) | **GET** /api/v4/alert-invites/{token} | Validate an invite token (public endpoint for redemption flow). |

<a id="alertinvitescreateinvite"></a>
# **AlertInvitesCreateInvite**
> AlertInviteResponse AlertInvitesCreateInvite (CreateAlertInviteRequest createAlertInviteRequest)

Generate an invite link for a follower to join an escalation step.

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
    public class AlertInvitesCreateInviteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertInvitesApi(httpClient, config, httpClientHandler);
            var createAlertInviteRequest = new CreateAlertInviteRequest(); // CreateAlertInviteRequest | 

            try
            {
                // Generate an invite link for a follower to join an escalation step.
                AlertInviteResponse result = apiInstance.AlertInvitesCreateInvite(createAlertInviteRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertInvitesApi.AlertInvitesCreateInvite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertInvitesCreateInviteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Generate an invite link for a follower to join an escalation step.
    ApiResponse<AlertInviteResponse> response = apiInstance.AlertInvitesCreateInviteWithHttpInfo(createAlertInviteRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertInvitesApi.AlertInvitesCreateInviteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createAlertInviteRequest** | [**CreateAlertInviteRequest**](CreateAlertInviteRequest.md) |  |  |

### Return type

[**AlertInviteResponse**](AlertInviteResponse.md)

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

<a id="alertinvitesredeeminvite"></a>
# **AlertInvitesRedeemInvite**
> void AlertInvitesRedeemInvite (string token)

Redeem an invite token.

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
    public class AlertInvitesRedeemInviteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertInvitesApi(httpClient, config, httpClientHandler);
            var token = "token_example";  // string | 

            try
            {
                // Redeem an invite token.
                apiInstance.AlertInvitesRedeemInvite(token);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertInvitesApi.AlertInvitesRedeemInvite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertInvitesRedeemInviteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Redeem an invite token.
    apiInstance.AlertInvitesRedeemInviteWithHttpInfo(token);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertInvitesApi.AlertInvitesRedeemInviteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **token** | **string** |  |  |

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
| **410** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="alertinvitesrevokeinvite"></a>
# **AlertInvitesRevokeInvite**
> void AlertInvitesRevokeInvite (string id)

Revoke an unredeemed invite.

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
    public class AlertInvitesRevokeInviteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertInvitesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Revoke an unredeemed invite.
                apiInstance.AlertInvitesRevokeInvite(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertInvitesApi.AlertInvitesRevokeInvite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertInvitesRevokeInviteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Revoke an unredeemed invite.
    apiInstance.AlertInvitesRevokeInviteWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertInvitesApi.AlertInvitesRevokeInviteWithHttpInfo: " + e.Message);
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
| **409** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="alertinvitesvalidateinvite"></a>
# **AlertInvitesValidateInvite**
> AlertInviteResponse AlertInvitesValidateInvite (string token)

Validate an invite token (public endpoint for redemption flow).

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
    public class AlertInvitesValidateInviteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertInvitesApi(httpClient, config, httpClientHandler);
            var token = "token_example";  // string | 

            try
            {
                // Validate an invite token (public endpoint for redemption flow).
                AlertInviteResponse result = apiInstance.AlertInvitesValidateInvite(token);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertInvitesApi.AlertInvitesValidateInvite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertInvitesValidateInviteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Validate an invite token (public endpoint for redemption flow).
    ApiResponse<AlertInviteResponse> response = apiInstance.AlertInvitesValidateInviteWithHttpInfo(token);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertInvitesApi.AlertInvitesValidateInviteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **token** | **string** |  |  |

### Return type

[**AlertInviteResponse**](AlertInviteResponse.md)

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
| **410** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

