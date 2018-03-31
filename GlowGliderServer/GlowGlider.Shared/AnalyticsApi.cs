using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GlowGlider.Shared
{
    public class AnalyticsApi
    {
        private readonly HttpClient _client = new HttpClient();

        public AnalyticsApi(string addressUri)
        {
            _client.BaseAddress = new Uri(addressUri);
        }

        public async Task PublishRoundData(AnalyticsData data)
        {
            data.Token = TokenBuilder.TokenFor(data);
            var json = JsonConvert.SerializeObject(data);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/analytics/", content);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiErrorException();
            }
        }
    }
}