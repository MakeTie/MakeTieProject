using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MakeTie.Bll.Utils.Interfaces;

namespace MakeTie.Bll.Utils
{
    public class HttpUtil : IHttpUtil
    {
        private readonly HttpClient _httpClient;

        public HttpUtil()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetAsync(string query, string authToken = null)
        {
            if (!string.IsNullOrEmpty(authToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }

            using (var response = await _httpClient.GetAsync(query))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}