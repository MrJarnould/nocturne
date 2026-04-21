# NightscoutFoundation.Nocturne.Api.BackfillApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**BackfillTriggerBackfill**](BackfillApi.md#backfilltriggerbackfill) | **POST** /api/v4/admin/backfill | Triggers a backfill operation to reprocess all legacy entries and treatments into V4 granular tables. Only one backfill may run at a time; concurrent calls return 409 Conflict. |

<a id="backfilltriggerbackfill"></a>
# **BackfillTriggerBackfill**
> BackfillResult BackfillTriggerBackfill ()

Triggers a backfill operation to reprocess all legacy entries and treatments into V4 granular tables. Only one backfill may run at a time; concurrent calls return 409 Conflict.

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
    public class BackfillTriggerBackfillExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new BackfillApi(httpClient, config, httpClientHandler);

            try
            {
                // Triggers a backfill operation to reprocess all legacy entries and treatments into V4 granular tables. Only one backfill may run at a time; concurrent calls return 409 Conflict.
                BackfillResult result = apiInstance.BackfillTriggerBackfill();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackfillApi.BackfillTriggerBackfill: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackfillTriggerBackfillWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Triggers a backfill operation to reprocess all legacy entries and treatments into V4 granular tables. Only one backfill may run at a time; concurrent calls return 409 Conflict.
    ApiResponse<BackfillResult> response = apiInstance.BackfillTriggerBackfillWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackfillApi.BackfillTriggerBackfillWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**BackfillResult**](BackfillResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | BackfillResult with processed counts on success; 409 if already running; 500 on internal error. |  -  |
| **409** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

