# NightscoutFoundation.Nocturne.Api.DeviceAgeApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DeviceAgeGetAllDeviceAges**](DeviceAgeApi.md#deviceagegetalldeviceages) | **GET** /api/v4/deviceage/all | Get all device ages in a single call |
| [**DeviceAgeGetBatteryAge**](DeviceAgeApi.md#deviceagegetbatteryage) | **GET** /api/v4/deviceage/battery | Get pump battery age (BAGE) |
| [**DeviceAgeGetCannulaAge**](DeviceAgeApi.md#deviceagegetcannulaage) | **GET** /api/v4/deviceage/cannula | Get cannula/site age (CAGE) |
| [**DeviceAgeGetInsulinAge**](DeviceAgeApi.md#deviceagegetinsulinage) | **GET** /api/v4/deviceage/insulin | Get insulin reservoir age (IAGE) |
| [**DeviceAgeGetSensorAge**](DeviceAgeApi.md#deviceagegetsensorage) | **GET** /api/v4/deviceage/sensor | Get sensor age (SAGE) Returns both Sensor Start and Sensor Change events |

<a id="deviceagegetalldeviceages"></a>
# **DeviceAgeGetAllDeviceAges**
> void DeviceAgeGetAllDeviceAges ()

Get all device ages in a single call

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
    public class DeviceAgeGetAllDeviceAgesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeviceAgeApi(httpClient, config, httpClientHandler);

            try
            {
                // Get all device ages in a single call
                apiInstance.DeviceAgeGetAllDeviceAges();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceAgeApi.DeviceAgeGetAllDeviceAges: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceAgeGetAllDeviceAgesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all device ages in a single call
    apiInstance.DeviceAgeGetAllDeviceAgesWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeviceAgeApi.DeviceAgeGetAllDeviceAgesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deviceagegetbatteryage"></a>
# **DeviceAgeGetBatteryAge**
> DeviceAgeInfo DeviceAgeGetBatteryAge (int? info = null, int? warn = null, int? urgent = null, string? display = null, bool? enableAlerts = null)

Get pump battery age (BAGE)

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
    public class DeviceAgeGetBatteryAgeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeviceAgeApi(httpClient, config, httpClientHandler);
            var info = 56;  // int? |  (optional) 
            var warn = 56;  // int? |  (optional) 
            var urgent = 56;  // int? |  (optional) 
            var display = "display_example";  // string? |  (optional) 
            var enableAlerts = true;  // bool? |  (optional) 

            try
            {
                // Get pump battery age (BAGE)
                DeviceAgeInfo result = apiInstance.DeviceAgeGetBatteryAge(info, warn, urgent, display, enableAlerts);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceAgeApi.DeviceAgeGetBatteryAge: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceAgeGetBatteryAgeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get pump battery age (BAGE)
    ApiResponse<DeviceAgeInfo> response = apiInstance.DeviceAgeGetBatteryAgeWithHttpInfo(info, warn, urgent, display, enableAlerts);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeviceAgeApi.DeviceAgeGetBatteryAgeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **info** | **int?** |  | [optional]  |
| **warn** | **int?** |  | [optional]  |
| **urgent** | **int?** |  | [optional]  |
| **display** | **string?** |  | [optional]  |
| **enableAlerts** | **bool?** |  | [optional]  |

### Return type

[**DeviceAgeInfo**](DeviceAgeInfo.md)

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

<a id="deviceagegetcannulaage"></a>
# **DeviceAgeGetCannulaAge**
> DeviceAgeInfo DeviceAgeGetCannulaAge (int? info = null, int? warn = null, int? urgent = null, string? display = null, bool? enableAlerts = null)

Get cannula/site age (CAGE)

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
    public class DeviceAgeGetCannulaAgeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeviceAgeApi(httpClient, config, httpClientHandler);
            var info = 56;  // int? |  (optional) 
            var warn = 56;  // int? |  (optional) 
            var urgent = 56;  // int? |  (optional) 
            var display = "display_example";  // string? |  (optional) 
            var enableAlerts = true;  // bool? |  (optional) 

            try
            {
                // Get cannula/site age (CAGE)
                DeviceAgeInfo result = apiInstance.DeviceAgeGetCannulaAge(info, warn, urgent, display, enableAlerts);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceAgeApi.DeviceAgeGetCannulaAge: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceAgeGetCannulaAgeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get cannula/site age (CAGE)
    ApiResponse<DeviceAgeInfo> response = apiInstance.DeviceAgeGetCannulaAgeWithHttpInfo(info, warn, urgent, display, enableAlerts);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeviceAgeApi.DeviceAgeGetCannulaAgeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **info** | **int?** |  | [optional]  |
| **warn** | **int?** |  | [optional]  |
| **urgent** | **int?** |  | [optional]  |
| **display** | **string?** |  | [optional]  |
| **enableAlerts** | **bool?** |  | [optional]  |

### Return type

[**DeviceAgeInfo**](DeviceAgeInfo.md)

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

<a id="deviceagegetinsulinage"></a>
# **DeviceAgeGetInsulinAge**
> DeviceAgeInfo DeviceAgeGetInsulinAge (int? info = null, int? warn = null, int? urgent = null, string? display = null, bool? enableAlerts = null)

Get insulin reservoir age (IAGE)

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
    public class DeviceAgeGetInsulinAgeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeviceAgeApi(httpClient, config, httpClientHandler);
            var info = 56;  // int? |  (optional) 
            var warn = 56;  // int? |  (optional) 
            var urgent = 56;  // int? |  (optional) 
            var display = "display_example";  // string? |  (optional) 
            var enableAlerts = true;  // bool? |  (optional) 

            try
            {
                // Get insulin reservoir age (IAGE)
                DeviceAgeInfo result = apiInstance.DeviceAgeGetInsulinAge(info, warn, urgent, display, enableAlerts);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceAgeApi.DeviceAgeGetInsulinAge: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceAgeGetInsulinAgeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get insulin reservoir age (IAGE)
    ApiResponse<DeviceAgeInfo> response = apiInstance.DeviceAgeGetInsulinAgeWithHttpInfo(info, warn, urgent, display, enableAlerts);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeviceAgeApi.DeviceAgeGetInsulinAgeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **info** | **int?** |  | [optional]  |
| **warn** | **int?** |  | [optional]  |
| **urgent** | **int?** |  | [optional]  |
| **display** | **string?** |  | [optional]  |
| **enableAlerts** | **bool?** |  | [optional]  |

### Return type

[**DeviceAgeInfo**](DeviceAgeInfo.md)

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

<a id="deviceagegetsensorage"></a>
# **DeviceAgeGetSensorAge**
> SensorAgeInfo DeviceAgeGetSensorAge (int? info = null, int? warn = null, int? urgent = null, string? display = null, bool? enableAlerts = null)

Get sensor age (SAGE) Returns both Sensor Start and Sensor Change events

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
    public class DeviceAgeGetSensorAgeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeviceAgeApi(httpClient, config, httpClientHandler);
            var info = 56;  // int? |  (optional) 
            var warn = 56;  // int? |  (optional) 
            var urgent = 56;  // int? |  (optional) 
            var display = "display_example";  // string? |  (optional) 
            var enableAlerts = true;  // bool? |  (optional) 

            try
            {
                // Get sensor age (SAGE) Returns both Sensor Start and Sensor Change events
                SensorAgeInfo result = apiInstance.DeviceAgeGetSensorAge(info, warn, urgent, display, enableAlerts);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceAgeApi.DeviceAgeGetSensorAge: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeviceAgeGetSensorAgeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get sensor age (SAGE) Returns both Sensor Start and Sensor Change events
    ApiResponse<SensorAgeInfo> response = apiInstance.DeviceAgeGetSensorAgeWithHttpInfo(info, warn, urgent, display, enableAlerts);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeviceAgeApi.DeviceAgeGetSensorAgeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **info** | **int?** |  | [optional]  |
| **warn** | **int?** |  | [optional]  |
| **urgent** | **int?** |  | [optional]  |
| **display** | **string?** |  | [optional]  |
| **enableAlerts** | **bool?** |  | [optional]  |

### Return type

[**SensorAgeInfo**](SensorAgeInfo.md)

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

