# NightscoutFoundation.Nocturne.Api.DeduplicationApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DeduplicationCancelJob**](DeduplicationApi.md#deduplicationcanceljob) | **POST** /api/v4/admin/deduplication/cancel/{jobId} |  |
| [**DeduplicationGetEntryLinkedRecords**](DeduplicationApi.md#deduplicationgetentrylinkedrecords) | **GET** /api/v4/admin/deduplication/entries/{entryId}/sources |  |
| [**DeduplicationGetJobStatus**](DeduplicationApi.md#deduplicationgetjobstatus) | **GET** /api/v4/admin/deduplication/status/{jobId} |  |
| [**DeduplicationGetRecordLinkedRecords**](DeduplicationApi.md#deduplicationgetrecordlinkedrecords) | **GET** /api/v4/admin/deduplication/records/{recordType}/{recordId}/sources |  |
| [**DeduplicationGetStateSpanLinkedRecords**](DeduplicationApi.md#deduplicationgetstatespanlinkedrecords) | **GET** /api/v4/admin/deduplication/state-spans/{stateSpanId}/sources |  |
| [**DeduplicationGetTreatmentLinkedRecords**](DeduplicationApi.md#deduplicationgettreatmentlinkedrecords) | **GET** /api/v4/admin/deduplication/treatments/{treatmentId}/sources |  |
| [**DeduplicationStartDeduplicationJob**](DeduplicationApi.md#deduplicationstartdeduplicationjob) | **POST** /api/v4/admin/deduplication/run |  |

<a id="deduplicationcanceljob"></a>
# **DeduplicationCancelJob**
> CancelJobResponse DeduplicationCancelJob (string jobId)



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
    public class DeduplicationCancelJobExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeduplicationApi(httpClient, config, httpClientHandler);
            var jobId = "jobId_example";  // string | 

            try
            {
                CancelJobResponse result = apiInstance.DeduplicationCancelJob(jobId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeduplicationApi.DeduplicationCancelJob: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeduplicationCancelJobWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<CancelJobResponse> response = apiInstance.DeduplicationCancelJobWithHttpInfo(jobId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeduplicationApi.DeduplicationCancelJobWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **jobId** | **string** |  |  |

### Return type

[**CancelJobResponse**](CancelJobResponse.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationgetentrylinkedrecords"></a>
# **DeduplicationGetEntryLinkedRecords**
> LinkedRecordsResponse DeduplicationGetEntryLinkedRecords (string entryId)



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
    public class DeduplicationGetEntryLinkedRecordsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeduplicationApi(httpClient, config, httpClientHandler);
            var entryId = "entryId_example";  // string | 

            try
            {
                LinkedRecordsResponse result = apiInstance.DeduplicationGetEntryLinkedRecords(entryId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeduplicationApi.DeduplicationGetEntryLinkedRecords: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeduplicationGetEntryLinkedRecordsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<LinkedRecordsResponse> response = apiInstance.DeduplicationGetEntryLinkedRecordsWithHttpInfo(entryId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeduplicationApi.DeduplicationGetEntryLinkedRecordsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **entryId** | **string** |  |  |

### Return type

[**LinkedRecordsResponse**](LinkedRecordsResponse.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationgetjobstatus"></a>
# **DeduplicationGetJobStatus**
> DeduplicationJobStatus DeduplicationGetJobStatus (string jobId)



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
    public class DeduplicationGetJobStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeduplicationApi(httpClient, config, httpClientHandler);
            var jobId = "jobId_example";  // string | 

            try
            {
                DeduplicationJobStatus result = apiInstance.DeduplicationGetJobStatus(jobId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeduplicationApi.DeduplicationGetJobStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeduplicationGetJobStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<DeduplicationJobStatus> response = apiInstance.DeduplicationGetJobStatusWithHttpInfo(jobId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeduplicationApi.DeduplicationGetJobStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **jobId** | **string** |  |  |

### Return type

[**DeduplicationJobStatus**](DeduplicationJobStatus.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationgetrecordlinkedrecords"></a>
# **DeduplicationGetRecordLinkedRecords**
> LinkedRecordsResponse DeduplicationGetRecordLinkedRecords (RecordType recordType, string recordId)



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
    public class DeduplicationGetRecordLinkedRecordsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeduplicationApi(httpClient, config, httpClientHandler);
            var recordType = (RecordType) "Entry";  // RecordType | 
            var recordId = "recordId_example";  // string | 

            try
            {
                LinkedRecordsResponse result = apiInstance.DeduplicationGetRecordLinkedRecords(recordType, recordId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeduplicationApi.DeduplicationGetRecordLinkedRecords: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeduplicationGetRecordLinkedRecordsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<LinkedRecordsResponse> response = apiInstance.DeduplicationGetRecordLinkedRecordsWithHttpInfo(recordType, recordId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeduplicationApi.DeduplicationGetRecordLinkedRecordsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **recordType** | **RecordType** |  |  |
| **recordId** | **string** |  |  |

### Return type

[**LinkedRecordsResponse**](LinkedRecordsResponse.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationgetstatespanlinkedrecords"></a>
# **DeduplicationGetStateSpanLinkedRecords**
> LinkedRecordsResponse DeduplicationGetStateSpanLinkedRecords (string stateSpanId)



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
    public class DeduplicationGetStateSpanLinkedRecordsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeduplicationApi(httpClient, config, httpClientHandler);
            var stateSpanId = "stateSpanId_example";  // string | 

            try
            {
                LinkedRecordsResponse result = apiInstance.DeduplicationGetStateSpanLinkedRecords(stateSpanId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeduplicationApi.DeduplicationGetStateSpanLinkedRecords: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeduplicationGetStateSpanLinkedRecordsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<LinkedRecordsResponse> response = apiInstance.DeduplicationGetStateSpanLinkedRecordsWithHttpInfo(stateSpanId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeduplicationApi.DeduplicationGetStateSpanLinkedRecordsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **stateSpanId** | **string** |  |  |

### Return type

[**LinkedRecordsResponse**](LinkedRecordsResponse.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationgettreatmentlinkedrecords"></a>
# **DeduplicationGetTreatmentLinkedRecords**
> LinkedRecordsResponse DeduplicationGetTreatmentLinkedRecords (string treatmentId)



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
    public class DeduplicationGetTreatmentLinkedRecordsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeduplicationApi(httpClient, config, httpClientHandler);
            var treatmentId = "treatmentId_example";  // string | 

            try
            {
                LinkedRecordsResponse result = apiInstance.DeduplicationGetTreatmentLinkedRecords(treatmentId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeduplicationApi.DeduplicationGetTreatmentLinkedRecords: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeduplicationGetTreatmentLinkedRecordsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<LinkedRecordsResponse> response = apiInstance.DeduplicationGetTreatmentLinkedRecordsWithHttpInfo(treatmentId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeduplicationApi.DeduplicationGetTreatmentLinkedRecordsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **treatmentId** | **string** |  |  |

### Return type

[**LinkedRecordsResponse**](LinkedRecordsResponse.md)

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
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationstartdeduplicationjob"></a>
# **DeduplicationStartDeduplicationJob**
> DeduplicationJobResponse DeduplicationStartDeduplicationJob ()



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
    public class DeduplicationStartDeduplicationJobExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DeduplicationApi(httpClient, config, httpClientHandler);

            try
            {
                DeduplicationJobResponse result = apiInstance.DeduplicationStartDeduplicationJob();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeduplicationApi.DeduplicationStartDeduplicationJob: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeduplicationStartDeduplicationJobWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<DeduplicationJobResponse> response = apiInstance.DeduplicationStartDeduplicationJobWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DeduplicationApi.DeduplicationStartDeduplicationJobWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**DeduplicationJobResponse**](DeduplicationJobResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

