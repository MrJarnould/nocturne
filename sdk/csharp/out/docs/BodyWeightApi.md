# NightscoutFoundation.Nocturne.Api.BodyWeightApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**BodyWeightCreate**](BodyWeightApi.md#bodyweightcreate) | **POST** /api/v4/body-weight | Create a single body weight record |
| [**BodyWeightCreateBodyWeights**](BodyWeightApi.md#bodyweightcreatebodyweights) | **POST** /api/v4/body-weight/batch | Create one or more body weight records (single object or array) |
| [**BodyWeightDeleteBodyWeight**](BodyWeightApi.md#bodyweightdeletebodyweight) | **DELETE** /api/v4/body-weight/{id} | Delete a body weight record by ID |
| [**BodyWeightGetBodyWeight**](BodyWeightApi.md#bodyweightgetbodyweight) | **GET** /api/v4/body-weight/{id} | Get a specific body weight record by ID |
| [**BodyWeightGetBodyWeights**](BodyWeightApi.md#bodyweightgetbodyweights) | **GET** /api/v4/body-weight | Get body weight records with optional pagination |
| [**BodyWeightUpdateBodyWeight**](BodyWeightApi.md#bodyweightupdatebodyweight) | **PUT** /api/v4/body-weight/{id} | Update an existing body weight record |

<a id="bodyweightcreate"></a>
# **BodyWeightCreate**
> BodyWeight BodyWeightCreate (BodyWeight bodyWeight)

Create a single body weight record

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
    public class BodyWeightCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BodyWeightApi(httpClient, config, httpClientHandler);
            var bodyWeight = new BodyWeight(); // BodyWeight | 

            try
            {
                // Create a single body weight record
                BodyWeight result = apiInstance.BodyWeightCreate(bodyWeight);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BodyWeightApi.BodyWeightCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BodyWeightCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a single body weight record
    ApiResponse<BodyWeight> response = apiInstance.BodyWeightCreateWithHttpInfo(bodyWeight);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BodyWeightApi.BodyWeightCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **bodyWeight** | [**BodyWeight**](BodyWeight.md) |  |  |

### Return type

[**BodyWeight**](BodyWeight.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="bodyweightcreatebodyweights"></a>
# **BodyWeightCreateBodyWeights**
> List&lt;BodyWeight&gt; BodyWeightCreateBodyWeights (Object body)

Create one or more body weight records (single object or array)

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
    public class BodyWeightCreateBodyWeightsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BodyWeightApi(httpClient, config, httpClientHandler);
            var body = null;  // Object | 

            try
            {
                // Create one or more body weight records (single object or array)
                List<BodyWeight> result = apiInstance.BodyWeightCreateBodyWeights(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BodyWeightApi.BodyWeightCreateBodyWeights: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BodyWeightCreateBodyWeightsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create one or more body weight records (single object or array)
    ApiResponse<List<BodyWeight>> response = apiInstance.BodyWeightCreateBodyWeightsWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BodyWeightApi.BodyWeightCreateBodyWeightsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | **Object** |  |  |

### Return type

[**List&lt;BodyWeight&gt;**](BodyWeight.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="bodyweightdeletebodyweight"></a>
# **BodyWeightDeleteBodyWeight**
> void BodyWeightDeleteBodyWeight (string id)

Delete a body weight record by ID

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
    public class BodyWeightDeleteBodyWeightExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BodyWeightApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a body weight record by ID
                apiInstance.BodyWeightDeleteBodyWeight(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BodyWeightApi.BodyWeightDeleteBodyWeight: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BodyWeightDeleteBodyWeightWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a body weight record by ID
    apiInstance.BodyWeightDeleteBodyWeightWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BodyWeightApi.BodyWeightDeleteBodyWeightWithHttpInfo: " + e.Message);
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
| **200** |  |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="bodyweightgetbodyweight"></a>
# **BodyWeightGetBodyWeight**
> BodyWeight BodyWeightGetBodyWeight (string id)

Get a specific body weight record by ID

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
    public class BodyWeightGetBodyWeightExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BodyWeightApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | Record ID

            try
            {
                // Get a specific body weight record by ID
                BodyWeight result = apiInstance.BodyWeightGetBodyWeight(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BodyWeightApi.BodyWeightGetBodyWeight: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BodyWeightGetBodyWeightWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a specific body weight record by ID
    ApiResponse<BodyWeight> response = apiInstance.BodyWeightGetBodyWeightWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BodyWeightApi.BodyWeightGetBodyWeightWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Record ID |  |

### Return type

[**BodyWeight**](BodyWeight.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="bodyweightgetbodyweights"></a>
# **BodyWeightGetBodyWeights**
> List&lt;BodyWeight&gt; BodyWeightGetBodyWeights (int? count = null, int? skip = null)

Get body weight records with optional pagination

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
    public class BodyWeightGetBodyWeightsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BodyWeightApi(httpClient, config, httpClientHandler);
            var count = 10;  // int? | Maximum number of records to return (default: 10) (optional)  (default to 10)
            var skip = 0;  // int? | Number of records to skip for pagination (default: 0) (optional)  (default to 0)

            try
            {
                // Get body weight records with optional pagination
                List<BodyWeight> result = apiInstance.BodyWeightGetBodyWeights(count, skip);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BodyWeightApi.BodyWeightGetBodyWeights: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BodyWeightGetBodyWeightsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get body weight records with optional pagination
    ApiResponse<List<BodyWeight>> response = apiInstance.BodyWeightGetBodyWeightsWithHttpInfo(count, skip);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BodyWeightApi.BodyWeightGetBodyWeightsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **count** | **int?** | Maximum number of records to return (default: 10) | [optional] [default to 10] |
| **skip** | **int?** | Number of records to skip for pagination (default: 0) | [optional] [default to 0] |

### Return type

[**List&lt;BodyWeight&gt;**](BodyWeight.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of body weight records ordered by most recent first |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="bodyweightupdatebodyweight"></a>
# **BodyWeightUpdateBodyWeight**
> BodyWeight BodyWeightUpdateBodyWeight (string id, BodyWeight bodyWeight)

Update an existing body weight record

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
    public class BodyWeightUpdateBodyWeightExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BodyWeightApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var bodyWeight = new BodyWeight(); // BodyWeight | 

            try
            {
                // Update an existing body weight record
                BodyWeight result = apiInstance.BodyWeightUpdateBodyWeight(id, bodyWeight);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BodyWeightApi.BodyWeightUpdateBodyWeight: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BodyWeightUpdateBodyWeightWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update an existing body weight record
    ApiResponse<BodyWeight> response = apiInstance.BodyWeightUpdateBodyWeightWithHttpInfo(id, bodyWeight);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BodyWeightApi.BodyWeightUpdateBodyWeightWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **bodyWeight** | [**BodyWeight**](BodyWeight.md) |  |  |

### Return type

[**BodyWeight**](BodyWeight.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

