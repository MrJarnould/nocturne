# NightscoutFoundation.Nocturne.Api.MemberInviteApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**MemberInviteAcceptInvite**](MemberInviteApi.md#memberinviteacceptinvite) | **POST** /api/v4/member-invites/{token}/accept | Accept an invite and join the tenant. |
| [**MemberInviteGetEffectivePermissions**](MemberInviteApi.md#memberinvitegeteffectivepermissions) | **GET** /api/v4/member-invites/members/{id}/effective-permissions | Get effective permissions for a member (union of role permissions + direct permissions). |
| [**MemberInviteGetFollowers**](MemberInviteApi.md#memberinvitegetfollowers) | **GET** /api/v4/member-invites/members/followers | List followers of the current tenant (members with the follower role). |
| [**MemberInviteGetInviteInfo**](MemberInviteApi.md#memberinvitegetinviteinfo) | **GET** /api/v4/member-invites/{token}/info | Get invite info for the accept page (anonymous). |
| [**MemberInviteGetMembers**](MemberInviteApi.md#memberinvitegetmembers) | **GET** /api/v4/member-invites/members | List all members of the current tenant. |
| [**MemberInviteSetMemberLimitTo24Hours**](MemberInviteApi.md#memberinvitesetmemberlimitto24hours) | **PUT** /api/v4/member-invites/members/{id}/limit-to-24-hours | Update the 24-hour data limit for a member. |
| [**MemberInviteSetMemberPermissions**](MemberInviteApi.md#memberinvitesetmemberpermissions) | **PUT** /api/v4/member-invites/members/{id}/permissions | Set direct permissions for a member. |
| [**MemberInviteSetMemberRoles**](MemberInviteApi.md#memberinvitesetmemberroles) | **PUT** /api/v4/member-invites/members/{id}/roles | Set roles for a member (replaces all role assignments). |

<a id="memberinviteacceptinvite"></a>
# **MemberInviteAcceptInvite**
> AcceptMemberInviteResult MemberInviteAcceptInvite (string token)

Accept an invite and join the tenant.

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
    public class MemberInviteAcceptInviteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MemberInviteApi(httpClient, config, httpClientHandler);
            var token = "token_example";  // string | 

            try
            {
                // Accept an invite and join the tenant.
                AcceptMemberInviteResult result = apiInstance.MemberInviteAcceptInvite(token);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MemberInviteApi.MemberInviteAcceptInvite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MemberInviteAcceptInviteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Accept an invite and join the tenant.
    ApiResponse<AcceptMemberInviteResult> response = apiInstance.MemberInviteAcceptInviteWithHttpInfo(token);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MemberInviteApi.MemberInviteAcceptInviteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **token** | **string** |  |  |

### Return type

[**AcceptMemberInviteResult**](AcceptMemberInviteResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="memberinvitegeteffectivepermissions"></a>
# **MemberInviteGetEffectivePermissions**
> List&lt;string&gt; MemberInviteGetEffectivePermissions (string id)

Get effective permissions for a member (union of role permissions + direct permissions).

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
    public class MemberInviteGetEffectivePermissionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MemberInviteApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Get effective permissions for a member (union of role permissions + direct permissions).
                List<string> result = apiInstance.MemberInviteGetEffectivePermissions(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MemberInviteApi.MemberInviteGetEffectivePermissions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MemberInviteGetEffectivePermissionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get effective permissions for a member (union of role permissions + direct permissions).
    ApiResponse<List<string>> response = apiInstance.MemberInviteGetEffectivePermissionsWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MemberInviteApi.MemberInviteGetEffectivePermissionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

**List<string>**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **403** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="memberinvitegetfollowers"></a>
# **MemberInviteGetFollowers**
> List&lt;TenantMemberDto&gt; MemberInviteGetFollowers ()

List followers of the current tenant (members with the follower role).

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
    public class MemberInviteGetFollowersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MemberInviteApi(httpClient, config, httpClientHandler);

            try
            {
                // List followers of the current tenant (members with the follower role).
                List<TenantMemberDto> result = apiInstance.MemberInviteGetFollowers();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MemberInviteApi.MemberInviteGetFollowers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MemberInviteGetFollowersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List followers of the current tenant (members with the follower role).
    ApiResponse<List<TenantMemberDto>> response = apiInstance.MemberInviteGetFollowersWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MemberInviteApi.MemberInviteGetFollowersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;TenantMemberDto&gt;**](TenantMemberDto.md)

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

<a id="memberinvitegetinviteinfo"></a>
# **MemberInviteGetInviteInfo**
> MemberInviteInfo MemberInviteGetInviteInfo (string token)

Get invite info for the accept page (anonymous).

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
    public class MemberInviteGetInviteInfoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MemberInviteApi(httpClient, config, httpClientHandler);
            var token = "token_example";  // string | 

            try
            {
                // Get invite info for the accept page (anonymous).
                MemberInviteInfo result = apiInstance.MemberInviteGetInviteInfo(token);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MemberInviteApi.MemberInviteGetInviteInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MemberInviteGetInviteInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get invite info for the accept page (anonymous).
    ApiResponse<MemberInviteInfo> response = apiInstance.MemberInviteGetInviteInfoWithHttpInfo(token);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MemberInviteApi.MemberInviteGetInviteInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **token** | **string** |  |  |

### Return type

[**MemberInviteInfo**](MemberInviteInfo.md)

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

<a id="memberinvitegetmembers"></a>
# **MemberInviteGetMembers**
> List&lt;TenantMemberDto&gt; MemberInviteGetMembers ()

List all members of the current tenant.

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
    public class MemberInviteGetMembersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MemberInviteApi(httpClient, config, httpClientHandler);

            try
            {
                // List all members of the current tenant.
                List<TenantMemberDto> result = apiInstance.MemberInviteGetMembers();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MemberInviteApi.MemberInviteGetMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MemberInviteGetMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List all members of the current tenant.
    ApiResponse<List<TenantMemberDto>> response = apiInstance.MemberInviteGetMembersWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MemberInviteApi.MemberInviteGetMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;TenantMemberDto&gt;**](TenantMemberDto.md)

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

<a id="memberinvitesetmemberlimitto24hours"></a>
# **MemberInviteSetMemberLimitTo24Hours**
> void MemberInviteSetMemberLimitTo24Hours (string id, SetMemberLimitTo24HoursRequest setMemberLimitTo24HoursRequest)

Update the 24-hour data limit for a member.

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
    public class MemberInviteSetMemberLimitTo24HoursExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MemberInviteApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var setMemberLimitTo24HoursRequest = new SetMemberLimitTo24HoursRequest(); // SetMemberLimitTo24HoursRequest | 

            try
            {
                // Update the 24-hour data limit for a member.
                apiInstance.MemberInviteSetMemberLimitTo24Hours(id, setMemberLimitTo24HoursRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MemberInviteApi.MemberInviteSetMemberLimitTo24Hours: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MemberInviteSetMemberLimitTo24HoursWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update the 24-hour data limit for a member.
    apiInstance.MemberInviteSetMemberLimitTo24HoursWithHttpInfo(id, setMemberLimitTo24HoursRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MemberInviteApi.MemberInviteSetMemberLimitTo24HoursWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **setMemberLimitTo24HoursRequest** | [**SetMemberLimitTo24HoursRequest**](SetMemberLimitTo24HoursRequest.md) |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |
| **403** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="memberinvitesetmemberpermissions"></a>
# **MemberInviteSetMemberPermissions**
> void MemberInviteSetMemberPermissions (string id, SetMemberPermissionsRequest setMemberPermissionsRequest)

Set direct permissions for a member.

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
    public class MemberInviteSetMemberPermissionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MemberInviteApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var setMemberPermissionsRequest = new SetMemberPermissionsRequest(); // SetMemberPermissionsRequest | 

            try
            {
                // Set direct permissions for a member.
                apiInstance.MemberInviteSetMemberPermissions(id, setMemberPermissionsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MemberInviteApi.MemberInviteSetMemberPermissions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MemberInviteSetMemberPermissionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Set direct permissions for a member.
    apiInstance.MemberInviteSetMemberPermissionsWithHttpInfo(id, setMemberPermissionsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MemberInviteApi.MemberInviteSetMemberPermissionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **setMemberPermissionsRequest** | [**SetMemberPermissionsRequest**](SetMemberPermissionsRequest.md) |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |
| **403** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="memberinvitesetmemberroles"></a>
# **MemberInviteSetMemberRoles**
> void MemberInviteSetMemberRoles (string id, SetMemberRolesRequest setMemberRolesRequest)

Set roles for a member (replaces all role assignments).

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
    public class MemberInviteSetMemberRolesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MemberInviteApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var setMemberRolesRequest = new SetMemberRolesRequest(); // SetMemberRolesRequest | 

            try
            {
                // Set roles for a member (replaces all role assignments).
                apiInstance.MemberInviteSetMemberRoles(id, setMemberRolesRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MemberInviteApi.MemberInviteSetMemberRoles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MemberInviteSetMemberRolesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Set roles for a member (replaces all role assignments).
    apiInstance.MemberInviteSetMemberRolesWithHttpInfo(id, setMemberRolesRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MemberInviteApi.MemberInviteSetMemberRolesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **setMemberRolesRequest** | [**SetMemberRolesRequest**](SetMemberRolesRequest.md) |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |
| **403** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

