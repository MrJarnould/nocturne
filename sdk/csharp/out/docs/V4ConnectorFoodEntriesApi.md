# NightscoutFoundation.Nocturne.Api.V4ConnectorFoodEntriesApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ConnectorFoodEntriesImportEntries**](V4ConnectorFoodEntriesApi.md#connectorfoodentriesimportentries) | **POST** /api/v4/connector-food-entries/import | Import connector food entries. |

<a id="connectorfoodentriesimportentries"></a>
# **ConnectorFoodEntriesImportEntries**
> List&lt;ConnectorFoodEntry&gt; ConnectorFoodEntriesImportEntries (List<ConnectorFoodEntryImport> connectorFoodEntryImport)

Import connector food entries.

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
    public class ConnectorFoodEntriesImportEntriesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4ConnectorFoodEntriesApi(httpClient, config, httpClientHandler);
            var connectorFoodEntryImport = new List<ConnectorFoodEntryImport>(); // List<ConnectorFoodEntryImport> | 

            try
            {
                // Import connector food entries.
                List<ConnectorFoodEntry> result = apiInstance.ConnectorFoodEntriesImportEntries(connectorFoodEntryImport);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4ConnectorFoodEntriesApi.ConnectorFoodEntriesImportEntries: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConnectorFoodEntriesImportEntriesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Import connector food entries.
    ApiResponse<List<ConnectorFoodEntry>> response = apiInstance.ConnectorFoodEntriesImportEntriesWithHttpInfo(connectorFoodEntryImport);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4ConnectorFoodEntriesApi.ConnectorFoodEntriesImportEntriesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectorFoodEntryImport** | [**List&lt;ConnectorFoodEntryImport&gt;**](ConnectorFoodEntryImport.md) |  |  |

### Return type

[**List&lt;ConnectorFoodEntry&gt;**](ConnectorFoodEntry.md)

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

