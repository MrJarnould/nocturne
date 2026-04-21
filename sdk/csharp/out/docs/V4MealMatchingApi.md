# NightscoutFoundation.Nocturne.Api.V4MealMatchingApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**MealMatchingAcceptMatch**](V4MealMatchingApi.md#mealmatchingacceptmatch) | **POST** /api/v4/meal-matching/accept | Accept a meal match |
| [**MealMatchingDismissMatch**](V4MealMatchingApi.md#mealmatchingdismissmatch) | **POST** /api/v4/meal-matching/dismiss | Dismiss a meal match |
| [**MealMatchingGetFoodEntry**](V4MealMatchingApi.md#mealmatchinggetfoodentry) | **GET** /api/v4/meal-matching/food-entries/{id} | Get a food entry for review |
| [**MealMatchingGetSuggestions**](V4MealMatchingApi.md#mealmatchinggetsuggestions) | **GET** /api/v4/meal-matching/suggestions | Get suggested meal matches for a date range |

<a id="mealmatchingacceptmatch"></a>
# **MealMatchingAcceptMatch**
> void MealMatchingAcceptMatch (AcceptMatchRequest acceptMatchRequest)

Accept a meal match

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
    public class MealMatchingAcceptMatchExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4MealMatchingApi(httpClient, config, httpClientHandler);
            var acceptMatchRequest = new AcceptMatchRequest(); // AcceptMatchRequest | 

            try
            {
                // Accept a meal match
                apiInstance.MealMatchingAcceptMatch(acceptMatchRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4MealMatchingApi.MealMatchingAcceptMatch: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MealMatchingAcceptMatchWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Accept a meal match
    apiInstance.MealMatchingAcceptMatchWithHttpInfo(acceptMatchRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4MealMatchingApi.MealMatchingAcceptMatchWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **acceptMatchRequest** | [**AcceptMatchRequest**](AcceptMatchRequest.md) |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="mealmatchingdismissmatch"></a>
# **MealMatchingDismissMatch**
> void MealMatchingDismissMatch (DismissMatchRequest dismissMatchRequest)

Dismiss a meal match

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
    public class MealMatchingDismissMatchExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4MealMatchingApi(httpClient, config, httpClientHandler);
            var dismissMatchRequest = new DismissMatchRequest(); // DismissMatchRequest | 

            try
            {
                // Dismiss a meal match
                apiInstance.MealMatchingDismissMatch(dismissMatchRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4MealMatchingApi.MealMatchingDismissMatch: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MealMatchingDismissMatchWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Dismiss a meal match
    apiInstance.MealMatchingDismissMatchWithHttpInfo(dismissMatchRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4MealMatchingApi.MealMatchingDismissMatchWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dismissMatchRequest** | [**DismissMatchRequest**](DismissMatchRequest.md) |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="mealmatchinggetfoodentry"></a>
# **MealMatchingGetFoodEntry**
> ConnectorFoodEntry MealMatchingGetFoodEntry (string id)

Get a food entry for review

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
    public class MealMatchingGetFoodEntryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4MealMatchingApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Get a food entry for review
                ConnectorFoodEntry result = apiInstance.MealMatchingGetFoodEntry(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4MealMatchingApi.MealMatchingGetFoodEntry: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MealMatchingGetFoodEntryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a food entry for review
    ApiResponse<ConnectorFoodEntry> response = apiInstance.MealMatchingGetFoodEntryWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4MealMatchingApi.MealMatchingGetFoodEntryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**ConnectorFoodEntry**](ConnectorFoodEntry.md)

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

<a id="mealmatchinggetsuggestions"></a>
# **MealMatchingGetSuggestions**
> List&lt;SuggestedMealMatch&gt; MealMatchingGetSuggestions (DateTimeOffset? from = null, DateTimeOffset? to = null)

Get suggested meal matches for a date range

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
    public class MealMatchingGetSuggestionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4MealMatchingApi(httpClient, config, httpClientHandler);
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 

            try
            {
                // Get suggested meal matches for a date range
                List<SuggestedMealMatch> result = apiInstance.MealMatchingGetSuggestions(from, to);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4MealMatchingApi.MealMatchingGetSuggestions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MealMatchingGetSuggestionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get suggested meal matches for a date range
    ApiResponse<List<SuggestedMealMatch>> response = apiInstance.MealMatchingGetSuggestionsWithHttpInfo(from, to);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4MealMatchingApi.MealMatchingGetSuggestionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **from** | **DateTimeOffset?** |  | [optional]  |
| **to** | **DateTimeOffset?** |  | [optional]  |

### Return type

[**List&lt;SuggestedMealMatch&gt;**](SuggestedMealMatch.md)

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

