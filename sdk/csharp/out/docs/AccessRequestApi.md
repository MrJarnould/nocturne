# NightscoutFoundation.Nocturne.Api.AccessRequestApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AccessRequestApprove**](AccessRequestApi.md#accessrequestapprove) | **POST** /api/v4/admin/access-requests/{subjectId}/approve |  |
| [**AccessRequestDeny**](AccessRequestApi.md#accessrequestdeny) | **POST** /api/v4/admin/access-requests/{subjectId}/deny |  |
| [**AccessRequestGetPendingRequests**](AccessRequestApi.md#accessrequestgetpendingrequests) | **GET** /api/v4/admin/access-requests |  |

<a id="accessrequestapprove"></a>
# **AccessRequestApprove**
> void AccessRequestApprove (string subjectId, ApproveAccessRequestRequest approveAccessRequestRequest)



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
    public class AccessRequestApproveExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AccessRequestApi(httpClient, config, httpClientHandler);
            var subjectId = "subjectId_example";  // string | 
            var approveAccessRequestRequest = new ApproveAccessRequestRequest(); // ApproveAccessRequestRequest | 

            try
            {
                apiInstance.AccessRequestApprove(subjectId, approveAccessRequestRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AccessRequestApi.AccessRequestApprove: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AccessRequestApproveWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.AccessRequestApproveWithHttpInfo(subjectId, approveAccessRequestRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AccessRequestApi.AccessRequestApproveWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **subjectId** | **string** |  |  |
| **approveAccessRequestRequest** | [**ApproveAccessRequestRequest**](ApproveAccessRequestRequest.md) |  |  |

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
| **200** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="accessrequestdeny"></a>
# **AccessRequestDeny**
> void AccessRequestDeny (string subjectId)



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
    public class AccessRequestDenyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AccessRequestApi(httpClient, config, httpClientHandler);
            var subjectId = "subjectId_example";  // string | 

            try
            {
                apiInstance.AccessRequestDeny(subjectId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AccessRequestApi.AccessRequestDeny: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AccessRequestDenyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.AccessRequestDenyWithHttpInfo(subjectId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AccessRequestApi.AccessRequestDenyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **subjectId** | **string** |  |  |

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
| **200** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="accessrequestgetpendingrequests"></a>
# **AccessRequestGetPendingRequests**
> List&lt;AccessRequestDto&gt; AccessRequestGetPendingRequests ()



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
    public class AccessRequestGetPendingRequestsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AccessRequestApi(httpClient, config, httpClientHandler);

            try
            {
                List<AccessRequestDto> result = apiInstance.AccessRequestGetPendingRequests();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AccessRequestApi.AccessRequestGetPendingRequests: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AccessRequestGetPendingRequestsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<List<AccessRequestDto>> response = apiInstance.AccessRequestGetPendingRequestsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AccessRequestApi.AccessRequestGetPendingRequestsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;AccessRequestDto&gt;**](AccessRequestDto.md)

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

