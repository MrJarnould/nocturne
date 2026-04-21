# NightscoutFoundation.Nocturne.Api.V4AlertSoundsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AlertCustomSoundsDeleteSound**](V4AlertSoundsApi.md#alertcustomsoundsdeletesound) | **DELETE** /api/v4/alert-sounds/{id} | Delete a custom sound. |
| [**AlertCustomSoundsGetSounds**](V4AlertSoundsApi.md#alertcustomsoundsgetsounds) | **GET** /api/v4/alert-sounds | List all custom sounds for the current tenant (metadata only, no audio data). |
| [**AlertCustomSoundsStreamSound**](V4AlertSoundsApi.md#alertcustomsoundsstreamsound) | **GET** /api/v4/alert-sounds/{id}/stream | Stream the raw audio bytes for a custom sound. |
| [**AlertCustomSoundsUploadSound**](V4AlertSoundsApi.md#alertcustomsoundsuploadsound) | **POST** /api/v4/alert-sounds | Upload a custom alert sound file. |

<a id="alertcustomsoundsdeletesound"></a>
# **AlertCustomSoundsDeleteSound**
> void AlertCustomSoundsDeleteSound (string id)

Delete a custom sound.

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
    public class AlertCustomSoundsDeleteSoundExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertSoundsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a custom sound.
                apiInstance.AlertCustomSoundsDeleteSound(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertSoundsApi.AlertCustomSoundsDeleteSound: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertCustomSoundsDeleteSoundWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a custom sound.
    apiInstance.AlertCustomSoundsDeleteSoundWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertSoundsApi.AlertCustomSoundsDeleteSoundWithHttpInfo: " + e.Message);
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

<a id="alertcustomsoundsgetsounds"></a>
# **AlertCustomSoundsGetSounds**
> List&lt;AlertCustomSoundResponse&gt; AlertCustomSoundsGetSounds ()

List all custom sounds for the current tenant (metadata only, no audio data).

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
    public class AlertCustomSoundsGetSoundsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertSoundsApi(httpClient, config, httpClientHandler);

            try
            {
                // List all custom sounds for the current tenant (metadata only, no audio data).
                List<AlertCustomSoundResponse> result = apiInstance.AlertCustomSoundsGetSounds();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertSoundsApi.AlertCustomSoundsGetSounds: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertCustomSoundsGetSoundsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List all custom sounds for the current tenant (metadata only, no audio data).
    ApiResponse<List<AlertCustomSoundResponse>> response = apiInstance.AlertCustomSoundsGetSoundsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertSoundsApi.AlertCustomSoundsGetSoundsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;AlertCustomSoundResponse&gt;**](AlertCustomSoundResponse.md)

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

<a id="alertcustomsoundsstreamsound"></a>
# **AlertCustomSoundsStreamSound**
> void AlertCustomSoundsStreamSound (string id)

Stream the raw audio bytes for a custom sound.

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
    public class AlertCustomSoundsStreamSoundExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertSoundsApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Stream the raw audio bytes for a custom sound.
                apiInstance.AlertCustomSoundsStreamSound(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertSoundsApi.AlertCustomSoundsStreamSound: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertCustomSoundsStreamSoundWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Stream the raw audio bytes for a custom sound.
    apiInstance.AlertCustomSoundsStreamSoundWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertSoundsApi.AlertCustomSoundsStreamSoundWithHttpInfo: " + e.Message);
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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="alertcustomsoundsuploadsound"></a>
# **AlertCustomSoundsUploadSound**
> AlertCustomSoundResponse AlertCustomSoundsUploadSound (string? contentType = null, string? contentDisposition = null, List<Object>? headers = null, long? length = null, string? name = null, string? fileName = null)

Upload a custom alert sound file.

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
    public class AlertCustomSoundsUploadSoundExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4AlertSoundsApi(httpClient, config, httpClientHandler);
            var contentType = "contentType_example";  // string? |  (optional) 
            var contentDisposition = "contentDisposition_example";  // string? |  (optional) 
            var headers = new List<Object>?(); // List<Object>? |  (optional) 
            var length = 789L;  // long? |  (optional) 
            var name = "name_example";  // string? |  (optional) 
            var fileName = "fileName_example";  // string? |  (optional) 

            try
            {
                // Upload a custom alert sound file.
                AlertCustomSoundResponse result = apiInstance.AlertCustomSoundsUploadSound(contentType, contentDisposition, headers, length, name, fileName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AlertSoundsApi.AlertCustomSoundsUploadSound: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AlertCustomSoundsUploadSoundWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Upload a custom alert sound file.
    ApiResponse<AlertCustomSoundResponse> response = apiInstance.AlertCustomSoundsUploadSoundWithHttpInfo(contentType, contentDisposition, headers, length, name, fileName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AlertSoundsApi.AlertCustomSoundsUploadSoundWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **contentType** | **string?** |  | [optional]  |
| **contentDisposition** | **string?** |  | [optional]  |
| **headers** | [**List&lt;Object&gt;?**](Object.md) |  | [optional]  |
| **length** | **long?** |  | [optional]  |
| **name** | **string?** |  | [optional]  |
| **fileName** | **string?** |  | [optional]  |

### Return type

[**AlertCustomSoundResponse**](AlertCustomSoundResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

