using System.Net;
using System.Threading.Tasks;

namespace Hackathon.WEB.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }

        public bool Error { get; set; }
        public T? Response { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public async Task<string?> GetErrorMessage()
        {
            if (!Error) return null;

            var statusCode = HttpResponseMessage.StatusCode;
            return statusCode switch
            {
                HttpStatusCode.NotFound => "Recurso no encontrado",
                HttpStatusCode.BadRequest => await HttpResponseMessage.Content.ReadAsStringAsync(),
                HttpStatusCode.Unauthorized => "Debes loguearte para realizar esta acción",
                HttpStatusCode.Forbidden => "No tienes permisos para ejecutar esta acción",
                _ => "Ha ocurrido un error inesperado",
            };
        }
    }
}
