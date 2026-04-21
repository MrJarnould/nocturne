# NightscoutFoundation.Nocturne.Api.FoodsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**FoodsAddFavorite**](FoodsApi.md#foodsaddfavorite) | **POST** /api/v4/foods/{foodId}/favorite | Add a food to favorites. |
| [**FoodsCreateFood**](FoodsApi.md#foodscreatefood) | **POST** /api/v4/foods | Create a new food record. |
| [**FoodsDeleteFood**](FoodsApi.md#foodsdeletefood) | **DELETE** /api/v4/foods/{foodId} | Delete a food from the database, handling any meal attributions that reference it. |
| [**FoodsGetFavorites**](FoodsApi.md#foodsgetfavorites) | **GET** /api/v4/foods/favorites | Get current user&#39;s favorite foods. |
| [**FoodsGetFood**](FoodsApi.md#foodsgetfood) | **GET** /api/v4/foods/{foodId} | Get a single food by ID. |
| [**FoodsGetFoodAttributionCount**](FoodsApi.md#foodsgetfoodattributioncount) | **GET** /api/v4/foods/{foodId}/attribution-count | Get how many meal attributions reference a specific food. |
| [**FoodsGetFoods**](FoodsApi.md#foodsgetfoods) | **GET** /api/v4/foods | List foods with optional filtering and pagination. This is a V4 endpoint (not Nightscout-legacy) used by the meal attribution UI. |
| [**FoodsGetRecentFoods**](FoodsApi.md#foodsgetrecentfoods) | **GET** /api/v4/foods/recent | Get recently used foods (excluding favorites). |
| [**FoodsRemoveFavorite**](FoodsApi.md#foodsremovefavorite) | **DELETE** /api/v4/foods/{foodId}/favorite | Remove a food from favorites. |
| [**FoodsUpdateFood**](FoodsApi.md#foodsupdatefood) | **PUT** /api/v4/foods/{foodId} | Update an existing food record by ID. |

<a id="foodsaddfavorite"></a>
# **FoodsAddFavorite**
> FileParameter FoodsAddFavorite (string foodId)

Add a food to favorites.

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
    public class FoodsAddFavoriteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FoodsApi(httpClient, config, httpClientHandler);
            var foodId = "foodId_example";  // string | 

            try
            {
                // Add a food to favorites.
                FileParameter result = apiInstance.FoodsAddFavorite(foodId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FoodsApi.FoodsAddFavorite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FoodsAddFavoriteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add a food to favorites.
    ApiResponse<FileParameter> response = apiInstance.FoodsAddFavoriteWithHttpInfo(foodId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FoodsApi.FoodsAddFavoriteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **foodId** | **string** |  |  |

### Return type

[**FileParameter**](FileParameter.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="foodscreatefood"></a>
# **FoodsCreateFood**
> Food FoodsCreateFood (Food food)

Create a new food record.

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
    public class FoodsCreateFoodExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FoodsApi(httpClient, config, httpClientHandler);
            var food = new Food(); // Food | 

            try
            {
                // Create a new food record.
                Food result = apiInstance.FoodsCreateFood(food);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FoodsApi.FoodsCreateFood: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FoodsCreateFoodWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a new food record.
    ApiResponse<Food> response = apiInstance.FoodsCreateFoodWithHttpInfo(food);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FoodsApi.FoodsCreateFoodWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **food** | [**Food**](Food.md) |  |  |

### Return type

[**Food**](Food.md)

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

<a id="foodsdeletefood"></a>
# **FoodsDeleteFood**
> void FoodsDeleteFood (string foodId, string? attributionMode = null)

Delete a food from the database, handling any meal attributions that reference it.

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
    public class FoodsDeleteFoodExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FoodsApi(httpClient, config, httpClientHandler);
            var foodId = "foodId_example";  // string | The food ID to delete.
            var attributionMode = "\"clear\"";  // string? | How to handle existing attributions: \"clear\" (default) sets them to Other, \"remove\" deletes them. (optional)  (default to "clear")

            try
            {
                // Delete a food from the database, handling any meal attributions that reference it.
                apiInstance.FoodsDeleteFood(foodId, attributionMode);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FoodsApi.FoodsDeleteFood: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FoodsDeleteFoodWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a food from the database, handling any meal attributions that reference it.
    apiInstance.FoodsDeleteFoodWithHttpInfo(foodId, attributionMode);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FoodsApi.FoodsDeleteFoodWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **foodId** | **string** | The food ID to delete. |  |
| **attributionMode** | **string?** | How to handle existing attributions: \&quot;clear\&quot; (default) sets them to Other, \&quot;remove\&quot; deletes them. | [optional] [default to &quot;clear&quot;] |

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

<a id="foodsgetfavorites"></a>
# **FoodsGetFavorites**
> List&lt;Food&gt; FoodsGetFavorites ()

Get current user's favorite foods.

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
    public class FoodsGetFavoritesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FoodsApi(httpClient, config, httpClientHandler);

            try
            {
                // Get current user's favorite foods.
                List<Food> result = apiInstance.FoodsGetFavorites();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FoodsApi.FoodsGetFavorites: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FoodsGetFavoritesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get current user's favorite foods.
    ApiResponse<List<Food>> response = apiInstance.FoodsGetFavoritesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FoodsApi.FoodsGetFavoritesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;Food&gt;**](Food.md)

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

<a id="foodsgetfood"></a>
# **FoodsGetFood**
> Food FoodsGetFood (string foodId)

Get a single food by ID.

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
    public class FoodsGetFoodExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FoodsApi(httpClient, config, httpClientHandler);
            var foodId = "foodId_example";  // string | 

            try
            {
                // Get a single food by ID.
                Food result = apiInstance.FoodsGetFood(foodId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FoodsApi.FoodsGetFood: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FoodsGetFoodWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a single food by ID.
    ApiResponse<Food> response = apiInstance.FoodsGetFoodWithHttpInfo(foodId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FoodsApi.FoodsGetFoodWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **foodId** | **string** |  |  |

### Return type

[**Food**](Food.md)

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

<a id="foodsgetfoodattributioncount"></a>
# **FoodsGetFoodAttributionCount**
> FoodAttributionCount FoodsGetFoodAttributionCount (string foodId)

Get how many meal attributions reference a specific food.

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
    public class FoodsGetFoodAttributionCountExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FoodsApi(httpClient, config, httpClientHandler);
            var foodId = "foodId_example";  // string | 

            try
            {
                // Get how many meal attributions reference a specific food.
                FoodAttributionCount result = apiInstance.FoodsGetFoodAttributionCount(foodId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FoodsApi.FoodsGetFoodAttributionCount: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FoodsGetFoodAttributionCountWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get how many meal attributions reference a specific food.
    ApiResponse<FoodAttributionCount> response = apiInstance.FoodsGetFoodAttributionCountWithHttpInfo(foodId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FoodsApi.FoodsGetFoodAttributionCountWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **foodId** | **string** |  |  |

### Return type

[**FoodAttributionCount**](FoodAttributionCount.md)

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

<a id="foodsgetfoods"></a>
# **FoodsGetFoods**
> List&lt;Food&gt; FoodsGetFoods (string? find = null, int? count = null, int? skip = null)

List foods with optional filtering and pagination. This is a V4 endpoint (not Nightscout-legacy) used by the meal attribution UI.

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
    public class FoodsGetFoodsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FoodsApi(httpClient, config, httpClientHandler);
            var find = "find_example";  // string? |  (optional) 
            var count = 56;  // int? |  (optional) 
            var skip = 56;  // int? |  (optional) 

            try
            {
                // List foods with optional filtering and pagination. This is a V4 endpoint (not Nightscout-legacy) used by the meal attribution UI.
                List<Food> result = apiInstance.FoodsGetFoods(find, count, skip);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FoodsApi.FoodsGetFoods: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FoodsGetFoodsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List foods with optional filtering and pagination. This is a V4 endpoint (not Nightscout-legacy) used by the meal attribution UI.
    ApiResponse<List<Food>> response = apiInstance.FoodsGetFoodsWithHttpInfo(find, count, skip);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FoodsApi.FoodsGetFoodsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **find** | **string?** |  | [optional]  |
| **count** | **int?** |  | [optional]  |
| **skip** | **int?** |  | [optional]  |

### Return type

[**List&lt;Food&gt;**](Food.md)

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

<a id="foodsgetrecentfoods"></a>
# **FoodsGetRecentFoods**
> List&lt;Food&gt; FoodsGetRecentFoods (int? limit = null)

Get recently used foods (excluding favorites).

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
    public class FoodsGetRecentFoodsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FoodsApi(httpClient, config, httpClientHandler);
            var limit = 20;  // int? |  (optional)  (default to 20)

            try
            {
                // Get recently used foods (excluding favorites).
                List<Food> result = apiInstance.FoodsGetRecentFoods(limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FoodsApi.FoodsGetRecentFoods: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FoodsGetRecentFoodsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get recently used foods (excluding favorites).
    ApiResponse<List<Food>> response = apiInstance.FoodsGetRecentFoodsWithHttpInfo(limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FoodsApi.FoodsGetRecentFoodsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **limit** | **int?** |  | [optional] [default to 20] |

### Return type

[**List&lt;Food&gt;**](Food.md)

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

<a id="foodsremovefavorite"></a>
# **FoodsRemoveFavorite**
> FileParameter FoodsRemoveFavorite (string foodId)

Remove a food from favorites.

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
    public class FoodsRemoveFavoriteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FoodsApi(httpClient, config, httpClientHandler);
            var foodId = "foodId_example";  // string | 

            try
            {
                // Remove a food from favorites.
                FileParameter result = apiInstance.FoodsRemoveFavorite(foodId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FoodsApi.FoodsRemoveFavorite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FoodsRemoveFavoriteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove a food from favorites.
    ApiResponse<FileParameter> response = apiInstance.FoodsRemoveFavoriteWithHttpInfo(foodId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FoodsApi.FoodsRemoveFavoriteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **foodId** | **string** |  |  |

### Return type

[**FileParameter**](FileParameter.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="foodsupdatefood"></a>
# **FoodsUpdateFood**
> Food FoodsUpdateFood (string foodId, Food food)

Update an existing food record by ID.

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
    public class FoodsUpdateFoodExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FoodsApi(httpClient, config, httpClientHandler);
            var foodId = "foodId_example";  // string | 
            var food = new Food(); // Food | 

            try
            {
                // Update an existing food record by ID.
                Food result = apiInstance.FoodsUpdateFood(foodId, food);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FoodsApi.FoodsUpdateFood: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FoodsUpdateFoodWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update an existing food record by ID.
    ApiResponse<Food> response = apiInstance.FoodsUpdateFoodWithHttpInfo(foodId, food);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FoodsApi.FoodsUpdateFoodWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **foodId** | **string** |  |  |
| **food** | [**Food**](Food.md) |  |  |

### Return type

[**Food**](Food.md)

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

