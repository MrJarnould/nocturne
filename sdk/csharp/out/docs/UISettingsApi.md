# NightscoutFoundation.Nocturne.Api.UISettingsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**UISettingsAddOrUpdateAlarmProfile**](UISettingsApi.md#uisettingsaddorupdatealarmprofile) | **POST** /api/v4/ui-settings/notifications/alarms/profiles | Save a specific alarm profile. |
| [**UISettingsDeleteAlarmProfile**](UISettingsApi.md#uisettingsdeletealarmprofile) | **DELETE** /api/v4/ui-settings/notifications/alarms/profiles/{profileId} | Delete an alarm profile by ID. |
| [**UISettingsGetAlarmConfiguration**](UISettingsApi.md#uisettingsgetalarmconfiguration) | **GET** /api/v4/ui-settings/notifications/alarms | Get alarm profiles configuration (xDrip+-style). |
| [**UISettingsGetSectionSettings**](UISettingsApi.md#uisettingsgetsectionsettings) | **GET** /api/v4/ui-settings/{section} | Get settings for a specific section. |
| [**UISettingsGetUISettings**](UISettingsApi.md#uisettingsgetuisettings) | **GET** /api/v4/ui-settings | Get all UI settings configuration for frontend settings pages. In demo mode, this fetches from the demo service. |
| [**UISettingsSaveAlarmConfiguration**](UISettingsApi.md#uisettingssavealarmconfiguration) | **PUT** /api/v4/ui-settings/notifications/alarms | Save alarm profiles configuration (xDrip+-style). |
| [**UISettingsSaveNotificationSettings**](UISettingsApi.md#uisettingssavenotificationsettings) | **PUT** /api/v4/ui-settings/notifications | Save notification settings including alarm configuration. |
| [**UISettingsSaveUISettings**](UISettingsApi.md#uisettingssaveuisettings) | **PUT** /api/v4/ui-settings | Save complete UI settings configuration. |

<a id="uisettingsaddorupdatealarmprofile"></a>
# **UISettingsAddOrUpdateAlarmProfile**
> UserAlarmConfiguration UISettingsAddOrUpdateAlarmProfile (AlarmProfileConfiguration alarmProfileConfiguration)

Save a specific alarm profile.

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
    public class UISettingsAddOrUpdateAlarmProfileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UISettingsApi(httpClient, config, httpClientHandler);
            var alarmProfileConfiguration = new AlarmProfileConfiguration(); // AlarmProfileConfiguration | The alarm profile to save

            try
            {
                // Save a specific alarm profile.
                UserAlarmConfiguration result = apiInstance.UISettingsAddOrUpdateAlarmProfile(alarmProfileConfiguration);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UISettingsApi.UISettingsAddOrUpdateAlarmProfile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UISettingsAddOrUpdateAlarmProfileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Save a specific alarm profile.
    ApiResponse<UserAlarmConfiguration> response = apiInstance.UISettingsAddOrUpdateAlarmProfileWithHttpInfo(alarmProfileConfiguration);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UISettingsApi.UISettingsAddOrUpdateAlarmProfileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **alarmProfileConfiguration** | [**AlarmProfileConfiguration**](AlarmProfileConfiguration.md) | The alarm profile to save |  |

### Return type

[**UserAlarmConfiguration**](UserAlarmConfiguration.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The saved alarm configuration |  -  |
| **400** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="uisettingsdeletealarmprofile"></a>
# **UISettingsDeleteAlarmProfile**
> UserAlarmConfiguration UISettingsDeleteAlarmProfile (string profileId)

Delete an alarm profile by ID.

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
    public class UISettingsDeleteAlarmProfileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UISettingsApi(httpClient, config, httpClientHandler);
            var profileId = "profileId_example";  // string | The profile ID to delete

            try
            {
                // Delete an alarm profile by ID.
                UserAlarmConfiguration result = apiInstance.UISettingsDeleteAlarmProfile(profileId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UISettingsApi.UISettingsDeleteAlarmProfile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UISettingsDeleteAlarmProfileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete an alarm profile by ID.
    ApiResponse<UserAlarmConfiguration> response = apiInstance.UISettingsDeleteAlarmProfileWithHttpInfo(profileId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UISettingsApi.UISettingsDeleteAlarmProfileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **profileId** | **string** | The profile ID to delete |  |

### Return type

[**UserAlarmConfiguration**](UserAlarmConfiguration.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The updated alarm configuration |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="uisettingsgetalarmconfiguration"></a>
# **UISettingsGetAlarmConfiguration**
> UserAlarmConfiguration UISettingsGetAlarmConfiguration ()

Get alarm profiles configuration (xDrip+-style).

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
    public class UISettingsGetAlarmConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UISettingsApi(httpClient, config, httpClientHandler);

            try
            {
                // Get alarm profiles configuration (xDrip+-style).
                UserAlarmConfiguration result = apiInstance.UISettingsGetAlarmConfiguration();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UISettingsApi.UISettingsGetAlarmConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UISettingsGetAlarmConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get alarm profiles configuration (xDrip+-style).
    ApiResponse<UserAlarmConfiguration> response = apiInstance.UISettingsGetAlarmConfigurationWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UISettingsApi.UISettingsGetAlarmConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**UserAlarmConfiguration**](UserAlarmConfiguration.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The alarm configuration |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="uisettingsgetsectionsettings"></a>
# **UISettingsGetSectionSettings**
> void UISettingsGetSectionSettings (string section)

Get settings for a specific section.

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
    public class UISettingsGetSectionSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UISettingsApi(httpClient, config, httpClientHandler);
            var section = "section_example";  // string | Section name: devices, therapy, algorithm, features, notifications, or services

            try
            {
                // Get settings for a specific section.
                apiInstance.UISettingsGetSectionSettings(section);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UISettingsApi.UISettingsGetSectionSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UISettingsGetSectionSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get settings for a specific section.
    apiInstance.UISettingsGetSectionSettingsWithHttpInfo(section);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UISettingsApi.UISettingsGetSectionSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **section** | **string** | Section name: devices, therapy, algorithm, features, notifications, or services |  |

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
| **200** | Settings for the specified section |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="uisettingsgetuisettings"></a>
# **UISettingsGetUISettings**
> UISettingsConfiguration UISettingsGetUISettings ()

Get all UI settings configuration for frontend settings pages. In demo mode, this fetches from the demo service.

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
    public class UISettingsGetUISettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UISettingsApi(httpClient, config, httpClientHandler);

            try
            {
                // Get all UI settings configuration for frontend settings pages. In demo mode, this fetches from the demo service.
                UISettingsConfiguration result = apiInstance.UISettingsGetUISettings();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UISettingsApi.UISettingsGetUISettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UISettingsGetUISettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all UI settings configuration for frontend settings pages. In demo mode, this fetches from the demo service.
    ApiResponse<UISettingsConfiguration> response = apiInstance.UISettingsGetUISettingsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UISettingsApi.UISettingsGetUISettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**UISettingsConfiguration**](UISettingsConfiguration.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Complete UI settings configuration |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="uisettingssavealarmconfiguration"></a>
# **UISettingsSaveAlarmConfiguration**
> UserAlarmConfiguration UISettingsSaveAlarmConfiguration (UserAlarmConfiguration userAlarmConfiguration)

Save alarm profiles configuration (xDrip+-style).

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
    public class UISettingsSaveAlarmConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UISettingsApi(httpClient, config, httpClientHandler);
            var userAlarmConfiguration = new UserAlarmConfiguration(); // UserAlarmConfiguration | The alarm configuration to save

            try
            {
                // Save alarm profiles configuration (xDrip+-style).
                UserAlarmConfiguration result = apiInstance.UISettingsSaveAlarmConfiguration(userAlarmConfiguration);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UISettingsApi.UISettingsSaveAlarmConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UISettingsSaveAlarmConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Save alarm profiles configuration (xDrip+-style).
    ApiResponse<UserAlarmConfiguration> response = apiInstance.UISettingsSaveAlarmConfigurationWithHttpInfo(userAlarmConfiguration);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UISettingsApi.UISettingsSaveAlarmConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userAlarmConfiguration** | [**UserAlarmConfiguration**](UserAlarmConfiguration.md) | The alarm configuration to save |  |

### Return type

[**UserAlarmConfiguration**](UserAlarmConfiguration.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The saved alarm configuration |  -  |
| **400** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="uisettingssavenotificationsettings"></a>
# **UISettingsSaveNotificationSettings**
> NotificationSettings UISettingsSaveNotificationSettings (NotificationSettings notificationSettings)

Save notification settings including alarm configuration.

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
    public class UISettingsSaveNotificationSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UISettingsApi(httpClient, config, httpClientHandler);
            var notificationSettings = new NotificationSettings(); // NotificationSettings | The notification settings to save

            try
            {
                // Save notification settings including alarm configuration.
                NotificationSettings result = apiInstance.UISettingsSaveNotificationSettings(notificationSettings);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UISettingsApi.UISettingsSaveNotificationSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UISettingsSaveNotificationSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Save notification settings including alarm configuration.
    ApiResponse<NotificationSettings> response = apiInstance.UISettingsSaveNotificationSettingsWithHttpInfo(notificationSettings);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UISettingsApi.UISettingsSaveNotificationSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **notificationSettings** | [**NotificationSettings**](NotificationSettings.md) | The notification settings to save |  |

### Return type

[**NotificationSettings**](NotificationSettings.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The saved notification settings |  -  |
| **400** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="uisettingssaveuisettings"></a>
# **UISettingsSaveUISettings**
> UISettingsConfiguration UISettingsSaveUISettings (UISettingsConfiguration uISettingsConfiguration)

Save complete UI settings configuration.

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
    public class UISettingsSaveUISettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new UISettingsApi(httpClient, config, httpClientHandler);
            var uISettingsConfiguration = new UISettingsConfiguration(); // UISettingsConfiguration | The complete settings to save

            try
            {
                // Save complete UI settings configuration.
                UISettingsConfiguration result = apiInstance.UISettingsSaveUISettings(uISettingsConfiguration);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UISettingsApi.UISettingsSaveUISettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UISettingsSaveUISettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Save complete UI settings configuration.
    ApiResponse<UISettingsConfiguration> response = apiInstance.UISettingsSaveUISettingsWithHttpInfo(uISettingsConfiguration);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UISettingsApi.UISettingsSaveUISettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **uISettingsConfiguration** | [**UISettingsConfiguration**](UISettingsConfiguration.md) | The complete settings to save |  |

### Return type

[**UISettingsConfiguration**](UISettingsConfiguration.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The saved settings |  -  |
| **400** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

