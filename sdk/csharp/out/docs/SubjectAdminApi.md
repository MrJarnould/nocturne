# NightscoutFoundation.Nocturne.Api.SubjectAdminApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**SubjectAdminSetPlatformAdmin**](SubjectAdminApi.md#subjectadminsetplatformadmin) | **PUT** /api/v4/admin/subjects/{id}/platform-admin | Grant or revoke platform admin for a subject. Blocks self-demotion if the caller is the last platform admin. |

<a id="subjectadminsetplatformadmin"></a>
# **SubjectAdminSetPlatformAdmin**
> FileParameter SubjectAdminSetPlatformAdmin (string id, SetPlatformAdminRequest setPlatformAdminRequest)

Grant or revoke platform admin for a subject. Blocks self-demotion if the caller is the last platform admin.

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
    public class SubjectAdminSetPlatformAdminExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new SubjectAdminApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var setPlatformAdminRequest = new SetPlatformAdminRequest(); // SetPlatformAdminRequest | 

            try
            {
                // Grant or revoke platform admin for a subject. Blocks self-demotion if the caller is the last platform admin.
                FileParameter result = apiInstance.SubjectAdminSetPlatformAdmin(id, setPlatformAdminRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SubjectAdminApi.SubjectAdminSetPlatformAdmin: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SubjectAdminSetPlatformAdminWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Grant or revoke platform admin for a subject. Blocks self-demotion if the caller is the last platform admin.
    ApiResponse<FileParameter> response = apiInstance.SubjectAdminSetPlatformAdminWithHttpInfo(id, setPlatformAdminRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SubjectAdminApi.SubjectAdminSetPlatformAdminWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **setPlatformAdminRequest** | [**SetPlatformAdminRequest**](SetPlatformAdminRequest.md) |  |  |

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

