# NightscoutFoundation.Nocturne.Api.ClockFacesApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ClockFacesCreate**](ClockFacesApi.md#clockfacescreate) | **POST** /api/v4/clockfaces | Create a new clock face |
| [**ClockFacesDelete**](ClockFacesApi.md#clockfacesdelete) | **DELETE** /api/v4/clockfaces/{id} | Delete a clock face (owner only) |
| [**ClockFacesGetById**](ClockFacesApi.md#clockfacesgetbyid) | **GET** /api/v4/clockfaces/{id} | Get a clock face configuration by ID (public, no authentication required) |
| [**ClockFacesList**](ClockFacesApi.md#clockfaceslist) | **GET** /api/v4/clockfaces | List all clock faces for the current user |
| [**ClockFacesUpdate**](ClockFacesApi.md#clockfacesupdate) | **PUT** /api/v4/clockfaces/{id} | Update an existing clock face (owner only) |

<a id="clockfacescreate"></a>
# **ClockFacesCreate**
> ClockFace ClockFacesCreate (CreateClockFaceRequest createClockFaceRequest)

Create a new clock face

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
    public class ClockFacesCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ClockFacesApi(httpClient, config, httpClientHandler);
            var createClockFaceRequest = new CreateClockFaceRequest(); // CreateClockFaceRequest | Clock face creation request

            try
            {
                // Create a new clock face
                ClockFace result = apiInstance.ClockFacesCreate(createClockFaceRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClockFacesApi.ClockFacesCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClockFacesCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a new clock face
    ApiResponse<ClockFace> response = apiInstance.ClockFacesCreateWithHttpInfo(createClockFaceRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClockFacesApi.ClockFacesCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createClockFaceRequest** | [**CreateClockFaceRequest**](CreateClockFaceRequest.md) | Clock face creation request |  |

### Return type

[**ClockFace**](ClockFace.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Created clock face |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="clockfacesdelete"></a>
# **ClockFacesDelete**
> void ClockFacesDelete (string id)

Delete a clock face (owner only)

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
    public class ClockFacesDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ClockFacesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | Clock face UUID

            try
            {
                // Delete a clock face (owner only)
                apiInstance.ClockFacesDelete(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClockFacesApi.ClockFacesDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClockFacesDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a clock face (owner only)
    apiInstance.ClockFacesDeleteWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClockFacesApi.ClockFacesDeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Clock face UUID |  |

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
| **204** | Success status |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="clockfacesgetbyid"></a>
# **ClockFacesGetById**
> ClockFacePublicDto ClockFacesGetById (string id)

Get a clock face configuration by ID (public, no authentication required)

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
    public class ClockFacesGetByIdExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ClockFacesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | Clock face UUID

            try
            {
                // Get a clock face configuration by ID (public, no authentication required)
                ClockFacePublicDto result = apiInstance.ClockFacesGetById(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClockFacesApi.ClockFacesGetById: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClockFacesGetByIdWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a clock face configuration by ID (public, no authentication required)
    ApiResponse<ClockFacePublicDto> response = apiInstance.ClockFacesGetByIdWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClockFacesApi.ClockFacesGetByIdWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Clock face UUID |  |

### Return type

[**ClockFacePublicDto**](ClockFacePublicDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Clock face configuration |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="clockfaceslist"></a>
# **ClockFacesList**
> List&lt;ClockFaceListItem&gt; ClockFacesList ()

List all clock faces for the current user

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
    public class ClockFacesListExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ClockFacesApi(httpClient, config, httpClientHandler);

            try
            {
                // List all clock faces for the current user
                List<ClockFaceListItem> result = apiInstance.ClockFacesList();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClockFacesApi.ClockFacesList: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClockFacesListWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List all clock faces for the current user
    ApiResponse<List<ClockFaceListItem>> response = apiInstance.ClockFacesListWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClockFacesApi.ClockFacesListWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;ClockFaceListItem&gt;**](ClockFaceListItem.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of clock faces |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="clockfacesupdate"></a>
# **ClockFacesUpdate**
> ClockFace ClockFacesUpdate (string id, UpdateClockFaceRequest updateClockFaceRequest)

Update an existing clock face (owner only)

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
    public class ClockFacesUpdateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ClockFacesApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | Clock face UUID
            var updateClockFaceRequest = new UpdateClockFaceRequest(); // UpdateClockFaceRequest | Update request

            try
            {
                // Update an existing clock face (owner only)
                ClockFace result = apiInstance.ClockFacesUpdate(id, updateClockFaceRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClockFacesApi.ClockFacesUpdate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClockFacesUpdateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update an existing clock face (owner only)
    ApiResponse<ClockFace> response = apiInstance.ClockFacesUpdateWithHttpInfo(id, updateClockFaceRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClockFacesApi.ClockFacesUpdateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Clock face UUID |  |
| **updateClockFaceRequest** | [**UpdateClockFaceRequest**](UpdateClockFaceRequest.md) | Update request |  |

### Return type

[**ClockFace**](ClockFace.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Updated clock face |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

