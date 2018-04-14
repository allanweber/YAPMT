using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YAPMT.Framework.Test
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsObjectAsync(this HttpClient httpClient, string requestUri, object obj)
        {
            var httpContent = GetHttpContent(obj);

            return httpClient.PostAsync(requestUri, httpContent);
        }

        public static Task<HttpResponseMessage> PutAsObjectAsync(this HttpClient httpClient, string requestUri, object obj)
        {
            var httpContent = GetHttpContent(obj);

            return httpClient.PutAsync(requestUri, httpContent);
        }

        public static Task<HttpResponseMessage> SendAsObjectAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, object obj)
        {
            var httpContent = GetHttpContent(obj);
            httpRequestMessage.Content = httpContent;

            return httpClient.SendAsync(httpRequestMessage);
        }

        private static StringContent GetHttpContent(object obj)
        {
            string content = JToken
                .FromObject(obj)
                .ToString();

            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
