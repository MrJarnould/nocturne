# NightscoutFoundation.Nocturne.Api.V4ConnectorSettingsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**MyFitnessPalSettingsGetSettings**](V4ConnectorSettingsApi.md#myfitnesspalsettingsgetsettings) | **GET** /api/v4/connectors/myfitnesspal/settings | Get global MyFitnessPal matching settings. |
| [**MyFitnessPalSettingsSaveSettings**](V4ConnectorSettingsApi.md#myfitnesspalsettingssavesettings) | **PUT** /api/v4/connectors/myfitnesspal/settings | Update global MyFitnessPal matching settings. |

<a id="myfitnesspalsettingsgetsettings"></a>
# **MyFitnessPalSettingsGetSettings**
> MyFitnessPalMatchingSettings MyFitnessPalSettingsGetSettings ()

Get global MyFitnessPal matching settings.

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
    public class MyFitnessPalSettingsGetSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ConnectorSettingsApi(httpClient, config, httpClientHandler);

            try
            {
                // Get global MyFitnessPal matching settings.
                MyFitnessPalMatchingSettings result = apiInstance.MyFitnessPalSettingsGetSettings();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ConnectorSettingsApi.MyFitnessPalSettingsGetSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MyFitnessPalSettingsGetSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get global MyFitnessPal matching settings.
    ApiResponse<MyFitnessPalMatchingSettings> response = apiInstance.MyFitnessPalSettingsGetSettingsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ConnectorSettingsApi.MyFitnessPalSettingsGetSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**MyFitnessPalMatchingSettings**](MyFitnessPalMatchingSettings.md)

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

<a id="myfitnesspalsettingssavesettings"></a>
# **MyFitnessPalSettingsSaveSettings**
> MyFitnessPalMatchingSettings MyFitnessPalSettingsSaveSettings (MyFitnessPalMatchingSettings myFitnessPalMatchingSettings)

Update global MyFitnessPal matching settings.

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
    public class MyFitnessPalSettingsSaveSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ConnectorSettingsApi(httpClient, config, httpClientHandler);
            var myFitnessPalMatchingSettings = new MyFitnessPalMatchingSettings(); // MyFitnessPalMatchingSettings | 

            try
            {
                // Update global MyFitnessPal matching settings.
                MyFitnessPalMatchingSettings result = apiInstance.MyFitnessPalSettingsSaveSettings(myFitnessPalMatchingSettings);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ConnectorSettingsApi.MyFitnessPalSettingsSaveSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MyFitnessPalSettingsSaveSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update global MyFitnessPal matching settings.
    ApiResponse<MyFitnessPalMatchingSettings> response = apiInstance.MyFitnessPalSettingsSaveSettingsWithHttpInfo(myFitnessPalMatchingSettings);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ConnectorSettingsApi.MyFitnessPalSettingsSaveSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **myFitnessPalMatchingSettings** | [**MyFitnessPalMatchingSettings**](MyFitnessPalMatchingSettings.md) |  |  |

### Return type

[**MyFitnessPalMatchingSettings**](MyFitnessPalMatchingSettings.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

