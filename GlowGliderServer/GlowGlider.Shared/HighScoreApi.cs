using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GlowGlider.Shared
{
    public class HighScoreApi
    {
        private readonly HttpClient _client = new HttpClient();

        public HighScoreApi(string addressUri)
        {
            _client.BaseAddress = new Uri(addressUri);
        }

        public async Task<IReadOnlyList<HighScoreData>> GetTop10Async()
        {
            var response = await _client.GetAsync("/api/highscores/");

            return await GetDataFromResponse(response);
        }

        public async Task<IReadOnlyList<HighScoreData>> GetRanksAroundPlayer(Guid playerId)
        {
            var response = await _client.GetAsync("/api/highscores/?playerId=" + playerId);

            return await GetDataFromResponse(response);
        }

        public async Task PublishScore(PublishRequest request)
        {
            request.Token = TokenBuilder.TokenFor(request);
            var json = JsonConvert.SerializeObject(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/highscores/", content);
            await GetDataFromResponse(response);
        }

        private static async Task<IReadOnlyList<HighScoreData>> GetDataFromResponse(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiErrorException();
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IReadOnlyList<HighScoreData>>(content);

            return data;
        }
    }

    public class ApiErrorException : Exception
    {
    }
}