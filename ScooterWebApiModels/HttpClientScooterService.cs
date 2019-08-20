using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScooterWebApiModels
{
    public static class HttpExtensions
    {
        public static async Task<T> GetContentAs<T>(this HttpResponseMessage response)
        {
            var contentAsString = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(contentAsString)) throw new Exception();

            try
            {
                return JsonConvert.DeserializeObject<T>(contentAsString);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }

    public class HttpClientScooterService
    {
        public async Task<T> Get<T>(string url)
        {
            try
            {
                var responseMessage = await new HttpClient().GetAsync(url);

                if (responseMessage.StatusCode == HttpStatusCode.NotModified)
                {
                    return default(T);
                }

                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    return await responseMessage.GetContentAs<T>();
                }

                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<HttpResponseMessage> PutModel(string url, string serializeObject)
        {
            try
            {
                return await new HttpClient().PutAsync(url, new StringContent(serializeObject, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<HttpResponseMessage> PostModel(string url, string serializeObject)
        {
            try
            {
                return await new HttpClient().PostAsync(url, new StringContent(serializeObject, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task Delete(string url)
        {
            try
            {
                await new HttpClient().DeleteAsync(url).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
