using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hackathon.WEB.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<T>();
                    return new HttpResponseWrapper<T>(data, false, response);
                }
                else
                {
                    return new HttpResponseWrapper<T>(default, true, response);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseWrapper<T>(default, true, null, ex.Message);
            }
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, data);
                return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
            }
            catch (Exception ex)
            {
                return new HttpResponseWrapper<object>(null, true, null, ex.Message);
            }
        }

        public async Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(url, data);
                return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
            }
            catch (Exception ex)
            {
                return new HttpResponseWrapper<object>(null, true, null, ex.Message);
            }
        }

        public async Task<HttpResponseWrapper<object>> DeleteAsync(string url) // Corrección aquí
        {
            try
            {
                var response = await _httpClient.DeleteAsync(url);
                return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
            }
            catch (Exception ex)
            {
                return new HttpResponseWrapper<object>(null, true, null, ex.Message);
            }
        }
    }
}
