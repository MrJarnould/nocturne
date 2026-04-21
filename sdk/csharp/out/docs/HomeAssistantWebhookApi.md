# NightscoutFoundation.Nocturne.Api.HomeAssistantWebhookApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**HomeAssistantWebhookReceiveWebhook**](HomeAssistantWebhookApi.md#homeassistantwebhookreceivewebhook) | **POST** /api/v4/connectors/home-assistant/webhook/{secret} | Receives a webhook from Home Assistant with entity state updates and creates corresponding glucose entries. |

<a id="homeassistantwebhookreceivewebhook"></a>
# **HomeAssistantWebhookReceiveWebhook**
> FileParameter HomeAssistantWebhookReceiveWebhook (string secret, HomeAssistantStateResponse homeAssistantStateResponse)

Receives a webhook from Home Assistant with entity state updates and creates corresponding glucose entries.

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
    public class HomeAssistantWebhookReceiveWebhookExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new HomeAssistantWebhookApi(httpClient, config, httpClientHandler);
            var secret = "secret_example";  // string | Shared webhook secret used to authenticate the request.
            var homeAssistantStateResponse = new HomeAssistantStateResponse(); // HomeAssistantStateResponse | Home Assistant state response containing entity data.

            try
            {
                // Receives a webhook from Home Assistant with entity state updates and creates corresponding glucose entries.
                FileParameter result = apiInstance.HomeAssistantWebhookReceiveWebhook(secret, homeAssistantStateResponse);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling HomeAssistantWebhookApi.HomeAssistantWebhookReceiveWebhook: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the HomeAssistantWebhookReceiveWebhookWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Receives a webhook from Home Assistant with entity state updates and creates corresponding glucose entries.
    ApiResponse<FileParameter> response = apiInstance.HomeAssistantWebhookReceiveWebhookWithHttpInfo(secret, homeAssistantStateResponse);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling HomeAssistantWebhookApi.HomeAssistantWebhookReceiveWebhookWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **secret** | **string** | Shared webhook secret used to authenticate the request. |  |
| **homeAssistantStateResponse** | [**HomeAssistantStateResponse**](HomeAssistantStateResponse.md) | Home Assistant state response containing entity data. |  |

### Return type

[**FileParameter**](FileParameter.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | No content on success; 401 if the secret is invalid; 404 if not configured. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

