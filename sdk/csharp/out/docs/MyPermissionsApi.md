# NightscoutFoundation.Nocturne.Api.MyPermissionsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**MyPermissionsGetMyPermissions**](MyPermissionsApi.md#mypermissionsgetmypermissions) | **GET** /api/v4/me/permissions | Get the current user&#39;s effective granted scopes for the current tenant. |

<a id="mypermissionsgetmypermissions"></a>
# **MyPermissionsGetMyPermissions**
> List&lt;string&gt; MyPermissionsGetMyPermissions ()

Get the current user's effective granted scopes for the current tenant.

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
    public class MyPermissionsGetMyPermissionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MyPermissionsApi(httpClient, config, httpClientHandler);

            try
            {
                // Get the current user's effective granted scopes for the current tenant.
                List<string> result = apiInstance.MyPermissionsGetMyPermissions();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MyPermissionsApi.MyPermissionsGetMyPermissions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MyPermissionsGetMyPermissionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get the current user's effective granted scopes for the current tenant.
    ApiResponse<List<string>> response = apiInstance.MyPermissionsGetMyPermissionsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MyPermissionsApi.MyPermissionsGetMyPermissionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
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
| **200** | The list of granted scope strings for the authenticated user on the current tenant. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

