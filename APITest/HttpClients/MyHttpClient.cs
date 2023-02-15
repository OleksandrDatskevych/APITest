using System.Text;
using Newtonsoft.Json;

namespace APITest.HttpClients
{
    public class MyHttpClient
    {
        private const string JsonMediaType = "application/json";
        private static HttpClient _httpClient;
        private static HttpRequestMessage _httpRequestMessage;
        private static RestResponse _restResponse;

        public static RestResponse GetRequest(string requestUrl, Dictionary<string, string>? headers) =>
            SendRequest(requestUrl, HttpMethod.Get, null, headers);

        public static RestResponse PostRequest(string requestUrl, HttpContent httpContent, Dictionary<string, string>? headers) =>
            SendRequest(requestUrl, HttpMethod.Post, httpContent, headers);

        public static RestResponse PutRequest(string requestUrl, HttpContent httpContent, Dictionary<string, string>? headers) =>
            SendRequest(requestUrl, HttpMethod.Put, httpContent, headers);

        public static RestResponse PatchRequest(string requestUrl, HttpContent httpContent, Dictionary<string, string>? headers) =>
            SendRequest(requestUrl, HttpMethod.Patch, httpContent, headers);

        public static RestResponse DeleteRequest(string requestUrl, HttpContent? httpContent, Dictionary<string, string>? headers) =>
            SendRequest(requestUrl, HttpMethod.Delete, httpContent, headers);

        public static RestResponse<T>? GetRequest<T>(string requestUrl, Dictionary<string, string>? headers) where T : class =>
            SendRequest<T>(requestUrl, HttpMethod.Get, null, headers);

        public static RestResponse<T>? PostRequest<T>(string requestUrl, HttpContent httpContent, Dictionary<string, string>? headers)
            where T : class => SendRequest<T>(requestUrl, HttpMethod.Post, httpContent, headers);

        public static RestResponse<T>? PutRequest<T>(string requestUrl, HttpContent httpContent, Dictionary<string, string>? headers)
            where T : class => SendRequest<T>(requestUrl, HttpMethod.Put, httpContent, headers);

        public static RestResponse<T>? PatchRequest<T>(string requestUrl, HttpContent httpContent, Dictionary<string, string>? headers)
            where T : class => SendRequest<T>(requestUrl, HttpMethod.Patch, httpContent, headers);

        public static RestResponse<TResponse> PostRequest<TRequest, TResponse>(
            string requestUrl, 
            TRequest requestModel, 
            Dictionary<string, string>? headers)
            where TRequest : class
            where TResponse : class => 
            SendRequest<TRequest, TResponse>(requestUrl, HttpMethod.Post, requestModel, headers);

        public static RestResponse<TResponse> PutRequest<TRequest, TResponse>(
            string requestUrl,
            TRequest requestModel,
            Dictionary<string, string>? headers)
            where TRequest : class
            where TResponse : class => 
            SendRequest<TRequest, TResponse>(requestUrl, HttpMethod.Put, requestModel, headers);

        public static RestResponse<TResponse> PatchRequest<TRequest, TResponse>(
            string requestUrl,
            TRequest requestModel,
            Dictionary<string, string>? headers)
            where TRequest : class
            where TResponse : class =>
            SendRequest<TRequest, TResponse>(requestUrl, HttpMethod.Patch, requestModel, headers);

        private static HttpClient CreateHttpClient(Dictionary<string, string>? headers)
        {
            var httpClient = new HttpClient();

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            return httpClient;
        }

        private static HttpRequestMessage CreateHttpRequestMessage(string requestUrl, HttpMethod httpMethod, HttpContent? httpContent)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = httpMethod;
            httpRequestMessage.RequestUri = new Uri(requestUrl);

            if (httpContent != null)
            {
                httpRequestMessage.Content = httpContent;
            }

            return httpRequestMessage;
        }

        private static RestResponse SendRequest(string requestUrl, HttpMethod httpMethod, HttpContent? httpContent, 
            Dictionary<string, string>? headers)
        {
            _httpClient = CreateHttpClient(headers);
            _httpRequestMessage = CreateHttpRequestMessage(requestUrl, httpMethod, httpContent);
            var httpResponse = _httpClient.SendAsync(_httpRequestMessage).Result;

            try
            {
                _restResponse = new RestResponse(httpResponse);
            }
            catch (Exception e)
            {
                throw new Exception($"Error with response: Status code: {httpResponse.StatusCode}, \nException: {e.Message}\n{e.InnerException}");
            }
            finally
            {
                _httpClient.Dispose();
                _httpRequestMessage.Dispose();
            }

            return _restResponse;
        }

        private static RestResponse<T>? SendRequest<T>(string requestUrl, HttpMethod httpMethod, HttpContent? httpContent,
            Dictionary<string, string>? headers) where T : class
        {
            _httpClient = CreateHttpClient(headers);
            _httpRequestMessage = CreateHttpRequestMessage(requestUrl, httpMethod, httpContent);
            var httpResponse = _httpClient.SendAsync(_httpRequestMessage).Result;

            try
            {
                _restResponse = new RestResponse<T>(httpResponse);
            }
            catch (Exception e)
            {
                throw new Exception($"Error with response: Status code: {httpResponse.StatusCode}, \nException: {e.Message}\n{e.InnerException}");
            }
            finally
            {
                _httpClient.Dispose();
                _httpRequestMessage.Dispose();
            }

            return _restResponse as RestResponse<T>;
        }

        private static RestResponse<TResponse> SendRequest<TRequest, TResponse>(string requestUrl, HttpMethod httpMethod, TRequest requestModel,
            Dictionary<string, string>? headers) where TResponse : class where TRequest : class
        {
            _httpClient = CreateHttpClient(headers);
            var httpContentString = JsonConvert.SerializeObject(requestModel);
            var httpContent = new StringContent(httpContentString, Encoding.UTF8, JsonMediaType);
            _httpRequestMessage = CreateHttpRequestMessage(requestUrl, httpMethod, httpContent);
            var httpResponse = _httpClient.SendAsync(_httpRequestMessage).Result;
            RestResponse<TResponse> restResponse;

            try
            {
                restResponse = new RestResponse<TResponse>(httpResponse);
            }
            catch (Exception e)
            {
                throw new Exception($"Error with response: Status code: {httpResponse.StatusCode}, \nException: {e.Message}\n{e.InnerException}");
            }
            finally
            {
                _httpClient.Dispose();
                _httpRequestMessage.Dispose();
            }

            return restResponse;
        }
    }
}
