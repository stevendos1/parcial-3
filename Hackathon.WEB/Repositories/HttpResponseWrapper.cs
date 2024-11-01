using System.Net.Http;

namespace Hackathon.WEB.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T response, bool error, HttpResponseMessage httpResponseMessage, string errorMessage = "")
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
            ErrorMessage = errorMessage;
        }

        public T Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }
        public string ErrorMessage { get; }
    }
}
