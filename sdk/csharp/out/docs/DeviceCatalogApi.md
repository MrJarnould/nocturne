# NightscoutFoundation.Nocturne.Api.DeviceCatalogApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DeviceCatalogGetCatalog**](DeviceCatalogApi.md#devicecataloggetcatalog) | **GET** /api/v4/devices/catalog | Get all known device models. |
| [**DeviceCatalogGetCatalogByCategory**](DeviceCatalogApi.md#devicecataloggetcatalogbycategory) | **GET** /api/v4/devices/catalog/{category} | Get device models filtered by category. |

<a id="devicecataloggetcatalog"></a>
# **DeviceCatalogGetCatalog**
> List&lt;DeviceCatalogEntry&gt; DeviceCatalogGetCatalog ()

Get all known device models.

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
    public class DeviceCatalogGetCatalogExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeviceCatalogApi(httpClient, config, httpClientHandler);

            try
            {
                // Get all known device models.
                List<DeviceCatalogEntry> result = apiInstance.DeviceCatalogGetCatalog();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceCatalogApi.DeviceCatalogGetCatalog: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceCatalogGetCatalogWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all known device models.
    ApiResponse<List<DeviceCatalogEntry>> response = apiInstance.DeviceCatalogGetCatalogWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeviceCatalogApi.DeviceCatalogGetCatalogWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;DeviceCatalogEntry&gt;**](DeviceCatalogEntry.md)

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

<a id="devicecataloggetcatalogbycategory"></a>
# **DeviceCatalogGetCatalogByCategory**
> List&lt;DeviceCatalogEntry&gt; DeviceCatalogGetCatalogByCategory (DeviceCategory category)

Get device models filtered by category.

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
    public class DeviceCatalogGetCatalogByCategoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeviceCatalogApi(httpClient, config, httpClientHandler);
            var category = (DeviceCategory) "InsulinPump";  // DeviceCategory | 

            try
            {
                // Get device models filtered by category.
                List<DeviceCatalogEntry> result = apiInstance.DeviceCatalogGetCatalogByCategory(category);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceCatalogApi.DeviceCatalogGetCatalogByCategory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceCatalogGetCatalogByCategoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get device models filtered by category.
    ApiResponse<List<DeviceCatalogEntry>> response = apiInstance.DeviceCatalogGetCatalogByCategoryWithHttpInfo(category);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeviceCatalogApi.DeviceCatalogGetCatalogByCategoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **category** | **DeviceCategory** |  |  |

### Return type

[**List&lt;DeviceCatalogEntry&gt;**](DeviceCatalogEntry.md)

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

