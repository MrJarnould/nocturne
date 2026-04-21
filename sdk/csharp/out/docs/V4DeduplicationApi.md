# NightscoutFoundation.Nocturne.Api.V4DeduplicationApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DeduplicationCancelJob**](V4DeduplicationApi.md#deduplicationcanceljob) | **POST** /api/v4/admin/deduplication/cancel/{jobId} | Cancel a running deduplication job. |
| [**DeduplicationGetEntryLinkedRecords**](V4DeduplicationApi.md#deduplicationgetentrylinkedrecords) | **GET** /api/v4/admin/deduplication/entries/{entryId}/sources | Get linked records for a specific entry by its canonical group. |
| [**DeduplicationGetJobStatus**](V4DeduplicationApi.md#deduplicationgetjobstatus) | **GET** /api/v4/admin/deduplication/status/{jobId} | Get the status of a deduplication job. |
| [**DeduplicationGetRecordLinkedRecords**](V4DeduplicationApi.md#deduplicationgetrecordlinkedrecords) | **GET** /api/v4/admin/deduplication/records/{recordType}/{recordId}/sources | Get linked records for any V4 record type by its canonical group. |
| [**DeduplicationGetStateSpanLinkedRecords**](V4DeduplicationApi.md#deduplicationgetstatespanlinkedrecords) | **GET** /api/v4/admin/deduplication/state-spans/{stateSpanId}/sources | Get linked records for a specific state span by its canonical group. |
| [**DeduplicationGetTreatmentLinkedRecords**](V4DeduplicationApi.md#deduplicationgettreatmentlinkedrecords) | **GET** /api/v4/admin/deduplication/treatments/{treatmentId}/sources | Get linked records for a specific treatment by its canonical group. |
| [**DeduplicationStartDeduplicationJob**](V4DeduplicationApi.md#deduplicationstartdeduplicationjob) | **POST** /api/v4/admin/deduplication/run | Start a deduplication job to link related records from different data sources. The job runs in the background and can be monitored using the status endpoint. |

<a id="deduplicationcanceljob"></a>
# **DeduplicationCancelJob**
> CancelJobResponse DeduplicationCancelJob (string jobId)

Cancel a running deduplication job.

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
            var apiInstance = new V4DeduplicationApi(httpClient, config, httpClientHandler);
            var jobId = "jobId_example";  // string | The job ID to cancel

            try
            {
                // Cancel a running deduplication job.
                CancelJobResponse result = apiInstance.DeduplicationCancelJob(jobId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationCancelJob: " + e.Message);
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
    // Cancel a running deduplication job.
    ApiResponse<CancelJobResponse> response = apiInstance.DeduplicationCancelJobWithHttpInfo(jobId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationCancelJobWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **jobId** | **string** | The job ID to cancel |  |

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
| **200** | Whether the job was successfully cancelled |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationgetentrylinkedrecords"></a>
# **DeduplicationGetEntryLinkedRecords**
> LinkedRecordsResponse DeduplicationGetEntryLinkedRecords (string entryId)

Get linked records for a specific entry by its canonical group.

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
            var apiInstance = new V4DeduplicationApi(httpClient, config, httpClientHandler);
            var entryId = "entryId_example";  // string | The entry ID

            try
            {
                // Get linked records for a specific entry by its canonical group.
                LinkedRecordsResponse result = apiInstance.DeduplicationGetEntryLinkedRecords(entryId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationGetEntryLinkedRecords: " + e.Message);
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
    // Get linked records for a specific entry by its canonical group.
    ApiResponse<LinkedRecordsResponse> response = apiInstance.DeduplicationGetEntryLinkedRecordsWithHttpInfo(entryId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationGetEntryLinkedRecordsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **entryId** | **string** | The entry ID |  |

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
| **200** | All linked records in the same canonical group |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationgetjobstatus"></a>
# **DeduplicationGetJobStatus**
> DeduplicationJobStatus DeduplicationGetJobStatus (string jobId)

Get the status of a deduplication job.

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
            var apiInstance = new V4DeduplicationApi(httpClient, config, httpClientHandler);
            var jobId = "jobId_example";  // string | The job ID returned from the run endpoint

            try
            {
                // Get the status of a deduplication job.
                DeduplicationJobStatus result = apiInstance.DeduplicationGetJobStatus(jobId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationGetJobStatus: " + e.Message);
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
    // Get the status of a deduplication job.
    ApiResponse<DeduplicationJobStatus> response = apiInstance.DeduplicationGetJobStatusWithHttpInfo(jobId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationGetJobStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **jobId** | **string** | The job ID returned from the run endpoint |  |

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
| **200** | Current status and progress of the job |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationgetrecordlinkedrecords"></a>
# **DeduplicationGetRecordLinkedRecords**
> LinkedRecordsResponse DeduplicationGetRecordLinkedRecords (RecordType recordType, string recordId)

Get linked records for any V4 record type by its canonical group.

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
            var apiInstance = new V4DeduplicationApi(httpClient, config, httpClientHandler);
            var recordType = (RecordType) "Entry";  // RecordType | The record type (e.g. SensorGlucose, Bolus, CarbIntake)
            var recordId = "recordId_example";  // string | The record ID

            try
            {
                // Get linked records for any V4 record type by its canonical group.
                LinkedRecordsResponse result = apiInstance.DeduplicationGetRecordLinkedRecords(recordType, recordId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationGetRecordLinkedRecords: " + e.Message);
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
    // Get linked records for any V4 record type by its canonical group.
    ApiResponse<LinkedRecordsResponse> response = apiInstance.DeduplicationGetRecordLinkedRecordsWithHttpInfo(recordType, recordId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationGetRecordLinkedRecordsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **recordType** | **RecordType** | The record type (e.g. SensorGlucose, Bolus, CarbIntake) |  |
| **recordId** | **string** | The record ID |  |

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
| **200** | All linked records in the same canonical group |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationgetstatespanlinkedrecords"></a>
# **DeduplicationGetStateSpanLinkedRecords**
> LinkedRecordsResponse DeduplicationGetStateSpanLinkedRecords (string stateSpanId)

Get linked records for a specific state span by its canonical group.

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
            var apiInstance = new V4DeduplicationApi(httpClient, config, httpClientHandler);
            var stateSpanId = "stateSpanId_example";  // string | The state span ID

            try
            {
                // Get linked records for a specific state span by its canonical group.
                LinkedRecordsResponse result = apiInstance.DeduplicationGetStateSpanLinkedRecords(stateSpanId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationGetStateSpanLinkedRecords: " + e.Message);
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
    // Get linked records for a specific state span by its canonical group.
    ApiResponse<LinkedRecordsResponse> response = apiInstance.DeduplicationGetStateSpanLinkedRecordsWithHttpInfo(stateSpanId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationGetStateSpanLinkedRecordsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **stateSpanId** | **string** | The state span ID |  |

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
| **200** | All linked records in the same canonical group |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationgettreatmentlinkedrecords"></a>
# **DeduplicationGetTreatmentLinkedRecords**
> LinkedRecordsResponse DeduplicationGetTreatmentLinkedRecords (string treatmentId)

Get linked records for a specific treatment by its canonical group.

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
            var apiInstance = new V4DeduplicationApi(httpClient, config, httpClientHandler);
            var treatmentId = "treatmentId_example";  // string | The treatment ID

            try
            {
                // Get linked records for a specific treatment by its canonical group.
                LinkedRecordsResponse result = apiInstance.DeduplicationGetTreatmentLinkedRecords(treatmentId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationGetTreatmentLinkedRecords: " + e.Message);
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
    // Get linked records for a specific treatment by its canonical group.
    ApiResponse<LinkedRecordsResponse> response = apiInstance.DeduplicationGetTreatmentLinkedRecordsWithHttpInfo(treatmentId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationGetTreatmentLinkedRecordsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **treatmentId** | **string** | The treatment ID |  |

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
| **200** | All linked records in the same canonical group |  -  |
| **404** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deduplicationstartdeduplicationjob"></a>
# **DeduplicationStartDeduplicationJob**
> DeduplicationJobResponse DeduplicationStartDeduplicationJob ()

Start a deduplication job to link related records from different data sources. The job runs in the background and can be monitored using the status endpoint.

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
            var apiInstance = new V4DeduplicationApi(httpClient, config, httpClientHandler);

            try
            {
                // Start a deduplication job to link related records from different data sources. The job runs in the background and can be monitored using the status endpoint.
                DeduplicationJobResponse result = apiInstance.DeduplicationStartDeduplicationJob();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationStartDeduplicationJob: " + e.Message);
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
    // Start a deduplication job to link related records from different data sources. The job runs in the background and can be monitored using the status endpoint.
    ApiResponse<DeduplicationJobResponse> response = apiInstance.DeduplicationStartDeduplicationJobWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4DeduplicationApi.DeduplicationStartDeduplicationJobWithHttpInfo: " + e.Message);
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
| **202** | Job ID for tracking progress |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

