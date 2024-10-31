using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hackathon.WEB.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _jsonOptions => new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await DeserializeResponse<T>(response);
                return new HttpResponseWrapper<T>(data, false, response);
            }
            return new HttpResponseWrapper<T>(default, true, response);
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var data = await DeserializeResponse<TResponse>(response);
                return new HttpResponseWrapper<TResponse>(data, false, response);
            }
            return new HttpResponseWrapper<TResponse>(default, true, response);
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, _jsonOptions)!;
        }
    }
}
