# NightscoutFoundation.Nocturne.Api.V4AdminApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**BackfillTriggerBackfill**](V4AdminApi.md#backfilltriggerbackfill) | **POST** /api/v4/admin/backfill | Trigger a full backfill of legacy entries and treatments into v4 granular tables. This operation is idempotent and safe to re-run. Records are matched by LegacyId to avoid creating duplicates. Only one backfill can run at a time. |

<a id="backfilltriggerbackfill"></a>
# **BackfillTriggerBackfill**
> BackfillResult BackfillTriggerBackfill ()

Trigger a full backfill of legacy entries and treatments into v4 granular tables. This operation is idempotent and safe to re-run. Records are matched by LegacyId to avoid creating duplicates. Only one backfill can run at a time.

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
            var apiInstance = new V4AdminApi(httpClient, config, httpClientHandler);

            try
            {
                // Trigger a full backfill of legacy entries and treatments into v4 granular tables. This operation is idempotent and safe to re-run. Records are matched by LegacyId to avoid creating duplicates. Only one backfill can run at a time.
                BackfillResult result = apiInstance.BackfillTriggerBackfill();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4AdminApi.BackfillTriggerBackfill: " + e.Message);
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
    // Trigger a full backfill of legacy entries and treatments into v4 granular tables. This operation is idempotent and safe to re-run. Records are matched by LegacyId to avoid creating duplicates. Only one backfill can run at a time.
    ApiResponse<BackfillResult> response = apiInstance.BackfillTriggerBackfillWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4AdminApi.BackfillTriggerBackfillWithHttpInfo: " + e.Message);
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
| **200** | Backfill result with counts of processed, failed, and skipped records |  -  |
| **409** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

