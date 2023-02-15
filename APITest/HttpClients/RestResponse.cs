using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace APITest.HttpClients
{
    public class RestResponse : HttpResponseMessage
    {
        public string ResponseData { get; }
        public HttpResponseHeaders ResponseHeaders { get; }

        public RestResponse(HttpResponseMessage responseMessage)
        {
            ResponseData = responseMessage.Content.ReadAsStringAsync().Result;
            ResponseHeaders = responseMessage.Headers;
            StatusCode = responseMessage.StatusCode;
        }

        public override string ToString()
        {
            return $"Status code: {StatusCode}\nResponse data: {ResponseData}";
        }
    }

    public class RestResponse<T> : RestResponse where T : class
    {
        public T? ResponseModel { get; }

        public RestResponse(HttpResponseMessage responseMessage) : base(responseMessage)
        {
            ResponseModel = JsonConvert.DeserializeObject<T>(ResponseData);
        }
    }
}
