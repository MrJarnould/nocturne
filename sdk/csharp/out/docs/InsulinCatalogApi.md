# NightscoutFoundation.Nocturne.Api.InsulinCatalogApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**InsulinCatalogGetCatalog**](InsulinCatalogApi.md#insulincataloggetcatalog) | **GET** /api/v4/insulins/catalog | Get all known insulin formulations. |
| [**InsulinCatalogGetCatalogByCategory**](InsulinCatalogApi.md#insulincataloggetcatalogbycategory) | **GET** /api/v4/insulins/catalog/{category} | Get insulin formulations filtered by category. |

<a id="insulincataloggetcatalog"></a>
# **InsulinCatalogGetCatalog**
> List&lt;InsulinFormulation&gt; InsulinCatalogGetCatalog ()

Get all known insulin formulations.

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
    public class InsulinCatalogGetCatalogExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new InsulinCatalogApi(httpClient, config, httpClientHandler);

            try
            {
                // Get all known insulin formulations.
                List<InsulinFormulation> result = apiInstance.InsulinCatalogGetCatalog();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InsulinCatalogApi.InsulinCatalogGetCatalog: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the InsulinCatalogGetCatalogWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all known insulin formulations.
    ApiResponse<List<InsulinFormulation>> response = apiInstance.InsulinCatalogGetCatalogWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InsulinCatalogApi.InsulinCatalogGetCatalogWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;InsulinFormulation&gt;**](InsulinFormulation.md)

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

<a id="insulincataloggetcatalogbycategory"></a>
# **InsulinCatalogGetCatalogByCategory**
> List&lt;InsulinFormulation&gt; InsulinCatalogGetCatalogByCategory (InsulinCategory category)

Get insulin formulations filtered by category.

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
    public class InsulinCatalogGetCatalogByCategoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new InsulinCatalogApi(httpClient, config, httpClientHandler);
            var category = (InsulinCategory) "RapidActing";  // InsulinCategory | 

            try
            {
                // Get insulin formulations filtered by category.
                List<InsulinFormulation> result = apiInstance.InsulinCatalogGetCatalogByCategory(category);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InsulinCatalogApi.InsulinCatalogGetCatalogByCategory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the InsulinCatalogGetCatalogByCategoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get insulin formulations filtered by category.
    ApiResponse<List<InsulinFormulation>> response = apiInstance.InsulinCatalogGetCatalogByCategoryWithHttpInfo(category);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InsulinCatalogApi.InsulinCatalogGetCatalogByCategoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **category** | **InsulinCategory** |  |  |

### Return type

[**List&lt;InsulinFormulation&gt;**](InsulinFormulation.md)

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

