# NightscoutFoundation.Nocturne.Api.V4PatientRecordApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**PatientRecordCreateDevice**](V4PatientRecordApi.md#patientrecordcreatedevice) | **POST** /api/v4/patient-record/devices | Create a new patient device |
| [**PatientRecordCreateInsulin**](V4PatientRecordApi.md#patientrecordcreateinsulin) | **POST** /api/v4/patient-record/insulins | Create a new patient insulin |
| [**PatientRecordDeleteDevice**](V4PatientRecordApi.md#patientrecorddeletedevice) | **DELETE** /api/v4/patient-record/devices/{id} | Delete a patient device |
| [**PatientRecordDeleteInsulin**](V4PatientRecordApi.md#patientrecorddeleteinsulin) | **DELETE** /api/v4/patient-record/insulins/{id} | Delete a patient insulin |
| [**PatientRecordGetDevices**](V4PatientRecordApi.md#patientrecordgetdevices) | **GET** /api/v4/patient-record/devices | Get all patient devices |
| [**PatientRecordGetInsulins**](V4PatientRecordApi.md#patientrecordgetinsulins) | **GET** /api/v4/patient-record/insulins | Get all patient insulins |
| [**PatientRecordGetPatientRecord**](V4PatientRecordApi.md#patientrecordgetpatientrecord) | **GET** /api/v4/patient-record | Get or create the patient record |
| [**PatientRecordUpdateDevice**](V4PatientRecordApi.md#patientrecordupdatedevice) | **PUT** /api/v4/patient-record/devices/{id} | Update a patient device |
| [**PatientRecordUpdateInsulin**](V4PatientRecordApi.md#patientrecordupdateinsulin) | **PUT** /api/v4/patient-record/insulins/{id} | Update a patient insulin |
| [**PatientRecordUpdatePatientRecord**](V4PatientRecordApi.md#patientrecordupdatepatientrecord) | **PUT** /api/v4/patient-record | Update the patient record |

<a id="patientrecordcreatedevice"></a>
# **PatientRecordCreateDevice**
> PatientDevice PatientRecordCreateDevice (PatientDevice patientDevice)

Create a new patient device

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
    public class PatientRecordCreateDeviceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PatientRecordApi(httpClient, config, httpClientHandler);
            var patientDevice = new PatientDevice(); // PatientDevice | 

            try
            {
                // Create a new patient device
                PatientDevice result = apiInstance.PatientRecordCreateDevice(patientDevice);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordCreateDevice: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatientRecordCreateDeviceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a new patient device
    ApiResponse<PatientDevice> response = apiInstance.PatientRecordCreateDeviceWithHttpInfo(patientDevice);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordCreateDeviceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **patientDevice** | [**PatientDevice**](PatientDevice.md) |  |  |

### Return type

[**PatientDevice**](PatientDevice.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="patientrecordcreateinsulin"></a>
# **PatientRecordCreateInsulin**
> PatientInsulin PatientRecordCreateInsulin (PatientInsulin patientInsulin)

Create a new patient insulin

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
    public class PatientRecordCreateInsulinExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PatientRecordApi(httpClient, config, httpClientHandler);
            var patientInsulin = new PatientInsulin(); // PatientInsulin | 

            try
            {
                // Create a new patient insulin
                PatientInsulin result = apiInstance.PatientRecordCreateInsulin(patientInsulin);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordCreateInsulin: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatientRecordCreateInsulinWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a new patient insulin
    ApiResponse<PatientInsulin> response = apiInstance.PatientRecordCreateInsulinWithHttpInfo(patientInsulin);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordCreateInsulinWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **patientInsulin** | [**PatientInsulin**](PatientInsulin.md) |  |  |

### Return type

[**PatientInsulin**](PatientInsulin.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="patientrecorddeletedevice"></a>
# **PatientRecordDeleteDevice**
> void PatientRecordDeleteDevice (string id)

Delete a patient device

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
    public class PatientRecordDeleteDeviceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PatientRecordApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a patient device
                apiInstance.PatientRecordDeleteDevice(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordDeleteDevice: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatientRecordDeleteDeviceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a patient device
    apiInstance.PatientRecordDeleteDeviceWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordDeleteDeviceWithHttpInfo: " + e.Message);
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
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="patientrecorddeleteinsulin"></a>
# **PatientRecordDeleteInsulin**
> void PatientRecordDeleteInsulin (string id)

Delete a patient insulin

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
    public class PatientRecordDeleteInsulinExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PatientRecordApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 

            try
            {
                // Delete a patient insulin
                apiInstance.PatientRecordDeleteInsulin(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordDeleteInsulin: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatientRecordDeleteInsulinWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a patient insulin
    apiInstance.PatientRecordDeleteInsulinWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordDeleteInsulinWithHttpInfo: " + e.Message);
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
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** |  |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="patientrecordgetdevices"></a>
# **PatientRecordGetDevices**
> List&lt;PatientDevice&gt; PatientRecordGetDevices ()

Get all patient devices

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
    public class PatientRecordGetDevicesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PatientRecordApi(httpClient, config, httpClientHandler);

            try
            {
                // Get all patient devices
                List<PatientDevice> result = apiInstance.PatientRecordGetDevices();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordGetDevices: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatientRecordGetDevicesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all patient devices
    ApiResponse<List<PatientDevice>> response = apiInstance.PatientRecordGetDevicesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordGetDevicesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;PatientDevice&gt;**](PatientDevice.md)

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

<a id="patientrecordgetinsulins"></a>
# **PatientRecordGetInsulins**
> List&lt;PatientInsulin&gt; PatientRecordGetInsulins ()

Get all patient insulins

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
    public class PatientRecordGetInsulinsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PatientRecordApi(httpClient, config, httpClientHandler);

            try
            {
                // Get all patient insulins
                List<PatientInsulin> result = apiInstance.PatientRecordGetInsulins();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordGetInsulins: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatientRecordGetInsulinsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all patient insulins
    ApiResponse<List<PatientInsulin>> response = apiInstance.PatientRecordGetInsulinsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordGetInsulinsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;PatientInsulin&gt;**](PatientInsulin.md)

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

<a id="patientrecordgetpatientrecord"></a>
# **PatientRecordGetPatientRecord**
> PatientRecord PatientRecordGetPatientRecord ()

Get or create the patient record

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
    public class PatientRecordGetPatientRecordExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PatientRecordApi(httpClient, config, httpClientHandler);

            try
            {
                // Get or create the patient record
                PatientRecord result = apiInstance.PatientRecordGetPatientRecord();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordGetPatientRecord: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatientRecordGetPatientRecordWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get or create the patient record
    ApiResponse<PatientRecord> response = apiInstance.PatientRecordGetPatientRecordWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordGetPatientRecordWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**PatientRecord**](PatientRecord.md)

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

<a id="patientrecordupdatedevice"></a>
# **PatientRecordUpdateDevice**
> PatientDevice PatientRecordUpdateDevice (string id, PatientDevice patientDevice)

Update a patient device

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
    public class PatientRecordUpdateDeviceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PatientRecordApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var patientDevice = new PatientDevice(); // PatientDevice | 

            try
            {
                // Update a patient device
                PatientDevice result = apiInstance.PatientRecordUpdateDevice(id, patientDevice);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordUpdateDevice: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatientRecordUpdateDeviceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update a patient device
    ApiResponse<PatientDevice> response = apiInstance.PatientRecordUpdateDeviceWithHttpInfo(id, patientDevice);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordUpdateDeviceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **patientDevice** | [**PatientDevice**](PatientDevice.md) |  |  |

### Return type

[**PatientDevice**](PatientDevice.md)

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

<a id="patientrecordupdateinsulin"></a>
# **PatientRecordUpdateInsulin**
> PatientInsulin PatientRecordUpdateInsulin (string id, PatientInsulin patientInsulin)

Update a patient insulin

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
    public class PatientRecordUpdateInsulinExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PatientRecordApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | 
            var patientInsulin = new PatientInsulin(); // PatientInsulin | 

            try
            {
                // Update a patient insulin
                PatientInsulin result = apiInstance.PatientRecordUpdateInsulin(id, patientInsulin);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordUpdateInsulin: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatientRecordUpdateInsulinWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update a patient insulin
    ApiResponse<PatientInsulin> response = apiInstance.PatientRecordUpdateInsulinWithHttpInfo(id, patientInsulin);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordUpdateInsulinWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** |  |  |
| **patientInsulin** | [**PatientInsulin**](PatientInsulin.md) |  |  |

### Return type

[**PatientInsulin**](PatientInsulin.md)

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

<a id="patientrecordupdatepatientrecord"></a>
# **PatientRecordUpdatePatientRecord**
> PatientRecord PatientRecordUpdatePatientRecord (PatientRecord patientRecord)

Update the patient record

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
    public class PatientRecordUpdatePatientRecordExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new V4PatientRecordApi(httpClient, config, httpClientHandler);
            var patientRecord = new PatientRecord(); // PatientRecord | 

            try
            {
                // Update the patient record
                PatientRecord result = apiInstance.PatientRecordUpdatePatientRecord(patientRecord);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordUpdatePatientRecord: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatientRecordUpdatePatientRecordWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update the patient record
    ApiResponse<PatientRecord> response = apiInstance.PatientRecordUpdatePatientRecordWithHttpInfo(patientRecord);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling V4PatientRecordApi.PatientRecordUpdatePatientRecordWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **patientRecord** | [**PatientRecord**](PatientRecord.md) |  |  |

### Return type

[**PatientRecord**](PatientRecord.md)

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

