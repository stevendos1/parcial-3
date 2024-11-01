using System.Threading.Tasks;

namespace Hackathon.WEB.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url);
        Task<HttpResponseWrapper<object>> Post<T>(string url, T data);
        Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T data);
        Task<HttpResponseWrapper<object>> DeleteAsync(string url); 
    }
}
