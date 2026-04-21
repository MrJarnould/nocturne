# NightscoutFoundation.Nocturne.Api.BatteryApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**BatteryGetBatteryReadings**](BatteryApi.md#batterygetbatteryreadings) | **GET** /api/v4/Battery/readings | Get battery readings for a device over a time period |
| [**BatteryGetBatteryStatistics**](BatteryApi.md#batterygetbatterystatistics) | **GET** /api/v4/Battery/statistics | Get battery statistics for a device or all devices |
| [**BatteryGetChargeCycles**](BatteryApi.md#batterygetchargecycles) | **GET** /api/v4/Battery/cycles | Get charge cycle history for a device |
| [**BatteryGetCurrentBatteryStatus**](BatteryApi.md#batterygetcurrentbatterystatus) | **GET** /api/v4/Battery/current | Get the current battery status for all tracked devices |
| [**BatteryGetKnownDevices**](BatteryApi.md#batterygetknowndevices) | **GET** /api/v4/Battery/devices | Get list of all known devices with battery data |

<a id="batterygetbatteryreadings"></a>
# **BatteryGetBatteryReadings**
> List&lt;BatteryReading&gt; BatteryGetBatteryReadings (string? device = null, DateTimeOffset? from = null, DateTimeOffset? to = null)

Get battery readings for a device over a time period

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
    public class BatteryGetBatteryReadingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BatteryApi(httpClient, config, httpClientHandler);
            var device = "device_example";  // string? | Device identifier (optional, returns all devices if not specified) (optional) 
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | Start time in milliseconds since Unix epoch (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | End time in milliseconds since Unix epoch (optional) 

            try
            {
                // Get battery readings for a device over a time period
                List<BatteryReading> result = apiInstance.BatteryGetBatteryReadings(device, from, to);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BatteryApi.BatteryGetBatteryReadings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BatteryGetBatteryReadingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get battery readings for a device over a time period
    ApiResponse<List<BatteryReading>> response = apiInstance.BatteryGetBatteryReadingsWithHttpInfo(device, from, to);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BatteryApi.BatteryGetBatteryReadingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **device** | **string?** | Device identifier (optional, returns all devices if not specified) | [optional]  |
| **from** | **DateTimeOffset?** | Start time in milliseconds since Unix epoch | [optional]  |
| **to** | **DateTimeOffset?** | End time in milliseconds since Unix epoch | [optional]  |

### Return type

[**List&lt;BatteryReading&gt;**](BatteryReading.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Battery readings for the specified period |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="batterygetbatterystatistics"></a>
# **BatteryGetBatteryStatistics**
> List&lt;BatteryStatistics&gt; BatteryGetBatteryStatistics (string? device = null, DateTimeOffset? from = null, DateTimeOffset? to = null)

Get battery statistics for a device or all devices

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
    public class BatteryGetBatteryStatisticsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BatteryApi(httpClient, config, httpClientHandler);
            var device = "device_example";  // string? | Device identifier (optional, returns all devices if not specified) (optional) 
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | Start time in milliseconds since Unix epoch (default: 7 days ago) (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | End time in milliseconds since Unix epoch (default: now) (optional) 

            try
            {
                // Get battery statistics for a device or all devices
                List<BatteryStatistics> result = apiInstance.BatteryGetBatteryStatistics(device, from, to);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BatteryApi.BatteryGetBatteryStatistics: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BatteryGetBatteryStatisticsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get battery statistics for a device or all devices
    ApiResponse<List<BatteryStatistics>> response = apiInstance.BatteryGetBatteryStatisticsWithHttpInfo(device, from, to);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BatteryApi.BatteryGetBatteryStatisticsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **device** | **string?** | Device identifier (optional, returns all devices if not specified) | [optional]  |
| **from** | **DateTimeOffset?** | Start time in milliseconds since Unix epoch (default: 7 days ago) | [optional]  |
| **to** | **DateTimeOffset?** | End time in milliseconds since Unix epoch (default: now) | [optional]  |

### Return type

[**List&lt;BatteryStatistics&gt;**](BatteryStatistics.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Battery statistics for the specified period |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="batterygetchargecycles"></a>
# **BatteryGetChargeCycles**
> List&lt;ChargeCycle&gt; BatteryGetChargeCycles (string? device = null, DateTimeOffset? from = null, DateTimeOffset? to = null, int? limit = null)

Get charge cycle history for a device

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
    public class BatteryGetChargeCyclesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BatteryApi(httpClient, config, httpClientHandler);
            var device = "device_example";  // string? | Device identifier (optional, returns all devices if not specified) (optional) 
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | Start time in milliseconds since Unix epoch (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | End time in milliseconds since Unix epoch (optional) 
            var limit = 100;  // int? | Maximum number of cycles to return (default: 100) (optional)  (default to 100)

            try
            {
                // Get charge cycle history for a device
                List<ChargeCycle> result = apiInstance.BatteryGetChargeCycles(device, from, to, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BatteryApi.BatteryGetChargeCycles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BatteryGetChargeCyclesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get charge cycle history for a device
    ApiResponse<List<ChargeCycle>> response = apiInstance.BatteryGetChargeCyclesWithHttpInfo(device, from, to, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BatteryApi.BatteryGetChargeCyclesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **device** | **string?** | Device identifier (optional, returns all devices if not specified) | [optional]  |
| **from** | **DateTimeOffset?** | Start time in milliseconds since Unix epoch | [optional]  |
| **to** | **DateTimeOffset?** | End time in milliseconds since Unix epoch | [optional]  |
| **limit** | **int?** | Maximum number of cycles to return (default: 100) | [optional] [default to 100] |

### Return type

[**List&lt;ChargeCycle&gt;**](ChargeCycle.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Charge cycles for the specified period |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="batterygetcurrentbatterystatus"></a>
# **BatteryGetCurrentBatteryStatus**
> CurrentBatteryStatus BatteryGetCurrentBatteryStatus (int? recentMinutes = null)

Get the current battery status for all tracked devices

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
    public class BatteryGetCurrentBatteryStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BatteryApi(httpClient, config, httpClientHandler);
            var recentMinutes = 30;  // int? | How many minutes back to consider for \"recent\" readings (default: 30) (optional)  (default to 30)

            try
            {
                // Get the current battery status for all tracked devices
                CurrentBatteryStatus result = apiInstance.BatteryGetCurrentBatteryStatus(recentMinutes);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BatteryApi.BatteryGetCurrentBatteryStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BatteryGetCurrentBatteryStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get the current battery status for all tracked devices
    ApiResponse<CurrentBatteryStatus> response = apiInstance.BatteryGetCurrentBatteryStatusWithHttpInfo(recentMinutes);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BatteryApi.BatteryGetCurrentBatteryStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **recentMinutes** | **int?** | How many minutes back to consider for \&quot;recent\&quot; readings (default: 30) | [optional] [default to 30] |

### Return type

[**CurrentBatteryStatus**](CurrentBatteryStatus.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Current battery status for all devices |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="batterygetknowndevices"></a>
# **BatteryGetKnownDevices**
> List&lt;string&gt; BatteryGetKnownDevices ()

Get list of all known devices with battery data

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
    public class BatteryGetKnownDevicesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BatteryApi(httpClient, config, httpClientHandler);

            try
            {
                // Get list of all known devices with battery data
                List<string> result = apiInstance.BatteryGetKnownDevices();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BatteryApi.BatteryGetKnownDevices: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BatteryGetKnownDevicesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get list of all known devices with battery data
    ApiResponse<List<string>> response = apiInstance.BatteryGetKnownDevicesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BatteryApi.BatteryGetKnownDevicesWithHttpInfo: " + e.Message);
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
| **200** | List of device identifiers |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

