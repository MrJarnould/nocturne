# NightscoutFoundation.Nocturne.Api.OidcApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**OidcCallback**](OidcApi.md#oidccallback) | **GET** /api/v4/oidc/callback | Handle OIDC callback from provider. Exchanges authorization code for tokens and creates session. |
| [**OidcGetLinkedIdentities**](OidcApi.md#oidcgetlinkedidentities) | **GET** /api/v4/oidc/link/identities | List OIDC identities linked to the currently-authenticated subject. |
| [**OidcGetProviders**](OidcApi.md#oidcgetproviders) | **GET** /api/v4/oidc/providers | Get available OIDC providers for login. |
| [**OidcGetSession**](OidcApi.md#oidcgetsession) | **GET** /api/v4/oidc/session | Get current session information including authentication status, roles, and permissions. |
| [**OidcGetUserInfo**](OidcApi.md#oidcgetuserinfo) | **GET** /api/v4/oidc/userinfo | Get current user information. |
| [**OidcLink**](OidcApi.md#oidclink) | **GET** /api/v4/oidc/link | Initiate the OIDC link flow. Redirects an already-authenticated caller to the OIDC provider&#39;s authorization endpoint so they can attach the external identity to their current account. |
| [**OidcLinkCallback**](OidcApi.md#oidclinkcallback) | **GET** /api/v4/oidc/link/callback | Handle the OIDC link callback. Verifies the authorization code against the IdP, then attaches the external identity to the currently-authenticated subject. Does NOT issue new session cookies. |
| [**OidcLogin**](OidcApi.md#oidclogin) | **GET** /api/v4/oidc/login | Initiate OIDC login flow Redirects to the OIDC provider&#39;s authorization endpoint |
| [**OidcLogout**](OidcApi.md#oidclogout) | **POST** /api/v4/oidc/logout | Logout and revoke the session. |
| [**OidcRefresh**](OidcApi.md#oidcrefresh) | **POST** /api/v4/oidc/refresh | Refresh the session tokens. Uses the refresh token to get a new access token. |
| [**OidcUnlinkIdentity**](OidcApi.md#oidcunlinkidentity) | **DELETE** /api/v4/oidc/link/identities/{identityId} | Unlink an OIDC identity from the currently-authenticated subject. Blocked if this would leave the subject with zero primary auth factors. |

<a id="oidccallback"></a>
# **OidcCallback**
> void OidcCallback (string? code = null, string? state = null, string? error = null, string? errorDescription = null)

Handle OIDC callback from provider. Exchanges authorization code for tokens and creates session.

Flow: verifies state cookie against the state parameter, exchanges code for tokens via HandleCallbackAsync, sets session cookies, and logs an Login audit event. On failure, logs FailedAuth and redirects to the error page.

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
    public class OidcCallbackExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);
            var code = "code_example";  // string? | Authorization code from provider. (optional) 
            var state = "state_example";  // string? | State parameter for CSRF verification. (optional) 
            var error = "error_example";  // string? | Error code from provider (if any). (optional) 
            var errorDescription = "errorDescription_example";  // string? | Error description from provider. (optional) 

            try
            {
                // Handle OIDC callback from provider. Exchanges authorization code for tokens and creates session.
                apiInstance.OidcCallback(code, state, error, errorDescription);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcCallback: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcCallbackWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Handle OIDC callback from provider. Exchanges authorization code for tokens and creates session.
    apiInstance.OidcCallbackWithHttpInfo(code, state, error, errorDescription);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcCallbackWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **code** | **string?** | Authorization code from provider. | [optional]  |
| **state** | **string?** | State parameter for CSRF verification. | [optional]  |
| **error** | **string?** | Error code from provider (if any). | [optional]  |
| **errorDescription** | **string?** | Error description from provider. | [optional]  |

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
| **302** | Redirect to return URL on success, or error page on failure. |  -  |
| **400** | Missing code or state parameters. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="oidcgetlinkedidentities"></a>
# **OidcGetLinkedIdentities**
> LinkedOidcIdentitiesResponse OidcGetLinkedIdentities ()

List OIDC identities linked to the currently-authenticated subject.

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
    public class OidcGetLinkedIdentitiesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);

            try
            {
                // List OIDC identities linked to the currently-authenticated subject.
                LinkedOidcIdentitiesResponse result = apiInstance.OidcGetLinkedIdentities();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcGetLinkedIdentities: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcGetLinkedIdentitiesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List OIDC identities linked to the currently-authenticated subject.
    ApiResponse<LinkedOidcIdentitiesResponse> response = apiInstance.OidcGetLinkedIdentitiesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcGetLinkedIdentitiesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**LinkedOidcIdentitiesResponse**](LinkedOidcIdentitiesResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **401** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="oidcgetproviders"></a>
# **OidcGetProviders**
> List&lt;OidcProviderInfo&gt; OidcGetProviders ()

Get available OIDC providers for login.

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
    public class OidcGetProvidersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);

            try
            {
                // Get available OIDC providers for login.
                List<OidcProviderInfo> result = apiInstance.OidcGetProviders();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcGetProviders: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcGetProvidersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get available OIDC providers for login.
    ApiResponse<List<OidcProviderInfo>> response = apiInstance.OidcGetProvidersWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcGetProvidersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;OidcProviderInfo&gt;**](OidcProviderInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of enabled OIDC providers. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="oidcgetsession"></a>
# **OidcGetSession**
> SessionInfo OidcGetSession ()

Get current session information including authentication status, roles, and permissions.

Returns IsAuthenticated = false for unauthenticated requests (does not return 401). This allows the frontend to check auth status without triggering redirect logic.

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
    public class OidcGetSessionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);

            try
            {
                // Get current session information including authentication status, roles, and permissions.
                SessionInfo result = apiInstance.OidcGetSession();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcGetSession: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcGetSessionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get current session information including authentication status, roles, and permissions.
    ApiResponse<SessionInfo> response = apiInstance.OidcGetSessionWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcGetSessionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**SessionInfo**](SessionInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Session information (always returns 200). |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="oidcgetuserinfo"></a>
# **OidcGetUserInfo**
> OidcUserInfo OidcGetUserInfo ()

Get current user information.

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
    public class OidcGetUserInfoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);

            try
            {
                // Get current user information.
                OidcUserInfo result = apiInstance.OidcGetUserInfo();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcGetUserInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcGetUserInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get current user information.
    ApiResponse<OidcUserInfo> response = apiInstance.OidcGetUserInfoWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcGetUserInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**OidcUserInfo**](OidcUserInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User information retrieved. |  -  |
| **401** | Not authenticated or user not found. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="oidclink"></a>
# **OidcLink**
> void OidcLink (string? provider = null, string? returnUrl = null)

Initiate the OIDC link flow. Redirects an already-authenticated caller to the OIDC provider's authorization endpoint so they can attach the external identity to their current account.

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
    public class OidcLinkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);
            var provider = "provider_example";  // string? |  (optional) 
            var returnUrl = "returnUrl_example";  // string? |  (optional) 

            try
            {
                // Initiate the OIDC link flow. Redirects an already-authenticated caller to the OIDC provider's authorization endpoint so they can attach the external identity to their current account.
                apiInstance.OidcLink(provider, returnUrl);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcLink: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcLinkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Initiate the OIDC link flow. Redirects an already-authenticated caller to the OIDC provider's authorization endpoint so they can attach the external identity to their current account.
    apiInstance.OidcLinkWithHttpInfo(provider, returnUrl);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcLinkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **provider** | **string?** |  | [optional]  |
| **returnUrl** | **string?** |  | [optional]  |

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
| **302** |  |  -  |
| **400** |  |  -  |
| **401** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="oidclinkcallback"></a>
# **OidcLinkCallback**
> void OidcLinkCallback (string? code = null, string? state = null, string? error = null, string? errorDescription = null)

Handle the OIDC link callback. Verifies the authorization code against the IdP, then attaches the external identity to the currently-authenticated subject. Does NOT issue new session cookies.

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
    public class OidcLinkCallbackExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);
            var code = "code_example";  // string? |  (optional) 
            var state = "state_example";  // string? |  (optional) 
            var error = "error_example";  // string? |  (optional) 
            var errorDescription = "errorDescription_example";  // string? |  (optional) 

            try
            {
                // Handle the OIDC link callback. Verifies the authorization code against the IdP, then attaches the external identity to the currently-authenticated subject. Does NOT issue new session cookies.
                apiInstance.OidcLinkCallback(code, state, error, errorDescription);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcLinkCallback: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcLinkCallbackWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Handle the OIDC link callback. Verifies the authorization code against the IdP, then attaches the external identity to the currently-authenticated subject. Does NOT issue new session cookies.
    apiInstance.OidcLinkCallbackWithHttpInfo(code, state, error, errorDescription);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcLinkCallbackWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **code** | **string?** |  | [optional]  |
| **state** | **string?** |  | [optional]  |
| **error** | **string?** |  | [optional]  |
| **errorDescription** | **string?** |  | [optional]  |

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
| **302** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="oidclogin"></a>
# **OidcLogin**
> void OidcLogin (string? provider = null, string? returnUrl = null)

Initiate OIDC login flow Redirects to the OIDC provider's authorization endpoint

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
    public class OidcLoginExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);
            var provider = "provider_example";  // string? | Provider ID (optional, uses default if not specified) (optional) 
            var returnUrl = "returnUrl_example";  // string? | URL to return to after login (optional) 

            try
            {
                // Initiate OIDC login flow Redirects to the OIDC provider's authorization endpoint
                apiInstance.OidcLogin(provider, returnUrl);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcLogin: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcLoginWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Initiate OIDC login flow Redirects to the OIDC provider's authorization endpoint
    apiInstance.OidcLoginWithHttpInfo(provider, returnUrl);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcLoginWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **provider** | **string?** | Provider ID (optional, uses default if not specified) | [optional]  |
| **returnUrl** | **string?** | URL to return to after login | [optional]  |

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
| **302** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="oidclogout"></a>
# **OidcLogout**
> LogoutResponse OidcLogout (string? providerId = null)

Logout and revoke the session.

Clears all session cookies (access token, refresh token, IsAuthenticated). If a refresh token is present, revokes it via LogoutAsync. When providerId is supplied and the provider supports RP-initiated logout, the response includes a ProviderLogoutUrl for the frontend to redirect to.

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
    public class OidcLogoutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);
            var providerId = "providerId_example";  // string? | Provider ID for RP-initiated logout (optional). (optional) 

            try
            {
                // Logout and revoke the session.
                LogoutResponse result = apiInstance.OidcLogout(providerId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcLogout: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcLogoutWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Logout and revoke the session.
    ApiResponse<LogoutResponse> response = apiInstance.OidcLogoutWithHttpInfo(providerId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcLogoutWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **providerId** | **string?** | Provider ID for RP-initiated logout (optional). | [optional]  |

### Return type

[**LogoutResponse**](LogoutResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Logout successful. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="oidcrefresh"></a>
# **OidcRefresh**
> OidcTokenResponse OidcRefresh ()

Refresh the session tokens. Uses the refresh token to get a new access token.

The refresh token is read from the cookie or the Refresh Authorization header (for API clients). On failure, all session cookies are cleared to force re-authentication. Logs TokenRefreshed on success.

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
    public class OidcRefreshExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);

            try
            {
                // Refresh the session tokens. Uses the refresh token to get a new access token.
                OidcTokenResponse result = apiInstance.OidcRefresh();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcRefresh: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcRefreshWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Refresh the session tokens. Uses the refresh token to get a new access token.
    ApiResponse<OidcTokenResponse> response = apiInstance.OidcRefreshWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcRefreshWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**OidcTokenResponse**](OidcTokenResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Tokens refreshed successfully. |  -  |
| **401** | No refresh token found, or refresh token is invalid/expired. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="oidcunlinkidentity"></a>
# **OidcUnlinkIdentity**
> void OidcUnlinkIdentity (string identityId)

Unlink an OIDC identity from the currently-authenticated subject. Blocked if this would leave the subject with zero primary auth factors.

Factor-count enforcement is handled atomically inside TryRemoveOidcIdentityAsync using a serializable transaction to prevent TOCTOU races between concurrent removals. Returns FactorRemovalResult to distinguish between not-found, last-factor, and success.

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
    public class OidcUnlinkIdentityExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OidcApi(httpClient, config, httpClientHandler);
            var identityId = "identityId_example";  // string | The unique identifier of the OIDC identity to unlink.

            try
            {
                // Unlink an OIDC identity from the currently-authenticated subject. Blocked if this would leave the subject with zero primary auth factors.
                apiInstance.OidcUnlinkIdentity(identityId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OidcApi.OidcUnlinkIdentity: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OidcUnlinkIdentityWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Unlink an OIDC identity from the currently-authenticated subject. Blocked if this would leave the subject with zero primary auth factors.
    apiInstance.OidcUnlinkIdentityWithHttpInfo(identityId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OidcApi.OidcUnlinkIdentityWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **identityId** | **string** | The unique identifier of the OIDC identity to unlink. |  |

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
| **204** | Identity unlinked successfully. |  -  |
| **401** | Not authenticated. |  -  |
| **404** | Identity not found. |  -  |
| **409** | Cannot remove the last primary sign-in method. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

