using System;
using System.Threading.Tasks;
using GlowGlider.Shared;
using UnityEngine;

namespace Analytics
{
    public class AnalyticsApiModel
    {
        private readonly AnalyticsApi _analyticsApi = new AnalyticsApi("https://glowglider.azurewebsites.net");

        public void TrackSession(ushort duration, string playerId, ushort score)
        {
            var analyticsData = new AnalyticsData
            {
                GameDuration = duration,
                PlayerId = playerId,
                Score = score,
            };

            Task.Run(async () => await PublishRoundData(analyticsData)).ContinueWith(res =>
            {
                Debug.Log("Uploaded analytics Data");
            });
        }

        private async Task PublishRoundData(AnalyticsData analyticsData)
        {
            try
            {
                await _analyticsApi.PublishRoundData(analyticsData);
            }
            catch (Exception e)
            {
                Debug.Log("upload failed: " + e);
                throw;
            }
        }
    }
}