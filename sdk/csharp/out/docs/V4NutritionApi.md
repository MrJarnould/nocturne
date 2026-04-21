# NightscoutFoundation.Nocturne.Api.V4NutritionApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**NutritionAddCarbIntakeFood**](V4NutritionApi.md#nutritionaddcarbintakefood) | **POST** /api/v4/nutrition/carbs/{id}/foods | Add a food breakdown entry to a carb intake record. |
| [**NutritionCreateCarbIntake**](V4NutritionApi.md#nutritioncreatecarbintake) | **POST** /api/v4/nutrition/carbs | Create a new carb intake |
| [**NutritionDeleteCarbIntake**](V4NutritionApi.md#nutritiondeletecarbintake) | **DELETE** /api/v4/nutrition/carbs/{id} | Delete a carb intake |
| [**NutritionDeleteCarbIntakeFood**](V4NutritionApi.md#nutritiondeletecarbintakefood) | **DELETE** /api/v4/nutrition/carbs/{id}/foods/{foodEntryId} | Remove a food breakdown entry. |
| [**NutritionGetCarbIntakeById**](V4NutritionApi.md#nutritiongetcarbintakebyid) | **GET** /api/v4/nutrition/carbs/{id} | Get a carb intake by ID |
| [**NutritionGetCarbIntakeFoods**](V4NutritionApi.md#nutritiongetcarbintakefoods) | **GET** /api/v4/nutrition/carbs/{id}/foods | Get food breakdown for a carb intake record. |
| [**NutritionGetCarbIntakes**](V4NutritionApi.md#nutritiongetcarbintakes) | **GET** /api/v4/nutrition/carbs | Get carb intakes with optional filtering |
| [**NutritionGetMeals**](V4NutritionApi.md#nutritiongetmeals) | **GET** /api/v4/nutrition/meals | Get carb intake records with food attribution status for the meals view. |
| [**NutritionUpdateCarbIntake**](V4NutritionApi.md#nutritionupdatecarbintake) | **PUT** /api/v4/nutrition/carbs/{id} | Update an existing carb intake |
| [**NutritionUpdateCarbIntakeFood**](V4NutritionApi.md#nutritionupdatecarbintakefood) | **PUT** /api/v4/nutrition/carbs/{id}/foods/{foodEntryId} | Update a food breakdown entry. |

<a id="nutritionaddcarbintakefood"></a>
# **NutritionAddCarbIntakeFood**
> TreatmentFoodBreakdown NutritionAddCarbIntakeFood (string id, CarbIntakeFoodRequest carbIntakeFoodRequest)

Add a food breakdown entry to a carb intake record.

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
    public class NutritionAddCarbIntakeFoodExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NutritionApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var carbIntakeFoodRequest = new CarbIntakeFoodRequest(); // CarbIntakeFoodRequest | 

            try
            {
                // Add a food breakdown entry to a carb intake record.
                TreatmentFoodBreakdown result = apiInstance.NutritionAddCarbIntakeFood(id, carbIntakeFoodRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NutritionApi.NutritionAddCarbIntakeFood: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NutritionAddCarbIntakeFoodWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add a food breakdown entry to a carb intake record.
    ApiResponse<TreatmentFoodBreakdown> response = apiInstance.NutritionAddCarbIntakeFoodWithHttpInfo(id, carbIntakeFoodRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NutritionApi.NutritionAddCarbIntakeFoodWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **carbIntakeFoodRequest** | [**CarbIntakeFoodRequest**](CarbIntakeFoodRequest.md) |  |  |

### Return type

[**TreatmentFoodBreakdown**](TreatmentFoodBreakdown.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="nutritioncreatecarbintake"></a>
# **NutritionCreateCarbIntake**
> CarbIntake NutritionCreateCarbIntake (CarbIntake carbIntake)

Create a new carb intake

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
    public class NutritionCreateCarbIntakeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NutritionApi(httpClient, config, httpClientHandler);
            var carbIntake = new CarbIntake(); // CarbIntake | 

            try
            {
                // Create a new carb intake
                CarbIntake result = apiInstance.NutritionCreateCarbIntake(carbIntake);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NutritionApi.NutritionCreateCarbIntake: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NutritionCreateCarbIntakeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a new carb intake
    ApiResponse<CarbIntake> response = apiInstance.NutritionCreateCarbIntakeWithHttpInfo(carbIntake);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NutritionApi.NutritionCreateCarbIntakeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **carbIntake** | [**CarbIntake**](CarbIntake.md) |  |  |

### Return type

[**CarbIntake**](CarbIntake.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="nutritiondeletecarbintake"></a>
# **NutritionDeleteCarbIntake**
> void NutritionDeleteCarbIntake (string id)

Delete a carb intake

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
    public class NutritionDeleteCarbIntakeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NutritionApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a carb intake
                apiInstance.NutritionDeleteCarbIntake(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NutritionApi.NutritionDeleteCarbIntake: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NutritionDeleteCarbIntakeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a carb intake
    apiInstance.NutritionDeleteCarbIntakeWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NutritionApi.NutritionDeleteCarbIntakeWithHttpInfo: " + e.Message);
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

<a id="nutritiondeletecarbintakefood"></a>
# **NutritionDeleteCarbIntakeFood**
> TreatmentFoodBreakdown NutritionDeleteCarbIntakeFood (string id, string foodEntryId)

Remove a food breakdown entry.

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
    public class NutritionDeleteCarbIntakeFoodExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NutritionApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var foodEntryId = "foodEntryId_example";  // string | 

            try
            {
                // Remove a food breakdown entry.
                TreatmentFoodBreakdown result = apiInstance.NutritionDeleteCarbIntakeFood(id, foodEntryId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NutritionApi.NutritionDeleteCarbIntakeFood: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NutritionDeleteCarbIntakeFoodWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove a food breakdown entry.
    ApiResponse<TreatmentFoodBreakdown> response = apiInstance.NutritionDeleteCarbIntakeFoodWithHttpInfo(id, foodEntryId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NutritionApi.NutritionDeleteCarbIntakeFoodWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **foodEntryId** | **string** |  |  |

### Return type

[**TreatmentFoodBreakdown**](TreatmentFoodBreakdown.md)

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

<a id="nutritiongetcarbintakebyid"></a>
# **NutritionGetCarbIntakeById**
> CarbIntake NutritionGetCarbIntakeById (string id)

Get a carb intake by ID

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
    public class NutritionGetCarbIntakeByIdExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NutritionApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Get a carb intake by ID
                CarbIntake result = apiInstance.NutritionGetCarbIntakeById(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NutritionApi.NutritionGetCarbIntakeById: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NutritionGetCarbIntakeByIdWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a carb intake by ID
    ApiResponse<CarbIntake> response = apiInstance.NutritionGetCarbIntakeByIdWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NutritionApi.NutritionGetCarbIntakeByIdWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**CarbIntake**](CarbIntake.md)

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

<a id="nutritiongetcarbintakefoods"></a>
# **NutritionGetCarbIntakeFoods**
> TreatmentFoodBreakdown NutritionGetCarbIntakeFoods (string id)

Get food breakdown for a carb intake record.

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
    public class NutritionGetCarbIntakeFoodsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NutritionApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Get food breakdown for a carb intake record.
                TreatmentFoodBreakdown result = apiInstance.NutritionGetCarbIntakeFoods(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NutritionApi.NutritionGetCarbIntakeFoods: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NutritionGetCarbIntakeFoodsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get food breakdown for a carb intake record.
    ApiResponse<TreatmentFoodBreakdown> response = apiInstance.NutritionGetCarbIntakeFoodsWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NutritionApi.NutritionGetCarbIntakeFoodsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |

### Return type

[**TreatmentFoodBreakdown**](TreatmentFoodBreakdown.md)

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

<a id="nutritiongetcarbintakes"></a>
# **NutritionGetCarbIntakes**
> PaginatedResponseOfCarbIntake NutritionGetCarbIntakes (DateTimeOffset? from = null, DateTimeOffset? to = null, int? limit = null, int? offset = null, string? sort = null, string? device = null, string? source = null)

Get carb intakes with optional filtering

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
    public class NutritionGetCarbIntakesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NutritionApi(httpClient, config, httpClientHandler);
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var limit = 100;  // int? |  (optional)  (default to 100)
            var offset = 0;  // int? |  (optional)  (default to 0)
            var sort = "\"timestamp_desc\"";  // string? |  (optional)  (default to "timestamp_desc")
            var device = "device_example";  // string? |  (optional) 
            var source = "source_example";  // string? |  (optional) 

            try
            {
                // Get carb intakes with optional filtering
                PaginatedResponseOfCarbIntake result = apiInstance.NutritionGetCarbIntakes(from, to, limit, offset, sort, device, source);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NutritionApi.NutritionGetCarbIntakes: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NutritionGetCarbIntakesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get carb intakes with optional filtering
    ApiResponse<PaginatedResponseOfCarbIntake> response = apiInstance.NutritionGetCarbIntakesWithHttpInfo(from, to, limit, offset, sort, device, source);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NutritionApi.NutritionGetCarbIntakesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **from** | **DateTimeOffset?** |  | [optional]  |
| **to** | **DateTimeOffset?** |  | [optional]  |
| **limit** | **int?** |  | [optional] [default to 100] |
| **offset** | **int?** |  | [optional] [default to 0] |
| **sort** | **string?** |  | [optional] [default to &quot;timestamp_desc&quot;] |
| **device** | **string?** |  | [optional]  |
| **source** | **string?** |  | [optional]  |

### Return type

[**PaginatedResponseOfCarbIntake**](PaginatedResponseOfCarbIntake.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="nutritiongetmeals"></a>
# **NutritionGetMeals**
> List&lt;MealCarbIntake&gt; NutritionGetMeals (DateTimeOffset? from = null, DateTimeOffset? to = null, bool? attributed = null)

Get carb intake records with food attribution status for the meals view.

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
    public class NutritionGetMealsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NutritionApi(httpClient, config, httpClientHandler);
            var from = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var to = DateTimeOffset.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? |  (optional) 
            var attributed = true;  // bool? |  (optional) 

            try
            {
                // Get carb intake records with food attribution status for the meals view.
                List<MealCarbIntake> result = apiInstance.NutritionGetMeals(from, to, attributed);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NutritionApi.NutritionGetMeals: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NutritionGetMealsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get carb intake records with food attribution status for the meals view.
    ApiResponse<List<MealCarbIntake>> response = apiInstance.NutritionGetMealsWithHttpInfo(from, to, attributed);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NutritionApi.NutritionGetMealsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **from** | **DateTimeOffset?** |  | [optional]  |
| **to** | **DateTimeOffset?** |  | [optional]  |
| **attributed** | **bool?** |  | [optional]  |

### Return type

[**List&lt;MealCarbIntake&gt;**](MealCarbIntake.md)

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

<a id="nutritionupdatecarbintake"></a>
# **NutritionUpdateCarbIntake**
> CarbIntake NutritionUpdateCarbIntake (string id, CarbIntake carbIntake)

Update an existing carb intake

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
    public class NutritionUpdateCarbIntakeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NutritionApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var carbIntake = new CarbIntake(); // CarbIntake | 

            try
            {
                // Update an existing carb intake
                CarbIntake result = apiInstance.NutritionUpdateCarbIntake(id, carbIntake);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NutritionApi.NutritionUpdateCarbIntake: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NutritionUpdateCarbIntakeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update an existing carb intake
    ApiResponse<CarbIntake> response = apiInstance.NutritionUpdateCarbIntakeWithHttpInfo(id, carbIntake);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NutritionApi.NutritionUpdateCarbIntakeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **carbIntake** | [**CarbIntake**](CarbIntake.md) |  |  |

### Return type

[**CarbIntake**](CarbIntake.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="nutritionupdatecarbintakefood"></a>
# **NutritionUpdateCarbIntakeFood**
> TreatmentFoodBreakdown NutritionUpdateCarbIntakeFood (string id, string foodEntryId, CarbIntakeFoodRequest carbIntakeFoodRequest)

Update a food breakdown entry.

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
    public class NutritionUpdateCarbIntakeFoodExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4NutritionApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var foodEntryId = "foodEntryId_example";  // string | 
            var carbIntakeFoodRequest = new CarbIntakeFoodRequest(); // CarbIntakeFoodRequest | 

            try
            {
                // Update a food breakdown entry.
                TreatmentFoodBreakdown result = apiInstance.NutritionUpdateCarbIntakeFood(id, foodEntryId, carbIntakeFoodRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4NutritionApi.NutritionUpdateCarbIntakeFood: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NutritionUpdateCarbIntakeFoodWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update a food breakdown entry.
    ApiResponse<TreatmentFoodBreakdown> response = apiInstance.NutritionUpdateCarbIntakeFoodWithHttpInfo(id, foodEntryId, carbIntakeFoodRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4NutritionApi.NutritionUpdateCarbIntakeFoodWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **foodEntryId** | **string** |  |  |
| **carbIntakeFoodRequest** | [**CarbIntakeFoodRequest**](CarbIntakeFoodRequest.md) |  |  |

### Return type

[**TreatmentFoodBreakdown**](TreatmentFoodBreakdown.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **404** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

