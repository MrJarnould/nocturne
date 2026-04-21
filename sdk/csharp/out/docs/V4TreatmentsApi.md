# NightscoutFoundation.Nocturne.Api.V4TreatmentsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**TreatmentsCreateTreatment**](V4TreatmentsApi.md#treatmentscreatetreatment) | **POST** /api/v4/treatments | Create a treatment with tracker integration. If the treatment&#39;s event type matches a tracker&#39;s trigger event types, the tracker instance will be automatically started/restarted. |
| [**TreatmentsCreateTreatments**](V4TreatmentsApi.md#treatmentscreatetreatments) | **POST** /api/v4/treatments/bulk | Create multiple treatments with tracker integration. |
| [**TreatmentsDeleteTreatment**](V4TreatmentsApi.md#treatmentsdeletetreatment) | **DELETE** /api/v4/treatments/{id} | Delete a treatment by ID |
| [**TreatmentsGetTreatment**](V4TreatmentsApi.md#treatmentsgettreatment) | **GET** /api/v4/treatments/{id} | Get a specific treatment by ID |
| [**TreatmentsGetTreatments**](V4TreatmentsApi.md#treatmentsgettreatments) | **GET** /api/v4/treatments | Get treatments with optional filtering and pagination. Unlike V1-V3 endpoints, this does NOT include StateSpan-derived basal data. For basal delivery, query /api/v4/state-spans?category&#x3D;BasalDelivery instead. |
| [**TreatmentsUpdateTreatment**](V4TreatmentsApi.md#treatmentsupdatetreatment) | **PUT** /api/v4/treatments/{id} | Update an existing treatment by ID |

<a id="treatmentscreatetreatment"></a>
# **TreatmentsCreateTreatment**
> Treatment TreatmentsCreateTreatment (Treatment treatment)

Create a treatment with tracker integration. If the treatment's event type matches a tracker's trigger event types, the tracker instance will be automatically started/restarted.

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
    public class TreatmentsCreateTreatmentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TreatmentsApi(httpClient, config, httpClientHandler);
            var treatment = new Treatment(); // Treatment | 

            try
            {
                // Create a treatment with tracker integration. If the treatment's event type matches a tracker's trigger event types, the tracker instance will be automatically started/restarted.
                Treatment result = apiInstance.TreatmentsCreateTreatment(treatment);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsCreateTreatment: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TreatmentsCreateTreatmentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a treatment with tracker integration. If the treatment's event type matches a tracker's trigger event types, the tracker instance will be automatically started/restarted.
    ApiResponse<Treatment> response = apiInstance.TreatmentsCreateTreatmentWithHttpInfo(treatment);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsCreateTreatmentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **treatment** | [**Treatment**](Treatment.md) |  |  |

### Return type

[**Treatment**](Treatment.md)

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

<a id="treatmentscreatetreatments"></a>
# **TreatmentsCreateTreatments**
> List&lt;Treatment&gt; TreatmentsCreateTreatments (List<Treatment> treatment)

Create multiple treatments with tracker integration.

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
    public class TreatmentsCreateTreatmentsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TreatmentsApi(httpClient, config, httpClientHandler);
            var treatment = new List<Treatment>(); // List<Treatment> | 

            try
            {
                // Create multiple treatments with tracker integration.
                List<Treatment> result = apiInstance.TreatmentsCreateTreatments(treatment);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsCreateTreatments: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TreatmentsCreateTreatmentsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create multiple treatments with tracker integration.
    ApiResponse<List<Treatment>> response = apiInstance.TreatmentsCreateTreatmentsWithHttpInfo(treatment);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsCreateTreatmentsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **treatment** | [**List&lt;Treatment&gt;**](Treatment.md) |  |  |

### Return type

[**List&lt;Treatment&gt;**](Treatment.md)

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

<a id="treatmentsdeletetreatment"></a>
# **TreatmentsDeleteTreatment**
> void TreatmentsDeleteTreatment (string id)

Delete a treatment by ID

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
    public class TreatmentsDeleteTreatmentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TreatmentsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a treatment by ID
                apiInstance.TreatmentsDeleteTreatment(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsDeleteTreatment: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TreatmentsDeleteTreatmentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a treatment by ID
    apiInstance.TreatmentsDeleteTreatmentWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsDeleteTreatmentWithHttpInfo: " + e.Message);
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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="treatmentsgettreatment"></a>
# **TreatmentsGetTreatment**
> Treatment TreatmentsGetTreatment (string id)

Get a specific treatment by ID

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
    public class TreatmentsGetTreatmentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TreatmentsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Get a specific treatment by ID
                Treatment result = apiInstance.TreatmentsGetTreatment(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsGetTreatment: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TreatmentsGetTreatmentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a specific treatment by ID
    ApiResponse<Treatment> response = apiInstance.TreatmentsGetTreatmentWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsGetTreatmentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**Treatment**](Treatment.md)

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

<a id="treatmentsgettreatments"></a>
# **TreatmentsGetTreatments**
> List&lt;Treatment&gt; TreatmentsGetTreatments (string? eventType = null, int? count = null, int? skip = null, string? find = null)

Get treatments with optional filtering and pagination. Unlike V1-V3 endpoints, this does NOT include StateSpan-derived basal data. For basal delivery, query /api/v4/state-spans?category=BasalDelivery instead.

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
    public class TreatmentsGetTreatmentsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TreatmentsApi(httpClient, config, httpClientHandler);
            var eventType = "eventType_example";  // string? | Optional filter by event type (optional) 
            var count = 100;  // int? | Maximum number of treatments to return (default: 100) (optional)  (default to 100)
            var skip = 0;  // int? | Number of treatments to skip for pagination (default: 0) (optional)  (default to 0)
            var find = "find_example";  // string? | Optional MongoDB-style query filter for advanced filtering (optional) 

            try
            {
                // Get treatments with optional filtering and pagination. Unlike V1-V3 endpoints, this does NOT include StateSpan-derived basal data. For basal delivery, query /api/v4/state-spans?category=BasalDelivery instead.
                List<Treatment> result = apiInstance.TreatmentsGetTreatments(eventType, count, skip, find);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsGetTreatments: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TreatmentsGetTreatmentsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get treatments with optional filtering and pagination. Unlike V1-V3 endpoints, this does NOT include StateSpan-derived basal data. For basal delivery, query /api/v4/state-spans?category=BasalDelivery instead.
    ApiResponse<List<Treatment>> response = apiInstance.TreatmentsGetTreatmentsWithHttpInfo(eventType, count, skip, find);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsGetTreatmentsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **eventType** | **string?** | Optional filter by event type | [optional]  |
| **count** | **int?** | Maximum number of treatments to return (default: 100) | [optional] [default to 100] |
| **skip** | **int?** | Number of treatments to skip for pagination (default: 0) | [optional] [default to 0] |
| **find** | **string?** | Optional MongoDB-style query filter for advanced filtering | [optional]  |

### Return type

[**List&lt;Treatment&gt;**](Treatment.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Array of treatments ordered by most recent first |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="treatmentsupdatetreatment"></a>
# **TreatmentsUpdateTreatment**
> Treatment TreatmentsUpdateTreatment (string id, Treatment treatment)

Update an existing treatment by ID

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
    public class TreatmentsUpdateTreatmentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4TreatmentsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var treatment = new Treatment(); // Treatment | 

            try
            {
                // Update an existing treatment by ID
                Treatment result = apiInstance.TreatmentsUpdateTreatment(id, treatment);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsUpdateTreatment: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TreatmentsUpdateTreatmentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update an existing treatment by ID
    ApiResponse<Treatment> response = apiInstance.TreatmentsUpdateTreatmentWithHttpInfo(id, treatment);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4TreatmentsApi.TreatmentsUpdateTreatmentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **treatment** | [**Treatment**](Treatment.md) |  |  |

### Return type

[**Treatment**](Treatment.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

